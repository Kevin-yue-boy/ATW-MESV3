using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.DataProcessing.Serializer
{
    public static class DeepCopy
    {

        /// <summary>
        /// 数据深度拷贝_Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T? JsonDeepCopy<T>(T t)
        {
            return Serializer_JsonNet.JsonToObject<T>(Serializer_JsonNet.ObjectToJson(t));
        }

    }
}
