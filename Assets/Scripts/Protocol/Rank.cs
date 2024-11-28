//Auto generated, do not edit it
//排行榜

using System;
using System.IO;
using System.Collections.Generic;
using Message.Pet;

namespace Message.Rank
{
    public enum TypeEnum
    {
        RankData = 1,
        Petdata = 2,
        GuildRankData = 3,
    }

    //排行数据
    public class RankData : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public long roleId; // roleId
        
		public string name; // 名字
        
		public int title; // 称号
        
		public int rank; // 排行
        
		public string __guildName; // 社团名
		private byte _guildName = 0; // 社团名 tag
		
		public bool hasGuildName()
		{
			return this._guildName == 1;
		}
		
		public string guildName
		{
			set
			{
				_guildName = 1;
				__guildName = value;
			}
			
			get
			{
				return __guildName;
			}
		}
        
		public int __roleLevel; // 等级
		private byte _roleLevel = 0; // 等级 tag
		
		public bool hasRoleLevel()
		{
			return this._roleLevel == 1;
		}
		
		public int roleLevel
		{
			set
			{
				_roleLevel = 1;
				__roleLevel = value;
			}
			
			get
			{
				return __roleLevel;
			}
		}
        
		public int left; // 左参数
        
		public long right; // 右参数
        
		public int icon; // 头像id
        
		public int __color; // 品阶
		private byte _color = 0; // 品阶 tag
		
		public bool hasColor()
		{
			return this._color == 1;
		}
		
		public int color
		{
			set
			{
				_color = 1;
				__color = value;
			}
			
			get
			{
				return __color;
			}
		}
        
		public int __star; // 星级
		private byte _star = 0; // 星级 tag
		
		public bool hasStar()
		{
			return this._star == 1;
		}
		
		public int star
		{
			set
			{
				_star = 1;
				__star = value;
			}
			
			get
			{
				return __star;
			}
		}
        
		public int __level; // 等级
		private byte _level = 0; // 等级 tag
		
		public bool hasLevel()
		{
			return this._level == 1;
		}
		
		public int level
		{
			set
			{
				_level = 1;
				__level = value;
			}
			
			get
			{
				return __level;
			}
		}
        

        //鏋勯�犲嚱鏁�
        public RankData() : base()
        {
			
			roleId = 0L;
			name = "";
			title = 0;
            
			rank = 0;
            
			_guildName = 0;
			__guildName = "";
			_roleLevel = 0;
			__roleLevel = 0;
            
			left = 0;
            
			right = 0L;
			icon = 0;
            
			_color = 0;
			__color = 0;
            
			_star = 0;
			__star = 0;
            
			_level = 0;
			__level = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			name = "";
			title = 0;
            
			rank = 0;
            
			_guildName = 0;
			__guildName = "";
			_roleLevel = 0;
			__roleLevel = 0;
            
			left = 0;
            
			right = 0L;
			icon = 0;
            
			_color = 0;
			__color = 0;
            
			_star = 0;
			__star = 0;
            
			_level = 0;
			__level = 0;
            

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
                roleId = XBuffer.ReadLong(buffer, ref offset);
                name = XBuffer.ReadString(buffer, ref offset);
                title = XBuffer.ReadInt(buffer, ref offset);
                rank = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					guildName = XBuffer.ReadString(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					roleLevel = XBuffer.ReadInt(buffer, ref offset);
				}
                left = XBuffer.ReadInt(buffer, ref offset);
                right = XBuffer.ReadLong(buffer, ref offset);
                icon = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					color = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					star = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					level = XBuffer.ReadInt(buffer, ref offset);
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
                XBuffer.WriteLong(roleId, buffer, ref offset);
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteInt(title, buffer, ref offset);
                XBuffer.WriteInt(rank, buffer, ref offset);
				XBuffer.WriteByte(_guildName, buffer, ref offset);
				if (_guildName == 1)
				{
					XBuffer.WriteString(guildName, buffer, ref offset);
				}
				XBuffer.WriteByte(_roleLevel, buffer, ref offset);
				if (_roleLevel == 1)
				{
					XBuffer.WriteInt(roleLevel, buffer, ref offset);
				}
                XBuffer.WriteInt(left, buffer, ref offset);
                XBuffer.WriteLong(right, buffer, ref offset);
                XBuffer.WriteInt(icon, buffer, ref offset);
				XBuffer.WriteByte(_color, buffer, ref offset);
				if (_color == 1)
				{
					XBuffer.WriteInt(color, buffer, ref offset);
				}
				XBuffer.WriteByte(_star, buffer, ref offset);
				if (_star == 1)
				{
					XBuffer.WriteInt(star, buffer, ref offset);
				}
				XBuffer.WriteByte(_level, buffer, ref offset);
				if (_level == 1)
				{
					XBuffer.WriteInt(level, buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //单独的宠物数据
    public class Petdata : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int petId; // 宠物id
        
		public PetBaseInfo baseinfo; // 基础数据
        
		public PetSkillInfo skillinfo; // 技能信息
        
		public PetSoulInfo title; // 战魂信息
        
		public int fightPower; // 战斗力
        
        public List<Property> rank{get; protected set;} //属性值列表

        //鏋勯�犲嚱鏁�
        public Petdata() : base()
        {
            rank = new List<Property>(); //属性值列表
			
			petId = 0;
            
			//baseinfo = ClassCacheManager.New<PetBaseInfo>();
			baseinfo = new PetBaseInfo();
			//skillinfo = ClassCacheManager.New<PetSkillInfo>();
			skillinfo = new PetSkillInfo();
			//title = ClassCacheManager.New<PetSoulInfo>();
			title = new PetSoulInfo();
			fightPower = 0;
            

            rank.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			//baseinfo = ClassCacheManager.New<PetBaseInfo>();
			baseinfo = new PetBaseInfo();
			//skillinfo = ClassCacheManager.New<PetSkillInfo>();
			skillinfo = new PetSkillInfo();
			//title = ClassCacheManager.New<PetSoulInfo>();
			title = new PetSoulInfo();
			fightPower = 0;
            

            rank.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			baseinfo = null;
			skillinfo = null;
			title = null;

            for (int a = 0,b = rank.Count; a < b; ++a)
            {
                //var _value_ = rank[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				rank[a] = null;
            }
            rank.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                petId = XBuffer.ReadInt(buffer, ref offset);
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //baseinfo = ClassCacheManager.New<PetBaseInfo>();
				baseinfo = new PetBaseInfo();
                baseinfo.Read(buffer, ref offset);
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //skillinfo = ClassCacheManager.New<PetSkillInfo>();
				skillinfo = new PetSkillInfo();
                skillinfo.Read(buffer, ref offset);
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //title = ClassCacheManager.New<PetSoulInfo>();
				title = new PetSoulInfo();
                title.Read(buffer, ref offset);
                fightPower = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    Property _value_ = null;
                    //_value_ = ClassCacheManager.New<Property>();
					_value_ = new Property();
                    _value_.Read(buffer, ref offset);
                    rank.Add(_value_);
                }
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
                XBuffer.WriteInt(petId, buffer, ref offset);
                if(baseinfo==null)
                    //baseinfo = ClassCacheManager.New<PetBaseInfo>();
					baseinfo = new PetBaseInfo();
                baseinfo.WriteWithType(buffer, ref offset);
                if(skillinfo==null)
                    //skillinfo = ClassCacheManager.New<PetSkillInfo>();
					skillinfo = new PetSkillInfo();
                skillinfo.WriteWithType(buffer, ref offset);
                if(title==null)
                    //title = ClassCacheManager.New<PetSoulInfo>();
					title = new PetSoulInfo();
                title.WriteWithType(buffer, ref offset);
                XBuffer.WriteInt(fightPower, buffer, ref offset);

                XBuffer.WriteShort((short)rank.Count,buffer, ref offset);
                for (int a = 0; a < rank.Count; ++a)
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
    //排行数据
    public class GuildRankData : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int rank; // 排行
        
		public string name; // 名字
        
		public int level; // 等级
        
		public int badge; // 徽章
        
		public int guildType; // 类型
        
		public string chairManName; // 社长名
        
		public long fightPower; // 战斗力
        
		public int memberNum; // 成员数
        
		public string notice; // 公告
        

        //鏋勯�犲嚱鏁�
        public GuildRankData() : base()
        {
			
			rank = 0;
            
			name = "";
			level = 0;
            
			badge = 0;
            
			guildType = 0;
            
			chairManName = "";
			fightPower = 0L;
			memberNum = 0;
            
			notice = "";

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			rank = 0;
            
			name = "";
			level = 0;
            
			badge = 0;
            
			guildType = 0;
            
			chairManName = "";
			fightPower = 0L;
			memberNum = 0;
            
			notice = "";

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
                level = XBuffer.ReadInt(buffer, ref offset);
                badge = XBuffer.ReadInt(buffer, ref offset);
                guildType = XBuffer.ReadInt(buffer, ref offset);
                chairManName = XBuffer.ReadString(buffer, ref offset);
                fightPower = XBuffer.ReadLong(buffer, ref offset);
                memberNum = XBuffer.ReadInt(buffer, ref offset);
                notice = XBuffer.ReadString(buffer, ref offset);

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
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteInt(badge, buffer, ref offset);
                XBuffer.WriteInt(guildType, buffer, ref offset);
                XBuffer.WriteString(chairManName, buffer, ref offset);
                XBuffer.WriteLong(fightPower, buffer, ref offset);
                XBuffer.WriteInt(memberNum, buffer, ref offset);
                XBuffer.WriteString(notice, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //排行信息
    public class ReqRankData : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 125201;
		public int rankType; // 类型
		public int page; // 当前页

    	//鏋勯�犲嚱鏁�
    	public ReqRankData()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			rankType = 0;
            
			page = 0;
            
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
                rankType = XBuffer.ReadInt(buffer, ref offset);
                page = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(rankType,buffer, ref offset);
					XBuffer.WriteInt(page,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //请求宠物数据
    public class ReqPetData : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 125202;
		public int petid; // 宠物id
		public long roleid; // 角色信息

    	//鏋勯�犲嚱鏁�
    	public ReqPetData()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petid = 0;
            
			roleid = 0L;
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
                petid = XBuffer.ReadInt(buffer, ref offset);
                roleid = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteInt(petid,buffer, ref offset);
					XBuffer.WriteLong(roleid,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //排行信息
    public class ResRankData : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 125101;
		public RankData __myRanke; // 我的排行信息
		private byte _myRanke = 0; // 我的排行信息 tag
		
		public bool hasMyRanke()
		{
			return this._myRanke == 1;
		}
		
		public RankData myRanke
		{
			set
			{
				_myRanke = 1;
				__myRanke = value;
			}
			
			get
			{
				return __myRanke;
			}
		}
		public int rankType; // 类型
        public List<RankData> data{get;protected set;} //数据

    	//鏋勯�犲嚱鏁�
    	public ResRankData()
    	{
            data = new List<RankData>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			_myRanke = 0;
			//__myRanke = ClassCacheManager.New<RankData>();
			__myRanke = new RankData();
			rankType = 0;
            
            data.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			__myRanke = null;

            for (int a = 0,b = data.Count; a < b; ++a)
            {
				data[a] = null;
                //var _value_ = data[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            data.Clear();
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
					real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
					//myRanke = ClassCacheManager.New<RankData>();
					myRanke = new RankData();
					myRanke.Read(buffer, ref offset);
				}
                rankType = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    RankData _value_ = null;
                    //_value_ = ClassCacheManager.New<RankData>();
					_value_ = new RankData();
                    _value_.Read(buffer, ref offset);
                    data.Add(_value_);
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
				XBuffer.WriteByte(_myRanke,buffer, ref offset);
				if (_myRanke == 1)
				{
					if(myRanke == null)
						//myRanke = ClassCacheManager.New<RankData>();
						myRanke = new RankData();
					myRanke.WriteWithType(buffer, ref offset);
				}
					XBuffer.WriteInt(rankType,buffer, ref offset);

                XBuffer.WriteShort((short)data.Count, buffer, ref offset);
                for(int a = 0; a < data.Count; ++a)
                {
					if(data[a] == null)
						UnityEngine.Debug.LogError("data has nil item, idx == " + a);
					else
						data[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //社团排行信息
    public class ResGuildRankData : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 125102;
		public GuildRankData __myGuild; // 我的社团排行信息
		private byte _myGuild = 0; // 我的社团排行信息 tag
		
		public bool hasMyGuild()
		{
			return this._myGuild == 1;
		}
		
		public GuildRankData myGuild
		{
			set
			{
				_myGuild = 1;
				__myGuild = value;
			}
			
			get
			{
				return __myGuild;
			}
		}
        public List<GuildRankData> data{get;protected set;} //数据

    	//鏋勯�犲嚱鏁�
    	public ResGuildRankData()
    	{
            data = new List<GuildRankData>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			_myGuild = 0;
			//__myGuild = ClassCacheManager.New<GuildRankData>();
			__myGuild = new GuildRankData();
            data.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			__myGuild = null;

            for (int a = 0,b = data.Count; a < b; ++a)
            {
				data[a] = null;
                //var _value_ = data[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            data.Clear();
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
					real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
					//myGuild = ClassCacheManager.New<GuildRankData>();
					myGuild = new GuildRankData();
					myGuild.Read(buffer, ref offset);
				}

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    GuildRankData _value_ = null;
                    //_value_ = ClassCacheManager.New<GuildRankData>();
					_value_ = new GuildRankData();
                    _value_.Read(buffer, ref offset);
                    data.Add(_value_);
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
				XBuffer.WriteByte(_myGuild,buffer, ref offset);
				if (_myGuild == 1)
				{
					if(myGuild == null)
						//myGuild = ClassCacheManager.New<GuildRankData>();
						myGuild = new GuildRankData();
					myGuild.WriteWithType(buffer, ref offset);
				}

                XBuffer.WriteShort((short)data.Count, buffer, ref offset);
                for(int a = 0; a < data.Count; ++a)
                {
					if(data[a] == null)
						UnityEngine.Debug.LogError("data has nil item, idx == " + a);
					else
						data[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //宠物单独信息
    public class ResPetData : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 125103;
		public Petdata data; // 数据

    	//鏋勯�犲嚱鏁�
    	public ResPetData()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//data = ClassCacheManager.New<Petdata>();
			data = new Petdata();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			data = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //data = ClassCacheManager.New<Petdata>();
				data = new Petdata();
                data.Read(buffer, ref offset);

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
					if(data == null)
						//data = ClassCacheManager.New<Petdata>();
						data = new Petdata();
					data.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}