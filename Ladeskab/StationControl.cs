﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbSimulator;

namespace Ladeskab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IUsbCharger _charger;
        private IDisplay _display;
        private int _oldId;
        private IDoor _door = new DoorSimulator();

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        StationControl()
        {
            _state = LadeskabState.Available;
        }
        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                        _display.IsCharging();
                    }
                    else
                    {
                        _display.ChargeError();
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.RFIDError();
                    }

                    break;
            }
        }

        public void DoorState()
        {
            switch (_state)
            {
                case LadeskabState.DoorOpen:
                    _display.IsReady();
                    break;
                case LadeskabState.Available:
                    _display.PresentRFID();
                    break;
                case LadeskabState.Locked:
                    _display.IsCharging();
                    break;
            }
        }

        public void ChargeState()
        {
            switch (_state)
            {
                case LadeskabState.DoorOpen:
                    _charger.StopCharge();
                    break;
                case LadeskabState.Locked:
                    _charger.StartCharge();
                    break;
                case LadeskabState.Available:
                    break;
            }
        }
    }
}
