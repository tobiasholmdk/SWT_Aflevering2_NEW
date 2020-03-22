using System;

namespace Ladeskab
{
    public class DoorSimulator : IDoor
    {
        public void UnlockDoor()
        {
            Console.WriteLine("Door Unlocked");
        }
        public void Locked()
        {
            Console.WriteLine("Door locked");
        }
    }
}