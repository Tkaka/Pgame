//Auto generated, do not edit it
//Vip

using System;
using System.IO;
using System.Collections.Generic;

namespace Message.Vip
{
    public enum TypeEnum
    {
    }


    //初始vip信息
    public class ResVipInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 123101;
		public int vipLevel; // VIP等级
		public int vipExp; // VIP当前经验
        public List<int> giftBagStateInfo{get;protected set;} //VIP等级礼包购买状态 1 未购买 2已购买

    	//鏋勯�犲嚱鏁�
    	public ResVipInfo()
    	{
            giftBagStateInfo = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			vipLevel = 0;
            
			vipExp = 0;
            
            giftBagStateInfo.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            giftBagStateInfo.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                vipLevel = XBuffer.ReadInt(buffer, ref offset);
                vipExp = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
            		var _value_ = XBuffer.ReadInt(buffer, ref offset);
            		giftBagStateInfo.Add(_value_);
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
					XBuffer.WriteInt(vipLevel,buffer, ref offset);
					XBuffer.WriteInt(vipExp,buffer, ref offset);

                XBuffer.WriteShort((short)giftBagStateInfo.Count, buffer, ref offset);
                for(int a = 0; a < giftBagStateInfo.Count; ++a)
                {
        			XBuffer.WriteInt(giftBagStateInfo[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //礼包购买状态改变
    public class ResGiftBagStateChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 123102;
		public int vipLevel; // VIP等级

    	//鏋勯�犲嚱鏁�
    	public ResGiftBagStateChange()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			vipLevel = 0;
            
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
                vipLevel = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(vipLevel,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //VIP信息改变
    public class ResVipInfoChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 123103;
		public int vipLevel; // VIP等级
		public int vipExp; // vip经验

    	//鏋勯�犲嚱鏁�
    	public ResVipInfoChange()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			vipLevel = 0;
            
			vipExp = 0;
            
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
                vipLevel = XBuffer.ReadInt(buffer, ref offset);
                vipExp = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(vipLevel,buffer, ref offset);
					XBuffer.WriteInt(vipExp,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //购买vip礼包
    public class ReqBuyVipGiftBag : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 123201;
		public int vipLevel; // VIP等级

    	//鏋勯�犲嚱鏁�
    	public ReqBuyVipGiftBag()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			vipLevel = 0;
            
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
                vipLevel = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(vipLevel,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}