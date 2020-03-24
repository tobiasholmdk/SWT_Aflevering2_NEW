using System;

namespace Ladeskab
{
    public class RFIDSimulator : IRFIDReader
    {
        public event EventHandler<RfidEventArgs> RFIDEvent;
        public int ID { get; set; }
        public void RfidDetected(int id)
        {
            OnReadRFID(new RfidEventArgs{ID = id});
        }

        private void OnReadRFID(RfidEventArgs e)
        {
            RFIDEvent?.Invoke(this, e);
        }
    }
}
