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

    public interface Test001
    {

        public Task Insert(object baseToolTypeResponse, ResponseModel responseModel);

    }

    public class BaseToolTypeBLL
    {
        #region Parameter

        /// <summary>
        /// 工具类型数据访问层
        /// </summary>
        private BaseToolTypeDAL BaseToolTypeDAL { get; set; }

        #endregion

        #region 构造函数

        public BaseToolTypeBLL(BaseToolTypeDAL baseToolTypeDAL)
        {
            this.BaseToolTypeDAL = baseToolTypeDAL;
        }

        #endregion

        #region 添加工具类型

        /// <summary>
        /// 添加工具类型信息
        /// </summary>
        /// <param name="baseToolTypeDTO">工具类型信息</param>
        /// <param name="responseModel">反馈添加结果</param>
        /// <returns></returns>
        public async Task Insert(BaseToolTypeDTO baseToolTypeDTO, ResponseModel responseModel)
        {
            try
            {

                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(baseToolTypeDTO, false, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 数据重复校验

                var baseToolTypeResponses = await BaseToolTypeDAL.Get();

                // 校验工具类型名称是否重复
                var exist_ToolTypeName = baseToolTypeResponses.Exists(it => { return it.ToolTypeName == baseToolTypeDTO.ToolTypeName; });
                if (exist_ToolTypeName)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"工具类型名称:{baseToolTypeDTO.ToolTypeName}已存在！";
                    return;
                }

                // 校验工具类型编码是否重复
                var exist_ToolTypeCode = baseToolTypeResponses.Exists(it => { return it.ToolTypeCode == baseToolTypeDTO.ToolTypeCode; });
                if (exist_ToolTypeCode)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"工具类型编码:{baseToolTypeDTO.ToolTypeCode}已存在！";
                    return;
                }

                #endregion

                // 初始化基础字段
                baseToolTypeDTO.GUID = Guid.NewGuid();
                baseToolTypeDTO.LastEditTime = DateTime.Now;
                baseToolTypeDTO.CreateTime = DateTime.Now;

                // 执行新增并返回结果
                responseModel.Result = (await BaseToolTypeDAL.Insert(baseToolTypeDTO)) == 1;
                responseModel.Msg += responseModel.Result ? "添加成功！" : "添加失败！";
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"异常报错:{ex.ToString()}！";
            }
        }

        #endregion

        #region 编辑工具类型

        /// <summary>
        /// 编辑工具类型信息
        /// </summary>
        /// <param name="baseToolTypeDTO">工具类型信息（新值）</param>
        /// <param name="baseToolTypeDTO_Old">工具类型信息（原始数据）</param>
        /// <param name="responseModel">反馈编辑结果</param>
        /// <returns></returns>
        public async Task Edit(BaseToolTypeDTO baseToolTypeDTO,
            BaseToolTypeDTO baseToolTypeDTO_Old, ResponseModel responseModel)
        {
            try
            {
                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(baseToolTypeDTO, baseToolTypeDTO_Old, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 数据重复校验

                var baseToolTypeResponses = await BaseToolTypeDAL.Get();

                // 校验工具类型名称是否重复（排除自身）
                var exist_ToolTypeName = baseToolTypeResponses.Exists(it => {
                    return it.ToolTypeName == baseToolTypeDTO.ToolTypeName
                    && it.GUID != baseToolTypeDTO_Old.GUID;
                });
                if (exist_ToolTypeName)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"工具类型名称:{baseToolTypeDTO.ToolTypeName}已存在！";
                    return;
                }

                // 校验工具类型编码是否重复（排除自身）
                var exist_ToolTypeCode = baseToolTypeResponses.Exists(it => {
                    return it.ToolTypeCode == baseToolTypeDTO.ToolTypeCode
                    && it.GUID != baseToolTypeDTO_Old.GUID;
                });
                if (exist_ToolTypeCode)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"工具类型编码:{baseToolTypeDTO.ToolTypeCode}已存在！";
                    return;
                }

                #endregion

                // 更新最后编辑时间
                baseToolTypeDTO.LastEditTime = DateTime.Now;

                // 执行编辑并返回结果
                responseModel.Result = (await BaseToolTypeDAL.Edit(baseToolTypeDTO)) == 1;
                responseModel.Msg += responseModel.Result ? "编辑成功！" : "编辑失败！";
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"异常报错:{ex.ToString()}！";
            }
        }

        #endregion

        #region 删除工具类型

        /// <summary>
        /// 删除工具类型信息
        /// </summary>
        /// <param name="baseToolTypeDTO">工具类型信息</param>
        /// <param name="responseModel">反馈删除结果</param>
        /// <returns></returns>
        public async Task Delete(BaseToolTypeDTO baseToolTypeDTO, ResponseModel responseModel)
        {
            try
            {
                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(baseToolTypeDTO, true, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 校验数据存在

                var baseToolTypeResponses = await BaseToolTypeDAL.Get();

                var exist = baseToolTypeResponses.Exists(it => {
                    return it.GUID == baseToolTypeDTO.GUID;
                });
                if (!exist)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"工具类型名称:{baseToolTypeDTO.ToolTypeName}不存在！";
                    return;
                }

                #endregion

                // 执行删除并返回结果
                responseModel.Result = (await BaseToolTypeDAL.Delete(baseToolTypeDTO)) == 1;
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
        /// 分页查询工具类型信息
        /// </summary>
        /// <param name="pagingQueryRequest">分页查询条件</param>
        /// <returns>分页后的工具类型信息列表</returns>
        public async Task<List<BaseToolTypeDTO>> PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest)
        {
            return await BaseToolTypeDAL.PagingQueryAsync(pagingQueryRequest);
        }

        #endregion

        #region 查找工具类型信息

        /// <summary>
        /// 查找工具类型信息
        /// </summary>
        /// <returns>工具类型信息列表</returns>
        public async Task<List<BaseToolTypeDTO>> Get()
        {
            return await BaseToolTypeDAL.Get();
        }

        #endregion

    }
}
