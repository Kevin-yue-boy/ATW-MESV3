using ATW.CommonBase.DataProcessing.DataCheck;

namespace ATW.CommonBase.DataProcessing.DataConverter
{
    public static class HexConverter
    {

        #region 将16进制字符串转换为Byte数组 

        /// <summary> 
        /// 将16进制字符串转换为Byte数组 
        /// </summary> 
        /// <param name="hexString">16进制字符串（支持大小写字母，可带0x前缀）</param> 
        /// <returns>转换后的Byte数组</returns> 
        /// <exception cref="ArgumentNullException">输入字符串为null</exception> 
        /// <exception cref="ArgumentException">输入字符串格式无效（长度为奇数或包含非16进制字符）</exception> 
        public static byte[] HexStringToBytes(this string hexString)
        {
            // 验证输入不为null 
            if (hexString == null)
                throw new ArgumentNullException(nameof(hexString), "输入的16进制字符串不能为null");

            // 移除可能的"0x"前缀 
            hexString = hexString.Trim().ToLowerInvariant();
            if (hexString.StartsWith("0x"))
                hexString = hexString.Substring(2);

            // 检查长度是否为偶数（1个字节对应2个16进制字符） 
            if (hexString.Length % 2 != 0)
                throw new ArgumentException("16进制字符串长度必须为偶数", nameof(hexString));

            // 检查是否包含无效字符 
            foreach (char c in hexString)
            {
                if (!c.CheckHexCharYN())
                    throw new ArgumentException($"16进制字符串包含无效字符: '{c}'", nameof(hexString));
            }

            // 转换为Byte数组 
            byte[] byteArray = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
            {
                // 每2个字符转换为1个字节（16进制转10进制） 
                string hexByte = hexString.Substring(i, 2);
                byteArray[i / 2] = Convert.ToByte(hexByte, 16);
            }

            return byteArray;
        }

        #endregion

    }
}
