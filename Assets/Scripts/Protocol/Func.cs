//Auto generated, do not edit it
//功能开启

using System;
using System.IO;
using System.Collections.Generic;

namespace Message.Func
{
    public enum TypeEnum
    {
    }


    //初始化功能解锁列表
    public class ResFuncList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 121201;
        public List<int> funcList{get;protected set;} //已解锁功能列表

    	//鏋勯�犲嚱鏁�
    	public ResFuncList()
    	{
            funcList = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            funcList.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            funcList.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
            		var _value_ = XBuffer.ReadInt(buffer, ref offset);
            		funcList.Add(_value_);
                }
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

                XBuffer.WriteShort((short)funcList.Count, buffer, ref offset);
                for(int a = 0; a < funcList.Count; ++a)
                {
        			XBuffer.WriteInt(funcList[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //功能解锁
    public class ResNewFunc : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 121202;
		public int func; // 开启的功能

    	//鏋勯�犲嚱鏁�
    	public ResNewFunc()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			func = 0;
            
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
                func = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(func,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}