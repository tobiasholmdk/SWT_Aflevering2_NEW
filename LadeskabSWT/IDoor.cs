using System;

namespace LadeskabSWT
{
    public class DoorStateChangeEventArgs : EventArgs
    {
        public bool Opened { set; get; }
    }
    public interface IDoor
    {
        event EventHandler<DoorStateChangeEventArgs> DoorStateChange;

        public void LockDoor()
        {
            
        }
        public void UnlockDoor()
        {
            
        }
    }
}