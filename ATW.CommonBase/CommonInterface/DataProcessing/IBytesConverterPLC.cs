namespace ATW.CommonBase.CommonInterface.DataProcessing
{
    public interface IBytesConverterPLC
    {

        #region UInt32

        #region Bytes转换UInt32

        /// <summary>
        /// Bytes转换UInt32
        /// </summary>
        /// <param name="Buffer"></param>
        /// <param name="Pos"></param>
        /// <returns></returns>
        public UInt32 GetUInt32(byte[] Buffer, int Pos);

        #endregion

        #region UInt32转换为Bytes

        /// <summary>
        /// UInt32转换为Bytes
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public byte[] SetUInt32(UInt32 Value);

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
        public float GetFloat(byte[] Buffer, int Pos);

        #endregion

        #region Float转换Bytes

        /// <summary>
        /// Float转换Bytes
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public byte[] SetFloat(float Value);

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
        public string GetString(byte[] Buffer, int Pos, int Length);

        #endregion

        #region String转换为Bytes

        /// <summary>
        /// String转换为Bytes
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public byte[] SetString(string Value);

        #endregion

        #endregion

    }
}
