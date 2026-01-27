using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.Global
{
    public class GlobalModel
    {

        /// <summary>
        /// 用户名称
        /// </summary>
        public static string UserName { get; set; } = "sys";

        /// <summary>
        /// 权限名称
        /// </summary>
        public static string AuthorityName { get; set; } = "sys";

        /// <summary>
        /// 用户位置
        /// </summary>
        public static string PageName { get; set; } = "Global";

        /// <summary>
        /// 程序加载完成
        /// </summary>
        public static bool AppLoadYN { get; set; } = false;

}
}
