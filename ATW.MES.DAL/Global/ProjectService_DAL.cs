using ATW.CommonBase.Method.Log;
using ATW.CommonBase.Model.Enum;
using ATW.CommonBase.Model.Log;
using CommunityToolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.DAL.Global
{
    public class ProjectService_DAL
    {

        #region Parameter

        private CreateDataBaseDAL createDataBaseDAL { get; set; }

        #endregion

        #region 构造函数

        public ProjectService_DAL(CreateDataBaseDAL createDataBaseDAL)
        {
            this.createDataBaseDAL = createDataBaseDAL;
        }

        #endregion

        public void StartServices()
        {
            //生成数据库表
            createDataBaseDAL.StartCreateMESDataBaseTable();
            Logger.OperateLog($"生成数据库表加载完成!");
        }

    }
}
