using SqlSugar;
using System.Net;

namespace ATW.CommonBase.Communicate.PLC.HSL
{
    public class HSL_Keyence_MC: ICommunicatePLC
    {

        #region Parameter

        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 8501;

        /// <summary>
        /// MES自己判定与PLC交互心跳地址
        /// </summary>
        private string HeartAddress { get; set; }

        /// <summary>
        /// 通讯状态
        /// </summary>
        public bool IsConnected { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public KeyenceNanoSerialOverTcp PLC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OperateResult connect { get; set; }

        #endregion

        #region 加载

        public HSL_Keyence_MC(string _IP, int _Port = 8501,string heartAddress="ZF0")
        {
            Activate();
            IP = _IP;
            Port = _Port;
            HeartAddress = heartAddress;
            Active = true;
            RunStart();
        }

        #endregion

        #region 连接PLC

        public bool active;

        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                if (value != active)
                {
                    try
                    {
                        if (value)
                        {
                            start();
                        }
                        else
                        {
                            stop();
                        }
                    }
                    catch
                    { }
                    finally
                    {
                        active = value;
                    }
                }
            }
        }

        /// <summary>
        /// 打开PLC
        /// </summary>
        private void start()
        {
            PLC = new KeyenceNanoSerialOverTcp(IP, Port);
            PLC.ConnectTimeOut = 1000;
            connect = (PLC as KeyenceNanoSerialOverTcp).ConnectServer();
        }

        /// <summary>
        /// 关闭PLC
        /// </summary>
        private void stop()
        {
            connect = PLC.ConnectClose();
        }

        #endregion

        #region PLC心跳

        private void RunStart()
        {
            Task.Run(() => {
                while (true)
                {
                    try
                    {
                        if (PLC!=null)
                        {
                            PLC.Write(HeartAddress, (UInt32)1);
                            WriteUInt32(HeartAddress, 1);
                            if (PLC.ReadUInt32(HeartAddress).Content==1)
                            {
                                IsConnected = true;
                            }
                            else
                            {
                                IsConnected = false;
                            }
                        }
                        else
                        {
                            IsConnected = false;
                        }
                    }
                    catch (Exception)
                    {
                        IsConnected = false;
                    }
                    System.Threading.Thread.Sleep(1000);
                }

            });
        }

        #endregion

        #region 读写Bool

        #region 读取Bool

        /// <summary>
        /// 读取Bool
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool ReadBool(string address)
        {
            return PLC.ReadBool(address).Content;
        }

        #endregion

        #region 写入Bool

        /// <summary>
        /// 写入Bool
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool WriteBool(string address, bool Val)
        {
            return PLC.Write(address, Val).IsSuccess;
        }

        #endregion

        #endregion

        #region 读写Int16

        #region 读取Int16

        /// <summary>
        /// 读取Int16
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Int16 ReadInt16(string address)
        {
            return PLC.ReadInt16(address).Content;
        }

        #endregion

        #region 写入Int16

        /// <summary>
        /// 写入Int16
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool WriteInt16(string address, Int16 Val)
        {
            return PLC.Write(address, Val).IsSuccess;
        }

        #endregion

        #endregion

        #region 读写UInt16

        #region 读取UInt16

        /// <summary>
        /// 读取UInt16
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public UInt16 ReadUInt16(string address)
        {
            return PLC.ReadUInt16(address).Content;
        }

        #endregion

        #region 写入UInt16

        /// <summary>
        /// 写入UInt16
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool WriteUInt16(string address, UInt16 Val)
        {
            return PLC.Write(address, Val).IsSuccess;
        }

        #endregion

        #endregion

        #region 读写Int32

        #region 读取Int32

        /// <summary>
        /// 读取Int32
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Int32 ReadInt32(string address)
        {
            return PLC.ReadInt32(address).Content;
        }

        #endregion

        #region 写入Int32

        /// <summary>
        /// 写入Int32
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool WriteInt32(string address, Int32 Val)
        {
            return PLC.Write(address, Val).IsSuccess;
        }

        #endregion

        #endregion

        #region 读写UInt32

        #region 读取UInt32

        /// <summary>
        /// 读取UInt32
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public UInt32 ReadUInt32(string address)
        {
            return PLC.ReadUInt32(address).Content;
        }

        #endregion

        #region 写入UInt32

        /// <summary>
        /// 写入UInt32
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool WriteUInt32(string address, UInt32 Val)
        {
            return PLC.Write(address, Val).IsSuccess;
        }

        #endregion

        #endregion

        #region 读写float

        #region 读取float

        /// <summary>
        /// 读取float
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public float ReadFloat(string address)
        {
            return PLC.ReadFloat(address).Content;
        }

        #endregion

        #region 写入float

        /// <summary>
        /// 写入float
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool WriteFloat(string address, float Val)
        {
            return PLC.Write(address, Val).IsSuccess;
        }

        #endregion

        #endregion

        #region 读写String

        #region 读取String

        /// <summary>
        /// 读取String
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public string ReadString(string address, ushort Length = 30)
        {
            return PLC.ReadString(address, Length).Content;
        }

        #endregion

        #region 写入string

        /// <summary>
        /// 写入string
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool WriteString(string address, string Val, ushort Length = 30)
        {
            return PLC.Write(address, Val).IsSuccess;
        }

        #endregion

        #region 读取String

        /// <summary>
        /// 读取String
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public string ReadString(string address, Encoding _Encoding, ushort Length = 30)
        {
            return PLC.ReadString(address, Length, _Encoding).Content;
        }

        #endregion

        #region 写入string

        /// <summary>
        /// 写入string
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool WriteString(string address, string Val, Encoding _Encoding, ushort Length = 30)
        {
            return PLC.Write(address, Val, _Encoding).IsSuccess;
        }

        #endregion

        #endregion

        #region 批量读写Bool

        #region 批量读

        /// <summary>
        /// 批量读
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool[] ReadBoolArr(string address, ushort Length = 30)
        {
            return PLC.ReadBool(address, Length).Content;
        }

        #endregion

        #endregion

        #region 批量读写

        #region 批量读

        /// <summary>
        /// 批量读
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public byte[] ReadByteArr(string address, ushort Length = 30)
        {
            return PLC.Read(address, Length).Content;
        }

        #endregion

        #region 批量写

        /// <summary>
        /// 批量写 基恩士批量写入一次性只能写入2024个byte
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool WriteByteArr(string address, byte[] Val)
        {
            bool result = true;
            var write_Qty = Val.Length % 2000 > 0 ? (Val.Length / 2000)+1 : (Val.Length / 2000);
            for (int i = 0; i < write_Qty; i++)
            {
                if (result)
                {
                    if (write_Qty == 1)
                    {
                        return PLC.Write(address, Val).IsSuccess;
                    }
                    else if (Val.Length % 2000 > 0 && i == write_Qty - 1)
                    {
                        byte[] buffer = new byte[Val.Length % 2000];
                        Array.Copy(Val, 2000 * i, buffer, 0, Val.Length % 2000);
                        result = PLC.Write(address.Substring(0, 2)
                            + Convert.ToString((i * 1000 + Convert.ToUInt32(address.Substring(2, address.Length - 2)))),
                            buffer).IsSuccess;
                    }
                    else
                    {
                        byte[] buffer = new byte[2000];
                        Array.Copy(Val, 2000 * i, buffer, 0, 2000);
                        result = PLC.Write(address.Substring(0, 2)
                            + Convert.ToString((i * 1000 + Convert.ToUInt32(address.Substring(2, address.Length - 2)))),
                            buffer).IsSuccess;
                    }
                }
            }
            return result;
        }

        #endregion

        #endregion

        #region 激活HSL

        public bool Activate()
        {
            return HslCommunication.Authorization.SetAuthorizationCode("34b93424-dc03-47ee-a2c1-aeaa12730521");
        }

        #endregion

    }
}
