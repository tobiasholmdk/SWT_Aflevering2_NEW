using System;

namespace Ladeskab
{
    public class DoorStateChangeEventArgs : EventArgs
    {
        // Value in mA (milliAmpere)
        public double Unlocked { set; get; }
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