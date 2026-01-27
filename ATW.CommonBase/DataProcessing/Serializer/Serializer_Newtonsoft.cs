namespace ATW.CommonBase.DataProcessing.Serializer
{
    public static class Serializer_Newtonsoft
    {

        #region 序列化

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson_NewtonsoftJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        #endregion

        #region 反序列化

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject_NewtonsoftJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        #endregion

    }
}
