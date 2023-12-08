using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kvaser.CanLib;
namespace WF_bonfire
{
    public partial class Form1 : Form
    {
        CanManager canManager;


        YATCH_PGNmanager yatch_PGNmanager;







        public Form1()
        {
            InitializeComponent();
            canManager = new CanManager();
            canManager.ListChannels();
            canManager.OpenChannel(0);
            canManager.SetBusParams();
            canManager.GoOnBus();

            yatch_PGNmanager=   new YATCH_PGNmanager();

            timer1_sysinfo.Tick += TimerSysifoTick;
            timer1_sysinfo.Start();

            timer2_SendCan.Tick += TimerSendCan;
            timer2_SendCan.Start();
        }




        byte Get_LowByte(int argNum)
        {
            byte _temp = 0x00;
            _temp = (byte)(argNum & 0x00FF);
            return _temp;
        }
        byte Get_HighByte(int argNum)
        {
            byte _temp = 0x00;
            _temp = (byte)((argNum & 0xFF00) >> 8);
            return _temp;
        }   

        int Combine_LowHighByte(byte argLow, byte argHigh)
        {
            int _temp = 0x00;
            _temp = (argHigh << 8) | argLow;
            return _temp;
        }

      
       
        // cu version
        void sendFF3029_CUversion()
        {
            // major data[4] minor data[5] rev msb data[7] lsbdata[6]
            byte major = 20;
            byte minor = 15;
            int rev = 1337;
            byte RevLow = Get_LowByte(rev);
            byte RevHigh = Get_HighByte(rev);

            VCIPGN temp_FF30 = yatch_PGNmanager.GetVCIPGN_From_Dict_By_UniquePGNAdrrs(0xff3029);
            int temp_fullpgn = temp_FF30.FUllPGN;
            int temp_rawpgn = temp_FF30.RawPgn;
            temp_FF30.SetByteAtIndex(4, major);
            temp_FF30.SetByteAtIndex(5, minor);
            temp_FF30.SetByteAtIndex(6, RevLow);
            temp_FF30.SetByteAtIndex(7, RevHigh);

            canManager.SendMessage(temp_FF30.FUllPGN, temp_FF30.Data);
        }
        //am version
        void sendFF5400_AMversion()
        {
            // major data[4] minor data[5] rev msb data[7] lsbdata[6]
            byte major = 20;
            byte minor = 15;
            int rev = 1337;
            byte RevLow = Get_LowByte(rev);
            byte RevHigh = Get_HighByte(rev);

            VCIPGN temp_FF30 = yatch_PGNmanager.GetVCIPGN_From_Dict_By_UniquePGNAdrrs(0xff5434);
            int temp_fullpgn = temp_FF30.FUllPGN;
            int temp_rawpgn = temp_FF30.RawPgn;
            temp_FF30.SetByteAtIndex(4, major);
            temp_FF30.SetByteAtIndex(5, minor);
            temp_FF30.SetByteAtIndex(6, RevLow);
            temp_FF30.SetByteAtIndex(7, RevHigh);

            canManager.SendMessage(temp_FF30.FUllPGN, temp_FF30.Data);
        }

        private void TimerSendCan(object sender, EventArgs e) {
            // canManager.SendMessage(_pgn_ff64, _data_ff64);
            //gRSCal 
           // Send_FF30();

        }
        private void TimerSysifoTick(object sender, EventArgs e)
        {
            
            sendFF3029_CUversion();
            sendFF5400_AMversion();
        }
    }
}


/*
          * 	

 0xff53
     NozzlePos[0] = data[3];
     NozzlePos[1] = data[2];

     BucketPos[0] = data[0];
     BucketPos[1] = 250 - data[5];

     EngineCmd[0] = data[6];
     EngineCmd[1] = data[7];

     Joystick_Zone = data[1];

          */