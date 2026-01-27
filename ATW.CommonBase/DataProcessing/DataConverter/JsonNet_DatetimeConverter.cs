namespace ATW.CommonBase.DataProcessing.DataConverter
{
    /// <summary>
    /// JsonNet 时间转换
    /// </summary>
    public class JsonNet_DatetimeConverter : System.Text.Json.Serialization.JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                string dateString = reader.GetString();
                return DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm:ss", null);
            }
            catch (FormatException)
            {
                // 尝试其他格式作为备选
                return DateTime.Parse(reader.GetString());
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
