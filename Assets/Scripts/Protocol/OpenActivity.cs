//Auto generated, do not edit it
//开服活动

using System;
using System.IO;
using System.Collections.Generic;

namespace Message.OpenActivity
{
    public enum TypeEnum
    {
        RankItem = 1,
    }

    //
    public class RankItem : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public string name; // 名字
        
		public int level; // 等级
        
		public int fightPower; // 战力
        

        //鏋勯�犲嚱鏁�
        public RankItem() : base()
        {
			
			name = "";
			level = 0;
            
			fightPower = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			name = "";
			level = 0;
            
			fightPower = 0;
            

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
                level = XBuffer.ReadInt(buffer, ref offset);
                fightPower = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteInt(fightPower, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //战力排行活动剩余时间
    public class ReqFightPowerRankOverTime : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 133201;

    	//鏋勯�犲嚱鏁�
    	public ReqFightPowerRankOverTime()
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
    //战力排行
    public class ReqFightPowerRank : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 133202;

    	//鏋勯�犲嚱鏁�
    	public ReqFightPowerRank()
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
    //嘉年华
    public class ReqCarnivalInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 133203;

    	//鏋勯�犲嚱鏁�
    	public ReqCarnivalInfo()
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
    //战力排行活动剩余时间
    public class ResFightPowerRankOverTime : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 133101;
		public long overTime; // 活动剩余时间（小于等于0就是结束了）

    	//鏋勯�犲嚱鏁�
    	public ResFightPowerRankOverTime()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			overTime = 0L;
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
                overTime = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(overTime,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //战力排行
    public class ResFightPowerRank : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 133102;
        public List<RankItem> rankItems{get;protected set;} //排名信息

    	//鏋勯�犲嚱鏁�
    	public ResFightPowerRank()
    	{
            rankItems = new List<RankItem>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            rankItems.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = rankItems.Count; a < b; ++a)
            {
				rankItems[a] = null;
                //var _value_ = rankItems[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            rankItems.Clear();
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
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    RankItem _value_ = null;
                    //_value_ = ClassCacheManager.New<RankItem>();
					_value_ = new RankItem();
                    _value_.Read(buffer, ref offset);
                    rankItems.Add(_value_);
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

                XBuffer.WriteShort((short)rankItems.Count, buffer, ref offset);
                for(int a = 0; a < rankItems.Count; ++a)
                {
					if(rankItems[a] == null)
						UnityEngine.Debug.LogError("rankItems has nil item, idx == " + a);
					else
						rankItems[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //嘉年华
    public class ResCarnivalInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 133103;
        public List<RankItem> rankItems{get;protected set;} //排名信息

    	//鏋勯�犲嚱鏁�
    	public ResCarnivalInfo()
    	{
            rankItems = new List<RankItem>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            rankItems.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = rankItems.Count; a < b; ++a)
            {
				rankItems[a] = null;
                //var _value_ = rankItems[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            rankItems.Clear();
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
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    RankItem _value_ = null;
                    //_value_ = ClassCacheManager.New<RankItem>();
					_value_ = new RankItem();
                    _value_.Read(buffer, ref offset);
                    rankItems.Add(_value_);
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

                XBuffer.WriteShort((short)rankItems.Count, buffer, ref offset);
                for(int a = 0; a < rankItems.Count; ++a)
                {
					if(rankItems[a] == null)
						UnityEngine.Debug.LogError("rankItems has nil item, idx == " + a);
					else
						rankItems[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}