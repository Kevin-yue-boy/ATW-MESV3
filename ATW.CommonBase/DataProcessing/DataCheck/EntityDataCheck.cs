using ATW.CommonBase.Model.Log;
using ATW.CommonBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.DataProcessing.DataCheck
{
    public static class EntityDataCheck
    {


        #region 校验实体数据类型 添加&删除

        /// <summary>
        /// 校验实体数据类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="IsDelete"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool CheckEntity<T>(T entity, bool IsDelete, out string msg) where T : class, new()
        {
            msg = "";
            string msg_err = "";
            bool result = true;
            Type type_entity = typeof(T);
            if (entity == null)
            {
                throw new ArgumentNullException("输入实体为null");
            }
            int index = -1;
            //取属性上的自定义特性 获取对象属性
            foreach (PropertyInfo propInfo in type_entity.GetProperties())
            {
                index++;
                object[] objAttrs = propInfo.GetCustomAttributes(typeof(EntityDataCheckModel), true);
                if (Convert.ToString(propInfo.Name) == "GUID"
                    && IsDelete
                    && string.IsNullOrWhiteSpace(propInfo.GetValue(entity)?.ToString()))
                {
                    if (string.IsNullOrWhiteSpace(propInfo.GetValue(entity)?.ToString()))
                    {
                        msg += $"请选择需要删除的数据";
                        return false;
                    }
                }
                if (objAttrs.Length > 0)
                {
                    EntityDataCheckModel entityDataCheckModel = objAttrs[0] as EntityDataCheckModel;
                    if (entityDataCheckModel != null)
                    {
                        if (!IsDelete
                            &&entityDataCheckModel.IsNullable == false 
                            && string.IsNullOrWhiteSpace(propInfo.GetValue(entity)?.ToString()))
                        {
                            msg_err += $"{entityDataCheckModel.ColumnDescription}:数据项为空\r";
                            result = false;
                        }
                        else
                        {
                            msg += $"{entityDataCheckModel.ColumnDescription}:" +
                                 $" {propInfo.GetValue(entity)?.ToString()} ;\r";
                        }
                    }
                }
            }
            msg = result ? msg : msg_err;
            return result;
        }

        #endregion

        #region 校验实体数据类型 编辑

        /// <summary>
        /// 校验实体数据类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="IsDelete"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool CheckEntity<T>(T entity,T entity_Old, out string msg) where T : class, new()
        {
            msg = "";
            string msg_err = "";
            bool result = true;
            Type type_entity = typeof(T);
            if (entity == null|| entity_Old==null)
            {
                throw new ArgumentNullException("输入实体为null");
            }
            int index = -1;
            //取属性上的自定义特性 获取对象属性
            foreach (PropertyInfo propInfo in type_entity.GetProperties())
            {
                index++;
                object[] objAttrs = propInfo.GetCustomAttributes(typeof(EntityDataCheckModel), true);
                if (Convert.ToString(propInfo.Name) == "GUID"
                    && string.IsNullOrWhiteSpace(propInfo.GetValue(entity)?.ToString()))
                {
                    if (string.IsNullOrWhiteSpace(propInfo.GetValue(entity)?.ToString()))
                    {
                        msg += $"请选择需要编辑的数据";
                        return false;
                    }
                }
                if (objAttrs.Length > 0)
                {
                    EntityDataCheckModel entityDataCheckModel = objAttrs[0] as EntityDataCheckModel;
                    if (entityDataCheckModel != null)
                    {
                        if (entityDataCheckModel.IsNullable == false
                            && string.IsNullOrWhiteSpace(propInfo.GetValue(entity)?.ToString()))
                        {
                            msg_err += $"{entityDataCheckModel.ColumnDescription}:数据项为空\r";
                            result = false;
                        }
                        else
                        {
                            if (propInfo.GetValue(entity_Old)?.ToString()!= propInfo.GetValue(entity)?.ToString())
                            {
                                msg += $"{entityDataCheckModel.ColumnDescription}:" +
                                $" {propInfo.GetValue(entity_Old)?.ToString()}" +
                                $"=>{propInfo.GetValue(entity)?.ToString()} ;\r";
                            }
                        }
                    }
                }
            }
            msg = result ? msg : msg_err;
            return result;
        }

        #endregion


    }
}
