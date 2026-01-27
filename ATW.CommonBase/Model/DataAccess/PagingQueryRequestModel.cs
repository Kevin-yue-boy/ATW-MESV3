using ATW.CommonBase.Method.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.Model.DataAccess
{
    public class PagingQueryRequestModel: ObservableValidator
    {

        /// <summary>
        /// 页面索引
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页面数量
        /// </summary>
        public int PageCount { get; set; } = 1;

        /// <summary>
        /// 最大页面数量
        /// </summary>
        public int MaxPageCount { get; set; } = 20;

        /// <summary>
        /// 数据总数
        /// </summary>
        public long DataCount { get; set; } = 0;

        /// <summary>
        /// 当前页面(页面显示用)
        /// </summary>
        public string ViewPageIndex { get; set; } = "1/1";

        /// <summary>
        /// 查询条件
        /// </summary>
        public object Predicate { get; set; }

        /// <summary>
        /// 排序条件
        /// </summary>
        public object FieldSelector { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string SearchSortType { get; set; }

        

    }
}
