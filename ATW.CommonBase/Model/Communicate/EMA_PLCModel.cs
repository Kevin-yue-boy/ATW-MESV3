using ATW.CommonBase.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.Model.Communicate
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, Inherited = true)]
    public class EMA_PLCModel : Attribute
    {
        public UInt32 SN { get; set; }
        public EnumDataType DataType { get; set; }
        public Int32 DataLength { get; set; }
        public Int32 ArrLength { get; set; }

    }
}
