namespace ATW.CommonBase.DataProcessing.DataConverter.PLC
{
    public class BytesConverter_Siemens_S7 : IBytesConverterPLC
    {

        #region UInt32

        #region Bytes转换UInt32

        /// <summary>
        /// Bytes转换UInt32
        /// </summary>
        /// <param name="Buffer"></param>
        /// <param name="Pos"></param>
        /// <returns></returns>
        public UInt32 GetUInt32(byte[] Buffer, int Pos)
        {
            UInt32 Result;
            Result = Buffer[Pos]; Result <<= 8;
            Result |= Buffer[Pos + 1]; Result <<= 8;
            Result |= Buffer[Pos + 2]; Result <<= 8;
            Result |= Buffer[Pos + 3];
            return Result;
        }

        #endregion

        #region UInt32转换为Bytes

        /// <summary>
        /// UInt32转换为Bytes
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public byte[] SetUInt32(UInt32 Value)
        {
            byte[] Buffer = new byte[4];
            Buffer[3] = (byte)(Value & 0xFF);
            Buffer[2] = (byte)((Value >> 8) & 0xFF);
            Buffer[1] = (byte)((Value >> 16) & 0xFF);
            Buffer[0] = (byte)((Value >> 24) & 0xFF);
            return Buffer;
        }

        #endregion

        #endregion

        #region Float

        #region Bytes转换Float

        /// <summary>
        /// Bytes转换Float
        /// </summary>
        /// <param name="Buffer"></param>
        /// <param name="Pos"></param>
        /// <returns></returns>
        public float GetFloat(byte[] Buffer, int Pos)
        {
            UInt32 Value = GetUInt32(Buffer, Pos);
            byte[] bytes = BitConverter.GetBytes(Value);
            return BitConverter.ToSingle(bytes, 0);
        }

        #endregion

        #region Float转换Bytes

        /// <summary>
        /// Float转换Bytes
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public byte[] SetFloat(float Value)
        {
            byte[] Buffer = new byte[4];
            byte[] FloatArray = BitConverter.GetBytes(Value);
            Buffer[0] = FloatArray[3];
            Buffer[1] = FloatArray[2];
            Buffer[2] = FloatArray[1];
            Buffer[3] = FloatArray[0];
            return Buffer;
        }

        #endregion

        #endregion

        #region String

        #region Bytes转换String

        /// <summary>
        /// Bytes转换String
        /// </summary>
        /// <param name="Buffer"></param>
        /// <param name="Pos"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public string GetString(byte[] Buffer, int Pos, int Length)
        {
            int size = (int)Buffer[Pos + 1];
            return Encoding.UTF8.GetString(Buffer, Pos + 2, size);
        }

        #endregion

        #region String转换为Bytes

        /// <summary>
        /// String转换为Bytes
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public byte[] SetString(string Value)
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                return new byte[2];
            }
            //注册Nuget包System.Text.Encoding.CodePages中的编码到.NET Core
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding fromEcoding = Encoding.GetEncoding("UTF-8");//返回utf-8的编码
            Encoding toEcoding = Encoding.GetEncoding("gb2312");
            byte[] fromBytes = fromEcoding.GetBytes(Value);
            byte[] Value_length = Encoding.Convert(fromEcoding, toEcoding, fromBytes);
            byte[] Buffer = new byte[Value_length.Length + 2];
            Array.Copy(Value_length, 0, Buffer, 2, Value_length.Length);
            //var Value_length = Encoding.ASCII.GetBytes(Value).Length;
            //int size = Value.Length;
            Buffer[0] = (byte)Value_length.Length;
            Buffer[1] = (byte)Value_length.Length;
            //Encoding.UTF8.GetBytes(Value, 0, size, Buffer, 2);
            return Buffer;
        }

        #endregion

        #endregion

    }
}
