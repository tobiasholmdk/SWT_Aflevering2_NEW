using System;

namespace Ladeskab
{
    public class DoorSimulator : IDoor
    {
        public void UnlockDoor()
        {
            Console.WriteLine("Door Unlocked");
        }
        public void LockDoor()
        {
            Console.WriteLine("Door locked");
        }
    }
}