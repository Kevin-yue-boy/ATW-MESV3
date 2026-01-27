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
    public class BaseProcessBLL
    {
        #region Parameter

        /// <summary>
        /// 工序数据访问层
        /// </summary>
        private BaseProcessDAL BaseProcessDAL { get; set; }

        #endregion

        #region 构造函数

        // 修复示例中构造函数的语法错误，保持参数注入逻辑一致
        public BaseProcessBLL(BaseProcessDAL baseProcessDAL)
        {
            this.BaseProcessDAL = baseProcessDAL;
        }

        #endregion

        #region 添加工序

        /// <summary>
        /// 添加工序信息
        /// </summary>
        /// <param name="baseProcessResponse">工序信息</param>
        /// <param name="responseModel">反馈添加结果</param>
        /// <returns></returns>
        public async Task Insert(BaseProcessResponse baseProcessResponse, ResponseModel responseModel)
        {
            try
            {
                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(baseProcessResponse, false, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 数据重复校验

                var baseProcessResponses = await BaseProcessDAL.Get();

                // 校验工序名称是否重复
                var exist_ProcessName = baseProcessResponses.Exists(it => { return it.ProcessName == baseProcessResponse.ProcessName; });
                if (exist_ProcessName)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"工序名称:{baseProcessResponse.ProcessName}已存在！";
                    return;
                }

                // 校验工序编码是否重复
                var exist_ProcessCode = baseProcessResponses.Exists(it => { return it.ProcessCode == baseProcessResponse.ProcessCode; });
                if (exist_ProcessCode)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"工序编码:{baseProcessResponse.ProcessCode}已存在！";
                    return;
                }

                #endregion

                // 初始化基础字段
                baseProcessResponse.GUID = Guid.NewGuid();
                baseProcessResponse.LastEditTime = DateTime.Now;
                baseProcessResponse.CreateTime = DateTime.Now;

                // 执行新增并返回结果
                responseModel.Result = (await BaseProcessDAL.Insert(baseProcessResponse)) == 1;
                responseModel.Msg += responseModel.Result ? "添加成功！" : "添加失败！";
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"异常报错:{ex.ToString()}！";
            }
        }

        #endregion

        #region 编辑工序

        /// <summary>
        /// 编辑工序信息
        /// </summary>
        /// <param name="baseProcessResponse">工序信息（新值）</param>
        /// <param name="baseProcessResponse_Old">工序信息（原始数据）</param>
        /// <param name="responseModel">反馈编辑结果</param>
        /// <returns></returns>
        public async Task Edit(BaseProcessResponse baseProcessResponse,
            BaseProcessResponse baseProcessResponse_Old, ResponseModel responseModel)
        {
            try
            {
                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(baseProcessResponse, baseProcessResponse_Old, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 数据重复校验

                var baseProcessResponses = await BaseProcessDAL.Get();

                // 校验工序名称是否重复（排除自身）
                var exist_ProcessName = baseProcessResponses.Exists(it => {
                    return it.ProcessName == baseProcessResponse.ProcessName
                    && it.GUID != baseProcessResponse_Old.GUID;
                });
                if (exist_ProcessName)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"工序名称:{baseProcessResponse.ProcessName}已存在！";
                    return;
                }

                // 校验工序编码是否重复（排除自身）
                var exist_ProcessCode = baseProcessResponses.Exists(it => {
                    return it.ProcessCode == baseProcessResponse.ProcessCode
                    && it.GUID != baseProcessResponse_Old.GUID;
                });
                if (exist_ProcessCode)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"工序编码:{baseProcessResponse.ProcessCode}已存在！";
                    return;
                }

                #endregion

                // 更新最后编辑时间
                baseProcessResponse.LastEditTime = DateTime.Now;

                // 执行编辑并返回结果
                responseModel.Result = (await BaseProcessDAL.Edit(baseProcessResponse)) == 1;
                responseModel.Msg += responseModel.Result ? "编辑成功！" : "编辑失败！";
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"异常报错:{ex.ToString()}！";
            }
        }

        #endregion

        #region 删除工序

        /// <summary>
        /// 删除工序信息
        /// </summary>
        /// <param name="baseProcessResponse">工序信息</param>
        /// <param name="responseModel">反馈删除结果</param>
        /// <returns></returns>
        public async Task Delete(BaseProcessResponse baseProcessResponse, ResponseModel responseModel)
        {
            try
            {
                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(baseProcessResponse, true, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 校验数据存在

                var baseProcessResponses = await BaseProcessDAL.Get();

                // 修复示例中存在的逻辑错误：原逻辑判断GUID != 目标GUID，此处修正为 == 来校验数据是否存在
                var exist = baseProcessResponses.Exists(it => {
                    return it.GUID == baseProcessResponse.GUID;
                });
                if (!exist)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"工序名称:{baseProcessResponse.ProcessName}不存在！";
                    return;
                }

                #endregion

                // 执行删除并返回结果
                responseModel.Result = (await BaseProcessDAL.Delete(baseProcessResponse)) == 1;
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
        /// 分页查询工序信息
        /// </summary>
        /// <param name="pagingQueryRequest">分页查询条件</param>
        /// <returns>分页后的工序信息列表</returns>
        public async Task<List<BaseProcessResponse>> PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest)
        {
            return await BaseProcessDAL.PagingQueryAsync(pagingQueryRequest);
        }

        #endregion
    }
}
