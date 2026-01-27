using ATW.CommonBase.Model.Communicate;
using ATW.CommonBase.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.DataProcessing.DataConverter.PLC
{
    public static class EntityConvert_PLC
    {

        #region 根据实体计算读取Byte数组数量

        /// <summary>
        /// 根据实体计算读取Byte数组数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static UInt16 ReadByteQtyByEntity(this object entity, IBytesConverterPLC iBCPLC)
        {
            UInt16 qty = 0;
            ReadByteQtyByEntity(entity, iBCPLC, ref qty);
            return qty;
        }

        #endregion

        #region 根据实体计算写入Byte数组数量

        /// <summary>
        /// 根据实体计算写入Byte数组数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static UInt16 WriteByteQtyByEntity(this object entity, IBytesConverterPLC iBCPLC)
        {
            UInt16 qty = 0;
            ReadByteQtyByEntity(entity, iBCPLC, ref qty);
            if (iBCPLC.GetType().Name == typeof(BytesConverter_Siemens_S7).Name)
            {
                return qty;
            }
            else
            {
                return Convert.ToUInt16(qty * 2);
            }

        }

        #endregion

        #region 根据实体计算读取Byte数组数量

        /// <summary>
        /// 根据实体计算读取Byte数组数量
        /// </summary>
        /// <returns></returns>
        private static void ReadByteQtyByEntity(object entity, IBytesConverterPLC iBCPLC, ref UInt16 qty)
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
                        if (iBCPLC.GetType().Name == typeof(BytesConverter_Siemens_S7).Name)
                        {
                            qty += 4;
                        }
                        else
                        {
                            qty += 2;
                        }
                    }
                    else if (eMA_PLCModel.DataType == EnumDataType.UInt32Arr)
                    {
                        for (int i = 0; i < eMA_PLCModel.ArrLength; i++)
                        {
                            if (iBCPLC.GetType().Name == typeof(BytesConverter_Siemens_S7).Name)
                            {
                                qty += 4;
                            }
                            else
                            {
                                qty += 2;
                            }
                        }
                    }
                    else if (eMA_PLCModel.DataType == EnumDataType.Float)
                    {
                        if (iBCPLC.GetType().Name == typeof(BytesConverter_Siemens_S7).Name)
                        {
                            qty += 4;
                        }
                        else
                        {
                            qty += 2;
                        }
                    }
                    else if (eMA_PLCModel.DataType == EnumDataType.FloatArr)
                    {
                        for (int i = 0; i < eMA_PLCModel.ArrLength; i++)
                        {
                            if (iBCPLC.GetType().Name == typeof(BytesConverter_Siemens_S7).Name)
                            {
                                qty += 4;
                            }
                            else
                            {
                                qty += 2;
                            }
                        }
                    }
                    else if (eMA_PLCModel.DataType == EnumDataType.String)
                    {
                        if (iBCPLC.GetType().Name == typeof(BytesConverter_Siemens_S7).Name)
                        {
                            qty += Convert.ToUInt16(eMA_PLCModel.DataLength);
                        }
                        else
                        {
                            qty += Convert.ToUInt16(eMA_PLCModel.DataLength / 2);
                        }
                    }
                    else if (eMA_PLCModel.DataType == EnumDataType.StringArr)
                    {
                        for (int i = 0; i < eMA_PLCModel.ArrLength; i++)
                        {
                            if (iBCPLC.GetType().Name == typeof(BytesConverter_Siemens_S7).Name)
                            {
                                qty += Convert.ToUInt16(eMA_PLCModel.DataLength);
                            }
                            else
                            {
                                qty += Convert.ToUInt16(eMA_PLCModel.DataLength / 2);
                            }
                        }
                    }
                }
                else if (value is System.Collections.IEnumerable enumerable && !(value is string))
                {
                    foreach (var item in enumerable)
                    {
                        ReadByteQtyByEntity(item, iBCPLC, ref qty);
                    }
                }
                else if (value != null)
                {
                    ReadByteQtyByEntity(value, iBCPLC, ref qty);
                }
            }
        }

        #endregion

        #region 将实体转换为Byte数组

        /// <summary>
        /// 将实体转换为Byte数组
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="iBCPLC"></param>
        /// <returns></returns>
        public static Byte[] EntityToBytes(object entity, IBytesConverterPLC iBCPLC)
        {
            Byte[] bts = new Byte[entity.WriteByteQtyByEntity(iBCPLC)];
            int index_pos = 0;
            EntityToBytes(entity, iBCPLC, bts, ref index_pos);
            return bts;
        }

        #endregion

        #region 将实体转换为Byte数组

        /// <summary>
        /// 将实体转换为Byte数组
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="iBCPLC"></param>
        /// <param name="bts"></param>
        /// <param name="index_pos"></param>
        private static void EntityToBytes(object entity, IBytesConverterPLC iBCPLC, Byte[] bts, ref int index_pos)
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
                        if (value.CheckUIntYN())
                        {
                            Array.Copy(iBCPLC.SetUInt32(Convert.ToUInt32(value)), 0, bts, index_pos, 4);
                        }
                        index_pos += 4;
                    }
                    else if (eMA_PLCModel.DataType == EnumDataType.UInt32Arr)
                    {
                        UInt32[] values = value as UInt32[];
                        for (int i = 0; i < eMA_PLCModel.ArrLength; i++)
                        {
                            if (values != null && i < values.Length)
                            {
                                if (values[i].CheckUIntYN())
                                {
                                    Array.Copy(iBCPLC.SetUInt32(Convert.ToUInt32(values[i])), 0, bts, index_pos, 4);
                                }
                            }
                            index_pos += 4;
                        }
                    }
                    else if (eMA_PLCModel.DataType == EnumDataType.Float)
                    {
                        if (value.CheckFloatYN())
                        {
                            Array.Copy(iBCPLC.SetFloat(Convert.ToSingle(value)), 0, bts, index_pos, 4);
                        }
                        index_pos += 4;
                    }
                    else if (eMA_PLCModel.DataType == EnumDataType.FloatArr)
                    {
                        float[] values = value as float[];
                        for (int i = 0; i < eMA_PLCModel.ArrLength; i++)
                        {
                            if (values != null && i < values.Length)
                            {
                                if (values[i].CheckFloatYN())
                                {
                                    Array.Copy(iBCPLC.SetFloat(Convert.ToSingle(values[i])), 0, bts, index_pos, 4);
                                }
                            }
                            index_pos += 4;
                        }
                    }
                    else if (eMA_PLCModel.DataType == EnumDataType.String)
                    {
                        if (!string.IsNullOrWhiteSpace(value?.ToString()))
                        {
                            var dts_value = iBCPLC.SetString(value.ToString());
                            Array.Copy(dts_value, 0, bts, index_pos, dts_value.Length);
                        }
                        index_pos += eMA_PLCModel.DataLength;

                    }
                    else if (eMA_PLCModel.DataType == EnumDataType.StringArr)
                    {
                        string[] values = value as string[];
                        for (int i = 0; i < eMA_PLCModel.ArrLength; i++)
                        {
                            if (values != null && i < values.Length)
                            {
                                if (!string.IsNullOrWhiteSpace(values[i]?.ToString()))
                                {
                                    var dts_value = iBCPLC.SetString(values[i].ToString());
                                    Array.Copy(dts_value, 0, bts, index_pos, dts_value.Length);
                                }
                            }
                            index_pos += eMA_PLCModel.DataLength;
                        }
                    }
                }
                else if (value is System.Collections.IEnumerable enumerable && !(value is string))
                {
                    foreach (var item in enumerable)
                    {
                        EntityToBytes(item, iBCPLC, bts, ref index_pos);
                    }
                }
                else if (value != null)
                {
                    EntityToBytes(value, iBCPLC, bts, ref index_pos);
                }
            }
        }

        #endregion

    }
}
