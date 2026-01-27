using ATW.CommonBase.CommonInterface.View;
using ATW.CommonBase.Method.Log;
using ATW.CommonBase.Method.ViewModel;
using ATW.CommonBase.Model.Enum;
using ATW.CommonBase.Model.Log;
using ATW.CommonBase.Model.View;
using ATW.MES.BLL.System.Log;
using ATW.MES.Model.Models.AppSettings;
using ATW.MES.Model.Models.System.Log;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ATW.MES.Client.ViewModels
{
    public partial class MainWindowViewModel
    {

        #region Parameter

        public Window win_Main { get; set; }

        public Grid gd_Main { get; set; }

        public Grid gd_uc { get; set; }

        public Image img_logo { get; set; }

        /// <summary>
        /// 系统配置文件
        /// </summary>
        private AppSettings appSettings { get; set; }

        /// <summary>
        /// Logo图片地址
        /// </summary>
        public string ImageFileName_Logo { get; set; }

        private ESLoggerBLL ESLogger { get; set; }

        private IUserControlRepositoryService IUCRepository { get; set; }

        #endregion

        #region 构造函数

        public MainWindowViewModel()
        {

            #region 注入ES日志 控件仓库接口

            this.ESLogger = Ioc.Default.GetRequiredService<ESLoggerBLL>();
            this.IUCRepository = Ioc.Default.GetRequiredService<IUserControlRepositoryService>();

            #endregion

            #region 系统配置文件 AppSettings

            appSettings = Ioc.Default.GetRequiredService<IOptionsMonitor<AppSettings>>().CurrentValue;

            #region 生成AppSettings  Json

            //AppSettings appSettings_CreatJson = new AppSettings();
            //appSettings_CreatJson.ConnStr_MESDB = "server=127.0.0.1;database=MESDB;uid=sa;pwd=P@SSW0RD;Encrypt=True;TrustServerCertificate=True;";
            //appSettings_CreatJson.Redis_ConnStr = "127.0.0.1:6379";
            //appSettings_CreatJson.Redis_EnableYN = true;
            //appSettings_CreatJson.FileAddress_Logo = "";

            //var str = Serializer_JsonNet.ObjectToJson(appSettings_CreatJson);
            //var str1 = Serializer_JsonNet.ObjectToJson(appSettings_CreatJson);

            #endregion

            #endregion

            #region 动态加载Logo图片

            ViewLogo();

            #endregion

        }

        #endregion

        #region 导航页面切换 NavCommand

        [RelayCommand]
        public async Task Nav(object obj)
        {
            try
            {
                var rb = obj as RadioButton;
                //导航页面切换 传参类型不是RadioButton类型 Name属性未按照规则定义不执行
                if (rb == null || rb.Name.ToString().Split('_').Length < 2) return;
                var name = (rb as RadioButton).Name.ToString().Split('_')[1];
                var uCRModel = IUCRepository.GetByName(name);
                //导航页面切换 用户控件未放入仓库则不执行
                if (uCRModel == null) return;
                if (gd_uc.Children.Count > 0)
                {

                    #region 卸载

                    //卸载
                    var uCRModel_old = IUCRepository.GetByView(gd_uc.Children[0]);
                    var vMB_old = uCRModel_old.ViewModel as ViewModelBase;
                    if (vMB_old != null)
                    {
                        vMB_old.UnLoad();
                    }

                    #endregion

                    gd_uc.Children.Remove(gd_uc.Children[0]);
                }

                #region 加载 注入用户控件 初始化操作 注入权限

                var vMB = uCRModel.ViewModel as ViewModelBase;
                if (vMB != null)
                {
                    //加载
                    vMB.Load(uCRModel.View);
                    //初始化操作
                    vMB.Initialize();
                    ESLogger.OperateLog($"进入{vMB.PageName}页面", EnumOperateLogType.SystemRunning);
                    GlobalModel.PageName = vMB.PageName;
                }

                #endregion

                gd_uc.Children.Add(uCRModel.View as UserControl);

            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region Loaded

        public RelayCommand<Object> LoadedCommand => new RelayCommand<Object>((object win_Main) =>
        {
            this.win_Main = win_Main as Window;
            gd_Main = this.win_Main.FindName("gd_Main") as Grid;
            gd_uc= this.win_Main.FindName("gd_uc") as Grid;
            //拖动Logo图片事件
            img_logo = this.win_Main.FindName("img_logo") as Image;
            img_logo.MouseLeftButtonDown += new MouseButtonEventHandler(this.Logo_MouseLeftButtonDown);
            //首页
            RadioButton rb_First = this.win_Main.FindName("LevelThree_EquipmentCommunicate") as RadioButton;
            Nav(rb_First);
        });

        #endregion

        #region Logo

        #region 动态加载Logo图片

        public void ViewLogo()
        {
            try
            {
                string FileName_Logo = appSettings.FileAddress_Logo;
                //|| !System.IO.File.Exists(appSettings.FileAddress_Logo)
                if (string.IsNullOrWhiteSpace(appSettings.FileAddress_Logo) )
                {
                    ImageFileName_Logo = @"/Common/Images/logo.jpg";
                }
                else
                {
                    ImageFileName_Logo = appSettings.FileAddress_Logo;
                }
            }
            catch (Exception)
            {
                ImageFileName_Logo = @"/Common/Images/logo.jpg";
            }
        }

        #endregion

        #region 拖动Logo图片事件

        /// <summary>
        /// 拖动Logo图片事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
                win_Main.DragMove();
            }
        }

        #endregion

        #endregion

        #region 基础控件 登录 最小化 最大化 关闭软件

        #region 登录

        #endregion

        #region 最小化 最大化 关闭软件

        #region 最小化

        /// <summary>
        /// 最小化
        /// </summary>
        public RelayCommand WindowMinimizedCommand => new RelayCommand(() =>
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        });

        #endregion

        #region 最大化

        /// <summary>
        /// 最大化
        /// </summary>
        public RelayCommand WindowMaximizedCommand => new RelayCommand(() =>
        {
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
        });

        #endregion

        #region 关闭软件

        /// <summary>
        /// 关闭软件
        /// </summary>
        public RelayCommand WindowCloseCommand => new RelayCommand(async () =>
        {
            //关闭程序
            Application.Current.Shutdown();
        });

        #endregion

        #endregion

        #endregion

    }
}
