using ATW.CommonBase.CommonInterface.View;
using ATW.CommonBase.Method.Log;
using ATW.CommonBase.Model.Enum;
using ATW.CommonBase.Model.Log;
using ATW.MES.Client.Global;
using ATW.MES.Model.Models.AppSettings;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Windows;

namespace ATW.MES.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        private static System.Threading.Mutex mutex;

        public App()
        {
            //禁用IPV6
            AppContext.SetSwitch("System.Net.DisableIPv6", true);
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            #region 系统日志文件地址 启动日志服务

            //日志服务
            Logger.StartServices();
            //系统日志文件地址
            Logger.FilePath = "D:\\Logs";

            #endregion

            #region 判断程序重复启动

            Logger.OperateLog($"MES系统开始启动: ");
            mutex = new System.Threading.Mutex(true, "OnlyRun_CRNS");
            if (mutex.WaitOne(0, false))
            {
                base.OnStartup(e);
            }
            else
            {
                Logger.OperateLog($"应用程序已运行!");
                MessageBox.Show("应用程序已运行");
                this.Shutdown();
                return;
            }

            #endregion

            #region 全局错误处理

            //全局错误处理
            //Thead，处理在非UI线程上未处理的异常,当前域未处理异常
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //处理在UI线程上未处理的异常
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            Logger.OperateLog($"全局错误处理服务加载完成!");

            #endregion

            #region IOC容器-配置默认服务 

            Ioc.Default.ConfigureServices(IOCServices.ConfigureServices());
            Logger.OperateLog($"IOC容器注册服务加载完成!");

            #endregion

            #region 启动项目基础服务

            Ioc.Default.GetRequiredService<MESClientServices>().StartServicesAsync();

            #endregion

            #region 启动窗口

            this.MainWindow = Ioc.Default.GetRequiredService<IUserControlRepositoryService>().GetByName("MainWindow").View as Window;
            this.MainWindow.Show();
            GlobalModel.AppLoadYN = true;

            #endregion

        }

        #region 全局错误处理

        /// <summary>
        /// Thead，处理在非UI线程上未处理的异常,当前域未处理异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string errMsg = e.ExceptionObject.ToString();
            MessageBox.Show($"程序异常崩溃 -[{errMsg}]");
        }

        /// <summary>
        /// 处理在UI线程上未处理的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //throw new NotImplementedException();
            // Set as resolved
            e.Handled = true;
            // Get exception info
            string errMsg = e.Exception.Message.ToString();
            MessageBox.Show($"操作异常崩溃 -[{errMsg}]");
        }

        #endregion

    }

}
