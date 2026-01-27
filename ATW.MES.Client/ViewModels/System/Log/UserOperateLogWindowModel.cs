using ATW.MES.Model.DTOs.System.Log;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Client.ViewModels.System.Log
{
    public class UserOperateLogWindowModel : ObservableValidator
    {

        #region Parameter

        /// <summary>
        /// 日志
        /// </summary>
        public string Log { get; set; }

        #endregion

        #region 加载

        public UserOperateLogWindowModel(UserOperateLogResponse userOperateLogResponse)
        {
            Log = $"记录:\r  {userOperateLogResponse.Log}\r" +
                  $"底层数据:\r  {userOperateLogResponse.UnderlyingData}";
        }

        #endregion



    }
}
