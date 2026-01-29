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
    public class BaseWorkTypeBLL
    {
        #region Parameter

        /// <summary>
        /// 工作类型数据访问层
        /// </summary>
        private BaseWorkTypeDAL BaseWorkTypeDAL { get; set; }

        #endregion

        #region 构造函数

        public BaseWorkTypeBLL(BaseWorkTypeDAL baseJobTypeDAL)
        {
            this.BaseWorkTypeDAL = baseJobTypeDAL;
        }

        #endregion

        #region 添加工作类型

        /// <summary>
        /// 添加工作类型信息
        /// </summary>
        /// <param name="baseJobTypeDTO">工作类型信息</param>
        /// <param name="responseModel">反馈添加结果</param>
        /// <returns></returns>
        public async Task Insert(BaseWorkTypeDTO baseJobTypeDTO, ResponseModel responseModel)
        {
            try
            {
                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(baseJobTypeDTO, false, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 数据重复校验

                var baseJobTypeResponses = await BaseWorkTypeDAL.Get();

                // 校验工作类型名称是否重复
                var exist_JobTypeName = baseJobTypeResponses.Exists(it => { return it.WorkTypeName == baseJobTypeDTO.WorkTypeName; });
                if (exist_JobTypeName)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"工作类型名称:{baseJobTypeDTO.WorkTypeName}已存在！";
                    return;
                }

                // 校验工作类型编码是否重复
                var exist_JobTypeCode = baseJobTypeResponses.Exists(it => { return it.WorkTypeCode == baseJobTypeDTO.WorkTypeCode; });
                if (exist_JobTypeCode)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"工作类型编码:{baseJobTypeDTO.WorkTypeCode}已存在！";
                    return;
                }

                #endregion

                // 初始化基础字段
                baseJobTypeDTO.GUID = Guid.NewGuid();
                baseJobTypeDTO.LastEditTime = DateTime.Now;
                baseJobTypeDTO.CreateTime = DateTime.Now;

                // 执行新增并返回结果
                responseModel.Result = (await BaseWorkTypeDAL.Insert(baseJobTypeDTO)) == 1;
                responseModel.Msg += responseModel.Result ? "添加成功！" : "添加失败！";
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"异常报错:{ex.ToString()}！";
            }
        }

        #endregion

        #region 编辑工作类型

        /// <summary>
        /// 编辑工作类型信息
        /// </summary>
        /// <param name="baseJobTypeDTO">工作类型信息（新值）</param>
        /// <param name="baseJobTypeDTO_Old">工作类型信息（原始数据）</param>
        /// <param name="responseModel">反馈编辑结果</param>
        /// <returns></returns>
        public async Task Edit(BaseWorkTypeDTO baseJobTypeDTO,
            BaseWorkTypeDTO baseJobTypeDTO_Old, ResponseModel responseModel)
        {
            try
            {
                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(baseJobTypeDTO, baseJobTypeDTO_Old, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 数据重复校验

                var baseJobTypeResponses = await BaseWorkTypeDAL.Get();

                // 校验工作类型名称是否重复（排除自身）
                var exist_JobTypeName = baseJobTypeResponses.Exists(it => {
                    return it.WorkTypeName == baseJobTypeDTO.WorkTypeName
                    && it.GUID != baseJobTypeDTO_Old.GUID;
                });
                if (exist_JobTypeName)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"工作类型名称:{baseJobTypeDTO.WorkTypeName}已存在！";
                    return;
                }

                // 校验工作类型编码是否重复（排除自身）
                var exist_JobTypeCode = baseJobTypeResponses.Exists(it => {
                    return it.WorkTypeCode == baseJobTypeDTO.WorkTypeCode
                    && it.GUID != baseJobTypeDTO_Old.GUID;
                });
                if (exist_JobTypeCode)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"工作类型编码:{baseJobTypeDTO.WorkTypeCode}已存在！";
                    return;
                }

                #endregion

                // 更新最后编辑时间
                baseJobTypeDTO.LastEditTime = DateTime.Now;

                // 执行编辑并返回结果
                responseModel.Result = (await BaseWorkTypeDAL.Edit(baseJobTypeDTO)) == 1;
                responseModel.Msg += responseModel.Result ? "编辑成功！" : "编辑失败！";
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"异常报错:{ex.ToString()}！";
            }
        }

        #endregion

        #region 删除工作类型

        /// <summary>
        /// 删除工作类型信息
        /// </summary>
        /// <param name="baseJobTypeDTO">工作类型信息</param>
        /// <param name="responseModel">反馈删除结果</param>
        /// <returns></returns>
        public async Task Delete(BaseWorkTypeDTO baseJobTypeDTO, ResponseModel responseModel)
        {
            try
            {
                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(baseJobTypeDTO, true, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 校验数据存在

                var baseJobTypeResponses = await BaseWorkTypeDAL.Get();

                var exist = baseJobTypeResponses.Exists(it => {
                    return it.GUID == baseJobTypeDTO.GUID;
                });
                if (!exist)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"工作类型名称:{baseJobTypeDTO.WorkTypeName}不存在！";
                    return;
                }

                #endregion

                // 执行删除并返回结果
                responseModel.Result = (await BaseWorkTypeDAL.Delete(baseJobTypeDTO)) == 1;
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
        /// 分页查询工作类型信息
        /// </summary>
        /// <param name="pagingQueryRequest">分页查询条件</param>
        /// <returns>分页后的工作类型信息列表</returns>
        public async Task<List<BaseWorkTypeDTO>> PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest)
        {
            return await BaseWorkTypeDAL.PagingQueryAsync(pagingQueryRequest);
        }

        #endregion

        #region 查找工作类型信息

        /// <summary>
        /// 查找工作类型信息
        /// </summary>
        /// <returns>工作类型信息列表</returns>
        public async Task<List<BaseWorkTypeDTO>> Get()
        {
            return await BaseWorkTypeDAL.Get();
        }

        #endregion
    }
}
