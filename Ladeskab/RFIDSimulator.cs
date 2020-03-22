using System;

namespace Ladeskab
{
    public class RFIDSimulator : IRFIDReader
    {
        public event EventHandler<RFIDSimulator> RFIDEvent;
        public int ID { get; set; }
        public void RfidDetected(int id)
        {
            OnReadRFID(new RFIDSimulator{ID = id});
        }

        private void OnReadRFID(RFIDSimulator e)
        {
            RFIDEvent?.Invoke(this, e);
        }
    }
}
