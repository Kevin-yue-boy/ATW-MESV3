using ATW.CommonBase.CommonInterface.DataAccess;
using ATW.CommonBase.DataAccess.Common;
using ATW.CommonBase.DataAccess.Redis;
using ATW.CommonBase.Method.Log;
using ATW.CommonBase.Model.Enum;
using ATW.CommonBase.Model.Log;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace ATW.CommonBase.Global
{
    public class ProjectService_CommonBase
    {

        #region Parameter

        private ICacheRepository ICR { get; set; }

        #endregion

        #region 构造函数

        public ProjectService_CommonBase(ICacheRepository iCR)
        {
            ICR = iCR;
        }

        #endregion

        public void StartServices()
        {
            
            //激活HSL
            var result_ActiveHSL = HslCommunication.Authorization.SetAuthorizationCode("34b93424-dc03-47ee-a2c1-aeaa12730521");
            Logger.OperateLog((result_ActiveHSL ? "HSL服务激活成功！" : "HSL服务激活失败！"));
            ICR.DeleteKey("MainDB");
            Logger.OperateLog("基础数据缓存已清空！");

        }

    }
}
