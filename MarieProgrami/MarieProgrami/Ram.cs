using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarieProgrami
{
    class Ram
    {
        public LinkedList<RamInfo> bellek;
        public Dictionary<string, string> commandList;

     
    }
    class RamInfo
    {
        public string adres;
        public string info;
        
        public int getAdresToHex()
        {
            return Convert.ToSByte(adres, 16);
        }
        public int getInfoToHex()
        {
            return Convert.ToSByte(info, 16);
        }
    }
}
