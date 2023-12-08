using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF_bonfire
{
    public class VCIPGN
    {
        int _rawPgn=0x0000;
        int _priority = 0x18;
        int _sourceAddress = 0x00;
        int _fullPgn = 0x00000000;
        int _RawPgn_Address_UniqueKey = 0x000000;
        byte[] _data;



        string _myPgn_Name = "0x18FEFC21";
        string _description = "Unknown PGN";
        public string Description
        {
            get { return _description; }
            private set { _description = value; }
        }
        public string MyPgn_Name
        {
            get { return _myPgn_Name; }
            private set { _myPgn_Name = value; }
        }

        /*
        int get_priority_fromFullPgn(int FullPgn)
        {
            int _temp = 0xAA;
            _temp = (FullPgn & 0x1C000000) >> 26;
            return _temp;
        }
        int get_sourceAddress_fromFullPgn(int FullPgn)
        {
            int _temp = 0xAA;
            _temp = (FullPgn & 0x03FF0000) >> 16;
            return _temp;
        }
        int get_rawPgn_fromFullPgn(int FullPgn)
        {
            int _temp = 0xAA;
            _temp = (FullPgn & 0x0000FFFF);
            return _temp;
        }

        int get_FullPgn_from_string(string pgn)
        {
            pgn = pgn.Replace("0x", "");
            int _temp = 0x00000000;
            if (pgn.Length == 8)
            {
                _temp = Convert.ToInt32(pgn, 16);
            }
            return _temp;
        }
        */
        public byte GetByteAtIndex(int index)
        {
            if(index >= 8)index = 7;
            if(index < 0)index = 0;
            return _data[index];
        }

        public void SetByteAtIndex(int index, byte value)
        {
            if (index >= 8) index = 7;
            if (index < 0) index = 0;
            _data[index] = value;
        }
        public bool get_BitIndex_fromByteAtByteIndex(int byteIndex, int bitIndex)
        {
            if (byteIndex >= 8) byteIndex = 7;
            if (byteIndex < 0) byteIndex = 0;
            if (bitIndex >= 8) bitIndex = 7;
            if (bitIndex < 0) bitIndex = 0;
            return (_data[byteIndex] & (1 << bitIndex)) != 0;
        }

        public void set_BitIndex_fromByteAtByteIndex(int byteIndex, int bitIndex, bool value)
        {
            if (byteIndex >= 8) byteIndex = 7;
            if (byteIndex < 0) byteIndex = 0;
            if (bitIndex >= 8) bitIndex = 7;
            if (bitIndex < 0) bitIndex = 0;
            if (value)
            {
                _data[byteIndex] |= (byte)(1 << bitIndex);
            }
            else
            {
                _data[byteIndex] &= (byte)~(1 << bitIndex);
            }
        }


        public byte[] Data
        {
            get { return _data; }
           private set { _data = value; }
        }
        public int FUllPGN
        {
            get { return _fullPgn; }
           private set { _fullPgn = value; }
        }

        public int SourceAddress
        {
            get { return _sourceAddress; }
            private set { _sourceAddress = value; }
        }

        public int Priority
        {
            get { return _priority; }
            private set { _priority = value; }
        }
        public int RawPgn
        {
            get { return _rawPgn; }
            private set { _rawPgn = value; }
        }

        public int RawPgn_Address_UniqueKey
        {
            get { return _RawPgn_Address_UniqueKey; }
            private set { _RawPgn_Address_UniqueKey = value; }
        }

        public VCIPGN(string myPgn_Name, string argDescription )
        {
            _description = argDescription;
            _myPgn_Name = myPgn_Name;

            _myPgn_Name = _myPgn_Name.Replace("0x", "");
            string _str_prio = _myPgn_Name.Substring(0, 2);
            string _str_Raw_pgn = _myPgn_Name.Substring(2, 4);
            string _str_adress = _myPgn_Name.Substring(6, 2);
            string _str_unique = _myPgn_Name.Substring(2, 6);
            _fullPgn = Convert.ToInt32(_myPgn_Name, 16);
            _priority = Convert.ToInt32(_str_prio, 16);
            _rawPgn = Convert.ToInt32(_str_Raw_pgn, 16);
            _sourceAddress = Convert.ToInt32(_str_adress, 16);
            _RawPgn_Address_UniqueKey = Convert.ToInt32(_str_unique, 16);

            _data = new byte[8];
        }
    }
}
