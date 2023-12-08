using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF_bonfire
{
    public class YATCH_PGNmanager
    {
      

        /*
         * the following pgns are sent by lcd 
0x18FF2100 (419373312)
0x18FF7300 (419394304)
0x18FF7400 (419394560)
         */
        VCIPGN vpn_0x18FF2100_out;
        VCIPGN vpn_0x18FF7300_out;
        VCIPGN vpn_0x18FF7400_out;
        VCIPGN vpn_0x18FF30029_CUVERSION;
        VCIPGN vpn_0x0CFF5434_AMVERSION;

       const int PGN_NUM = 5;

        VCIPGN[] _myPGNarray;
        Dictionary<int, VCIPGN> _myPGNdictionary ;
        public YATCH_PGNmanager() {

            _myPGNdictionary = new Dictionary<int, VCIPGN>();
            vpn_0x18FF30029_CUVERSION = new VCIPGN("0x18FF3029", "CU Version pv or adr29");
            vpn_0x0CFF5434_AMVERSION = new VCIPGN("0x0CFF5434", " AM Version pv"); 

            //vpn_0x0CFF5400= new VCIPGN("0x0CFF5400", " AM Version pv");// major data[4] minor data[5] rev msb data[7] lsbdata[6]
            //vpn_0x18FEFC00= new VCIPGN("0x18FEFC00", "Indication pv or adr21?");
            //vpn_0x18FF4900= new VCIPGN("0x18FF4900", "Faults pv or adr29");
            //vpn_0x18FF5900= new VCIPGN("0x18FF5900", "raw switch states pv or adr29");
            //vpn_0x18FF6400= new VCIPGN("0x18FF6400", "calibration pv or adr29");
            //vpn_0x18FF8E00= new VCIPGN("0x18FF8E00", "Station 1 control inputs pv");
            //vpn_0x18FF8F00= new VCIPGN("0x18FF8F00", "Station 2 control inputs pv");
            //vpn_0x18FF6500= new VCIPGN("0x18FF6500", "Dynamic Positioning pv");
            //vpn_0x18FF6600= new VCIPGN("0x18FF6600", "DP Diagnostics 1 pv");
            //vpn_0x18FF6700= new VCIPGN("0x18FF6700", "DP Diagnostics 2");
            //vpn_0x09F11200 = new VCIPGN("0x09F11200", "GPS Headings pv");
            //vpn_0x09F80100= new VCIPGN("0x09F80100", "GPS Position pv");

            vpn_0x18FF2100_out = new VCIPGN("0x18FF2100", "sent out from pv");
            vpn_0x18FF7300_out= new VCIPGN("0x18FF7300", "sent out from pv");
            vpn_0x18FF7400_out= new VCIPGN("0x18FF7400", "sent out from pv");
     

            _myPGNarray = new VCIPGN[PGN_NUM] {
                vpn_0x18FF2100_out,
                vpn_0x18FF7300_out, 
                vpn_0x18FF7400_out,
                vpn_0x18FF30029_CUVERSION, 
                vpn_0x0CFF5434_AMVERSION,
    
            };

            for(int i =0;i< PGN_NUM; i++)
            {
                _myPGNdictionary.Add(_myPGNarray[i].RawPgn_Address_UniqueKey, _myPGNarray[i]);
            }

        }

        public VCIPGN GetVCIPGN_From_Dict_By_UniquePGNAdrrs(int arguniquerawPgnAddres)
        {
            VCIPGN _temp = new VCIPGN("0x00000000", "Unknown PGN");
            if (_myPGNdictionary.ContainsKey(arguniquerawPgnAddres))
            {
                _temp = _myPGNdictionary[arguniquerawPgnAddres];
            }
            return _temp;
        }
    }
}
