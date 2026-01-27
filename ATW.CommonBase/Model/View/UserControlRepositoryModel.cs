using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ATW.CommonBase.Model.View
{
    public class UserControlRepositoryModel
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string  Name { get; set; }

        /// <summary>
        /// View
        /// </summary>
        public object View { get; set; }

        /// <summary>
        /// ViewModel
        /// </summary>
        public object ViewModel { get; set; }

    }
}
