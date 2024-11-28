//Auto generated, do not edit it
//背包消息

using System;
using System.IO;
using System.Collections.Generic;
using Message.Pet;

namespace Message.Arena
{
    public enum TypeEnum
    {
        PlayerInfo = 1,
        Reward = 2,
        RankInfo = 3,
        ArenaResult = 4,
    }

    //玩家信息
    public class PlayerInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int rank; // 排名
        
		public string name; // 名字
        
		public long fightPower; // 战斗力
        
		public int __worshipNum; // 膜拜次数
		private byte _worshipNum = 0; // 膜拜次数 tag
		
		public bool hasWorshipNum()
		{
			return this._worshipNum == 1;
		}
		
		public int worshipNum
		{
			set
			{
				_worshipNum = 1;
				__worshipNum = value;
			}
			
			get
			{
				return __worshipNum;
			}
		}
        
		public int modle; // 模型
        
		public long roleId; // 角色id
        
		public bool __canWorship; // 能否被膜拜
		private byte _canWorship = 0; // 能否被膜拜 tag
		
		public bool hasCanWorship()
		{
			return this._canWorship == 1;
		}
		
		public bool canWorship
		{
			set
			{
				_canWorship = 1;
				__canWorship = value;
			}
			
			get
			{
				return __canWorship;
			}
		}
        

        //鏋勯�犲嚱鏁�
        public PlayerInfo() : base()
        {
			
			rank = 0;
            
			name = "";
			fightPower = 0L;
			_worshipNum = 0;
			__worshipNum = 0;
            
			modle = 0;
            
			roleId = 0L;
			_canWorship = 0;
			__canWorship = false;
            canWorship = false;

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			rank = 0;
            
			name = "";
			fightPower = 0L;
			_worshipNum = 0;
			__worshipNum = 0;
            
			modle = 0;
            
			roleId = 0L;
			_canWorship = 0;
			__canWorship = false;
            canWorship = false;

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
                rank = XBuffer.ReadInt(buffer, ref offset);
                name = XBuffer.ReadString(buffer, ref offset);
                fightPower = XBuffer.ReadLong(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					worshipNum = XBuffer.ReadInt(buffer, ref offset);
				}
                modle = XBuffer.ReadInt(buffer, ref offset);
                roleId = XBuffer.ReadLong(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					canWorship = XBuffer.ReadBool(buffer, ref offset);
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
            XBuffer.WriteByte(1, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(rank, buffer, ref offset);
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteLong(fightPower, buffer, ref offset);
				XBuffer.WriteByte(_worshipNum, buffer, ref offset);
				if (_worshipNum == 1)
				{
					XBuffer.WriteInt(worshipNum, buffer, ref offset);
				}
                XBuffer.WriteInt(modle, buffer, ref offset);
                XBuffer.WriteLong(roleId, buffer, ref offset);
				XBuffer.WriteByte(_canWorship, buffer, ref offset);
				if (_canWorship == 1)
				{
					XBuffer.WriteBool(canWorship, buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //奖励结构
    public class Reward : BaseMsgStruct
    {
		public int id; // 奖励id
        
		public int state; // 领取状态 0未完成 1完成未领奖 2已领奖
        

        //鏋勯�犲嚱鏁�
        public Reward() : base()
        {
			
			id = 0;
            
			state = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			state = 0;
            

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
                id = XBuffer.ReadInt(buffer, ref offset);
                state = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(id, buffer, ref offset);
                XBuffer.WriteInt(state, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //排名奖励结构
    public class RankInfo : BaseMsgStruct
    {
		public int rank; // 排名
        
		public string roleName; // 玩家名字
        
		public string guildName; // 工会名字
        
		public int level; // 等级
        
		public long fightPower; // 战力
        
		public long roleId; // 角色ID
        
		public int __iconId; // 头像框ID
		private byte _iconId = 0; // 头像框ID tag
		
		public bool hasIconId()
		{
			return this._iconId == 1;
		}
		
		public int iconId
		{
			set
			{
				_iconId = 1;
				__iconId = value;
			}
			
			get
			{
				return __iconId;
			}
		}
        

        //鏋勯�犲嚱鏁�
        public RankInfo() : base()
        {
			
			rank = 0;
            
			roleName = "";
			guildName = "";
			level = 0;
            
			fightPower = 0L;
			roleId = 0L;
			_iconId = 0;
			__iconId = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			rank = 0;
            
			roleName = "";
			guildName = "";
			level = 0;
            
			fightPower = 0L;
			roleId = 0L;
			_iconId = 0;
			__iconId = 0;
            

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
                rank = XBuffer.ReadInt(buffer, ref offset);
                roleName = XBuffer.ReadString(buffer, ref offset);
                guildName = XBuffer.ReadString(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                fightPower = XBuffer.ReadLong(buffer, ref offset);
                roleId = XBuffer.ReadLong(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					iconId = XBuffer.ReadInt(buffer, ref offset);
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
            XBuffer.WriteByte(3, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(rank, buffer, ref offset);
                XBuffer.WriteString(roleName, buffer, ref offset);
                XBuffer.WriteString(guildName, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteLong(fightPower, buffer, ref offset);
                XBuffer.WriteLong(roleId, buffer, ref offset);
				XBuffer.WriteByte(_iconId, buffer, ref offset);
				if (_iconId == 1)
				{
					XBuffer.WriteInt(iconId, buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //连续挑战结果
    public class ArenaResult : BaseMsgStruct
    {
		public int round; // 回合
        
		public int state; // 1成功 2失败
        
		public int core; // 积分
        

        //鏋勯�犲嚱鏁�
        public ArenaResult() : base()
        {
			
			round = 0;
            
			state = 0;
            
			core = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			round = 0;
            
			state = 0;
            
			core = 0;
            

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
                round = XBuffer.ReadInt(buffer, ref offset);
                state = XBuffer.ReadInt(buffer, ref offset);
                core = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(round, buffer, ref offset);
                XBuffer.WriteInt(state, buffer, ref offset);
                XBuffer.WriteInt(core, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //主界面
    public class ResArenaInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109101;
		public int arenaNum; // 挑战次数
		public int changeNum; // 换一换次数
		public int BuyNum; // 购买次数
		public long __coolTime; // 冷却时间 到期时间点
		private byte _coolTime = 0; // 冷却时间 到期时间点 tag
		
		public bool hasCoolTime()
		{
			return this._coolTime == 1;
		}
		
		public long coolTime
		{
			set
			{
				_coolTime = 1;
				__coolTime = value;
			}
			
			get
			{
				return __coolTime;
			}
		}
        public List<PlayerInfo> info{get;protected set;} //玩家信息

    	//鏋勯�犲嚱鏁�
    	public ResArenaInfo()
    	{
            info = new List<PlayerInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			arenaNum = 0;
            
			changeNum = 0;
            
			BuyNum = 0;
            
			_coolTime = 0;
			__coolTime = 0L;
            info.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = info.Count; a < b; ++a)
            {
				info[a] = null;
                //var _value_ = info[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            info.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                arenaNum = XBuffer.ReadInt(buffer, ref offset);
                changeNum = XBuffer.ReadInt(buffer, ref offset);
                BuyNum = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					coolTime = XBuffer.ReadLong(buffer, ref offset);
				}

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    PlayerInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<PlayerInfo>();
					_value_ = new PlayerInfo();
                    _value_.Read(buffer, ref offset);
                    info.Add(_value_);
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
					XBuffer.WriteInt(arenaNum,buffer, ref offset);
					XBuffer.WriteInt(changeNum,buffer, ref offset);
					XBuffer.WriteInt(BuyNum,buffer, ref offset);
				XBuffer.WriteByte(_coolTime,buffer, ref offset);
				if (_coolTime == 1)
				{
					XBuffer.WriteLong(coolTime,buffer, ref offset);
				}

                XBuffer.WriteShort((short)info.Count, buffer, ref offset);
                for(int a = 0; a < info.Count; ++a)
                {
					if(info[a] == null)
						UnityEngine.Debug.LogError("info has nil item, idx == " + a);
					else
						info[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //清除冷却
    public class ResClearCoolTime : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109102;

    	//鏋勯�犲嚱鏁�
    	public ResClearCoolTime()
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
    //排名奖励
    public class ResRankRewardInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109103;
		public int type; // 奖励类型 1排名 2积分
		public int __highestRank; // 最高排名
		private byte _highestRank = 0; // 最高排名 tag
		
		public bool hasHighestRank()
		{
			return this._highestRank == 1;
		}
		
		public int highestRank
		{
			set
			{
				_highestRank = 1;
				__highestRank = value;
			}
			
			get
			{
				return __highestRank;
			}
		}
		public int __score; // 当前积分
		private byte _score = 0; // 当前积分 tag
		
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
        public List<Reward> reward{get;protected set;} //奖励信息

    	//鏋勯�犲嚱鏁�
    	public ResRankRewardInfo()
    	{
            reward = new List<Reward>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			type = 0;
            
			_highestRank = 0;
			__highestRank = 0;
            
			_score = 0;
			__score = 0;
            
            reward.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = reward.Count; a < b; ++a)
            {
				reward[a] = null;
                //var _value_ = reward[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            reward.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                type = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					highestRank = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					score = XBuffer.ReadInt(buffer, ref offset);
				}

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    Reward _value_ = null;
                    //_value_ = ClassCacheManager.New<Reward>();
					_value_ = new Reward();
                    _value_.Read(buffer, ref offset);
                    reward.Add(_value_);
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
					XBuffer.WriteInt(type,buffer, ref offset);
				XBuffer.WriteByte(_highestRank,buffer, ref offset);
				if (_highestRank == 1)
				{
					XBuffer.WriteInt(highestRank,buffer, ref offset);
				}
				XBuffer.WriteByte(_score,buffer, ref offset);
				if (_score == 1)
				{
					XBuffer.WriteInt(score,buffer, ref offset);
				}

                XBuffer.WriteShort((short)reward.Count, buffer, ref offset);
                for(int a = 0; a < reward.Count; ++a)
                {
					if(reward[a] == null)
						UnityEngine.Debug.LogError("reward has nil item, idx == " + a);
					else
						reward[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //奖励领取返回
    public class ResRankReward : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109104;
		public int type; // 奖励类型 1排名 2积分
        public List<Reward> rewards{get;protected set;} //奖励信息

    	//鏋勯�犲嚱鏁�
    	public ResRankReward()
    	{
            rewards = new List<Reward>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			type = 0;
            
            rewards.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = rewards.Count; a < b; ++a)
            {
				rewards[a] = null;
                //var _value_ = rewards[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            rewards.Clear();
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
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    Reward _value_ = null;
                    //_value_ = ClassCacheManager.New<Reward>();
					_value_ = new Reward();
                    _value_.Read(buffer, ref offset);
                    rewards.Add(_value_);
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
					XBuffer.WriteInt(type,buffer, ref offset);

                XBuffer.WriteShort((short)rewards.Count, buffer, ref offset);
                for(int a = 0; a < rewards.Count; ++a)
                {
					if(rewards[a] == null)
						UnityEngine.Debug.LogError("rewards has nil item, idx == " + a);
					else
						rewards[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //挑战
    public class ResArena : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109105;
		public int arenaNum; // 挑战剩余次数
		public long coolTime; // 冷却时间 到期时间点
		public int core; // 积分
		public int rank; // 排名

    	//鏋勯�犲嚱鏁�
    	public ResArena()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			arenaNum = 0;
            
			coolTime = 0L;
			core = 0;
            
			rank = 0;
            
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
                arenaNum = XBuffer.ReadInt(buffer, ref offset);
                coolTime = XBuffer.ReadLong(buffer, ref offset);
                core = XBuffer.ReadInt(buffer, ref offset);
                rank = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(arenaNum,buffer, ref offset);
					XBuffer.WriteLong(coolTime,buffer, ref offset);
					XBuffer.WriteInt(core,buffer, ref offset);
					XBuffer.WriteInt(rank,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //连续挑战结果
    public class ResContinueArena : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109106;
		public string name; // 对手名字
		public long fightPower; // 战斗力
		public int model; // 对手头像
		public long roleId; // 对手RoleID
		public int rank; // 对手排名
		public int arenaNum; // 挑战次数
        public List<ArenaResult> result{get;protected set;} //积分

    	//鏋勯�犲嚱鏁�
    	public ResContinueArena()
    	{
            result = new List<ArenaResult>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			name = "";
			fightPower = 0L;
			model = 0;
            
			roleId = 0L;
			rank = 0;
            
			arenaNum = 0;
            
            result.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = result.Count; a < b; ++a)
            {
				result[a] = null;
                //var _value_ = result[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            result.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                name = XBuffer.ReadString(buffer, ref offset);
                fightPower = XBuffer.ReadLong(buffer, ref offset);
                model = XBuffer.ReadInt(buffer, ref offset);
                roleId = XBuffer.ReadLong(buffer, ref offset);
                rank = XBuffer.ReadInt(buffer, ref offset);
                arenaNum = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    ArenaResult _value_ = null;
                    //_value_ = ClassCacheManager.New<ArenaResult>();
					_value_ = new ArenaResult();
                    _value_.Read(buffer, ref offset);
                    result.Add(_value_);
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
					XBuffer.WriteString(name,buffer, ref offset);
					XBuffer.WriteLong(fightPower,buffer, ref offset);
					XBuffer.WriteInt(model,buffer, ref offset);
					XBuffer.WriteLong(roleId,buffer, ref offset);
					XBuffer.WriteInt(rank,buffer, ref offset);
					XBuffer.WriteInt(arenaNum,buffer, ref offset);

                XBuffer.WriteShort((short)result.Count, buffer, ref offset);
                for(int a = 0; a < result.Count; ++a)
                {
					if(result[a] == null)
						UnityEngine.Debug.LogError("result has nil item, idx == " + a);
					else
						result[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //玩家排名信息发生改变
    public class ResRankInfoChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109107;
        public List<PlayerInfo> info{get;protected set;} //玩家信息

    	//鏋勯�犲嚱鏁�
    	public ResRankInfoChange()
    	{
            info = new List<PlayerInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            info.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = info.Count; a < b; ++a)
            {
				info[a] = null;
                //var _value_ = info[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            info.Clear();
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
                    PlayerInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<PlayerInfo>();
					_value_ = new PlayerInfo();
                    _value_.Read(buffer, ref offset);
                    info.Add(_value_);
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

                XBuffer.WriteShort((short)info.Count, buffer, ref offset);
                for(int a = 0; a < info.Count; ++a)
                {
					if(info[a] == null)
						UnityEngine.Debug.LogError("info has nil item, idx == " + a);
					else
						info[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //购买挑战次数结果
    public class ResBuyResult : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109108;
		public int count; // 已经购买过的次数

    	//鏋勯�犲嚱鏁�
    	public ResBuyResult()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			count = 0;
            
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
                count = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(count,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //换一换次数
    public class ResChangeNum : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109109;
		public int count; // 换的次数

    	//鏋勯�犲嚱鏁�
    	public ResChangeNum()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			count = 0;
            
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
                count = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(count,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //排行榜信息
    public class ResRank : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109110;
        public List<RankInfo> rank{get;protected set;} //排行榜

    	//鏋勯�犲嚱鏁�
    	public ResRank()
    	{
            rank = new List<RankInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            rank.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = rank.Count; a < b; ++a)
            {
				rank[a] = null;
                //var _value_ = rank[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            rank.Clear();
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
                    RankInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<RankInfo>();
					_value_ = new RankInfo();
                    _value_.Read(buffer, ref offset);
                    rank.Add(_value_);
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

                XBuffer.WriteShort((short)rank.Count, buffer, ref offset);
                for(int a = 0; a < rank.Count; ++a)
                {
					if(rank[a] == null)
						UnityEngine.Debug.LogError("rank has nil item, idx == " + a);
					else
						rank[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //购买次数
    public class ResBuyNum : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109111;
		public int num; // 剩余次数

    	//鏋勯�犲嚱鏁�
    	public ResBuyNum()
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
    //最高排名改变
    public class ResChangeHighestRank : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109112;
		public int highestRank; // 最高排名

    	//鏋勯�犲嚱鏁�
    	public ResChangeHighestRank()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			highestRank = 0;
            
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
                highestRank = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(highestRank,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //查看他人信息返回
    public class ResSeeOther : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109113;
		public string playerName; // 玩家名字
		public int level; // 玩家等级
		public int rank; // 玩家排名
		public int victoryCount; // 玩家胜利场数
		public string xuanYan; // 宣言
		public string guildName; // 社团
		public int xianShou; // 先手值
		public long fightPower; // 战斗力
		public int iconId; // 头像框ID
        public List<EquipedPetInfo> pets{get;protected set;} //上阵宠物信息

    	//鏋勯�犲嚱鏁�
    	public ResSeeOther()
    	{
            pets = new List<EquipedPetInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			playerName = "";
			level = 0;
            
			rank = 0;
            
			victoryCount = 0;
            
			xuanYan = "";
			guildName = "";
			xianShou = 0;
            
			fightPower = 0L;
			iconId = 0;
            
            pets.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = pets.Count; a < b; ++a)
            {
				pets[a] = null;
                //var _value_ = pets[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            pets.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                playerName = XBuffer.ReadString(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                rank = XBuffer.ReadInt(buffer, ref offset);
                victoryCount = XBuffer.ReadInt(buffer, ref offset);
                xuanYan = XBuffer.ReadString(buffer, ref offset);
                guildName = XBuffer.ReadString(buffer, ref offset);
                xianShou = XBuffer.ReadInt(buffer, ref offset);
                fightPower = XBuffer.ReadLong(buffer, ref offset);
                iconId = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    EquipedPetInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<EquipedPetInfo>();
					_value_ = new EquipedPetInfo();
                    _value_.Read(buffer, ref offset);
                    pets.Add(_value_);
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
					XBuffer.WriteString(playerName,buffer, ref offset);
					XBuffer.WriteInt(level,buffer, ref offset);
					XBuffer.WriteInt(rank,buffer, ref offset);
					XBuffer.WriteInt(victoryCount,buffer, ref offset);
					XBuffer.WriteString(xuanYan,buffer, ref offset);
					XBuffer.WriteString(guildName,buffer, ref offset);
					XBuffer.WriteInt(xianShou,buffer, ref offset);
					XBuffer.WriteLong(fightPower,buffer, ref offset);
					XBuffer.WriteInt(iconId,buffer, ref offset);

                XBuffer.WriteShort((short)pets.Count, buffer, ref offset);
                for(int a = 0; a < pets.Count; ++a)
                {
					if(pets[a] == null)
						UnityEngine.Debug.LogError("pets has nil item, idx == " + a);
					else
						pets[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //挑战
    public class ReqArena : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109201;
		public long roleId; // 挑战的角色id
		public int times; // 次数
		public int rank; // 挑战的排名
		public int __consumeItem; // 是否消耗道具直接购买次数挑战1券 2钻石
		private byte _consumeItem = 0; // 是否消耗道具直接购买次数挑战1券 2钻石 tag
		
		public bool hasConsumeItem()
		{
			return this._consumeItem == 1;
		}
		
		public int consumeItem
		{
			set
			{
				_consumeItem = 1;
				__consumeItem = value;
			}
			
			get
			{
				return __consumeItem;
			}
		}

    	//鏋勯�犲嚱鏁�
    	public ReqArena()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			times = 0;
            
			rank = 0;
            
			_consumeItem = 0;
			__consumeItem = 0;
            
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
                roleId = XBuffer.ReadLong(buffer, ref offset);
                times = XBuffer.ReadInt(buffer, ref offset);
                rank = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					consumeItem = XBuffer.ReadInt(buffer, ref offset);
				}

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
					XBuffer.WriteLong(roleId,buffer, ref offset);
					XBuffer.WriteInt(times,buffer, ref offset);
					XBuffer.WriteInt(rank,buffer, ref offset);
				XBuffer.WriteByte(_consumeItem,buffer, ref offset);
				if (_consumeItem == 1)
				{
					XBuffer.WriteInt(consumeItem,buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //清除冷却
    public class ReqClearCoolTime : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109202;

    	//鏋勯�犲嚱鏁�
    	public ReqClearCoolTime()
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
    //换一批
    public class ReqChangeTarget : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109203;

    	//鏋勯�犲嚱鏁�
    	public ReqChangeTarget()
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
    //排名信息
    public class ReqRank : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109204;

    	//鏋勯�犲嚱鏁�
    	public ReqRank()
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
    //排名奖励
    public class ReqRankReward : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109205;
		public int id; // id

    	//鏋勯�犲嚱鏁�
    	public ReqRankReward()
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
    //积分奖励
    public class ReqCoreReward : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109206;
		public int __id; // id
		private byte _id = 0; // id tag
		
		public bool hasId()
		{
			return this._id == 1;
		}
		
		public int id
		{
			set
			{
				_id = 1;
				__id = value;
			}
			
			get
			{
				return __id;
			}
		}
		public bool oneKey; // 是否一键

    	//鏋勯�犲嚱鏁�
    	public ReqCoreReward()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			_id = 0;
			__id = 0;
            
			oneKey = false;
            oneKey = false;
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
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					id = XBuffer.ReadInt(buffer, ref offset);
				}
        		oneKey = XBuffer.ReadBool(buffer, ref offset);

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
				XBuffer.WriteByte(_id,buffer, ref offset);
				if (_id == 1)
				{
					XBuffer.WriteInt(id,buffer, ref offset);
				}
					XBuffer.WriteBool(oneKey,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //膜拜
    public class ReqWorship : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109207;
		public long __id; // id
		private byte _id = 0; // id tag
		
		public bool hasId()
		{
			return this._id == 1;
		}
		
		public long id
		{
			set
			{
				_id = 1;
				__id = value;
			}
			
			get
			{
				return __id;
			}
		}
		public bool oneKey; // 是否一键

    	//鏋勯�犲嚱鏁�
    	public ReqWorship()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			_id = 0;
			__id = 0L;
			oneKey = false;
            oneKey = false;
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
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					id = XBuffer.ReadLong(buffer, ref offset);
				}
        		oneKey = XBuffer.ReadBool(buffer, ref offset);

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
				XBuffer.WriteByte(_id,buffer, ref offset);
				if (_id == 1)
				{
					XBuffer.WriteLong(id,buffer, ref offset);
				}
					XBuffer.WriteBool(oneKey,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //购买挑战次数
    public class ReqBuyNum : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109208;
		public int comsumeItem; // 消耗道具 1券 2钻石

    	//鏋勯�犲嚱鏁�
    	public ReqBuyNum()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			comsumeItem = 0;
            
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
                comsumeItem = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(comsumeItem,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //请求查看他人
    public class ReqSeeOther : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109209;
		public long roleId; // 对方的角色ID

    	//鏋勯�犲嚱鏁�
    	public ReqSeeOther()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
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
                roleId = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(roleId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //请求竞技场信息
    public class ReqAreanInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 109210;

    	//鏋勯�犲嚱鏁�
    	public ReqAreanInfo()
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
}