namespace ATW.CommonBase.Model
{
    public class ResponseModel
    {

        /// <summary>
        /// 反馈结果
        /// </summary>
        public bool Result { get; set; } = false;

        /// <summary>
        /// 代号
        /// </summary>
        public uint SN { get; set; }

        /// <summary>
        /// 异常代码
        /// </summary>
        public string ErrCode { get { return $"E_{SN.ToString().PadLeft(6, '0')}"; } }

        /// <summary>
        /// 反馈消息
        /// </summary>
        public string Msg { get; set; } = "success";

    }
}
