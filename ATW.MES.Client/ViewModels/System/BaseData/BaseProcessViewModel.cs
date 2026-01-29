using ATW.CommonBase.File.Excel;
using ATW.CommonBase.Method.ViewModel;
using ATW.CommonBase.Model.DataAccess;
using ATW.MES.BLL.System.BaseData;
using ATW.MES.BLL.System.Log;
using ATW.MES.Model.DTOs.System.BaseData;
using ATW.MES.Model.Models.System.Log;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Client.ViewModels.System.BaseData
{
    public partial class BaseProcessViewModel : ViewModelBaseMethod<BaseProcessDTO>
    {

        #region Parameter

        /// <summary>
        /// 工序业务逻辑类
        /// </summary>
        private BaseProcessBLL BaseProcessBLL { get; set; } = null;

        /// <summary>
        /// ES日志
        /// </summary>
        private ESLoggerBLL ESLogger { get; set; }

        #endregion

        #region 初始化

        public override async void Initialize()
        {
            PageName = "工序信息";
            RefreshPage();

            // 注入工序业务类
            this.BaseProcessBLL = Ioc.Default.GetRequiredService<BaseProcessBLL>();
            // 注入ES日志
            this.ESLogger = Ioc.Default.GetRequiredService<ESLoggerBLL>();

            //初始化查询
            Search = "";
            await Get();
        }

        #endregion

        #region 重写分页查询

        /// <summary>
        /// 分页查询工序信息
        /// </summary>
        /// <param name="pagingQueryRequest">分页查询条件</param>
        public override async Task PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest)
        {
            Models = await BaseProcessBLL.PagingQueryAsync(PagingQueryRequest);
            RefreshPage();
        }

        #endregion

        #region 条件查询

        [RelayCommand]
        public async Task Get()
        {
            try
            {
                // 空判断，按工序名称模糊查询（保持和示例一致的查询逻辑）
                PagingQueryRequest.Predicate = string.IsNullOrWhiteSpace(Search) ? null :
                  (Func<BaseProcessDTO, bool>)(it => it.ProcessName.Contains(Search));
                PagingQueryRequest.PageIndex = 1; // 查询结果重置到第一页
                await PagingQueryAsync(PagingQueryRequest);
                //Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
                //dialog.FileName = $"生产参数数据_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                //dialog.Filter = "Excel 文件(*.xls)|*.xls";
                //if ((bool)dialog.ShowDialog())
                //{
                //    Excel_Export_NPOI.ExportEntitiesToXlsxFile<BaseProcessResponse>(Models, dialog.FileName);
                //}
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
        /// 添加工序
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
                await BaseProcessBLL.Insert(Model, responseModel);
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
        /// 编辑工序
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
                await BaseProcessBLL.Edit(Model, Model_Old, responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"\r{ex.ToString()}";
            }
            finally
            {
                sw.Stop();
                // 记录编辑操作日志（修正示例中日志类型错误：InsertData → EditData 更合理）
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
        /// 删除工序
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task Delete()
        {
            // 增加删除确认提示
            if (MessageBox.Show($"确认删除工序【{Model_Old?.ProcessName}】吗？", "删除确认",
                MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            ResponseModel responseModel = new ResponseModel();
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                await BaseProcessBLL.Delete(Model_Old, responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Result = false;
                responseModel.Msg += $"\r{ex.ToString()}";
            }
            finally
            {
                sw.Stop();
                // 记录删除操作日志（修正示例中日志类型错误：InsertData → DeleteData 更合理）
                ESLogger.OperateLog(responseModel.Msg, Model, EnumOperateLogType.DeleteData,
                    responseModel.Result, sw.ElapsedMilliseconds);

                if (responseModel.Result)
                {
                    // 删除成功后刷新分页数据
                    await PagingQueryAsync(PagingQueryRequest);
                    // 清空旧模型
                    Model_Old = new BaseProcessDTO();
                }
                // 提示操作结果
                MessageBox.Show(responseModel.Msg);
            }
        }

        #endregion

    }
}
