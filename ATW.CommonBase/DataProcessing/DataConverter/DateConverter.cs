using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.DataProcessing.DataConverter
{
    public static class DateConverter
    {

        #region 获取开始日期到结束日期之间的所有年份（包含首尾年份），返回字符串格式的年份列表

        /// <summary>
        /// 获取开始日期到结束日期之间的所有年份（包含首尾年份），返回字符串格式的年份列表
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns>包含所有年份的字符串列表</returns>
        /// <exception cref="ArgumentException">当结束日期早于开始日期时抛出</exception>
        public static List<string> GetYearsBetweenDates(DateTime start, DateTime end)
        {
            // 验证输入：确保结束日期不早于开始日期
            if (end < start)
            {
                throw new ArgumentException("结束日期不能早于开始日期", nameof(end));
            }

            List<string> yearList = new List<string>();

            // 获取开始年份和结束年份
            int startYear = start.Year;
            int endYear = end.Year;

            // 遍历从开始年到结束年的所有年份，转换为字符串并添加到列表
            for (int year = startYear; year <= endYear; year++)
            {
                yearList.Add(year.ToString());
            }

            return yearList;
        }

        #endregion

        #region  获取开始日期到结束日期之间的所有月份（包含首尾月份），返回"YYYYMM"格式的字符串列表

        /// <summary>
        /// 获取开始日期到结束日期之间的所有月份（包含首尾月份），返回"YYYYMM"格式的字符串列表
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns>包含所有月份的字符串列表（格式：YYYYMM）</returns>
        /// <exception cref="ArgumentException">当结束日期早于开始日期时抛出</exception>
        public static List<string> GetMonthsBetweenDates(DateTime start, DateTime end)
        {
            // 输入验证：确保结束日期不早于开始日期
            if (end < start)
            {
                throw new ArgumentException("结束日期不能早于开始日期", nameof(end));
            }

            List<string> monthList = new List<string>();
            // 从开始日期的当月开始遍历
            DateTime currentMonth = new DateTime(start.Year, start.Month, 1);
            // 结束日期的当月（取当月第一天，方便判断）
            DateTime endMonth = new DateTime(end.Year, end.Month, 1);

            // 循环遍历每个月，直到超过结束月份
            while (currentMonth <= endMonth)
            {
                // 格式化为"YYYYMM"的字符串（补零，确保月份是两位）
                string monthStr = currentMonth.ToString("yyyyMM");
                monthList.Add(monthStr);

                // 切换到下一个月的第一天
                currentMonth = currentMonth.AddMonths(1);
            }

            return monthList;
        }

        #endregion

        #region 获取开始日期到结束日期之间的所有日期（包含首尾日期），返回"YYYYMMDD"格式的字符串列表

        /// <summary>
        /// 获取开始日期到结束日期之间的所有日期（包含首尾日期），返回"YYYYMMDD"格式的字符串列表
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns>包含所有日期的字符串列表（格式：YYYYMMDD）</returns>
        /// <exception cref="ArgumentException">当结束日期早于开始日期时抛出</exception>
        public static List<string> GetDaysBetweenDates(DateTime start, DateTime end)
        {
            // 输入验证：确保结束日期不早于开始日期
            if (end < start)
            {
                throw new ArgumentException("结束日期不能早于开始日期", nameof(end));
            }

            List<string> dayList = new List<string>();
            // 从开始日期开始遍历，避免时分秒干扰，只保留日期部分
            DateTime currentDay = start.Date;

            // 循环遍历每一天，直到超过结束日期
            while (currentDay <= end.Date)
            {
                // 格式化为"YYYYMMDD"的字符串（补零，确保月/日都是两位）
                string dayStr = currentDay.ToString("yyyyMMdd");
                dayList.Add(dayStr);

                // 切换到下一天
                currentDay = currentDay.AddDays(1);
            }

            return dayList;
        }

        #endregion

    }
}
