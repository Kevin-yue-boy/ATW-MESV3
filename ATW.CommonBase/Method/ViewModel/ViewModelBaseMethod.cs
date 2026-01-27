using ATW.CommonBase.Model.DataAccess;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.Method.ViewModel
{
    public partial class ViewModelBaseMethod<T> :ViewModelBase where T : class, new()
    {

        #region Parameter

        public string Search { get; set; }

        /// <summary>
        /// 查询条件实体
        /// </summary>
        public PagingQueryRequestModel PagingQueryRequest { get; set; } = new PagingQueryRequestModel();

        #region 展示数据

        /// <summary>
        /// 实体类
        /// </summary>
        public T? Model { get; set; }

        /// <summary>
        /// 双击原数据
        /// </summary>
        public T? Model_Old { get; set; }

        /// <summary>
        /// 实体类集合（DGV显示用）
        /// </summary>
        public List<T>? Models { get; set; }

        #endregion

        #endregion

        #region 刷新页面

        public void RefreshPage()
        {
            Model = new T();
            Model_Old = new T();
        }        

        #endregion

        #region 双击行

        /// <summary>
        /// 行双击
        /// </summary>
        /// <param name="smodel"></param>
        [RelayCommand]
        public virtual void ShowDetail(Object smodel)
        {
            if (smodel != null)
            {
                Model_Old = smodel as T;
                if (Model_Old != null)
                {
                    Model = DeepCopy.JsonDeepCopy(Model_Old);
                }
            }
        }

        #endregion

        #region 分页查询

        #region 分页查询

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pagingQueryRequest"></param>
        public async virtual Task PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest)
        {
        }

        #endregion

        #region 首页

        /// <summary>
        /// 首页
        /// </summary>
        [RelayCommand]
        public async Task FirstPage()
        {
            try
            {
                //获取首页数据
                PagingQueryRequest.PageIndex = 1;
                await PagingQueryAsync(PagingQueryRequest);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region 上一页

        /// <summary>
        /// 上一页
        /// </summary>
        [RelayCommand]
        public async Task BeforePage()
        {
            try
            {
                if (PagingQueryRequest.PageIndex > 1)
                {
                    PagingQueryRequest.PageIndex -= 1;
                }
                await PagingQueryAsync(PagingQueryRequest);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region 下一页

        /// <summary>
        /// 下一页
        /// </summary>
        [RelayCommand]
        public async Task NextPage()
        {
            try
            {
                if (PagingQueryRequest.PageIndex < PagingQueryRequest.PageCount)
                {
                    PagingQueryRequest.PageIndex += 1;
                }
                await PagingQueryAsync(PagingQueryRequest);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region 尾页

        /// <summary>
        /// 尾页
        /// </summary>
        [RelayCommand]
        public async Task EndPage()
        {
            try
            {
                PagingQueryRequest.PageIndex = PagingQueryRequest.PageCount;
                await PagingQueryAsync(PagingQueryRequest);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #endregion

        #region 初始化 卸载

        #region 初始化

        public override async void Initialize()
        {

        }

        #endregion

        #region 卸载

        public override void UnLoad()
        {

        }

        #endregion

        #endregion

    }
}
