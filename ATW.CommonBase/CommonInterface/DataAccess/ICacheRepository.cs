using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.CommonInterface.DataAccess
{
    public interface ICacheRepository
    {

        #region 基础方法

        /// <summary>
        /// 检查Redis连接状态
        /// </summary>
        bool IsConnected { get;  }

        /// <summary>
        /// 获取Redis服务器信息
        /// </summary>
        public IServer GetServer();

        /// <summary>
        /// 删除键
        /// </summary>
        public bool DeleteKey(string key);

        /// <summary>
        /// 检查键是否存在
        /// </summary>
        public bool KeyExists(string key);

        /// <summary>
        /// 设置键过期时间
        /// </summary>
        public bool SetKeyExpire(string key, TimeSpan expiry);

        #endregion

        #region String类型操作

        /// <summary>
        /// 设置字符串值
        /// </summary>
        public   Task<bool> StringSetAsync(string key, object value, TimeSpan expiry);

        /// <summary>
        /// 获取字符串值
        /// </summary>
        public   Task<T?> StringGetAsync<T>(string key);

        #endregion

        #region List类型操作

        #region 添加元素

        /// <summary>
        /// 从列表左侧添加元素
        /// </summary>
        public  Task<long> ListLeftPushAsync(string key, object value);

        /// <summary>
        /// 从列表右侧添加元素
        /// </summary>
        public  Task<long> ListRightPushAsync(string key, object value);

        #endregion

        #region 弹出元素

        /// <summary>
        /// 从列表左侧弹出元素
        /// </summary>
        public  Task<T?> ListLeftPopAsync<T>(string key);

        /// <summary>
        /// 从列表右侧弹出元素
        /// </summary>
        public Task<T?> ListRightPopAsync<T>(string key);

        #endregion

        #region 获取列表元素

        /// <summary>
        /// 获取列表元素 排序从左开始
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop">-1等于获取所有元素</param>
        /// <returns></returns>
        public  Task<List<T?>> ListRangeAsync<T>(string key, long start = 0, long stop = 0);

        #endregion

        #region 移除列表中的元素

        /// <summary>
        /// 移除列表中的元素
        /// </summary>
        public   Task<long> ListRemoveAsync(string key, object value, long count = 0);

        #endregion

        #region 获取列表长度

        /// <summary>
        /// 获取列表长度
        /// </summary>
        public long ListLength(string key) ;

        #endregion

        #endregion

        #region Hash类型操作

        /// <summary>
        /// 设置Hash字段值
        /// </summary>
        public   Task<bool> HashSetAsync(string key, string hashField, object value);

        /// <summary>
        /// 获取Hash字段值
        /// </summary>
        public Task<T?> HashGetAsync<T>(string key, string hashField);

        /// <summary>
        /// 删除Hash字段
        /// </summary>
        public Task<bool> HashDeleteAsync(string key, string hashField);

        /// <summary>
        /// 判断Hash字段是否存在
        /// </summary>
        public   Task<bool> HashExistsAsync(string key, string hashField);

        #endregion

        #region Set类型操作

        /// <summary>
        /// 添加Set元素
        /// </summary>
        public bool SetAdd(string key, object value);

        /// <summary>
        /// 获取Set所有元素
        /// </summary>
        public List<T?> SetMembers<T>(string key);

        /// <summary>
        /// 移除Set元素
        /// </summary>
        public bool SetRemove(string key, object value);

        /// <summary>
        /// 判断元素是否在Set中
        /// </summary>
        public bool SetContains(string key, object value);

        #endregion

        #region SortedSet类型操作

        /// <summary>
        /// 添加SortedSet元素（带分数）
        /// </summary>
        /// <param name="key">集合键名</param>
        /// <param name="value">元素值</param>
        /// <param name="score">排序分数</param>
        /// <returns>是否新增元素（true=新增，false=更新分数）</returns>
        public bool SortedSetAdd<T>(string key, T value, double score);

        #endregion

    }
}
