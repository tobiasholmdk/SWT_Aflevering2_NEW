using System;

namespace LadeskabSWT
{
    public class RfidEventArgs : EventArgs
    {
        public int ID { set; get; }
    }
    public interface IRFIDReader
    {
        event EventHandler<RfidEventArgs> RFIDEvent;
        
        public void RfidDetected(int id)
        {
        }
    }
}