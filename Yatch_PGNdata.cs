using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF_bonfire
{
    public class Yatch_PGNdata
    {
        //  @GPSDevice = GPS("GPS", 0x7f); // 0x7f is the address of the GPS device

        #region SendByLCD



        //=======================================================
        // address 00looks like it's sending the data from the pv780 and have adress 0x00  none of these are handled by the Motherboard MBII
        //=======================================================

        int _BOW_THRUSTER_ENABLE_DISABLE_0x18FF7500 = 0x18FF7500; //fyi lcd sends this out: if (gBowThrusterDisabled) B0 = 1 else B0 = 0; from lcd sta1 or 2 no difference
        byte[] _data_FF75;

        int _DP_COMMANDED_HEADING_0x18FF7400 = 0x18FF7400;         //fyi lcd sends this out B0 = Heading *100  B1 = heading *100 >> 8
        byte[] _data_FF74;

        int _DP_COMMANDED_POSITION_0x18FF7300 = 0x18FF7300;
        byte[] _data_FF73;
        //fyi lcd sends this out this way: 
        //
        // int longitudeSend = int(longitude * 1E7);
        // int latitudeSend = int(latitude * 1E7);
        // data[0] = latitudeSend;
        // data[1] = latitudeSend >> 8;
        //data[2] = latitudeSend >> 16;
        //data[3] = latitudeSend >> 24;
        //data[4] = longitudeSend;
        //data[5] = longitudeSend >> 8;
        //data[6] = longitudeSend >> 16;
        //data[7] = longitudeSend >> 24;



        int _DP_CONTROL_0x18FF2100 = 0x18FF2100;
        byte[] _data_FF21;

        //fyi lcd seds this  way. mode is a enum DPMode { HeadingAndPosition = 0, PositionOnly = 1, HeadingOnly = 2 };
        // it is set by void Setmde(DpMode mode){ this.node =mode; } and called by button 1 press in menuuHandler 
        //data[0] = mode + 1;
        //data[2] = 0;
        //data[3] = 0;

        #endregion



        #region PGNS_SentByMotherBoard

        //=======================================================address 0x29
        //0xFF30: Send messages to clutch panel (also Received by LDC as "CU Version") ------------------------_MUST BE KeepAlive
        int _pgn_ff30 = 0x18FF3029;
        byte[] _data_FF30;
        //0xFF53: Send messages to CANBUS station  (also Received by LDC as "System Information")   -----------_MUST BE KeepAlive
        int _pgn_ff53 = 0x18FF5329;
        byte[] _data_FF53;
        //0xFF49 : Control Unit Faults (also Received by LDC as "Faults")
        int _pgn_ff49 = 0x18FF4929;
        byte[] _data_FF49;
        //0xFF59: Raw Analog Inputs: Port Int, Stbd Int, Autopilot, Switch States (also Received by LDC as "Raw Switch States)
        int _pgn_ff59 = 0x18FF5929;
        byte[] _data_FF59;
        //0xFF62: Scaled Control Commands: Port Noz, Stbd Noz
        int _pgn_ff62 = 0x18FF6229;
        byte[] _data_FF62;
        //0xFF63: Scaled Control Commands: Port Bkt, Stbd Bkt
        int _pgn_ff63 = 0x18FF6329;
        byte[] _data_FF63;
        //0xFF64: Calibration Feedback  (also Received by LDC as "Calibration")
        int _pgn_ff64 = 0x18FF6429;
        byte[] _data_FF64;
        //0xFF60: System Parameters (always transmit)
        int _pgn_ff60 = 0x18FF6029;
        byte[] _data_FF60;
        //=======================================================address 0x21
        //0xFEFC : Livorsi Gauges  (also Received by LDC as "Indication")
        int _pgn_fefc = 0x18FEFC21;
        byte[] _data_FEFC;

        #endregion

        #region PGNS_ReceivedByMotherBoard
        //=======================================================address 0x00
        //0xFF50: CAN bus station address 0x00
        int _pgn_ff50 = 0x18FF5000;
        byte[] _data_FF50;
        //0xFF51: CAN Bus Station address 0x00
        int _pgn_ff51 = 0x18FF5100;
        byte[] _data_FF51;
        //0xFF31: Clutch Panel #1 address 0x00
        int _pgn_ff31 = 0x18FF3100;
        byte[] _data_FF31;
        //0xFF32: Clutch Panel #2 address 0x00
        int _pgn_ff32 = 0x18FF3200;
        byte[] _data_FF32;
        //0xFF34: Clutch Panel #3 address 0x00
        int _pgn_ff34 = 0x18FF3400;
        byte[] _data_FF34;
        //0xFF35: Booster Panel#1 address 0x00
        int _pgn_ff35 = 0x18FF3500;
        byte[] _data_FF35;
        //0xFF34: Booster Panel#2 address 0x00
        int _pgn_ff36 = 0x18FF3600;
        byte[] _data_FF36;
        //0xFF36: Booster Panel#3 address 0x00
        int _pgn_ff37 = 0x18FF3700;
        byte[] _data_FF37;
        //0xFF36: Calinbration Feedback address 0x00
        int _pgn_ff38 = 0x18FF3800;
        byte[] _data_FF38;
        //0xFFFC: CAN Configure address 0x00
        int _pgn_fffc = 0x18FFFC00;
        byte[] _data_FFFC;
        #endregion

        #region ReceivedBy LCD  onkly
        //=======================================================address 0x00 priority 0x0C
        //0xFF54: AM Version   -----------------------------------------------------------------_MUST BE KeepAlive
        int _pgn_ff54 = 0x0CFF5400;
        byte[] _data_FF54;
        //=======================================================address 0x00
        //0cFF8E: Station 1 Control Inputs
        int _pgn_ff8e = 0x18FF8E00;
        byte[] _data_FF8E;
        //0cFF8F: Station 2 Control Inputs
        int _pgn_ff8f = 0x18FF8F00;
        byte[] _data_FF8F;
        //0xFF65: Dynamic positionnning
        int _pgn_ff65 = 0x18FF6500;
        byte[] _data_FF65;
        //0xFF66: DP Diagnostics 1
        int _pgn_ff66 = 0x18FF6600;
        byte[] _data_FF66;
        //0xFF67: DP Diagnostics 2
        int _pgn_ff67 = 0x18FF6700;
        byte[] _data_FF67;
        //=======================================================address 0x00 priority 0x09
        //0xF112: GPS Heading
        int _pgn_f112 = 0x09F11200;
        byte[] _data_F112;
        //0xF801: GPS Position
        int _pgn_f801 = 0x09F80100;
        byte[] _data_F801;
        #endregion

        Dictionary<string, byte[]> _data_Dictionary;
        public Yatch_PGNdata() {

            _data_FF21 = new byte[8];
            _data_FF30 = new byte[8];
            _data_FF53 = new byte[8];
            _data_FF49 = new byte[8];
            _data_FF59 = new byte[8];
            _data_FF62 = new byte[8];
            _data_FF63 = new byte[8];
            _data_FF64 = new byte[8];
            _data_FF60 = new byte[8];
            _data_FEFC = new byte[8];
            _data_FF50 = new byte[8];
            _data_FF51 = new byte[8];
            _data_FF31 = new byte[8];
            _data_FF32 = new byte[8];
            _data_FF34 = new byte[8];
            _data_FF35 = new byte[8];
            _data_FF36 = new byte[8];
            _data_FF37 = new byte[8];
            _data_FF38 = new byte[8];
            _data_FFFC = new byte[8];
            _data_FF54 = new byte[8];
            _data_FF8E = new byte[8];
            _data_FF8F = new byte[8];
            _data_FF65 = new byte[8];
            _data_FF66 = new byte[8];
            _data_FF67 = new byte[8];
            _data_FF73 = new byte[8];
            _data_FF74 = new byte[8];
            _data_FF75 = new byte[8];
            _data_F801 = new byte[8];
            _data_F112 = new byte[8];


            _data_Dictionary = new Dictionary<string, byte[]>();
            _data_Dictionary.Add("0x18FF2100", _data_FF21);
            _data_Dictionary.Add("0x18FF3029", _data_FF30);
            _data_Dictionary.Add("0x18FF3100", _data_FF31);
            _data_Dictionary.Add("0x18FF3200", _data_FF32);
            _data_Dictionary.Add("0x18FF3400", _data_FF34);
            _data_Dictionary.Add("0x18FF3500", _data_FF35);
            _data_Dictionary.Add("0x18FF3600", _data_FF36);
            _data_Dictionary.Add("0x18FF3700", _data_FF37);
            _data_Dictionary.Add("0x18FF3800", _data_FF38);
            _data_Dictionary.Add("0x18FF4929", _data_FF49);
            _data_Dictionary.Add("0x18FF5000", _data_FF50);
            _data_Dictionary.Add("0x18FF5100", _data_FF51);
            _data_Dictionary.Add("0x18FF5329", _data_FF53);
            _data_Dictionary.Add("0x18FF5929", _data_FF59);
            _data_Dictionary.Add("0x18FF6329", _data_FF63);
            _data_Dictionary.Add("0x18FF6429", _data_FF64);
            _data_Dictionary.Add("0x18FF6029", _data_FF60);
            _data_Dictionary.Add("0x18FF6229", _data_FF62);
            _data_Dictionary.Add("0x18FF6500", _data_FF65);
            _data_Dictionary.Add("0x18FF6600", _data_FF66);
            _data_Dictionary.Add("0x18FF6700", _data_FF67);
            _data_Dictionary.Add("0x18FFFC00", _data_FFFC);
            _data_Dictionary.Add("0x0CFF5400", _data_FF54);
            _data_Dictionary.Add("0x18FF8E00", _data_FF8E);
            _data_Dictionary.Add("0x18FF8F00", _data_FF8F);
            _data_Dictionary.Add("0x18FF7300", _data_FF73);
            _data_Dictionary.Add("0x18FF7400", _data_FF74);
            _data_Dictionary.Add("0x18FF7500", _data_FF75);
            _data_Dictionary.Add("0x09F11200", _data_F112);
            _data_Dictionary.Add("0x09F80100", _data_F801);
            _data_Dictionary.Add("0x18FEFC21", _data_FEFC);


            set_allData_to0();
        }

        public int Get_Convert_StringPGN_to_int(string argpgn)
        {
            int _pgn = 0;
            try
            {
                _pgn = Convert.ToInt32(argpgn, 16);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return _pgn;
        }

        public byte[] Get_dataFromDictionaryPGN(string argPGN)
        {
            byte[] _data = new byte[8];
            try
            {
                _data = _data_Dictionary[argPGN];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return _data;
        }

      
        public void Set_dataFromDictionaryPGN(string argPGN, byte[] argData)
        {
            try
            {
                _data_Dictionary[argPGN] = argData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Update_aByte_yIndex(string argPGN, int argIndex, byte argData)
        {
            try
            {
                _data_Dictionary[argPGN][argIndex] = argData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public byte Get_aByte_yIndex(string argPGN, int argIndex)
        {
            byte _data = 0;
            try
            {
                _data = _data_Dictionary[argPGN][argIndex];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return _data;
        }

        void set_allData_to0() {

            Set_dataFromDictionaryPGN("0x18FF2100", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF3029", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF3100", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF3200", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF3400", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF3500", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF3600", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF3700", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF3800", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF4929", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF5000", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF5100", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF5329", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF5929", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF6329", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF6429", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF6029", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF6229", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF6500", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF6600", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF6700", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FFFC00", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x0CFF5400", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF8E00", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF8F00", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF7300", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF7400", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FF7500", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x09F11200", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x09F80100", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Set_dataFromDictionaryPGN("0x18FEFC21", new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
        }
        void set_KeepAlive_PGN_Data() {

            _data_FF30[0]=  0x00;
            _data_FF30[1] = 0x00;
            _data_FF30[2] = 0x00;
            _data_FF30[3] = 0x00;
            _data_FF30[4] = 0x00;
            _data_FF30[5] = 0x00;
            _data_FF30[6] = 0x00;
            _data_FF30[7] = 0x00;

            _data_FF53[0] = 0x00;
            _data_FF53[1] = 0x00;
            _data_FF53[2] = 0x00;
            _data_FF53[3] = 0x00;
            _data_FF53[4] = 0x00;
            _data_FF53[5] = 0x00;
            _data_FF53[6] = 0x00;
            _data_FF53[7] = 0x00;

            _data_FF54[0] = 0x00;
            _data_FF54[1] = 0x00;
            _data_FF54[2] = 0x00;
            _data_FF54[3] = 0x00;
            _data_FF54[4] = 0x00;
            _data_FF54[5] = 0x00;
            _data_FF54[6] = 0x00;
            _data_FF54[7] = 0x00;

        }

        
    }
}
