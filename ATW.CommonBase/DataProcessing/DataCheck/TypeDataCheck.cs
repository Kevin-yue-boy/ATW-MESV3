namespace ATW.CommonBase.DataProcessing.DataCheck
{
    public static class TypeDataCheck
    {

        /// <summary>
        /// 校验对象是否可以转换为 bool 类型
        /// </summary>
        public static bool CheckBoolYN(this object value) => TryConvert<bool>(value);

        /// <summary>
        /// 校验对象是否可以转换为 short 类型
        /// </summary>
        public static bool CheckShortYN(this object value) => TryConvert<short>(value);

        /// <summary>
        /// 校验对象是否可以转换为 ushort 类型
        /// </summary>
        public static bool CheckUShortYN(this object value) => TryConvert<ushort>(value);

        /// <summary>
        /// 校验对象是否可以转换为 int 类型
        /// </summary>
        public static bool CheckIntYN(this object value) => TryConvert<int>(value);

        /// <summary>
        /// 校验对象是否可以转换为 uint 类型
        /// </summary>
        public static bool CheckUIntYN(this object value) => TryConvert<uint>(value);

        /// <summary>
        /// 校验对象是否可以转换为 long 类型
        /// </summary>
        public static bool CheckLongYN(this object value) => TryConvert<long>(value);

        /// <summary>
        /// 校验对象是否可以转换为 ulong 类型
        /// </summary>
        public static bool CheckULongYN(this object value) => TryConvert<ulong>(value);

        /// <summary>
        /// 校验对象是否可以转换为 decimal 类型
        /// </summary>
        public static bool CheckDecimalYN(this object value) => TryConvert<decimal>(value);

        /// <summary>
        /// 校验对象是否可以转换为 float 类型
        /// </summary>
        public static bool CheckFloatYN(this object value) => TryConvert<float>(value);

        /// <summary>
        /// 校验对象是否可以转换为 double 类型
        /// </summary>
        public static bool CheckDoubleYN(this object value) => TryConvert<double>(value);

        /// <summary>
        /// 校验对象是否可以转换为 DateTime 类型
        /// </summary>
        public static bool CheckDateTimeYN(this object value) => TryConvert<DateTime>(value);

        #region 核心转换逻辑

        /// <summary>
        /// 核心转换逻辑
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">数据源</param>
        /// <returns></returns>
        private static bool TryConvert<T>(this object value) where T : struct
        {
            if (value == null)
                return false;

            // 如果是目标类型直接返回 true
            if (value is T)
                return true;

            // 尝试通过 Convert.ChangeType 转换
            try
            {
                var converted = Convert.ChangeType(value, typeof(T));
                return converted is T;
            }
            catch (OverflowException)   // 数值溢出
            {
                return false;
            }
            catch (FormatException)     // 格式错误（如字符串解析失败）
            {
                return false;
            }
            catch (InvalidCastException) // 无法转换的类型
            {
                return false;
            }
        }

        #endregion

        /// <summary> 
        /// 校验字符是否为有效的16进制字符（0-9, a-f, A-F）
        /// </summary> 
        public static bool CheckHexCharYN(this char c)
        {
            return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
        }

    }
}
