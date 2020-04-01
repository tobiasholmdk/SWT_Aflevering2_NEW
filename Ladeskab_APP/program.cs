using System;
using LadeskabSWT;

namespace Ladeskab_APP
{
class Program
    {
        static void Main(string[] args)
        {
                DoorSimulator door = new DoorSimulator();
                IRFIDReader rfidReader = new RFIDSimulator();
                IDisplay disp = new DisplaySimulator();
                IUsbCharger charger = new UsbChargerSimulator();
                ILog log = new LogSimulator();
                var sC = new StationControl(disp,door,rfidReader,charger,log);
                bool finish = false;
            do
            {
              
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;
                charger.StartCharge();
                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.DoorOpen();
                        break;

                    case 'C':
                        door.DoorClosed();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.RfidDetected(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
