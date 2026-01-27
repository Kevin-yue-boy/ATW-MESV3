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
    public class BaseProductTypeBLL
    {
        #region Parameter

        /// <summary>
        /// 产品类型数据访问层
        /// </summary>
        private BaseProductTypeDAL BaseProductTypeDAL { get; set; }

        #endregion

        #region 构造函数

        public BaseProductTypeBLL(BaseProductTypeDAL baseProductTypeDAL)
        {
            this.BaseProductTypeDAL = baseProductTypeDAL;
        }

        #endregion

        #region 添加产品类型

        /// <summary>
        /// 添加产品类型信息
        /// </summary>
        /// <param name="baseProductTypeResponse">产品类型信息</param>
        /// <param name="responseModel">反馈添加结果</param>
        /// <returns></returns>
        public async Task Insert(BaseProductTypeResponse baseProductTypeResponse, ResponseModel responseModel)
        {
            try
            {
                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(baseProductTypeResponse, false, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 数据重复校验

                var baseProductTypeResponses = await BaseProductTypeDAL.Get();

                // 校验产品类型名称是否重复
                var exist_ProductTypeName = baseProductTypeResponses.Exists(it => { return it.ProductTypeName == baseProductTypeResponse.ProductTypeName; });
                if (exist_ProductTypeName)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"产品类型名称:{baseProductTypeResponse.ProductTypeName}已存在！";
                    return;
                }

                // 校验产品类型编码是否重复
                var exist_ProductTypeCode = baseProductTypeResponses.Exists(it => { return it.ProductTypeCode == baseProductTypeResponse.ProductTypeCode; });
                if (exist_ProductTypeCode)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"产品类型编码:{baseProductTypeResponse.ProductTypeCode}已存在！";
                    return;
                }

                #endregion

                // 初始化基础字段
                baseProductTypeResponse.GUID = Guid.NewGuid();
                baseProductTypeResponse.LastEditTime = DateTime.Now;
                baseProductTypeResponse.CreateTime = DateTime.Now;

                // 执行新增并返回结果
                responseModel.Result = (await BaseProductTypeDAL.Insert(baseProductTypeResponse)) == 1;
                responseModel.Msg += responseModel.Result ? "添加成功！" : "添加失败！";
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"异常报错:{ex.ToString()}！";
            }
        }

        #endregion

        #region 编辑产品类型

        /// <summary>
        /// 编辑产品类型信息
        /// </summary>
        /// <param name="baseProductTypeResponse">产品类型信息（新值）</param>
        /// <param name="baseProductTypeResponse_Old">产品类型信息（原始数据）</param>
        /// <param name="responseModel">反馈编辑结果</param>
        /// <returns></returns>
        public async Task Edit(BaseProductTypeResponse baseProductTypeResponse,
            BaseProductTypeResponse baseProductTypeResponse_Old, ResponseModel responseModel)
        {
            try
            {
                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(baseProductTypeResponse, baseProductTypeResponse_Old, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 数据重复校验

                var baseProductTypeResponses = await BaseProductTypeDAL.Get();

                // 校验产品类型名称是否重复（排除自身）
                var exist_ProductTypeName = baseProductTypeResponses.Exists(it => {
                    return it.ProductTypeName == baseProductTypeResponse.ProductTypeName
                    && it.GUID != baseProductTypeResponse_Old.GUID;
                });
                if (exist_ProductTypeName)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"产品类型名称:{baseProductTypeResponse.ProductTypeName}已存在！";
                    return;
                }

                // 校验产品类型编码是否重复（排除自身）
                var exist_ProductTypeCode = baseProductTypeResponses.Exists(it => {
                    return it.ProductTypeCode == baseProductTypeResponse.ProductTypeCode
                    && it.GUID != baseProductTypeResponse_Old.GUID;
                });
                if (exist_ProductTypeCode)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"产品类型编码:{baseProductTypeResponse.ProductTypeCode}已存在！";
                    return;
                }

                #endregion

                // 更新最后编辑时间
                baseProductTypeResponse.LastEditTime = DateTime.Now;

                // 执行编辑并返回结果
                responseModel.Result = (await BaseProductTypeDAL.Edit(baseProductTypeResponse)) == 1;
                responseModel.Msg += responseModel.Result ? "编辑成功！" : "编辑失败！";
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"异常报错:{ex.ToString()}！";
            }
        }

        #endregion

        #region 删除产品类型

        /// <summary>
        /// 删除产品类型信息
        /// </summary>
        /// <param name="baseProductTypeResponse">产品类型信息</param>
        /// <param name="responseModel">反馈删除结果</param>
        /// <returns></returns>
        public async Task Delete(BaseProductTypeResponse baseProductTypeResponse, ResponseModel responseModel)
        {
            try
            {
                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(baseProductTypeResponse, true, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 校验数据存在

                var baseProductTypeResponses = await BaseProductTypeDAL.Get();

                var exist = baseProductTypeResponses.Exists(it => {
                    return it.GUID == baseProductTypeResponse.GUID;
                });
                if (!exist)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"产品类型名称:{baseProductTypeResponse.ProductTypeName}不存在！";
                    return;
                }

                #endregion

                // 执行删除并返回结果
                responseModel.Result = (await BaseProductTypeDAL.Delete(baseProductTypeResponse)) == 1;
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
        /// 分页查询产品类型信息
        /// </summary>
        /// <param name="pagingQueryRequest">分页查询条件</param>
        /// <returns>分页后的产品类型信息列表</returns>
        public async Task<List<BaseProductTypeResponse>> PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest)
        {
            return await BaseProductTypeDAL.PagingQueryAsync(pagingQueryRequest);
        }

        #endregion
    }
}
