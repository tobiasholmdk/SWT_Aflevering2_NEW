using System;
using System.IO;

namespace LadeskabSWT
{
    public class StationControl
    {
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        public LadeskabState _state {get; set;}
        public int _oldId { get; set; }
        private IDisplay _display;
        private IDoor _door;
        private IRFIDReader _RFID;
        private IUsbCharger _charger;
        private string logFile = "logfile.txt";

        public StationControl(IDisplay display, IDoor door, IRFIDReader RFID, IUsbCharger charger)
        {
            _display = display;
            _door = door;
            _RFID = RFID;
            _charger = charger;
            _state = LadeskabState.Available;
            
            _door.DoorStateChange += DoorChange;
            _RFID.RFIDEvent += RfidDetected;
        }
        public void RfidDetected(Object o, RfidEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _oldId = e.ID;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", e.ID);
                        }
                        _state = LadeskabState.Locked;
                        _display.IsCharging();
                        _charger.StartCharge();
                    }
                    else
                    {
                        _display.ChargeError();
                    }
                    break;

                case LadeskabState.DoorOpen:
                    _display.IsReady();
                    break;

                case LadeskabState.Locked:
                    if (e.ID == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        _display.IsCharged();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", e.ID);
                        }
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.RFIDError();
                    }
                    break;
            }
        }
        
        public void DoorChange(Object o, DoorStateChangeEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.DoorOpen:
                    if (!e.Opened)
                    {
                        _display.IsCharging();
                        _state = LadeskabState.Available;
                    }
                    else
                    { 
                        _display.IsReady();
                    }
                    break;

                case LadeskabState.Available:
                    if (e.Opened)
                    {
                        _display.IsReady();
                        _state = LadeskabState.DoorOpen;
                    }
                    else
                    {
                        _display.PresentRFID();
                    }
                    break;

                case LadeskabState.Locked:
                    _display.IsCharging();
                
                    break;

            }
        }

    }
}