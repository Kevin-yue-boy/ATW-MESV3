using ATW.CommonBase.Method.ViewModel;
using ATW.CommonBase.Model.DataAccess;
using ATW.MES.BLL.System.Log;
using ATW.MES.Client.Views.SystemViews.Log;
using ATW.MES.Model.DTOs.System.Log;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Client.ViewModels.System.Log
{
    public partial class UserOperateLogViewModel : ViewModelBaseMethod<UserOperateLogResponse>
    {

        #region Parameter

        /// <summary>
        /// 搜索类型
        /// </summary>
        public string SearchType { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 用户操作日志业务类
        /// </summary>
        private UserOperateLogBLL UserLogBLL { get; set; } = null;

        /// <summary>
        /// ES日志
        /// </summary>
        private ESLoggerBLL ESLogger { get; set; }

        #endregion

        #region 初始化 VS 中 Git 状态图标含义速查表VS 中 Git 状态图标含义速查表

        public override async void Initialize()
        {
            PageName = "用户操作日志";
            RefreshPage();
            PagingQueryRequest.MaxPageCount = 100;

            SearchType = "全部日志";
            StartDate = DateTime.Now.AddMonths(-1);
            EndDate = DateTime.Now;

            //注入用户操作日志业务类
            this.UserLogBLL = Ioc.Default.GetRequiredService<UserOperateLogBLL>();
            //注入ES日志
            this.ESLogger = Ioc.Default.GetRequiredService<ESLoggerBLL>();

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

            //空判断
            PagingQueryRequest.Predicate = string.IsNullOrWhiteSpace(Search) ? null :
             (Func<UserOperateLogResponse, IComparable>)(f => f.UserName);
            var field = SearchType == "全部日志" ? "" :
                 SearchType == "用户名称" ? "UserName" :
                 SearchType == "页面名称" ? "PageName" :
                 SearchType == "客户端IP" ? "ClientIP" :
                 SearchType == "结果" ? "Result" : "";
            Models = await UserLogBLL.PagingQueryAsync(PagingQueryRequest, field, Search, StartDate, EndDate);
            RefreshPage();
        }



        #endregion

        #region 条件查询

        [RelayCommand]
        public async Task Get()
        {
            try
            {
                ////空判断
                //PagingQueryRequest.Predicate = string.IsNullOrWhiteSpace(Search) ? null :
                //  (Func<EquipmentCommunicateResponse, bool>)(it => it.CommunicateName == Search);
                //PagingQueryRequest.PageIndex = 1;
                await PagingQueryAsync(PagingQueryRequest);
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region 双击行

        /// <summary>
        /// 行双击
        /// </summary>
        /// <param name="smodel"></param>
        public override void ShowDetail(Object smodel)
        {
            if (smodel != null)
            {
                var userOperateLogResponse = smodel as UserOperateLogResponse;
                if (userOperateLogResponse != null)
                {
                    UserOperateLogWindow win = new UserOperateLogWindow();
                    win.DataContext = new UserOperateLogWindowModel(userOperateLogResponse);
                    win.ShowDialog();
                }
            }
        }

        #endregion

    }
}
