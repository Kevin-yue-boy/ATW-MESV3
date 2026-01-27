using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.Models.Equipment.BaseEquipment
{
    public class PLCConnectAddressModel
    {

        public string IP { get; set; }
        public int Port { get; set; }
        public string HeartAddress { get; set; }


    }
}
