namespace ATW.CommonBase.DataProcessing.DataConverter
{
    public static class BytesConverter
    {

        #region 将Byte数组的高低字节转换 

        /// <summary>
        /// 将Byte数组的高低字节转换
        /// -基恩士String字符串转换用
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] BytesHLSwap(byte[] buffer)
        {
            byte temp;
            for (int i = 0; i < buffer.Length / 2; i++)
            {
                temp = buffer[i * 2];
                buffer[i * 2] = buffer[i * 2 + 1];
                buffer[i * 2 + 1] = temp;
            }
            return buffer;
        }

        #endregion

        #region 将Byte数组转换为Hex

        /// <summary> 
        /// 将Byte数组转换为16进制字符串
        /// </summary> 
        public static string BytesToHexString(this byte[] bytes)
        {
            if (bytes == null) return "";
            string hex = BitConverter.ToString(bytes).Replace("-", "");
            return hex;
        }

        #endregion

        #region Byte数组转为字符串


        #endregion

    }
}
