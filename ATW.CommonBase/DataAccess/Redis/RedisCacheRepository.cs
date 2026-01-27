using ATW.CommonBase.CommonInterface.DataAccess;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.DataAccess.Redis
{
    public class RedisCacheRepository : IDisposable, ICacheRepository
    {

        #region Parameter

        private readonly IConnectionMultiplexer _redis;
        private IDatabase _db;
        private bool _disposed = false;

        #endregion

        #region 构造函数

        public RedisCacheRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _db = _redis.GetDatabase();
        }

        #endregion

        #region 基础方法

        /// <summary>
        /// 检查Redis连接状态
        /// </summary>
        public bool IsConnected => _redis.IsConnected;

        /// <summary>
        /// 获取Redis服务器信息
        /// </summary>
        public IServer GetServer()
        {
            var endpoints = _redis.GetEndPoints();
            return _redis.GetServer(endpoints.First());
        }

        /// <summary>
        /// 删除键
        /// </summary>
        public bool DeleteKey(string key) => _db.KeyDelete(key);

        /// <summary>
        /// 检查键是否存在
        /// </summary>
        public bool KeyExists(string key) => _db.KeyExists(key);

        /// <summary>
        /// 设置键过期时间
        /// </summary>
        public bool SetKeyExpire(string key, TimeSpan expiry) => _db.KeyExpire(key, expiry);

        #endregion

        #region String类型操作

        /// <summary>
        /// 设置字符串值
        /// </summary>
        public async Task<bool> StringSetAsync(string key, object value, TimeSpan expiry )
        {
            var json =  Serializer_JsonNet.ObjectToJson(value);
            return await _db.StringSetAsync(key, json, expiry);
        }

        /// <summary>
        /// 获取字符串值
        /// </summary>
        public async Task<T?> StringGetAsync<T>(string key)
        {
            var json = await _db.StringGetAsync(key);
            return json.HasValue ? Serializer_JsonNet.JsonToObject<T>(json): default;
        }

        #endregion

        #region List类型操作

        #region 添加元素

        /// <summary>
        /// 从列表左侧添加元素
        /// </summary>
        public async Task<long> ListLeftPushAsync(string key, object value)
        {
            var json = Serializer_JsonNet.ObjectToJson(value);
            return await _db.ListLeftPushAsync(key, json);
        }

        /// <summary>
        /// 从列表右侧添加元素
        /// </summary>
        public async Task<long> ListRightPushAsync(string key, object value)
        {
            var json = Serializer_JsonNet.ObjectToJson(value);
            return await _db.ListRightPushAsync(key, json);
        }

        #endregion

        #region 弹出元素

        /// <summary>
        /// 从列表左侧弹出元素
        /// </summary>
        public async Task<T?> ListLeftPopAsync<T>(string key)
        {
            var json = await _db.ListLeftPopAsync(key);
            return json.HasValue ? Serializer_JsonNet.JsonToObject<T>(json) : default;
        }

        /// <summary>
        /// 从列表右侧弹出元素
        /// </summary>
        public async Task<T?> ListRightPopAsync<T>(string key)
        {
            var json = await _db.ListRightPopAsync(key);
            return json.HasValue ? Serializer_JsonNet.JsonToObject<T>(json) : default;
        }

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
        public async Task<List<T?>> ListRangeAsync<T>(string key, long start = 0, long stop = 0)
        {
            var values = await _db.ListRangeAsync(key, start, stop);
            return values.Select(json => Serializer_JsonNet.JsonToObject<T>(json)).ToList();
        }

        #endregion

        #region 移除列表中的元素

        /// <summary>
        /// 移除列表中的元素
        /// </summary>
        public async Task<long> ListRemoveAsync(string key, object value, long count = 0)
        {
            var json = Serializer_JsonNet.ObjectToJson(value);
            return await _db.ListRemoveAsync(key, json, count);
        }

        #endregion

        #region 获取列表长度

        /// <summary>
        /// 获取列表长度
        /// </summary>
        public long ListLength(string key) => _db.ListLength(key);

        #endregion

        #endregion

        #region Hash类型操作

        /// <summary>
        /// 设置Hash字段值
        /// </summary>
        public async Task<bool> HashSetAsync(string key, string hashField, object value)
        {
            var json = Serializer_JsonNet.ObjectToJson(value);
            return await _db.HashSetAsync(key, hashField, json);
        }

        /// <summary>
        /// 获取Hash字段值
        /// </summary>
        public async Task<T?> HashGetAsync<T>(string key, string hashField)
        {
            var json =await _db.HashGetAsync(key, hashField);
            return json.HasValue ? Serializer_JsonNet.JsonToObject<T>(json) : default;
        }

        /// <summary>
        /// 删除Hash字段
        /// </summary>interface
        public async Task<bool> HashDeleteAsync(string key, string hashField) => await _db.HashDeleteAsync(key, hashField);

        /// <summary>
        /// 判断Hash字段是否存在
        /// </summary>
        public async Task<bool> HashExistsAsync(string key, string hashField) => await _db.HashExistsAsync(key, hashField);

        #endregion

        #region Set类型操作

        /// <summary>
        /// 添加Set元素
        /// </summary>
        public bool SetAdd(string key, object value)
        {
            var json = Serializer_JsonNet.ObjectToJson(value);
            return _db.SetAdd(key, json);
        }

        /// <summary>
        /// 获取Set所有元素
        /// </summary>
        public List<T?> SetMembers<T>(string key)
        {
            var values = _db.SetMembers(key);
            return values.Select(json => Serializer_JsonNet.JsonToObject<T>(json)).ToList();
        }

        /// <summary>
        /// 移除Set元素
        /// </summary>
        public bool SetRemove(string key, object value)
        {
            var json = Serializer_JsonNet.ObjectToJson(value);
            return _db.SetRemove(key, json);
        }

        /// <summary>
        /// 判断元素是否在Set中
        /// </summary>
        public bool SetContains(string key, object value)
        {
            var json = Serializer_JsonNet.ObjectToJson(value);
            return _db.SetContains(key, json);
        }

        #endregion

        #region SortedSet类型操作

        /// <summary>
        /// 添加SortedSet元素（带分数）
        /// </summary>
        /// <param name="key">集合键名</param>
        /// <param name="value">元素值</param>
        /// <param name="score">排序分数</param>
        /// <returns>是否新增元素（true=新增，false=更新分数）</returns>
        public bool SortedSetAdd<T>(string key, T value, double score)
        {
            var json = Serializer_JsonNet.ObjectToJson(value);
            return _db.SortedSetAdd(key, json, score);
        }

        #endregion

        #region 释放资源

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _redis?.Dispose();
            }

            _disposed = true;
        }

        ~RedisCacheRepository()
        {
            Dispose(false);
        }
        #endregion

    }
}
