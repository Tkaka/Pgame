//Auto generated, do not edit it
//GM消息

using System;
using System.IO;
using System.Collections.Generic;

namespace Message.GM
{
    public enum TypeEnum
    {
    }


    //GM命令
    public class ReqGM : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 102299;
		public string commandType; // 命令类型
		public string parameters; // 参数列表

    	//鏋勯�犲嚱鏁�
    	public ReqGM()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			commandType = "";
			parameters = "";
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                commandType = XBuffer.ReadString(buffer, ref offset);
                parameters = XBuffer.ReadString(buffer, ref offset);

    		    short _count_ = 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    	//鍐欏叆鏁版嵁
    	public override void Write(byte[] buffer, ref int offset)
    	{
            try
            {
                base.Write(buffer, ref offset);
					XBuffer.WriteString(commandType,buffer, ref offset);
					XBuffer.WriteString(parameters,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}