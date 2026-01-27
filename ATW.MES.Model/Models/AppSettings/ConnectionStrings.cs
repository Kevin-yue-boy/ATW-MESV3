using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.Models.AppSettings
{
    public class ConnectionStrings
    {

        /// <summary>
        /// MES连接字符串 
        /// </summary>
        public string ConnStr_MESDB { get; set; }

        /// <summary>
        /// Redis数据库通用连接字符串
        /// </summary>
        public string Redis_ConnStr { get; set; }

        /// <summary>
        /// 是否启用Redis
        /// </summary>
        public bool Redis_EnableYN { get; set; }

    }
}
