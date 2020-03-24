using System;
using System.Threading;

namespace LadeskabSWT
{
    public class DoorSimulator : IDoor
    {
        public bool _unlocked { get; set; }
        public event EventHandler<DoorStateChangeEventArgs> DoorStateChange;
        
        public DoorSimulator()
        {
            _unlocked = true;
        }
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
        
        public void DoorOpen()
        {
            WhenDoorStateChange(new DoorStateChangeEventArgs() {Opened = true});
        }
        public void DoorClosed()
        {
            WhenDoorStateChange(new DoorStateChangeEventArgs() {Opened = false });
        }
        
        private void WhenDoorStateChange(DoorStateChangeEventArgs e)
        {
            DoorStateChange?.Invoke(this, e);
        }

        //public void SetDoorState(bool s)
        //{
        //    switch (s)
        //    {
        //        case false:
        //            OnDoorClose(new DoorStateChangeEventArgs{Unlocked = s});
        //            break;
        //        case true:
        //            OnDoorOpen(new DoorStateChangeEventArgs {Unlocked = s });
        //            break;
        //    }
        //}


        //private void OnDoorOpen(DoorStateChangeEventArgs e)
        //{
        //    DoorStateChange?.Invoke(this, e);
        //}
    }
}