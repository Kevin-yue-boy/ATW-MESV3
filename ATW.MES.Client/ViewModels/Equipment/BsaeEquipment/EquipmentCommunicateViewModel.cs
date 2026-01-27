using ATW.CommonBase.DataAccess.ElasticSearch;
using ATW.CommonBase.DataProcessing.DataExpand;
using ATW.CommonBase.Method.CustomException;
using ATW.CommonBase.Method.Log;
using ATW.CommonBase.Method.ViewModel;
using ATW.CommonBase.Model.DataAccess;
using ATW.CommonBase.Model.Enum;
using ATW.CommonBase.Model.View;
using ATW.CommonBase.Services.View;
using ATW.MES.BLL.Equipment.BaseEquipment;
using ATW.MES.BLL.System.Log;
using ATW.MES.Model.DTOs.Equipment.BaseEquipment;
using ATW.MES.Model.Models.System.Log;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Client.ViewModels.Equipment.BsaeEquipment
{
    public partial class EquipmentCommunicateViewModel : ViewModelBaseMethod<EquipmentCommunicateResponse>
    {

        #region Parameter VS 中 Git 状态图标含义速查表

        /// <summary>
        /// 设备通讯业务类
        /// </summary>
        private EquipmentCommunicateBLL EquComBLL { get; set; } = null;

        /// <summary>
        /// ES日志
        /// </summary>
        private ESLoggerBLL ESLogger { get; set; }

        #endregion

        #region 初始化

        public override async void Initialize()
        {
            PageName = "设备通讯信息";
            RefreshPage();

            //注入设备通讯业务类
            this.EquComBLL = Ioc.Default.GetRequiredService<EquipmentCommunicateBLL>();
            //注入ES日志
            this.ESLogger= Ioc.Default.GetRequiredService<ESLoggerBLL>();

            //初始化查询
            Search = "";
            await Get();

        }

        #endregion

        #region 重写分页查询

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pagingQueryRequest"></param>
        public override async Task PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest)
        {
            Models= await EquComBLL.PagingQueryAsync(PagingQueryRequest);
            RefreshPage();
        }

        #endregion

        #region 条件查询

        [RelayCommand]
        public async Task Get()
        {
            try
            {
                //空判断
                PagingQueryRequest.Predicate = string.IsNullOrWhiteSpace(Search) ? null :
                  (Func<EquipmentCommunicateResponse, bool>)(it => it.CommunicateName == Search);
                PagingQueryRequest.PageIndex = 1;
                await PagingQueryAsync(PagingQueryRequest);
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region 添加

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task Insert()
        {
            ResponseModel responseModel = new ResponseModel();
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                await EquComBLL.Insert(Model, responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"\r{ex.ToString()}";
            }
            finally
            {
                sw.Stop();
                ESLogger.OperateLog(responseModel.Msg, Model, EnumOperateLogType.AddData,
                    responseModel.Result, sw.ElapsedMilliseconds);
                if (responseModel.Result)
                {
                    await PagingQueryAsync(PagingQueryRequest);
                }
                MessageBox.Show(responseModel.Msg);
            }
        }

        #endregion

        #region 编辑

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task Edit()
        {
            ResponseModel responseModel = new ResponseModel();
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                await EquComBLL.Edit(Model,Model_Old, responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"\r{ex.ToString()}";
            }
            finally
            {
                sw.Stop();
                ESLogger.OperateLog(responseModel.Msg, Model,Model_Old, EnumOperateLogType.EditData,
                    responseModel.Result, sw.ElapsedMilliseconds);
                if (responseModel.Result)
                {
                    await PagingQueryAsync(PagingQueryRequest);
                }
                MessageBox.Show(responseModel.Msg);
            }
        }

        #endregion

        #region 删除

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task Delete()
        {
            ResponseModel responseModel = new ResponseModel();
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                await EquComBLL.Delete(Model_Old, responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"\r{ex.ToString()}";
            }
            finally
            {
                sw.Stop();
                ESLogger.OperateLog(responseModel.Msg, Model, EnumOperateLogType.DeleteData,
                    responseModel.Result, sw.ElapsedMilliseconds);
                if (responseModel.Result)
                {
                    await PagingQueryAsync(PagingQueryRequest);
                }
                MessageBox.Show(responseModel.Msg);
            }
        }

        #endregion

    }
}
