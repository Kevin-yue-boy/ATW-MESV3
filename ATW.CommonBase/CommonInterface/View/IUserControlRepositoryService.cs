using ATW.CommonBase.Model.View;
using NetTaste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ATW.CommonBase.CommonInterface.View
{
    public interface IUserControlRepositoryService
    {

        #region 获取View及ViewModel通过name

        /// <summary>
        /// 获取View及ViewModel
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UserControlRepositoryModel GetByName(string name);

        #endregion

        #region 获取View及ViewModel通过View

        /// <summary>
        /// 获取View及ViewModel
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UserControlRepositoryModel GetByView(object view);

        #endregion

        #region 将用户控件注入仓库

        /// <summary>
        /// 将用户控件注入仓库
        /// </summary>
        /// <param name="assembly"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddUserControl(Assembly assembly);

        #endregion

    }
}
