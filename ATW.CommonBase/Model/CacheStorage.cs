using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.Model
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class CacheStorage : Attribute
    {

        /// <summary>
        /// 启用缓存
        /// </summary>
        public bool EnableYN { get; set; } = false;

    }
}
