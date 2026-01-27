namespace ATW.CommonBase.CommonInterface.Communicate
{
    public interface ICommunicatePLC
    {
        /// <summary>
        /// 激活dll
        /// </summary>
        /// <returns></returns>
        bool Activate();

        /// <summary>
        /// 通讯状态
        /// </summary>
        bool IsConnected { get; set; }

        #region PLC读写

        #region 读写Bool

        #region 读取Bool

        /// <summary>
        /// 读取Bool
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        bool ReadBool(string address);

        #endregion

        #region 写入Bool

        /// <summary>
        /// 写入Bool
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        bool WriteBool(string address, bool Val);

        #endregion

        #endregion

        #region 读写Int16

        #region 读取Int16

        /// <summary>
        /// 读取Int16
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        Int16 ReadInt16(string address);

        #endregion

        #region 写入Int16

        /// <summary>
        /// 写入Int16
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        bool WriteInt16(string address, Int16 Val);

        #endregion

        #endregion

        #region 读写UInt16

        #region 读取UInt16

        /// <summary>
        /// 读取UInt16
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        UInt16 ReadUInt16(string address);

        #endregion

        #region 写入UInt16

        /// <summary>
        /// 写入UInt16
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        bool WriteUInt16(string address, UInt16 Val);

        #endregion

        #endregion

        #region 读写Int32

        #region 读取Int32

        /// <summary>
        /// 读取Int32
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        Int32 ReadInt32(string address);

        #endregion

        #region 写入Int32

        /// <summary>
        /// 写入Int32
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        bool WriteInt32(string address, Int32 Val);

        #endregion

        #endregion

        #region 读写UInt32

        #region 读取UInt32

        /// <summary>
        /// 读取UInt32
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        UInt32 ReadUInt32(string address);

        #endregion

        #region 写入UInt32

        /// <summary>
        /// 写入UInt32
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        bool WriteUInt32(string address, UInt32 Val);

        #endregion

        #endregion

        #region 读写float

        #region 读取float

        /// <summary>
        /// 读取float
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        float ReadFloat(string address);

        #endregion

        #region 写入float

        /// <summary>
        /// 写入float
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        bool WriteFloat(string address, float Val);

        #endregion

        #endregion

        #region 读写String

        #region 读取String

        /// <summary>
        /// 读取String
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        string ReadString(string address, ushort Length = 25);

        #endregion

        #region 写入string

        /// <summary>
        /// 写入string
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        bool WriteString(string address, string Val, ushort Length = 30);

        #endregion

        #region 读取String

        /// <summary>
        /// 读取String
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        string ReadString(string address, Encoding _Encoding, ushort Length = 30);

        #endregion

        #region 写入string

        /// <summary>
        /// 写入string
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        bool WriteString(string address, string Val, Encoding _Encoding, ushort Length = 30);

        #endregion

        #endregion

        #region 批量读写Bool

        #region 批量读Bool

        /// <summary>
        /// 批量读Bool
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        bool[] ReadBoolArr(string address, ushort Length = 30);

        #endregion

        #endregion

        #region 批量读写

        #region 批量读

        /// <summary>
        /// 批量读
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        byte[] ReadByteArr(string address, ushort Length = 30);

        #endregion

        #region 批量写

        /// <summary>
        /// 批量写
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        bool WriteByteArr(string address, byte[] Val);

        #endregion

        #endregion

        #endregion
    }
}
