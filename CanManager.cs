using Kvaser.CanLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace WF_bonfire
{
    public class CanManager
    {
        private int handle;
        public delegate void MessageReceivedHandler(string message);
        public event MessageReceivedHandler OnMessageReceived;
        byte[] datareceivedFFFA;
        string strid = "";
        public CanManager()
        {
            Canlib.canInitializeLibrary();
            datareceivedFFFA = new byte[8];
        }

        public void ListChannels()
        {
            Canlib.canStatus stat = Canlib.canGetNumberOfChannels(out int numberOfChannels);
            CheckStatus(stat, "canGetNumberOfChannels");

            if (stat == Canlib.canStatus.canOK)
            {
                Console.WriteLine($"Found {numberOfChannels} channels");

                for (int i = 0; i < numberOfChannels; i++)
                {
                    stat = Canlib.canGetChannelData(i, Canlib.canCHANNELDATA_DEVDESCR_ASCII, out object deviceName);
                    CheckStatus(stat, "canGetChannelData (Device Name)");

                    stat = Canlib.canGetChannelData(i, Canlib.canCHANNELDATA_CHAN_NO_ON_CARD, out object deviceChannel);
                    CheckStatus(stat, "canGetChannelData (Channel Number)");

                    Console.WriteLine($"Found channel: {i} {deviceName} {((UInt32)deviceChannel + 1)}");
                }
            }
        }


        public void OpenChannel(int channelNumber)
        {
            handle = Canlib.canOpenChannel(channelNumber, Canlib.canOPEN_ACCEPT_VIRTUAL);
            CheckStatus((Canlib.canStatus)handle, "canOpenChannel");
        }

        public void SetBusParams()
        {
            var status = Canlib.canSetBusParams(handle, Canlib.canBITRATE_250K, 0, 0, 0, 0);
            CheckStatus(status, "canSetBusParams");
        }

        public void GoOnBus()
        {
            var status = Canlib.canBusOn(handle);
            CheckStatus(status, "canBusOn");
        }

        public void SendMessage(int id, byte[] message)
        {
            var status = Canlib.canWrite(handle, id, message, 8, Canlib.canMSG_EXT);
            CheckStatus(status, "canWrite");
        }


        public void ReceiveMessageold()
        {
            Canlib.canStatus status;
            byte[] data = new byte[8];
            int id;
            int dlc;
            int flags;
            long timestamp;
            bool finished = false;

            Console.WriteLine("Press the Spacebar to stop receiving messages.");

            while (!finished)
            {
                status = Canlib.canReadWait(handle, out id, data, out dlc, out flags, out timestamp, 100);

                if (status == Canlib.canStatus.canOK)
                {
                    Console.WriteLine($"Received Message: ID={id}, DLC={dlc}, Data={BitConverter.ToString(data, 0, dlc)}, Timestamp={timestamp}");
                }
                else if (status != Canlib.canStatus.canERR_NOMSG)
                {
                    CheckStatus(status, "canReadWait");
                    break;  // Exit the loop if an error occurs
                }

                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                    {
                        finished = true;
                    }
                }
            }
        }
        bool receivingMessages = true; // Flag to start/stop receiving messages


        public void ReceiveMessage(bool start)
        {
            if (start)
            {
                byte[] data = new byte[8];
                int id;
                int dlc;
                int flags;
                long timestamp;
                Canlib.canStatus status;

                status = Canlib.canReadWait(handle, out id, data, out dlc, out flags, out timestamp, 100);

                if (status == Canlib.canStatus.canOK)
                {
                    string message = $"Received Message: ID={id}, DLC={dlc}, Data={BitConverter.ToString(data, 0, dlc)}, Timestamp={timestamp}";
                    OnMessageReceived?.Invoke(message);
                }
                else if (status != Canlib.canStatus.canERR_NOMSG)
                {
                    CheckStatus(status, "canReadWait");
                }
            }
        }

        public string ReceiveMessageV2(bool start)
        {
            string meg = ".";
            Canlib.canStatus status;
            byte[] data = new byte[8];
            int id;
            int dlc;
            int flags;
            long timestamp;
            bool finished = false;

            Console.WriteLine("Press the Spacebar to stop receiving messages.");


            status = Canlib.canReadWait(handle, out id, data, out dlc, out flags, out timestamp, 100);

            if (status == Canlib.canStatus.canOK)
            {
                meg = $"Received Message: ID={id}, DLC={dlc}, Data={BitConverter.ToString(data, 0, dlc)}, Timestamp={timestamp}";
            }
            else if (status != Canlib.canStatus.canERR_NOMSG)
            {
                CheckStatus(status, "canReadWait");
                meg = "caneait"; // Exit the loop if an error occurs
            }

            return meg;

        }


        public byte[] ReceiveMessageSpecificForAxioTest()
        {
            string meg = ".";
            Canlib.canStatus status;
            byte[] data = new byte[8];
            int id;
            int dlc;
            int flags;
            long timestamp;
            bool finished = false;

            Console.WriteLine("Press the Spacebar to stop receiving messages.");


            status = Canlib.canReadWait(handle, out id, data, out dlc, out flags, out timestamp, 100);

            if (status == Canlib.canStatus.canOK)
            {
                meg = $"Received Message: ID={id}, DLC={dlc}, Data={BitConverter.ToString(data, 0, dlc)}, Timestamp={timestamp}";

                if (id == 0x18FFA00)
                {
                    data[0] = 1;
                    data[1] = 1;

                    return data;
                }
            }
            else if (status != Canlib.canStatus.canERR_NOMSG)
            {
                CheckStatus(status, "canReadWait");
                meg = "caneait"; // Exit the loop if an error occurs
            }

            return data;

        }

        public byte[] RGetdatas()
        {
            string meg = ".";
            Canlib.canStatus status;
            byte[] data = new byte[8];
            int id;
            int dlc;
            int flags;
            long timestamp;
            bool finished = false;

            //Console.WriteLine("Press the Spacebar to stop receiving messages.");


            status = Canlib.canReadWait(handle, out id, data, out dlc, out flags, out timestamp, 100);

            if (status == Canlib.canStatus.canOK)
            {
                //meg = $"Received Message: ID={id}, DLC={dlc}, Data={BitConverter.ToString(data, 0, dlc)}, Timestamp={timestamp}";

                if (id == 419428864/*0x18FFA00*/)
                {


                    // return data;
                    datareceivedFFFA[0] = data[0];
                    datareceivedFFFA[1] = data[1];
                    datareceivedFFFA[2] = data[2];
                    datareceivedFFFA[3] = data[3];
                    datareceivedFFFA[4] = data[4];
                    datareceivedFFFA[5] = data[5];
                    datareceivedFFFA[6] = data[6];
                    datareceivedFFFA[7] = data[7];
                }
            }
            else if (status != Canlib.canStatus.canERR_NOMSG)
            {
                CheckStatus(status, "canReadWait");
                //meg = "caneait"; // Exit the loop if an error occurs
            }

            if (id == 419428864)
                strid = id.ToString();
            return datareceivedFFFA;

        }
        public string getid()
        {

            return strid;
        }

        public void GoOffBus()
        {
            var status = Canlib.canBusOff(handle);
            CheckStatus(status, "canBusOff");
        }

        public void CloseChannel()
        {
            var status = Canlib.canClose(handle);
            CheckStatus(status, "canClose");
        }


        private void CheckStatus(Canlib.canStatus status, string method)
        {
            if (status < 0)
            {
                string errorText;
                Canlib.canGetErrorText(status, out errorText);

                // Check if there's an open form
                if (System.Windows.Forms.Application.OpenForms.Count > 0)
                {
                    // Use the first open form to invoke the MessageBox
                    System.Windows.Forms.Application.OpenForms[0].Invoke(new MethodInvoker(delegate
                    {
                        MessageBox.Show($"{method} failed: {errorText}");
                    }));
                }
                else
                {
                    // Fallback if no form is open
                    Console.WriteLine($"{method} failed: {errorText}");
                }
            }
        }
        private void CheckStatussold(Canlib.canStatus status, string method)
        {
            if (status < 0)
            {
                string errorText;
                Canlib.canGetErrorText(status, out errorText);
                Console.WriteLine($"{method} failed: {errorText}");
            }
        }
    }
}
