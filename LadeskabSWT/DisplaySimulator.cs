
using System;

namespace LadeskabSWT
{
    public class DisplaySimulator : IDisplay
    {
        public void IsReady()
        {
            Console.WriteLine("Tilslut telefon");
        }
        public void PresentRFID()
        {
            Console.WriteLine("Indlæs RFID");
        }
        public void ChargeError()
        {
            Console.WriteLine("Tilslutningsfejl");
        }
        public void IsCharging()
        {
            Console.WriteLine("Ladeskab optaget");
        }
        public void RFIDError()
        {
            Console.WriteLine("RFID fejl, Prøv igen");
        }
        public void IsCharged()
        {
            Console.WriteLine("Fjern telefon");
        }
    }
}