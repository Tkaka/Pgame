//Auto generated, do not edit it
//副本消息

using System;
using System.IO;
using System.Collections.Generic;

namespace Message.GuildBattle
{
    public enum TypeEnum
    {
        GuildInfo = 1,
        MemberApplyInfo = 2,
        FighterInfo = 3,
        ClashInfo = 4,
        RoundInfo = 5,
        RoleRankItem = 6,
        GuildRankItem = 7,
    }

    //公会信息
    public class GuildInfo : BaseMsgStruct
    {
		public string name; // 公会名
        
		public int aliveNum; // 存活人数
        
		public int maxNum; // 公会人数上限
        

        //鏋勯�犲嚱鏁�
        public GuildInfo() : base()
        {
			
			name = "";
			aliveNum = 0;
            
			maxNum = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			name = "";
			aliveNum = 0;
            
			maxNum = 0;
            

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
                aliveNum = XBuffer.ReadInt(buffer, ref offset);
                maxNum = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(aliveNum, buffer, ref offset);
                XBuffer.WriteInt(maxNum, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //报名信息
    public class MemberApplyInfo : BaseMsgStruct
    {
		public string name; // 昵称
        
		public int level; // 等级
        
		public int job; // 职位
        
		public bool apply; // 是否报名
        
		public int headIcon; // 头像ID
        

        //鏋勯�犲嚱鏁�
        public MemberApplyInfo() : base()
        {
			
			name = "";
			level = 0;
            
			job = 0;
            
			apply = false;
            apply = false;
			headIcon = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			name = "";
			level = 0;
            
			job = 0;
            
			apply = false;
            apply = false;
			headIcon = 0;
            

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
                job = XBuffer.ReadInt(buffer, ref offset);
                apply = XBuffer.ReadBool(buffer, ref offset);
                headIcon = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(2, buffer, ref offset);
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
                XBuffer.WriteInt(job, buffer, ref offset);
                XBuffer.WriteBool(apply, buffer, ref offset);
                XBuffer.WriteInt(headIcon, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //玩家信息
    public class FighterInfo : BaseMsgStruct
    {
		public string name; // 昵称
        
		public string guildName; // 公会名
        
		public int group; // 阵容组
        
		public int hp; // 剩余血量百分比
        
		public int headIcon; // 头像ID
        
		public bool win; // 胜利
        
		public int level; // 等级
        

        //鏋勯�犲嚱鏁�
        public FighterInfo() : base()
        {
			
			name = "";
			guildName = "";
			group = 0;
            
			hp = 0;
            
			headIcon = 0;
            
			win = false;
            win = false;
			level = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			name = "";
			guildName = "";
			group = 0;
            
			hp = 0;
            
			headIcon = 0;
            
			win = false;
            win = false;
			level = 0;
            

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
                guildName = XBuffer.ReadString(buffer, ref offset);
                group = XBuffer.ReadInt(buffer, ref offset);
                hp = XBuffer.ReadInt(buffer, ref offset);
                headIcon = XBuffer.ReadInt(buffer, ref offset);
                win = XBuffer.ReadBool(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(3, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteString(guildName, buffer, ref offset);
                XBuffer.WriteInt(group, buffer, ref offset);
                XBuffer.WriteInt(hp, buffer, ref offset);
                XBuffer.WriteInt(headIcon, buffer, ref offset);
                XBuffer.WriteBool(win, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //对阵信息
    public class ClashInfo : BaseMsgStruct
    {
		public FighterInfo member; // 公会成员
        
		public FighterInfo enemy; // 敌人
        
		public int serialNum; // 场次序号
        

        //鏋勯�犲嚱鏁�
        public ClashInfo() : base()
        {
			
			//member = ClassCacheManager.New<FighterInfo>();
			member = new FighterInfo();
			//enemy = ClassCacheManager.New<FighterInfo>();
			enemy = new FighterInfo();
			serialNum = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//member = ClassCacheManager.New<FighterInfo>();
			member = new FighterInfo();
			//enemy = ClassCacheManager.New<FighterInfo>();
			enemy = new FighterInfo();
			serialNum = 0;
            

        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			member = null;
			enemy = null;

        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //member = ClassCacheManager.New<FighterInfo>();
				member = new FighterInfo();
                member.Read(buffer, ref offset);
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //enemy = ClassCacheManager.New<FighterInfo>();
				enemy = new FighterInfo();
                enemy.Read(buffer, ref offset);
                serialNum = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(4, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                if(member==null)
                    //member = ClassCacheManager.New<FighterInfo>();
					member = new FighterInfo();
                member.WriteWithType(buffer, ref offset);
                if(enemy==null)
                    //enemy = ClassCacheManager.New<FighterInfo>();
					enemy = new FighterInfo();
                enemy.WriteWithType(buffer, ref offset);
                XBuffer.WriteInt(serialNum, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //轮次信息
    public class RoundInfo : BaseMsgStruct
    {
		public int roundNum; // 轮次号
        
        public List<ClashInfo> clashInfo{get; protected set;} //对阵信息

        //鏋勯�犲嚱鏁�
        public RoundInfo() : base()
        {
            clashInfo = new List<ClashInfo>(); //对阵信息
			
			roundNum = 0;
            

            clashInfo.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roundNum = 0;
            

            clashInfo.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = clashInfo.Count; a < b; ++a)
            {
                //var _value_ = clashInfo[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				clashInfo[a] = null;
            }
            clashInfo.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                roundNum = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    ClashInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<ClashInfo>();
					_value_ = new ClashInfo();
                    _value_.Read(buffer, ref offset);
                    clashInfo.Add(_value_);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(5, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(roundNum, buffer, ref offset);

                XBuffer.WriteShort((short)clashInfo.Count,buffer, ref offset);
                for (int a = 0; a < clashInfo.Count; ++a)
                {
					if(clashInfo[a] == null)
						UnityEngine.Debug.LogError("clashInfo has nil item, idx == " + a);
					else
						clashInfo[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //个人排名信息
    public class RoleRankItem : BaseMsgStruct
    {
		public string roleName; // 角色昵称
        
		public string guildName; // 公会名
        
		public int winNum; // 连胜数
        

        //鏋勯�犲嚱鏁�
        public RoleRankItem() : base()
        {
			
			roleName = "";
			guildName = "";
			winNum = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleName = "";
			guildName = "";
			winNum = 0;
            

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
                roleName = XBuffer.ReadString(buffer, ref offset);
                guildName = XBuffer.ReadString(buffer, ref offset);
                winNum = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(6, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteString(roleName, buffer, ref offset);
                XBuffer.WriteString(guildName, buffer, ref offset);
                XBuffer.WriteInt(winNum, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //公会排名信息
    public class GuildRankItem : BaseMsgStruct
    {
		public string guildName; // 公会名
        
		public int __score; // 连胜数 or 积分
		private byte _score = 0; // 连胜数 or 积分 tag
		
		public bool hasScore()
		{
			return this._score == 1;
		}
		
		public int score
		{
			set
			{
				_score = 1;
				__score = value;
			}
			
			get
			{
				return __score;
			}
		}
        

        //鏋勯�犲嚱鏁�
        public GuildRankItem() : base()
        {
			
			guildName = "";
			_score = 0;
			__score = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			guildName = "";
			_score = 0;
			__score = 0;
            

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
                guildName = XBuffer.ReadString(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					score = XBuffer.ReadInt(buffer, ref offset);
				}

    		    short _count_ = 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(7, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteString(guildName, buffer, ref offset);
				XBuffer.WriteByte(_score, buffer, ref offset);
				if (_score == 1)
				{
					XBuffer.WriteInt(score, buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //点击公会战按钮
    public class ReqGuildBattleInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 120201;

    	//鏋勯�犲嚱鏁�
    	public ReqGuildBattleInfo()
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
    //公会成员报名信息
    public class ReqApplyInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 120202;

    	//鏋勯�犲嚱鏁�
    	public ReqApplyInfo()
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
    //兑换
    public class ReqExchange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 120205;
		public int id; // 兑换id

    	//鏋勯�犲嚱鏁�
    	public ReqExchange()
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
    //获取排行榜信息
    public class ReqRankInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 120206;
		public int type; // 1~5表示初赛和复赛。0表示总排行

    	//鏋勯�犲嚱鏁�
    	public ReqRankInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			type = 0;
            
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
                type = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(type,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //昨日战况
    public class ReqYesterdayFightInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 120208;
		public int round; // 轮次号，最高轮次填-1

    	//鏋勯�犲嚱鏁�
    	public ReqYesterdayFightInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			round = 0;
            
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
                round = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(round,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //接收实时战况
    public class ReqOpenFightInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 120209;

    	//鏋勯�犲嚱鏁�
    	public ReqOpenFightInfo()
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
    //取消接收实时战况
    public class ReqCloseFightInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 120210;

    	//鏋勯�犲嚱鏁�
    	public ReqCloseFightInfo()
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
    //报名
    public class ReqApply : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 120211;
        public List<int> petIds{get;protected set;} //阵型（阵型为空时需要发一个计算好的默认阵容）

    	//鏋勯�犲嚱鏁�
    	public ReqApply()
    	{
            petIds = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            petIds.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            petIds.Clear();
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
            		petIds.Add(_value_);
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

                XBuffer.WriteShort((short)petIds.Count, buffer, ref offset);
                for(int a = 0; a < petIds.Count; ++a)
                {
        			XBuffer.WriteInt(petIds[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //公会战报名信息
    public class ResApplyInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 120101;
		public int totalScore; // 公会总积分
		public int totalRank; // 公会总排名
		public int applyNum; // 报名人数
		public int maxNum; // 公会人数上限
		public bool apply; // 是否报名
        public List<GuildInfo> guildInfos{get;protected set;} //各个公会存活信息

    	//鏋勯�犲嚱鏁�
    	public ResApplyInfo()
    	{
            guildInfos = new List<GuildInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			totalScore = 0;
            
			totalRank = 0;
            
			applyNum = 0;
            
			maxNum = 0;
            
			apply = false;
            apply = false;
            guildInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = guildInfos.Count; a < b; ++a)
            {
				guildInfos[a] = null;
                //var _value_ = guildInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            guildInfos.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                totalScore = XBuffer.ReadInt(buffer, ref offset);
                totalRank = XBuffer.ReadInt(buffer, ref offset);
                applyNum = XBuffer.ReadInt(buffer, ref offset);
                maxNum = XBuffer.ReadInt(buffer, ref offset);
        		apply = XBuffer.ReadBool(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    GuildInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<GuildInfo>();
					_value_ = new GuildInfo();
                    _value_.Read(buffer, ref offset);
                    guildInfos.Add(_value_);
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
					XBuffer.WriteInt(totalScore,buffer, ref offset);
					XBuffer.WriteInt(totalRank,buffer, ref offset);
					XBuffer.WriteInt(applyNum,buffer, ref offset);
					XBuffer.WriteInt(maxNum,buffer, ref offset);
					XBuffer.WriteBool(apply,buffer, ref offset);

                XBuffer.WriteShort((short)guildInfos.Count, buffer, ref offset);
                for(int a = 0; a < guildInfos.Count; ++a)
                {
					if(guildInfos[a] == null)
						UnityEngine.Debug.LogError("guildInfos has nil item, idx == " + a);
					else
						guildInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //公会战战况
    public class ResFightInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 120102;
		public RoundInfo roundInfo; // 当前轮次信息
		public string roleName; // 最大连胜者名字
		public int winNum; // 最大连胜数
		public int aliveNum; // 本公会存活人数
		public int maxNum; // 本公会人数上限
        public List<GuildInfo> guildInfos{get;protected set;} //各个公会存活信息

    	//鏋勯�犲嚱鏁�
    	public ResFightInfo()
    	{
            guildInfos = new List<GuildInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//roundInfo = ClassCacheManager.New<RoundInfo>();
			roundInfo = new RoundInfo();
			roleName = "";
			winNum = 0;
            
			aliveNum = 0;
            
			maxNum = 0;
            
            guildInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			roundInfo = null;

            for (int a = 0,b = guildInfos.Count; a < b; ++a)
            {
				guildInfos[a] = null;
                //var _value_ = guildInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            guildInfos.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //roundInfo = ClassCacheManager.New<RoundInfo>();
				roundInfo = new RoundInfo();
                roundInfo.Read(buffer, ref offset);
                roleName = XBuffer.ReadString(buffer, ref offset);
                winNum = XBuffer.ReadInt(buffer, ref offset);
                aliveNum = XBuffer.ReadInt(buffer, ref offset);
                maxNum = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    GuildInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<GuildInfo>();
					_value_ = new GuildInfo();
                    _value_.Read(buffer, ref offset);
                    guildInfos.Add(_value_);
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
					if(roundInfo == null)
						//roundInfo = ClassCacheManager.New<RoundInfo>();
						roundInfo = new RoundInfo();
					roundInfo.WriteWithType(buffer, ref offset);
					XBuffer.WriteString(roleName,buffer, ref offset);
					XBuffer.WriteInt(winNum,buffer, ref offset);
					XBuffer.WriteInt(aliveNum,buffer, ref offset);
					XBuffer.WriteInt(maxNum,buffer, ref offset);

                XBuffer.WriteShort((short)guildInfos.Count, buffer, ref offset);
                for(int a = 0; a < guildInfos.Count; ++a)
                {
					if(guildInfos[a] == null)
						UnityEngine.Debug.LogError("guildInfos has nil item, idx == " + a);
					else
						guildInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //公会战结果
    public class ResResult : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 120103;
		public int guildRank; // 本公会排名
		public int score; // 本公会积分
		public int personalRank; // 个人排名
		public int winNum; // 个人连胜
        public List<GuildRankItem> guildRankItems{get;protected set;} //公会排名前三
        public List<RoleRankItem> roleRankItems{get;protected set;} //个人排名前三

    	//鏋勯�犲嚱鏁�
    	public ResResult()
    	{
            guildRankItems = new List<GuildRankItem>();
            roleRankItems = new List<RoleRankItem>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			guildRank = 0;
            
			score = 0;
            
			personalRank = 0;
            
			winNum = 0;
            
            guildRankItems.Clear();
            roleRankItems.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = guildRankItems.Count; a < b; ++a)
            {
				guildRankItems[a] = null;
                //var _value_ = guildRankItems[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            guildRankItems.Clear();
            for (int a = 0,b = roleRankItems.Count; a < b; ++a)
            {
				roleRankItems[a] = null;
                //var _value_ = roleRankItems[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            roleRankItems.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                guildRank = XBuffer.ReadInt(buffer, ref offset);
                score = XBuffer.ReadInt(buffer, ref offset);
                personalRank = XBuffer.ReadInt(buffer, ref offset);
                winNum = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    GuildRankItem _value_ = null;
                    //_value_ = ClassCacheManager.New<GuildRankItem>();
					_value_ = new GuildRankItem();
                    _value_.Read(buffer, ref offset);
                    guildRankItems.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    RoleRankItem _value_ = null;
                    //_value_ = ClassCacheManager.New<RoleRankItem>();
					_value_ = new RoleRankItem();
                    _value_.Read(buffer, ref offset);
                    roleRankItems.Add(_value_);
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
					XBuffer.WriteInt(guildRank,buffer, ref offset);
					XBuffer.WriteInt(score,buffer, ref offset);
					XBuffer.WriteInt(personalRank,buffer, ref offset);
					XBuffer.WriteInt(winNum,buffer, ref offset);

                XBuffer.WriteShort((short)guildRankItems.Count, buffer, ref offset);
                for(int a = 0; a < guildRankItems.Count; ++a)
                {
					if(guildRankItems[a] == null)
						UnityEngine.Debug.LogError("guildRankItems has nil item, idx == " + a);
					else
						guildRankItems[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)roleRankItems.Count, buffer, ref offset);
                for(int a = 0; a < roleRankItems.Count; ++a)
                {
					if(roleRankItems[a] == null)
						UnityEngine.Debug.LogError("roleRankItems has nil item, idx == " + a);
					else
						roleRankItems[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //公会成员报名信息
    public class ResMemberApplyInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 120104;
        public List<MemberApplyInfo> memberApplyInfos{get;protected set;} //成员报名信息

    	//鏋勯�犲嚱鏁�
    	public ResMemberApplyInfo()
    	{
            memberApplyInfos = new List<MemberApplyInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            memberApplyInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = memberApplyInfos.Count; a < b; ++a)
            {
				memberApplyInfos[a] = null;
                //var _value_ = memberApplyInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            memberApplyInfos.Clear();
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
                    MemberApplyInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<MemberApplyInfo>();
					_value_ = new MemberApplyInfo();
                    _value_.Read(buffer, ref offset);
                    memberApplyInfos.Add(_value_);
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

                XBuffer.WriteShort((short)memberApplyInfos.Count, buffer, ref offset);
                for(int a = 0; a < memberApplyInfos.Count; ++a)
                {
					if(memberApplyInfos[a] == null)
						UnityEngine.Debug.LogError("memberApplyInfos has nil item, idx == " + a);
					else
						memberApplyInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //排行榜
    public class ResRankInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 120106;
        public List<RoleRankItem> roleRankItems{get;protected set;} //个人排名
        public List<GuildRankItem> guildRankItems{get;protected set;} //公会排名

    	//鏋勯�犲嚱鏁�
    	public ResRankInfo()
    	{
            roleRankItems = new List<RoleRankItem>();
            guildRankItems = new List<GuildRankItem>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            roleRankItems.Clear();
            guildRankItems.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = roleRankItems.Count; a < b; ++a)
            {
				roleRankItems[a] = null;
                //var _value_ = roleRankItems[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            roleRankItems.Clear();
            for (int a = 0,b = guildRankItems.Count; a < b; ++a)
            {
				guildRankItems[a] = null;
                //var _value_ = guildRankItems[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            guildRankItems.Clear();
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
                    RoleRankItem _value_ = null;
                    //_value_ = ClassCacheManager.New<RoleRankItem>();
					_value_ = new RoleRankItem();
                    _value_.Read(buffer, ref offset);
                    roleRankItems.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    GuildRankItem _value_ = null;
                    //_value_ = ClassCacheManager.New<GuildRankItem>();
					_value_ = new GuildRankItem();
                    _value_.Read(buffer, ref offset);
                    guildRankItems.Add(_value_);
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

                XBuffer.WriteShort((short)roleRankItems.Count, buffer, ref offset);
                for(int a = 0; a < roleRankItems.Count; ++a)
                {
					if(roleRankItems[a] == null)
						UnityEngine.Debug.LogError("roleRankItems has nil item, idx == " + a);
					else
						roleRankItems[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)guildRankItems.Count, buffer, ref offset);
                for(int a = 0; a < guildRankItems.Count; ++a)
                {
					if(guildRankItems[a] == null)
						UnityEngine.Debug.LogError("guildRankItems has nil item, idx == " + a);
					else
						guildRankItems[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //回合战况
    public class ResRoundFightInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 120107;
		public RoundInfo roundInfo; // 轮次信息

    	//鏋勯�犲嚱鏁�
    	public ResRoundFightInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//roundInfo = ClassCacheManager.New<RoundInfo>();
			roundInfo = new RoundInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			roundInfo = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //roundInfo = ClassCacheManager.New<RoundInfo>();
				roundInfo = new RoundInfo();
                roundInfo.Read(buffer, ref offset);

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
					if(roundInfo == null)
						//roundInfo = ClassCacheManager.New<RoundInfo>();
						roundInfo = new RoundInfo();
					roundInfo.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //报名成功
    public class ResApply : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 120109;

    	//鏋勯�犲嚱鏁�
    	public ResApply()
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
    //兑换
    public class ResExchange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 120110;
		public int id; // 兑换id
		public int critical; // 暴击倍数

    	//鏋勯�犲嚱鏁�
    	public ResExchange()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			critical = 0;
            
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
                critical = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(critical,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}