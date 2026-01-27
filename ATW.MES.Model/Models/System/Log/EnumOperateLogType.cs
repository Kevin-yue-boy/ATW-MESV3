using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.Models.System.Log
{
    public enum EnumOperateLogType
    {

        [Description("系统运行")]
        SystemRunning,

        [Description("添加数据")]
        AddData,

        [Description("删除数据")]
        DeleteData,

        [Description("变更数据")]
        EditData,

    }
}
