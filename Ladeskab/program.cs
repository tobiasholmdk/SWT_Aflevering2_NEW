using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;
using UsbSimulator;
namespace Ladeskab
{
class Program
    {
        static void Main(string[] args)
        {
				// Assemble your system here from all the classes
                DoorSimulator door = new DoorSimulator();
                IRFIDReader rfidReader = new RFIDSimulator();
                IDisplay disp = new DisplaySimulator();
                IUsbCharger charger = new UsbChargerSimulator();
                StationControl sC = new StationControl(disp,door,rfidReader,charger);
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
