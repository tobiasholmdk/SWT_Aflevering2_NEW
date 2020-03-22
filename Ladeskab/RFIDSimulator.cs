namespace Ladeskab
{
    public class RFIDSimulator : IRFIDReader
    {
        private int ID; 
        public int RfidDetected(int id)
        {
            id = ID;
            return ID; 
        }
    }
}