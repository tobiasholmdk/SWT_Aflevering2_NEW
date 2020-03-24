using System;

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

        public LadeskabState _state {get; private set;}
        public int _oldId { get; private set; }

        private IDisplay _display;
        private IDoor _door;
        private IRFIDReader _RFIDReader;
        private IUsbCharger _chargeControl;

        public StationControl(IDisplay display, IDoor door, IRFIDReader RFIDReader, IUsbCharger chargeControl)
        {
            _display = display;
            _door = door;
            _RFIDReader = RFIDReader;
            _chargeControl = chargeControl;

            _state = LadeskabState.Available;

            _door.DoorStateChange += HandleDoorStateChanged;
            _RFIDReader.RFIDEvent += HandleRfidState;
        }

        private void HandleDoorStateChanged(Object s, DoorStateChangeEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.DoorOpen:
                    if (!e.Unlocked)
                    {
                        _state = LadeskabState.Available;
                        _display.IsCharging();
                    }
                    else
                    { 
                        _display.IsReady();
                    }
                    break;

                case LadeskabState.Available:
                    if (e.Unlocked)
                    {
                        _state = LadeskabState.DoorOpen;
                        _display.IsReady();
                    }
                    else
                    {
                        _display.PresentRFID();
                    }
                    break;

                case LadeskabState.Locked:
                    break;

            }
        }

        private void HandleRfidState(Object s, RfidEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    if (_chargeControl.Connected)
                    {
                        _door.LockDoor();
                        _oldId = e.ID;
                        _state = LadeskabState.Locked;
                        _display.IsCharging();
                        _chargeControl.StartCharge();
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
                        _chargeControl.StopCharge();
                        _door.UnlockDoor();
                        _display.IsCharged();
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.RFIDError();
                    }
                    break;
            }
        }
    }
}