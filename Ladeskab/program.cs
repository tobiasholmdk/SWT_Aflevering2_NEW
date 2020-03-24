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

            bool finish = false;
            do
            {
                DoorSimulator door = new DoorSimulator();
                IRFIDReader rfidReader = new RFIDSimulator();
                IDisplay disp = new DisplaySimulator();
                IUsbCharger charger = new UsbChargerSimulator();
                StationControl sC = new StationControl(disp,door,rfidReader,charger);
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
                        door.SetDoorState(true);
                        break;

                    case 'C':
                        door.SetDoorState(false);
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
