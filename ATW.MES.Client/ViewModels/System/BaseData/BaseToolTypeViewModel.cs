using ATW.CommonBase.Method.ViewModel;
using ATW.CommonBase.Model.DataAccess;
using ATW.MES.BLL.System.BaseData;
using ATW.MES.BLL.System.Log;
using ATW.MES.Model.DTOs.System.BaseData;
using ATW.MES.Model.Models.System.Log;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ATW.MES.Client.ViewModels.System.BaseData
{
    public partial class BaseToolTypeViewModel : ViewModelBaseMethod<BaseToolTypeDTO>
    {

        #region Parameter

        /// <summary>
        /// 工具类型业务逻辑类
        /// </summary>
        private BaseToolTypeBLL BaseToolTypeBLL { get; set; } = null;

        /// <summary>
        /// ES日志
        /// </summary>
        private ESLoggerBLL ESLogger { get; set; }

        #endregion

        #region 初始化

        public override async void Initialize()
        {
            PageName = "工具类型信息"; 
            RefreshPage();

            // 注入工具类型业务类
            this.BaseToolTypeBLL = Ioc.Default.GetRequiredService<BaseToolTypeBLL>();
            // 注入ES日志
            this.ESLogger = Ioc.Default.GetRequiredService<ESLoggerBLL>();

            //初始化查询
            Search = "";
            await Get();

        }

        #endregion

        #region 重写分页查询

        /// <summary>
        /// 分页查询工装类型信息
        /// </summary>
        /// <param name="pagingQueryRequest">分页查询条件</param>
        public override async Task PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest)
        {
            Models = await BaseToolTypeBLL.PagingQueryAsync(PagingQueryRequest);
            RefreshPage();
        }

        #endregion

        #region 条件查询

        [RelayCommand]
        public async Task Get()
        {
            try
            {
                // 空判断，按工具类型名称模糊查询
                PagingQueryRequest.Predicate = string.IsNullOrWhiteSpace(Search) ? null :
                  (Func<BaseToolTypeDTO, bool>)(it => it.ToolTypeName.Contains(Search));
                PagingQueryRequest.PageIndex = 1; // 查询结果重置到第一页
                await PagingQueryAsync(PagingQueryRequest);
            }
            catch (Exception ex)
            {
                // 捕获查询异常，友好提示
                MessageBox.Show($"条件查询失败：{ex.Message}");
            }
        }

        #endregion

        #region 添加

        /// <summary>
        /// 添加工具类型
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
                await BaseToolTypeBLL.Insert(Model, responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"\r{ex.ToString()}";
            }
            finally
            {
                sw.Stop();
                // 记录新增操作日志
                ESLogger.OperateLog(responseModel.Msg, Model, EnumOperateLogType.AddData,
                    responseModel.Result, sw.ElapsedMilliseconds);

                if (responseModel.Result)
                {
                    // 添加成功后刷新分页数据
                    await PagingQueryAsync(PagingQueryRequest);
                }
                // 提示操作结果
                MessageBox.Show(responseModel.Msg);
            }
        }

        #endregion

        #region 编辑

        /// <summary>
        /// 编辑工具类型
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
                await BaseToolTypeBLL.Edit(Model, Model_Old, responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"\r{ex.ToString()}";
            }
            finally
            {
                sw.Stop();
                // 记录编辑操作日志
                ESLogger.OperateLog(responseModel.Msg, Model, Model_Old, EnumOperateLogType.EditData,
                    responseModel.Result, sw.ElapsedMilliseconds);

                if (responseModel.Result)
                {
                    // 编辑成功后刷新分页数据
                    await PagingQueryAsync(PagingQueryRequest);
                }
                // 提示操作结果
                MessageBox.Show(responseModel.Msg);
            }
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除工具类型
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task Delete()
        {
            // 增加删除确认提示
            if (MessageBox.Show($"确认删除工具类型【{Model_Old?.ToolTypeName}】吗？", "删除确认",
                MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            ResponseModel responseModel = new ResponseModel();
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                await BaseToolTypeBLL.Delete(Model_Old, responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"\r{ex.ToString()}";
            }
            finally
            {
                sw.Stop();
                // 记录删除操作日志
                ESLogger.OperateLog(responseModel.Msg, Model_Old, EnumOperateLogType.DeleteData,
                    responseModel.Result, sw.ElapsedMilliseconds);

                if (responseModel.Result)
                {
                    // 删除成功后刷新分页数据
                    await PagingQueryAsync(PagingQueryRequest);
                }
                // 提示操作结果
                MessageBox.Show(responseModel.Msg);
            }
        }

        #endregion

        #region 下拉框_IO配方名称

        //#region IO配方名称点击事件

        //public RelayCommand<Object> GetWorkTypeName_ClickCommand => new RelayCommand<Object>((Object smodel) =>
        //{
        //    if (smodel != null)
        //    {
        //        var _IORecipe = smodel as EquipmentMa_IORecipe;

        //    }
        //});

        //#endregion

        //#region IO配方名称下拉事件

        //public RelayCommand<Object> GetWorkTypeName_DropDownOpenedCommand => new RelayCommand<Object>((Object smodel) =>
        //{
        //    IORecipeNames = MySqlATWHelper.QueryToList<EquipmentMa_IORecipe>();
        //    if (IORecipeNames == null || IORecipeNames.Count < 1)
        //    {
        //        MessageBox.Show($"未查询到继电器配方相关信息，请添加！");
        //    }
        //});

        //#endregion

        #endregion

    }
}
