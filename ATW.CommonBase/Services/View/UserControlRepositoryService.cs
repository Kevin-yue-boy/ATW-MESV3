using ATW.CommonBase.CommonInterface.View;
using ATW.CommonBase.Model.Communicate;
using ATW.CommonBase.Model.View;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ATW.CommonBase.Services.View
{
    public class UserControlRepositoryService : IUserControlRepositoryService
    {

        #region Parameter

        private List<UserControlRepositoryModel> UCRs { get; set; }

        private object obj = new object();

        #endregion

        #region 构造函数

        public UserControlRepositoryService()
        {
            UCRs = new List<UserControlRepositoryModel>();
        }

        #endregion

        #region 添加View

        /// <summary>
        /// 添加View
        /// </summary>
        /// <param name="name"></param>
        /// <param name="view"></param>
        private void SetView(string name, object view)
        {
            lock (obj)
            {
                if (UCRs != null)
                {
                    var UCR = UCRs.Find(it => { return it.Name == name; });
                    if (UCR != null)
                    {
                        UCR.View = view;
                    }
                    else
                    {
                        UCRs.Add(new UserControlRepositoryModel()
                        {
                            Name = name,
                            View = view
                        });
                    }
                }
            }
        }

        #endregion

        #region 添加ViewModel

        /// <summary>
        /// 添加ViewModel
        /// </summary>
        /// <param name="name"></param>
        /// <param name="viewModel"></param>
        private void SetViewModel(string name, object viewModel)
        {
            lock (obj)
            {
                if (UCRs != null)
                {
                    var UCR = UCRs.Find(it => { return it.Name == name; });
                    if (UCR != null)
                    {
                        UCR.ViewModel = viewModel;
                    }
                    else
                    {
                        UCRs.Add(new UserControlRepositoryModel()
                        {
                            Name = name,
                            ViewModel = viewModel
                        });
                    }
                }
            }
        }

        #endregion

        #region 获取View及ViewModel通过Name

        /// <summary>
        /// 获取View及ViewModel
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UserControlRepositoryModel GetByName(string name)
        {
            lock (obj)
            {
                return UCRs.Find(it => { return it.Name == name; });
            }
        }

        #endregion

        #region 获取View及ViewModel通过View

        /// <summary>
        /// 获取View及ViewModel
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UserControlRepositoryModel GetByView(object View)
        {
            lock (obj)
            {
                return UCRs.Find(it => { return it.View == View; });
            }
        }

        #endregion

        #region 将用户控件注入仓库

        /// <summary>
        /// 将用户控件注入仓库
        /// </summary>
        /// <param name="assembly"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddUserControl(Assembly assembly)
        {

            // 收集要扫描的程序集
            var assemblies = new List<Assembly> { assembly };

            // 查找所有类名包含"xx"的非抽象类
            var dalTypes = assemblies.SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract
                && (
                    (t.Name.Length > "ViewModel".Length
                    && t.Name.Substring((t.Name.Length - "ViewModel".Length), "ViewModel".Length) == "ViewModel"))
                    || (
                  (t.Name.Length > "View".Length
                && (t.Name.Substring((t.Name.Length - "View".Length), "View".Length) == "View")))
                )
                .ToList();

            if (dalTypes != null && dalTypes.Count > 0)
            {
                foreach (var type in dalTypes)
                {
                    if (type.Name.Length > "ViewModel".Length
                        &&type.Name.Substring((type.Name.Length - "ViewModel".Length ), "ViewModel".Length) == "ViewModel")
                    {
                        SetViewModel(type.Name.Substring(0, (type.Name.Length - "ViewModel".Length )), Activator.CreateInstance(type));
                    }
                    else if (type.Name.Length > "View".Length
                        && type.Name.Substring((type.Name.Length - "View".Length ), "View".Length) == "View")
                    {
                        SetView(type.Name.Substring(0, (type.Name.Length - "View".Length)), Activator.CreateInstance(type));
                    }
                }
            }

            if (UCRs != null && UCRs.Count > 0)
            {
                foreach (var it in UCRs)
                {
                    if (it.View != null && it.ViewModel != null && it.Name != null)
                    {
                        (it.View as ContentControl).DataContext = it.ViewModel;
                    }
                }
            }


        }

        #endregion

    }
}
