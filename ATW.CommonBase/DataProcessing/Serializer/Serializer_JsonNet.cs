namespace ATW.CommonBase.DataProcessing.Serializer
{
    public static class Serializer_JsonNet
    {

        #region 序列化

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            return System.Text.Json.JsonSerializer.Serialize(obj, SerializerGlobal.jsonOption_DatetimeConverter);
        }

        #endregion

        #region 反序列化

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T? JsonToObject<T>(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(json, SerializerGlobal.jsonOption_DatetimeConverter);
        }

        #endregion

    }
}
