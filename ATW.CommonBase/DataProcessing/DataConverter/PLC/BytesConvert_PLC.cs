using ATW.CommonBase.Model.Communicate;
using ATW.CommonBase.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.DataProcessing.DataConverter.PLC
{
    public static class BytesConvert_PLC
    {

        #region Bytes转换为实体类List集合

        /// <summary>
        /// Bytes转换为实体类List集合
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="bts"></param>
        /// <param name="iBCPLC"></param>
        /// <returns></returns>
        public static int BytesToEntitys<T>(List<T> entitys, int entity_Qty, IBytesConverterPLC iBCPLC, Byte[] bts, int index_pos) where T : class, new()
        {
            for (int i = 0; i < entity_Qty; i++)
            {
                T entity = new T();
                BytesToEntity(entity, iBCPLC, bts, ref index_pos);
                entitys.Add(entity);
            }
            return index_pos;
        }

        #endregion

        #region Bytes转换为实体类

        /// <summary>
        /// Bytes转换为实体类
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="bts"></param>
        /// <param name="iBCPLC"></param>
        /// <returns></returns>
        public static int BytesToEntity(object entity, IBytesConverterPLC iBCPLC, Byte[] bts, int index_pos)
        {
            BytesToEntity(entity, iBCPLC, bts, ref index_pos);
            return index_pos;
        }

        #endregion

        #region Bytes转换为实体类

        /// <summary>
        /// Bytes转换为实体类
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="bDAP"></param>
        /// <param name="bts"></param>
        /// <param name="index_pos"></param>
        private static void BytesToEntity(object entity, IBytesConverterPLC iBCPLC, Byte[] bts, ref int index_pos)
        {
            var type = entity.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                var value = prop.GetValue(entity);
                var propName = prop.Name;
                object[] objAttrs = prop.GetCustomAttributes(typeof(EMA_PLCModel), true);
                if (objAttrs.Length > 0 && (objAttrs[0] as EMA_PLCModel).DataType != EnumDataType.Model)
                {
                    var eMA_PLCModel = (objAttrs[0] as EMA_PLCModel);
                    if (eMA_PLCModel.DataType == EnumDataType.UInt32)
                    {
                        var _value = Convert.ChangeType(iBCPLC.GetUInt32(bts, index_pos), prop.PropertyType);
                        prop.SetValue(entity, _value);
                        index_pos += 4;
                    }
                    else if (eMA_PLCModel.DataType == EnumDataType.UInt32Arr)
                    {
                        UInt32[] values = new UInt32[eMA_PLCModel.ArrLength];
                        for (int i = 0; i < eMA_PLCModel.ArrLength; i++)
                        {
                            values[i] = iBCPLC.GetUInt32(bts, index_pos);
                            index_pos += 4;
                        }
                        var _value = Convert.ChangeType(values, prop.PropertyType);
                        prop.SetValue(entity, _value);
                    }
                    else if (eMA_PLCModel.DataType == EnumDataType.Float)
                    {
                        var _value = Convert.ChangeType(iBCPLC.GetFloat(bts, index_pos), prop.PropertyType);
                        prop.SetValue(entity, _value);
                        index_pos += 4;
                    }
                    else if (eMA_PLCModel.DataType == EnumDataType.FloatArr)
                    {
                        float[] values = new float[eMA_PLCModel.ArrLength];
                        for (int i = 0; i < eMA_PLCModel.ArrLength; i++)
                        {
                            values[i] = iBCPLC.GetFloat(bts, index_pos);
                            index_pos += 4;
                        }
                        var _value = Convert.ChangeType(values, prop.PropertyType);
                        prop.SetValue(entity, _value);
                    }
                    else if (eMA_PLCModel.DataType == EnumDataType.String)
                    {
                        var _value = Convert.ChangeType(iBCPLC.GetString(bts, index_pos, eMA_PLCModel.DataLength), prop.PropertyType);
                        prop.SetValue(entity, _value);
                        index_pos += eMA_PLCModel.DataLength;
                    }
                    else if (eMA_PLCModel.DataType == EnumDataType.StringArr)
                    {
                        string[] values = new string[eMA_PLCModel.ArrLength];
                        for (int i = 0; i < eMA_PLCModel.ArrLength; i++)
                        {
                            values[i] = iBCPLC.GetString(bts, index_pos, eMA_PLCModel.DataLength);
                            index_pos += eMA_PLCModel.DataLength;
                        }
                        var _value = Convert.ChangeType(values, prop.PropertyType);
                        prop.SetValue(entity, _value);
                    }
                }
                else if (value is System.Collections.IEnumerable enumerable && !(value is string))
                {
                    foreach (var item in enumerable)
                    {
                        BytesToEntity(item, iBCPLC, bts, ref index_pos);
                    }
                }
                else if (value != null)
                {
                    BytesToEntity(value, iBCPLC, bts, ref index_pos);
                }
            }
        }

        #endregion

    }
}
