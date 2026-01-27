namespace ATW.CommonBase.DataProcessing.DataConverter
{
    public static class StringTypeConverter
    {

        #region 将字符串首字母转为小写（处理空值、空字符串、单字符等边界情况）

        /// <summary>
        /// 将字符串首字母转为小写（处理空值、空字符串、单字符等边界情况）
        /// </summary>
        /// <param name="input">待处理的字符串</param>
        /// <returns>首字母小写后的字符串，空值返回空字符串</returns>
        public static string FirstCharToLower(this string input)
        {
            // 1. 空值/空字符串直接返回空
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            // 2. 单字符字符串直接转小写
            if (input.Length == 1)
            {
                return input.ToLowerInvariant(); // 用Invariant避免区域文化差异
            }

            // 3. 多字符字符串：首字母小写 + 剩余字符保持原样
            return char.ToLowerInvariant(input[0]) + input.Substring(1);
        }

        #endregion

    }
}
