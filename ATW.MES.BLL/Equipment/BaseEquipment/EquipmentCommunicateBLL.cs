using ATW.CommonBase.CommonInterface.Communicate;
using ATW.CommonBase.Communicate.PLC.HSL;
using ATW.CommonBase.DataProcessing.DataCheck;
using ATW.CommonBase.DataProcessing.Serializer;
using ATW.CommonBase.Method.CustomException;
using ATW.CommonBase.Model;
using ATW.CommonBase.Model.DataAccess;
using ATW.MES.DAL.Equipment.BaseEquipment;
using ATW.MES.DAL.Process.ProcessRoute;
using ATW.MES.Model.DTOs.Equipment.BaseEquipment;
using ATW.MES.Model.DTOs.Process.ProcessRoute;
using ATW.MES.Model.Models.Equipment.BaseEquipment;
using CommunityToolkit.Mvvm.DependencyInjection;
using Elastic.Clients.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace ATW.MES.BLL.Equipment.BaseEquipment
{
    public class EquipmentCommunicateBLL
    {

        #region Parameter

        /// <summary>
        /// 设备通讯数据
        /// </summary>
        private EquipmentCommunicateDAL EquComDAL { get; set; }
        /// <summary>
        /// PLC通讯仓储
        /// </summary>
        private IPLCCommunicateRepositoryService IPLCComRepo { get; set; }

        #endregion

        #region 构造函数

        public EquipmentCommunicateBLL(IPLCCommunicateRepositoryService iPLCComRepo,EquipmentCommunicateDAL equComBLL)
        {
            this.IPLCComRepo = iPLCComRepo;
            this.EquComDAL = equComBLL;
        }

        #endregion

        #region 添加设备通讯

        /// <summary>
        /// 添加设备通讯
        /// </summary>
        /// <param name="equComDTO">通讯信息</param>
        /// <param name="responseModel">反馈添加结果</param>
        /// <returns></returns>
        public async Task Insert(EquipmentCommunicateDTO equComDTO, ResponseModel responseModel)
        {
            try
            {

                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(equComDTO, false, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 数据重复校验

                var equComResponses = await EquComDAL.Get();

                //校验通讯名称是否重复
                var exist_CommunicateName = equComResponses.Exists(it => { return it.CommunicateName == equComDTO.CommunicateName; });
                if (exist_CommunicateName)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"通讯名称:{equComDTO.CommunicateName}已存在！";
                    return;
                }

                //校验通讯编码是否重复
                var exist_CommunicateCode = equComResponses.Exists(it => { return it.CommunicateCode == equComDTO.CommunicateCode; });
                if (exist_CommunicateCode)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"通讯编码:{equComDTO.CommunicateCode}已存在！";
                    return;
                }

                #endregion

                equComDTO.GUID = Guid.NewGuid();
                equComDTO.LastEditTime = DateTime.Now;
                equComDTO.CreateTime = DateTime.Now;
                responseModel.Result = (await EquComDAL.Insert(equComDTO)) == 1;
                responseModel.Msg += responseModel.Result ? "添加成功！" : "添加失败！";
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"异常报错:{ex.ToString()}！";
            }
        }

        #endregion

        #region 编辑设备通讯

        /// <summary>
        /// 编辑设备通讯
        /// </summary>
        /// <param name="equComDTO">通讯信息</param>
        /// <param name="equComDTO">通讯信息_原始数据</param>
        /// <param name="responseModel">反馈添加结果</param>
        /// <returns></returns>
        public async Task Edit(EquipmentCommunicateDTO equComDTO,
            EquipmentCommunicateDTO equComDTO_Old, ResponseModel responseModel)
        {
            try
            {

                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(equComDTO, equComDTO_Old, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 数据重复校验

                var equComResponses = await EquComDAL.Get();

                //校验通讯名称是否重复
                var exist_CommunicateName = equComResponses.Exists(it => {
                    return it.CommunicateName == equComDTO.CommunicateName
                    && it.GUID != equComDTO_Old.GUID;
                });
                if (exist_CommunicateName)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"通讯名称:{equComDTO.CommunicateName}已存在！";
                    return;
                }

                //校验通讯编码是否重复
                var exist_CommunicateCode = equComResponses.Exists(it => {
                    return it.CommunicateCode == equComDTO.CommunicateCode
                    && it.GUID != equComDTO_Old.GUID;
                });
                if (exist_CommunicateCode)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"通讯编码:{equComDTO.CommunicateCode}已存在！";
                    return;
                }

                #endregion

                equComDTO.LastEditTime = DateTime.Now;
                responseModel.Result = (await EquComDAL.Edit(equComDTO)) == 1;
                responseModel.Msg += responseModel.Result ? "编辑成功！" : "编辑失败！";
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"异常报错:{ex.ToString()}！";
            }
        }

        #endregion

        #region 删除设备通讯

        /// <summary>
        /// 删除设备通讯
        /// </summary>
        /// <param name="equComDTO">通讯信息</param>
        /// <param name="responseModel">反馈删除结果</param>
        /// <returns></returns>
        public async Task Delete(EquipmentCommunicateDTO equComDTO, ResponseModel responseModel)
        {
            try
            {

                #region 校验实体 生成日志

                string msg_log = "";
                responseModel.Result = EntityDataCheck.CheckEntity(equComDTO,true, out msg_log);
                responseModel.Msg = msg_log;
                if (!responseModel.Result) { return; }

                #endregion

                #region 校验数据存在

                var equComResponses = await EquComDAL.Get();

                var exist = equComResponses.Exists(it => {
                    return it.GUID != equComDTO.GUID;
                });
                if (!exist)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"通讯名称:{equComDTO.CommunicateName}不存在！";
                    return;
                }

                #endregion

                responseModel.Result = (await EquComDAL.Delete(equComDTO)) == 1;
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
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<EquipmentCommunicateDTO>> PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest)
        {
            return await EquComDAL.PagingQueryAsync(pagingQueryRequest);
        }

        #endregion

        #region 加载所有通讯

        /// <summary>
        /// 加载所有通讯
        /// </summary>
        /// <returns></returns>
        public async Task LoadALLCommunicateAsync()
        {
            var Communicates = await EquComDAL.Get();
            if (Communicates != null && Communicates.Count > 0)
            {
                Communicates.ForEach(async it => {
                    await LoadCommunicateAsync(it);
                });
            }
        }

        #endregion

        #region 加载设备通讯

        /// <summary>
        /// 加载设备通讯
        /// </summary>
        /// <returns></returns>
        public async Task LoadCommunicateAsync(EquipmentCommunicateDTO equipmentCommunicateDTO)
        {
            if (equipmentCommunicateDTO.Enable && equipmentCommunicateDTO.CommunicateType == "KeyencePLC")
            {
                var pLCCAModel = Serializer_JsonNet.JsonToObject<PLCConnectAddressModel>(equipmentCommunicateDTO.ConnectAddress);
                ICommunicatePLC IPLC = new HSL_Keyence_MC(pLCCAModel.IP, pLCCAModel.Port, pLCCAModel.HeartAddress);
                IPLCComRepo.SetConnect(IPLC, equipmentCommunicateDTO.GUID);
            }
            else if (equipmentCommunicateDTO.Enable && equipmentCommunicateDTO.CommunicateType == "SiemensPLC")
            {
                var pLCCAModel = Serializer_JsonNet.JsonToObject<PLCConnectAddressModel>(equipmentCommunicateDTO.ConnectAddress);
                ICommunicatePLC IPLC = new HSL_Siemens_S7(pLCCAModel.IP, pLCCAModel.Port, pLCCAModel.HeartAddress);
                IPLCComRepo.SetConnect(IPLC, equipmentCommunicateDTO.GUID);
            }
        }

        #endregion

    }
}
