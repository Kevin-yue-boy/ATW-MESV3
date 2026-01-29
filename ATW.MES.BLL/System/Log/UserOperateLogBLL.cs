using ATW.CommonBase.CommonInterface.Communicate;
using ATW.CommonBase.Model.DataAccess;
using ATW.MES.DAL.Equipment.BaseEquipment;
using ATW.MES.DAL.System.Log;
using ATW.MES.Model.DTOs.Equipment.BaseEquipment;
using ATW.MES.Model.DTOs.System.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.BLL.System.Log
{
    public class UserOperateLogBLL
    {

        #region Parameter

        /// <summary>
        /// 用户操作日志
        /// </summary>
        private UserOperateLogDAL UserLogDAL { get; set; }

        #endregion

        #region 构造函数

        public UserOperateLogBLL(UserOperateLogDAL userLog)
        {
            this.UserLogDAL = userLog;
        }

        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserOperateLogDTO>> PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest
            , string field
            , string value
            , DateTime start
            , DateTime end)
        {
            return await UserLogDAL.PagingQueryAsync(pagingQueryRequest, field,value, start,end);
        }

        #endregion



    }
}
