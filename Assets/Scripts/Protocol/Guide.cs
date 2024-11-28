//Auto generated, do not edit it
//新手引导

using System;
using System.IO;
using System.Collections.Generic;

namespace Message.Guide
{
    public enum TypeEnum
    {
    }


    //已完成引导
    public class ResGuideList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 126101;
		public bool enable; // 是否开启新手
        public List<int> finishList{get;protected set;} //已完成引导

    	//鏋勯�犲嚱鏁�
    	public ResGuideList()
    	{
            finishList = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			enable = false;
            enable = false;
            finishList.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            finishList.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
        		enable = XBuffer.ReadBool(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
            		var _value_ = XBuffer.ReadInt(buffer, ref offset);
            		finishList.Add(_value_);
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
					XBuffer.WriteBool(enable,buffer, ref offset);

                XBuffer.WriteShort((short)finishList.Count, buffer, ref offset);
                for(int a = 0; a < finishList.Count; ++a)
                {
        			XBuffer.WriteInt(finishList[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //记录完成引导
    public class ReqFinishGuide : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 126201;
		public int id; // 引导id

    	//鏋勯�犲嚱鏁�
    	public ReqFinishGuide()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
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
                id = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(id,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}