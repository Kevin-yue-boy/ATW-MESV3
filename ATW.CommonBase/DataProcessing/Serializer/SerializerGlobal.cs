namespace ATW.CommonBase.DataProcessing.Serializer
{
    public static class SerializerGlobal
    {
        /// <summary>
        /// JsonNet 时间转换
        /// </summary>
        public static readonly JsonSerializerOptions jsonOption_DatetimeConverter = new()
        {
            Converters = { new JsonNet_DatetimeConverter() },
            WriteIndented = false
        };
    }
}
