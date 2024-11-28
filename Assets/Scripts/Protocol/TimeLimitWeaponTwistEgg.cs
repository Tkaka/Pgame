//Auto generated, do not edit it
//限时武器扭蛋

using System;
using System.IO;
using System.Collections.Generic;
using Message.Bag;

namespace Message.TimeLimitWeaponTwistEgg
{
    public enum TypeEnum
    {
    }


    //获取限时武器扭蛋活动信息
    public class ReqTLWeaponTwistEggInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 132201;

    	//鏋勯�犲嚱鏁�
    	public ReqTLWeaponTwistEggInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
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

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //抽取
    public class ReqDraw : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 132202;
		public int num; // 抽取数量

    	//鏋勯�犲嚱鏁�
    	public ReqDraw()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			num = 0;
            
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
                num = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(num,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //限时武器扭蛋活动信息
    public class ResTLWeaponTwistEggInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 132101;
		public long closeTime; // 活动剩余时间

    	//鏋勯�犲嚱鏁�
    	public ResTLWeaponTwistEggInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			closeTime = 0L;
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
                closeTime = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(closeTime,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //限时武器扭蛋活动信息
    public class ResDraw : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 132102;
		public int num; // 抽取数量
        public List<ItemInfo> itemInfos{get;protected set;} //奖励道具

    	//鏋勯�犲嚱鏁�
    	public ResDraw()
    	{
            itemInfos = new List<ItemInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			num = 0;
            
            itemInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = itemInfos.Count; a < b; ++a)
            {
				itemInfos[a] = null;
                //var _value_ = itemInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            itemInfos.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                num = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    ItemInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<ItemInfo>();
					_value_ = new ItemInfo();
                    _value_.Read(buffer, ref offset);
                    itemInfos.Add(_value_);
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
					XBuffer.WriteInt(num,buffer, ref offset);

                XBuffer.WriteShort((short)itemInfos.Count, buffer, ref offset);
                for(int a = 0; a < itemInfos.Count; ++a)
                {
					if(itemInfos[a] == null)
						UnityEngine.Debug.LogError("itemInfos has nil item, idx == " + a);
					else
						itemInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}