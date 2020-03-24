using System;

namespace Ladeskab
{
    public class DoorSimulator : IDoor
    {
        private bool _unlocked { get; set; }
        private bool _formerState;
        public event EventHandler<DoorStateChangeEventArgs> DoorStateChange;
        
        public void UnlockDoor()
        {
            Console.WriteLine("Door Unlocked");
            _unlocked = true;
        }

        public void LockDoor()
        {
            Console.WriteLine("Door locked");
            _unlocked = false;
        }

        public void SetDoorState(bool s)
        {
            if (s == false)
            {
                OnDoorClose(new DoorStateChangeEventArgs{Unlocked = s});
                _formerState = s;
            }
            else if (s == true)
            {
                OnDoorOpen(new DoorStateChangeEventArgs { Unlocked = s });
                _formerState = s;
            }
        }

        private void OnDoorOpen(DoorStateChangeEventArgs e)
        {
            DoorStateChange?.Invoke(this, e);
        }

        private void OnDoorClose(DoorStateChangeEventArgs e)
        {
            DoorStateChange?.Invoke(this, e);
        }
    }
}