using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.Models.AppSettings
{
    public class AppSettings
    {

        /// <summary>
        /// MES连接字符串 
        /// </summary>
        public string MainDB_ConnStr { get; set; }

        /// <summary>
        /// Redis数据库通用连接字符串
        /// </summary>
        public string Redis_ConnStr { get; set; }

        /// <summary>
        /// 是否启用Redis
        /// </summary>
        public bool Redis_EnableYN { get; set; }

        /// <summary>
        /// 文件地址_Logo
        /// </summary>
        public string FileAddress_Logo { get; set; }

        /// <summary>
        /// 是否启用ElasticSearch
        /// </summary>
        public bool ES_EnableYN { get; set; }

        /// <summary>
        /// ES_Url地址
        /// </summary>
        public string ES_Url { get; set; }

        /// <summary>
        /// ES数据库超时时间
        /// </summary>
        public int ES_RequestTimeout { get; set; }

    }
}
