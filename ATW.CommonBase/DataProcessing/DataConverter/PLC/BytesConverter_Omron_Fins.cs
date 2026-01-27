namespace ATW.CommonBase.DataProcessing.DataConverter.PLC
{
    public class BytesConverter_Omron_Fins : IBytesConverterPLC
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

            return System.BitConverter.ToUInt32(BytesConverter.BytesHLSwap(Buffer.Skip(Pos).Take(4).ToArray()));
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
            Array.Copy(BitConverter.GetBytes(Value), 0, Buffer, 0, 4);
            return BytesConverter.BytesHLSwap(Buffer);
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
            return (Single)Math.Round(System.BitConverter.ToSingle(BytesConverter.BytesHLSwap(Buffer.Skip(Pos).Take(4).ToArray()), 0), 6);
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
            Buffer = BitConverter.GetBytes(Value);
            return BytesConverter.BytesHLSwap(Buffer);
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
            return Encoding.Default.GetString
                (BytesConverter.BytesHLSwap(Buffer.Skip(Pos).Take(Length).ToArray()), 0, Length)
                .Replace("\0", "");
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
                return new byte[0];
            }
            var buffer = Encoding.UTF8.GetBytes(Value);
            var result = new byte[(buffer.Length % 2 == 1 ? buffer.Length + 1 : buffer.Length)];
            Array.Copy(buffer, 0, result, 0, buffer.Length);
            return BytesConverter.BytesHLSwap(result);
        }

        #endregion

        #endregion

    }
}
