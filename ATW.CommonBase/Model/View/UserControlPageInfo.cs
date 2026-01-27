using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.Model.View
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class UserControlPageInfo : Attribute
    {

        public string PageName { get; set; }

    }
}
