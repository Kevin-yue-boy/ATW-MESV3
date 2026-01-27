using ATW.CommonBase.CommonInterface.DataAccess;
using ATW.CommonBase.DataProcessing.DataExpand;
using ATW.CommonBase.Model;
using ATW.CommonBase.Model.DataAccess;
using ATW.CommonBase.Model.Enum;
using Elastic.Clients.Elasticsearch.MachineLearning;
using Newtonsoft.Json.Linq;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ATW.CommonBase.DataAccess.Common
{
    public class CacheUniteSqlSugarRepository
    {

        #region Parameter

        private ISqlSugarClient DB { get; set; }

        private ICacheRepository ICR { get; set; }

        #endregion

        #region 构造函数

        public CacheUniteSqlSugarRepository(ISqlSugarClient sqlSugarClient, ICacheRepository iCR)
        {
            DB = sqlSugarClient.AsTenant().GetConnectionScope("MainDB");
            ICR = iCR;
        }

        #endregion

        #region 通用查询方法

        /// <summary>
        /// 通用联合查询方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<List<T>?> GetListAsync<T>() where T : class, new()
        {

            #region Parameter

            List<T>? values = new List<T>();
            var cacheStorage = typeof(T)?.GetCustomAttribute<CacheStorage>();
            string tableName = typeof(T)?.GetCustomAttribute<SugarTable>()?.TableName
            ?? typeof(T).Name;

            #endregion

            #region 如果不使用缓存技术

            if (!cacheStorage.EnableYN)
            {
                throw new ArgumentNullException($"数据实体({typeof(T).Name})未配置缓存功能!");
            }

            #endregion

            #region 缓存捞取数据

            //先从Redis捞基础数据
            values = await ICR.HashGetAsync<List<T>>("MainDB", tableName);
            if (values != null && values.Count > 0) return values;

            #endregion

            #region 从数据库捞取数据

            values = await DB.Queryable<T>().ToListAsync();

            #endregion

            #region 数据存入缓存

            await ICR.HashSetAsync("MainDB", tableName, values);

            #endregion

            return values;
        }

        #endregion

        #region 条件查询获取数据集合

        /// <summary>
        /// 条件查询获取数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<List<T?>> GetListAsync<T>(Func<T, bool> predicate) where T : class, new()
        {
            var values = await GetListAsync<T>();
            if (values == null || values.Count < 1) return null;
            return values.Where(predicate)?.ToList();
        }

        #endregion

        #region 条件查询获取数据集合并排序

        /// <summary>
        /// 条件查询获取数据集合并排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="fieldSelector">排序条件</param>
        /// <param name="DescYN">是否倒叙</param>
        /// <returns></returns>
        public async Task<List<T?>> GetListAsync<T>(Func<T, bool> predicate, Func<T, IComparable> fieldSelector, bool DescYN = false) where T : class, new()
        {
            var values = await GetListAsync<T>();
            if (values == null || values.Count < 1) return null;
            if (DescYN)
            {
                return values.Where(predicate)?.OrderByDescending(fieldSelector)?.ToList();
            }
            else
            {
                return values.Where(predicate)?.OrderBy(fieldSelector)?.ToList();
            }
        }

        #endregion

        #region 条件查询获取第一条数据

        /// <summary>
        /// 条件查询获取第一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        public async Task<T?> GetFirstAsync<T>(Func<T, bool> predicate) where T : class, new()
        {
            var values = await GetListAsync<T>();
            if (values == null || values.Count < 1
                || values?.Where(predicate)?.ToList().Count() < 1) return null;
            return (values as List<T>).Where(predicate)?.First();
        }

        #endregion

        #region 条件查询获取第一条数据并排序

        /// <summary>
        /// 条件查询获取第一条数据并排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="fieldSelector">排序条件</param>
        /// <param name="DescYN">是否倒叙</param>
        /// <returns></returns>
        public async Task<T?> GetFirstAsync<T>(Func<T, bool> predicate, Func<T, IComparable> fieldSelector, bool DescYN = false) where T : class, new()
        {
            var values = await GetListAsync<T>();
            if (values == null || values.Count < 1
                || values?.Where(predicate)?.ToList().Count() < 1) return null;
            if (DescYN)
            {
                return values.Where(predicate)?.OrderByDescending(fieldSelector)?.First();
            }
            else
            {
                return values.Where(predicate)?.OrderBy(fieldSelector)?.First();
            }
        }

        #endregion

        #region 清空某张表缓存

        /// <summary>
        /// 清空某张表缓存
        /// </summary>
        public async void RemoveList<T>() where T : class, new()
        {
            string tableName = typeof(T)?.GetCustomAttribute<SugarTable>()?.TableName
             ?? typeof(T).Name;
            await ICR.HashDeleteAsync("MainDB", tableName);
        }

        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pagingQueryRequest"></param>
        /// <returns></returns>
        public async Task<List<T>> PagingQueryAsync<T>(PagingQueryRequestModel pagingQueryRequest) where T : class, new()
        {
            return await Task.Run((Func<Task<List<T>>?>)(async Task<List<T>> () =>
            {
                var models = await GetListAsync<T>();
                var buffer = models == null || models.Count() < 1 ? null :
                       pagingQueryRequest.Predicate == null ? models : models.Where((pagingQueryRequest.Predicate as Func<T,bool>));
                buffer = buffer == null ? null :
                    pagingQueryRequest.FieldSelector == null ? buffer :
                    pagingQueryRequest.SearchSortType == EnumSortType.Desc.EnumToDesc() ? buffer.OrderByDescending((pagingQueryRequest.FieldSelector as Func<T, IComparable>))
                    : buffer.OrderBy((pagingQueryRequest.FieldSelector as Func<T, IComparable>));
                pagingQueryRequest.DataCount = buffer == null ? 0 : buffer.Count();
                var Datas = buffer == null ? null : (buffer.Skip((int)(pagingQueryRequest.MaxPageCount * (pagingQueryRequest.PageIndex - 1))).Take(pagingQueryRequest.MaxPageCount).ToList() as List<T>);
                if (Datas != null && Datas.Count > 0) { ListIdIncrementHelper.AssignIncrementalId<T>(Datas); }
                pagingQueryRequest.PageCount = pagingQueryRequest.DataCount % pagingQueryRequest.MaxPageCount > 0 ? Convert.ToInt32(pagingQueryRequest.DataCount / pagingQueryRequest.MaxPageCount) + 1 : Convert.ToInt32(pagingQueryRequest.DataCount / pagingQueryRequest.MaxPageCount);
                pagingQueryRequest.ViewPageIndex = $"{pagingQueryRequest.PageIndex}/{pagingQueryRequest.PageCount}";
                return Datas;
            }));
        }

        #endregion

    }
}
