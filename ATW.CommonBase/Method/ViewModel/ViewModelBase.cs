using ATW.CommonBase.CommonInterface.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ATW.CommonBase.Method.ViewModel
{
    public abstract partial class ViewModelBase: ObservableValidator
    {

        #region Parameter

        #region 页面控件

        /// <summary>
        /// 页面名称
        /// </summary>
        public string PageName { get; set; }

        public UserControl uc { get; set; } = null;
        public Grid gd_Main { get; set; } = null;

        #endregion

        #endregion

        #region 加载、卸载、初始化、注入权限

        #region 初始化

        public virtual async void Initialize()
        {
        }

        #endregion

        #region 加载

        public virtual void Load(object uc)
        {
            //注入页面控件
            if (this.uc == null)
            {
                this.uc = uc as UserControl;
                this.gd_Main = this.uc.FindName("gd_Main") as Grid;
            }
        }

        #endregion

        #region 卸载

        public virtual void UnLoad()
        {

        }

        #endregion

        #endregion

    }

}
