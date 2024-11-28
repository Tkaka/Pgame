//Auto generated, do not edit it
//限时装备宝箱

using System;
using System.IO;
using System.Collections.Generic;
using Message.Bag;

namespace Message.TimeLimitEquipBox
{
    public enum TypeEnum
    {
        RankInfo = 1,
    }

    //积分排名
    public class RankInfo : BaseMsgStruct
    {
		public string name; // 昵称
        
		public int score; // 积分
        

        //鏋勯�犲嚱鏁�
        public RankInfo() : base()
        {
			
			name = "";
			score = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			name = "";
			score = 0;
            

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
                TypeEnum _real_type_;
                name = XBuffer.ReadString(buffer, ref offset);
                score = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(1, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteInt(score, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //获取活动信息
    public class ReqTLEquipBoxInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 130201;

    	//鏋勯�犲嚱鏁�
    	public ReqTLEquipBoxInfo()
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
    //宝箱抽取
    public class ReqTLEquipBoxDraw : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 130202;
		public int num; // 1：单抽 10：十连抽
		public bool free; // 是否免费抽取

    	//鏋勯�犲嚱鏁�
    	public ReqTLEquipBoxDraw()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			num = 0;
            
			free = false;
            free = false;
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
        		free = XBuffer.ReadBool(buffer, ref offset);

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
					XBuffer.WriteBool(free,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //开累计积分箱子
    public class ReqTLEquipBoxOpen : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 130203;
		public int index; // 箱子下标

    	//鏋勯�犲嚱鏁�
    	public ReqTLEquipBoxOpen()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			index = 0;
            
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
                index = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(index,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //返回活动信息
    public class ResTLEquipBoxInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 130101;
		public int freeDrawNum; // 免费单抽剩余次数
		public int score; // 积分
		public int rank; // 排名
		public long closeTime; // 活动结束时间
        public List<RankInfo> rankInfos{get;protected set;} //积分排名
        public List<bool> boxFlag{get;protected set;} //箱子是否已开启

    	//鏋勯�犲嚱鏁�
    	public ResTLEquipBoxInfo()
    	{
            rankInfos = new List<RankInfo>();
            boxFlag = new List<bool>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			freeDrawNum = 0;
            
			score = 0;
            
			rank = 0;
            
			closeTime = 0L;
            rankInfos.Clear();
            boxFlag.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = rankInfos.Count; a < b; ++a)
            {
				rankInfos[a] = null;
                //var _value_ = rankInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            rankInfos.Clear();
            boxFlag.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                freeDrawNum = XBuffer.ReadInt(buffer, ref offset);
                score = XBuffer.ReadInt(buffer, ref offset);
                rank = XBuffer.ReadInt(buffer, ref offset);
                closeTime = XBuffer.ReadLong(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    RankInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<RankInfo>();
					_value_ = new RankInfo();
                    _value_.Read(buffer, ref offset);
                    rankInfos.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
            		var _value_ = XBuffer.ReadBool(buffer, ref offset);
                    boxFlag.Add(_value_);
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
					XBuffer.WriteInt(freeDrawNum,buffer, ref offset);
					XBuffer.WriteInt(score,buffer, ref offset);
					XBuffer.WriteInt(rank,buffer, ref offset);
					XBuffer.WriteLong(closeTime,buffer, ref offset);

                XBuffer.WriteShort((short)rankInfos.Count, buffer, ref offset);
                for(int a = 0; a < rankInfos.Count; ++a)
                {
					if(rankInfos[a] == null)
						UnityEngine.Debug.LogError("rankInfos has nil item, idx == " + a);
					else
						rankInfos[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)boxFlag.Count, buffer, ref offset);
                for(int a = 0; a < boxFlag.Count; ++a)
                {
                    XBuffer.WriteBool(boxFlag[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //限时装备宝箱抽取
    public class ResTLEquipBoxDraw : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 130102;
		public int num; // 1：单抽 10：十连抽
		public int score; // 积分
		public int rank; // 排名
        public List<ItemInfo> itemInfos{get;protected set;} //奖励道具

    	//鏋勯�犲嚱鏁�
    	public ResTLEquipBoxDraw()
    	{
            itemInfos = new List<ItemInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			num = 0;
            
			score = 0;
            
			rank = 0;
            
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
                score = XBuffer.ReadInt(buffer, ref offset);
                rank = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(score,buffer, ref offset);
					XBuffer.WriteInt(rank,buffer, ref offset);

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
    //开累计积分箱子
    public class ResTLEquipBoxOpen : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 130103;
		public int index; // 箱子下标
        public List<ItemInfo> itemInfos{get;protected set;} //奖励道具

    	//鏋勯�犲嚱鏁�
    	public ResTLEquipBoxOpen()
    	{
            itemInfos = new List<ItemInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			index = 0;
            
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
                index = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(index,buffer, ref offset);

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