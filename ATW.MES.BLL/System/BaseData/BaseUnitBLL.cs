using ATW.CommonBase.DataProcessing.DataCheck;
using ATW.CommonBase.Model.DataAccess;
using ATW.CommonBase.Model;
using ATW.MES.DAL.System.BaseData;
using ATW.MES.Model.DTOs.System.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.BLL.System.BaseData
{
    public class BaseUnitBLL
    {
        #region Parameter

        /// <summary>
        /// 单位数据访问层
        /// </summary>
        private BaseUnitDAL BaseUnitDAL { get; set; }

        #endregion

        #region 构造函数

        public BaseUnitBLL(BaseUnitDAL baseUnitDAL)
        {
            this.BaseUnitDAL = baseUnitDAL;
        }

        #endregion

        #region 添加单位

        /// <summary>
        /// 添加单位信息
        /// </summary>
        /// <param name="baseUnitResponse">单位信息</param>
        /// <param name="responseModel">反馈添加结果</param>
        /// <returns></returns>
        public async Task Insert(BaseUnitResponse baseUnitResponse, ResponseModel responseModel)
        {
            try
            {
                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(baseUnitResponse, false, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 数据重复校验

                var baseUnitResponses = await BaseUnitDAL.Get();

                // 校验单位名称是否重复
                var exist_UnitName = baseUnitResponses.Exists(it => { return it.UnitName == baseUnitResponse.UnitName; });
                if (exist_UnitName)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"单位名称:{baseUnitResponse.UnitName}已存在！";
                    return;
                }

                #endregion

                // 初始化基础字段
                baseUnitResponse.GUID = Guid.NewGuid();
                baseUnitResponse.LastEditTime = DateTime.Now;
                baseUnitResponse.CreateTime = DateTime.Now;

                // 执行新增并返回结果
                responseModel.Result = (await BaseUnitDAL.Insert(baseUnitResponse)) == 1;
                responseModel.Msg += responseModel.Result ? "添加成功！" : "添加失败！";
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"异常报错:{ex.ToString()}！";
            }
        }

        #endregion

        #region 编辑单位

        /// <summary>
        /// 编辑单位信息
        /// </summary>
        /// <param name="baseUnitResponse">单位信息（新值）</param>
        /// <param name="baseUnitResponse_Old">单位信息（原始数据）</param>
        /// <param name="responseModel">反馈编辑结果</param>
        /// <returns></returns>
        public async Task Edit(BaseUnitResponse baseUnitResponse,
            BaseUnitResponse baseUnitResponse_Old, ResponseModel responseModel)
        {
            try
            {
                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(baseUnitResponse, baseUnitResponse_Old, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 数据重复校验

                var baseUnitResponses = await BaseUnitDAL.Get();

                // 校验单位名称是否重复（排除自身）
                var exist_UnitName = baseUnitResponses.Exists(it => {
                    return it.UnitName == baseUnitResponse.UnitName
                    && it.GUID != baseUnitResponse_Old.GUID;
                });
                if (exist_UnitName)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"单位名称:{baseUnitResponse.UnitName}已存在！";
                    return;
                }

                #endregion

                // 更新最后编辑时间
                baseUnitResponse.LastEditTime = DateTime.Now;

                // 执行编辑并返回结果
                responseModel.Result = (await BaseUnitDAL.Edit(baseUnitResponse)) == 1;
                responseModel.Msg += responseModel.Result ? "编辑成功！" : "编辑失败！";
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"异常报错:{ex.ToString()}！";
            }
        }

        #endregion

        #region 删除单位

        /// <summary>
        /// 删除单位信息
        /// </summary>
        /// <param name="baseUnitResponse">单位信息</param>
        /// <param name="responseModel">反馈删除结果</param>
        /// <returns></returns>
        public async Task Delete(BaseUnitResponse baseUnitResponse, ResponseModel responseModel)
        {
            try
            {
                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(baseUnitResponse, true, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 校验数据存在

                var baseUnitResponses = await BaseUnitDAL.Get();

                var exist = baseUnitResponses.Exists(it => {
                    return it.GUID == baseUnitResponse.GUID;
                });
                if (!exist)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"单位名称:{baseUnitResponse.UnitName}不存在！";
                    return;
                }

                #endregion

                // 执行删除并返回结果
                responseModel.Result = (await BaseUnitDAL.Delete(baseUnitResponse)) == 1;
                responseModel.Msg += responseModel.Result ? "删除成功！" : "删除失败！";
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"异常报错:{ex.ToString()}！";
            }
        }

        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询单位信息
        /// </summary>
        /// <param name="pagingQueryRequest">分页查询条件</param>
        /// <returns>分页后的单位信息列表</returns>
        public async Task<List<BaseUnitResponse>> PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest)
        {
            return await BaseUnitDAL.PagingQueryAsync(pagingQueryRequest);
        }

        #endregion
    }
}
