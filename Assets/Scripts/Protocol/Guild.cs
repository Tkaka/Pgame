//Auto generated, do not edit it
//工会

using System;
using System.IO;
using System.Collections.Generic;
using Message.Pet;

namespace Message.Guild
{
    public enum TypeEnum
    {
        HongbaoRole = 11,
        Hongbao = 12,
        HongbaoRankRole = 13,
        Member = 1,
        GuildInfo = 2,
        GuildListInfo = 3,
        ExpHomeRole = 4,
        Applyer = 5,
        DonateInfo = 6,
        QuickPet = 7,
        ExpRole = 8,
        PosPet = 9,
        WishRole = 10,
    }

    //成员
    public class Member : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public long roleId; // ID
        
		public string name; // 名字
        
		public int level; // 等级
        
		public int job; // 职位
        
		public long lastLogin; // 最近登陆
        
		public long fightPower; // 战斗力
        
		public long contribution; // 贡献
        
		public int dailyContribution; // 今日贡献
        
		public int title; // 称号
        
		public int model; // 头像
        
		public int profession; // 职业
        
        public List<EquipedPetInfo> pets{get; protected set;} //宠物列表

        //鏋勯�犲嚱鏁�
        public Member() : base()
        {
            pets = new List<EquipedPetInfo>(); //宠物列表
			
			roleId = 0L;
			name = "";
			level = 0;
            
			job = 0;
            
			lastLogin = 0L;
			fightPower = 0L;
			contribution = 0L;
			dailyContribution = 0;
            
			title = 0;
            
			model = 0;
            
			profession = 0;
            

            pets.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			name = "";
			level = 0;
            
			job = 0;
            
			lastLogin = 0L;
			fightPower = 0L;
			contribution = 0L;
			dailyContribution = 0;
            
			title = 0;
            
			model = 0;
            
			profession = 0;
            

            pets.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = pets.Count; a < b; ++a)
            {
                //var _value_ = pets[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				pets[a] = null;
            }
            pets.Clear();
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
                level = XBuffer.ReadInt(buffer, ref offset);
                job = XBuffer.ReadInt(buffer, ref offset);
                lastLogin = XBuffer.ReadLong(buffer, ref offset);
                fightPower = XBuffer.ReadLong(buffer, ref offset);
                contribution = XBuffer.ReadLong(buffer, ref offset);
                dailyContribution = XBuffer.ReadInt(buffer, ref offset);
                title = XBuffer.ReadInt(buffer, ref offset);
                model = XBuffer.ReadInt(buffer, ref offset);
                profession = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
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
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteInt(job, buffer, ref offset);
                XBuffer.WriteLong(lastLogin, buffer, ref offset);
                XBuffer.WriteLong(fightPower, buffer, ref offset);
                XBuffer.WriteLong(contribution, buffer, ref offset);
                XBuffer.WriteInt(dailyContribution, buffer, ref offset);
                XBuffer.WriteInt(title, buffer, ref offset);
                XBuffer.WriteInt(model, buffer, ref offset);
                XBuffer.WriteInt(profession, buffer, ref offset);

                XBuffer.WriteShort((short)pets.Count,buffer, ref offset);
                for (int a = 0; a < pets.Count; ++a)
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
    //工会信息
    public class GuildInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public long id; // ID
        
		public int level; // 等级
        
		public string name; // 名字
        
		public int guildType; // 社团类别
        
		public int badge; // 徽章
        
		public string chairmanName; // 社长名
        
		public int memberNum; // 人数
        
		public int rank; // 排名
        
		public int chairmanNum; // 社长数
        
		public int deputyLeaderNum; // 副社长数
        
		public int eliteNum; // 精英数
        
		public int roleJob; // 当前玩家的职位
        
		public string notice; // 公会公告
        
		public int __mailNum; // 已发邮件数
		private byte _mailNum = 0; // 已发邮件数 tag
		
		public bool hasMailNum()
		{
			return this._mailNum == 1;
		}
		
		public int mailNum
		{
			set
			{
				_mailNum = 1;
				__mailNum = value;
			}
			
			get
			{
				return __mailNum;
			}
		}
        
		public int __limitType; // 社团限制类型
		private byte _limitType = 0; // 社团限制类型 tag
		
		public bool hasLimitType()
		{
			return this._limitType == 1;
		}
		
		public int limitType
		{
			set
			{
				_limitType = 1;
				__limitType = value;
			}
			
			get
			{
				return __limitType;
			}
		}
        
		public int __levelLimt; // 社团等级限制
		private byte _levelLimt = 0; // 社团等级限制 tag
		
		public bool hasLevelLimt()
		{
			return this._levelLimt == 1;
		}
		
		public int levelLimt
		{
			set
			{
				_levelLimt = 1;
				__levelLimt = value;
			}
			
			get
			{
				return __levelLimt;
			}
		}
        
        public List<Member> members{get; protected set;} //成员

        //鏋勯�犲嚱鏁�
        public GuildInfo() : base()
        {
            members = new List<Member>(); //成员
			
			id = 0L;
			level = 0;
            
			name = "";
			guildType = 0;
            
			badge = 0;
            
			chairmanName = "";
			memberNum = 0;
            
			rank = 0;
            
			chairmanNum = 0;
            
			deputyLeaderNum = 0;
            
			eliteNum = 0;
            
			roleJob = 0;
            
			notice = "";
			_mailNum = 0;
			__mailNum = 0;
            
			_limitType = 0;
			__limitType = 0;
            
			_levelLimt = 0;
			__levelLimt = 0;
            

            members.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0L;
			level = 0;
            
			name = "";
			guildType = 0;
            
			badge = 0;
            
			chairmanName = "";
			memberNum = 0;
            
			rank = 0;
            
			chairmanNum = 0;
            
			deputyLeaderNum = 0;
            
			eliteNum = 0;
            
			roleJob = 0;
            
			notice = "";
			_mailNum = 0;
			__mailNum = 0;
            
			_limitType = 0;
			__limitType = 0;
            
			_levelLimt = 0;
			__levelLimt = 0;
            

            members.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = members.Count; a < b; ++a)
            {
                //var _value_ = members[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				members[a] = null;
            }
            members.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                id = XBuffer.ReadLong(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                name = XBuffer.ReadString(buffer, ref offset);
                guildType = XBuffer.ReadInt(buffer, ref offset);
                badge = XBuffer.ReadInt(buffer, ref offset);
                chairmanName = XBuffer.ReadString(buffer, ref offset);
                memberNum = XBuffer.ReadInt(buffer, ref offset);
                rank = XBuffer.ReadInt(buffer, ref offset);
                chairmanNum = XBuffer.ReadInt(buffer, ref offset);
                deputyLeaderNum = XBuffer.ReadInt(buffer, ref offset);
                eliteNum = XBuffer.ReadInt(buffer, ref offset);
                roleJob = XBuffer.ReadInt(buffer, ref offset);
                notice = XBuffer.ReadString(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					mailNum = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					limitType = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					levelLimt = XBuffer.ReadInt(buffer, ref offset);
				}

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    Member _value_ = null;
                    //_value_ = ClassCacheManager.New<Member>();
					_value_ = new Member();
                    _value_.Read(buffer, ref offset);
                    members.Add(_value_);
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
                XBuffer.WriteLong(id, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteInt(guildType, buffer, ref offset);
                XBuffer.WriteInt(badge, buffer, ref offset);
                XBuffer.WriteString(chairmanName, buffer, ref offset);
                XBuffer.WriteInt(memberNum, buffer, ref offset);
                XBuffer.WriteInt(rank, buffer, ref offset);
                XBuffer.WriteInt(chairmanNum, buffer, ref offset);
                XBuffer.WriteInt(deputyLeaderNum, buffer, ref offset);
                XBuffer.WriteInt(eliteNum, buffer, ref offset);
                XBuffer.WriteInt(roleJob, buffer, ref offset);
                XBuffer.WriteString(notice, buffer, ref offset);
				XBuffer.WriteByte(_mailNum, buffer, ref offset);
				if (_mailNum == 1)
				{
					XBuffer.WriteInt(mailNum, buffer, ref offset);
				}
				XBuffer.WriteByte(_limitType, buffer, ref offset);
				if (_limitType == 1)
				{
					XBuffer.WriteInt(limitType, buffer, ref offset);
				}
				XBuffer.WriteByte(_levelLimt, buffer, ref offset);
				if (_levelLimt == 1)
				{
					XBuffer.WriteInt(levelLimt, buffer, ref offset);
				}

                XBuffer.WriteShort((short)members.Count,buffer, ref offset);
                for (int a = 0; a < members.Count; ++a)
                {
					if(members[a] == null)
						UnityEngine.Debug.LogError("members has nil item, idx == " + a);
					else
						members[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //工会列表信息
    public class GuildListInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public long id; // ID
        
		public int level; // 等级
        
		public string name; // 名字
        
		public int guildType; // 社团类别
        
		public int badge; // 徽章
        
		public int memberNum; // 人数
        
		public int rank; // 排名
        
		public int limitType; // 限制类型
        
		public int limitLevel; // 限制等级
        
		public bool isApplying; // 是否申请中
        

        //鏋勯�犲嚱鏁�
        public GuildListInfo() : base()
        {
			
			id = 0L;
			level = 0;
            
			name = "";
			guildType = 0;
            
			badge = 0;
            
			memberNum = 0;
            
			rank = 0;
            
			limitType = 0;
            
			limitLevel = 0;
            
			isApplying = false;
            isApplying = false;

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0L;
			level = 0;
            
			name = "";
			guildType = 0;
            
			badge = 0;
            
			memberNum = 0;
            
			rank = 0;
            
			limitType = 0;
            
			limitLevel = 0;
            
			isApplying = false;
            isApplying = false;

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
                id = XBuffer.ReadLong(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                name = XBuffer.ReadString(buffer, ref offset);
                guildType = XBuffer.ReadInt(buffer, ref offset);
                badge = XBuffer.ReadInt(buffer, ref offset);
                memberNum = XBuffer.ReadInt(buffer, ref offset);
                rank = XBuffer.ReadInt(buffer, ref offset);
                limitType = XBuffer.ReadInt(buffer, ref offset);
                limitLevel = XBuffer.ReadInt(buffer, ref offset);
                isApplying = XBuffer.ReadBool(buffer, ref offset);

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
                XBuffer.WriteLong(id, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteInt(guildType, buffer, ref offset);
                XBuffer.WriteInt(badge, buffer, ref offset);
                XBuffer.WriteInt(memberNum, buffer, ref offset);
                XBuffer.WriteInt(rank, buffer, ref offset);
                XBuffer.WriteInt(limitType, buffer, ref offset);
                XBuffer.WriteInt(limitLevel, buffer, ref offset);
                XBuffer.WriteBool(isApplying, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //训练所角色
    public class ExpHomeRole : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public long roleId; // ID
        
		public string name; // 名字
        
		public int level; // 等级
        
		public bool star; // 星标
        
		public int icon; // 头像
        

        //鏋勯�犲嚱鏁�
        public ExpHomeRole() : base()
        {
			
			roleId = 0L;
			name = "";
			level = 0;
            
			star = false;
            star = false;
			icon = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			name = "";
			level = 0;
            
			star = false;
            star = false;
			icon = 0;
            

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
                level = XBuffer.ReadInt(buffer, ref offset);
                star = XBuffer.ReadBool(buffer, ref offset);
                icon = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteLong(roleId, buffer, ref offset);
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteBool(star, buffer, ref offset);
                XBuffer.WriteInt(icon, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //申请者
    public class Applyer : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public long roleId; // ID
        
		public string name; // 名字
        
		public int level; // 等级
        
		public long fightPower; // 战斗力
        
		public int title; // 称号
        
		public int model; // 头像
        
		public long time; // 申请时间
        

        //鏋勯�犲嚱鏁�
        public Applyer() : base()
        {
			
			roleId = 0L;
			name = "";
			level = 0;
            
			fightPower = 0L;
			title = 0;
            
			model = 0;
            
			time = 0L;

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			name = "";
			level = 0;
            
			fightPower = 0L;
			title = 0;
            
			model = 0;
            
			time = 0L;

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
                level = XBuffer.ReadInt(buffer, ref offset);
                fightPower = XBuffer.ReadLong(buffer, ref offset);
                title = XBuffer.ReadInt(buffer, ref offset);
                model = XBuffer.ReadInt(buffer, ref offset);
                time = XBuffer.ReadLong(buffer, ref offset);

    		    short _count_ = 0;
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
                XBuffer.WriteLong(roleId, buffer, ref offset);
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteLong(fightPower, buffer, ref offset);
                XBuffer.WriteInt(title, buffer, ref offset);
                XBuffer.WriteInt(model, buffer, ref offset);
                XBuffer.WriteLong(time, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //捐献信息
    public class DonateInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int target; // 捐献目标
        
		public long exp; // 经验
        
		public int level; // 等级
        

        //鏋勯�犲嚱鏁�
        public DonateInfo() : base()
        {
			
			target = 0;
            
			exp = 0L;
			level = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			target = 0;
            
			exp = 0L;
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
                target = XBuffer.ReadInt(buffer, ref offset);
                exp = XBuffer.ReadLong(buffer, ref offset);
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
            XBuffer.WriteByte(6, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(target, buffer, ref offset);
                XBuffer.WriteLong(exp, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //加速宠物
    public class QuickPet : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int id; // ID
        
		public int petId; // 宠物id
        
		public int level; // 等级
        
		public int color; // 品阶
        
		public int exp; // 当前经验
        
		public bool hasQuick; // 已加速
        

        //鏋勯�犲嚱鏁�
        public QuickPet() : base()
        {
			
			id = 0;
            
			petId = 0;
            
			level = 0;
            
			color = 0;
            
			exp = 0;
            
			hasQuick = false;
            hasQuick = false;

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			petId = 0;
            
			level = 0;
            
			color = 0;
            
			exp = 0;
            
			hasQuick = false;
            hasQuick = false;

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
                petId = XBuffer.ReadInt(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                color = XBuffer.ReadInt(buffer, ref offset);
                exp = XBuffer.ReadInt(buffer, ref offset);
                hasQuick = XBuffer.ReadBool(buffer, ref offset);

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
                XBuffer.WriteInt(id, buffer, ref offset);
                XBuffer.WriteInt(petId, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteInt(color, buffer, ref offset);
                XBuffer.WriteInt(exp, buffer, ref offset);
                XBuffer.WriteBool(hasQuick, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //加速玩家
    public class ExpRole : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public long roleId; // ID
        
		public string name; // 名字
        
        public List<QuickPet> pets{get; protected set;} //宠物

        //鏋勯�犲嚱鏁�
        public ExpRole() : base()
        {
            pets = new List<QuickPet>(); //宠物
			
			roleId = 0L;
			name = "";

            pets.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			name = "";

            pets.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = pets.Count; a < b; ++a)
            {
                //var _value_ = pets[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				pets[a] = null;
            }
            pets.Clear();
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

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    QuickPet _value_ = null;
                    //_value_ = ClassCacheManager.New<QuickPet>();
					_value_ = new QuickPet();
                    _value_.Read(buffer, ref offset);
                    pets.Add(_value_);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(8, buffer, ref offset);
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

                XBuffer.WriteShort((short)pets.Count,buffer, ref offset);
                for (int a = 0; a < pets.Count; ++a)
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
    //位置宠物
    public class PosPet : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int id; // 位置
        
		public int petId; // 宠物id
        

        //鏋勯�犲嚱鏁�
        public PosPet() : base()
        {
			
			id = 0;
            
			petId = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			petId = 0;
            

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
                petId = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(9, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(id, buffer, ref offset);
                XBuffer.WriteInt(petId, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //许愿玩家
    public class WishRole : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public long roleId; // 角色id
        
		public string name; // 名字
        
		public int icon; // 头像
        
		public int level; // 等级
        
		public int itemId; // 许愿道具id
        
		public int num; // 剩余数量
        
		public int type; // 资质
        
		public int presentNum; // 赠送次数
        

        //鏋勯�犲嚱鏁�
        public WishRole() : base()
        {
			
			roleId = 0L;
			name = "";
			icon = 0;
            
			level = 0;
            
			itemId = 0;
            
			num = 0;
            
			type = 0;
            
			presentNum = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			name = "";
			icon = 0;
            
			level = 0;
            
			itemId = 0;
            
			num = 0;
            
			type = 0;
            
			presentNum = 0;
            

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
                icon = XBuffer.ReadInt(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                itemId = XBuffer.ReadInt(buffer, ref offset);
                num = XBuffer.ReadInt(buffer, ref offset);
                type = XBuffer.ReadInt(buffer, ref offset);
                presentNum = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(10, buffer, ref offset);
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
                XBuffer.WriteInt(icon, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteInt(itemId, buffer, ref offset);
                XBuffer.WriteInt(num, buffer, ref offset);
                XBuffer.WriteInt(type, buffer, ref offset);
                XBuffer.WriteInt(presentNum, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //红包玩家
    public class HongbaoRole : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public string name; // 玩家名字
        
		public long roleId; // 角色id
        
		public int num; // 获得数量
        
		public int icon; // 头像
        

        //鏋勯�犲嚱鏁�
        public HongbaoRole() : base()
        {
			
			name = "";
			roleId = 0L;
			num = 0;
            
			icon = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			name = "";
			roleId = 0L;
			num = 0;
            
			icon = 0;
            

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
                roleId = XBuffer.ReadLong(buffer, ref offset);
                num = XBuffer.ReadInt(buffer, ref offset);
                icon = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(11, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteLong(roleId, buffer, ref offset);
                XBuffer.WriteInt(num, buffer, ref offset);
                XBuffer.WriteInt(icon, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //红包
    public class Hongbao : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public long id; // 红包id 1金币 2钻石 3神器之源
        
		public string name; // 玩家名字
        
		public int type; // 红包表id
        
		public int sumMoney; // 总钱
        
		public int naxNum; // 最大数量
        
        public List<HongbaoRole> roles{get; protected set;} //抢红包的玩家列表

        //鏋勯�犲嚱鏁�
        public Hongbao() : base()
        {
            roles = new List<HongbaoRole>(); //抢红包的玩家列表
			
			id = 0L;
			name = "";
			type = 0;
            
			sumMoney = 0;
            
			naxNum = 0;
            

            roles.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0L;
			name = "";
			type = 0;
            
			sumMoney = 0;
            
			naxNum = 0;
            

            roles.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = roles.Count; a < b; ++a)
            {
                //var _value_ = roles[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				roles[a] = null;
            }
            roles.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                id = XBuffer.ReadLong(buffer, ref offset);
                name = XBuffer.ReadString(buffer, ref offset);
                type = XBuffer.ReadInt(buffer, ref offset);
                sumMoney = XBuffer.ReadInt(buffer, ref offset);
                naxNum = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    HongbaoRole _value_ = null;
                    //_value_ = ClassCacheManager.New<HongbaoRole>();
					_value_ = new HongbaoRole();
                    _value_.Read(buffer, ref offset);
                    roles.Add(_value_);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(12, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteLong(id, buffer, ref offset);
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteInt(type, buffer, ref offset);
                XBuffer.WriteInt(sumMoney, buffer, ref offset);
                XBuffer.WriteInt(naxNum, buffer, ref offset);

                XBuffer.WriteShort((short)roles.Count,buffer, ref offset);
                for (int a = 0; a < roles.Count; ++a)
                {
					if(roles[a] == null)
						UnityEngine.Debug.LogError("roles has nil item, idx == " + a);
					else
						roles[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //红包排名玩家
    public class HongbaoRankRole : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public string name; // 玩家名字
        
		public int num; // 个数
        
		public int value; // 价值
        
		public int icon; // 头像
        
		public int rank; // 排名
        

        //鏋勯�犲嚱鏁�
        public HongbaoRankRole() : base()
        {
			
			name = "";
			num = 0;
            
			value = 0;
            
			icon = 0;
            
			rank = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			name = "";
			num = 0;
            
			value = 0;
            
			icon = 0;
            
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
                TypeEnum _real_type_;
                name = XBuffer.ReadString(buffer, ref offset);
                num = XBuffer.ReadInt(buffer, ref offset);
                value = XBuffer.ReadInt(buffer, ref offset);
                icon = XBuffer.ReadInt(buffer, ref offset);
                rank = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(13, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteInt(num, buffer, ref offset);
                XBuffer.WriteInt(value, buffer, ref offset);
                XBuffer.WriteInt(icon, buffer, ref offset);
                XBuffer.WriteInt(rank, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //创建
    public class ReqCreate : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116201;
		public string name; // 名字
		public int badge; // 徽章
		public int type; // 类型 0休闲 1竞技

    	//鏋勯�犲嚱鏁�
    	public ReqCreate()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			name = "";
			badge = 0;
            
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
                name = XBuffer.ReadString(buffer, ref offset);
                badge = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteString(name,buffer, ref offset);
					XBuffer.WriteInt(badge,buffer, ref offset);
					XBuffer.WriteInt(type,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //修改公告
    public class ReqChangeNotice : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116202;
		public string content; // 内容

    	//鏋勯�犲嚱鏁�
    	public ReqChangeNotice()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			content = "";
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
                content = XBuffer.ReadString(buffer, ref offset);

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
					XBuffer.WriteString(content,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //改名
    public class ReqChangeName : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116203;
		public string name; // 名字

    	//鏋勯�犲嚱鏁�
    	public ReqChangeName()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			name = "";
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
                name = XBuffer.ReadString(buffer, ref offset);

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
					XBuffer.WriteString(name,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //解散
    public class ReqBreak : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116204;

    	//鏋勯�犲嚱鏁�
    	public ReqBreak()
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
    //修改徽章
    public class ReqChangeBadge : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116205;
		public int badge; // 徽章

    	//鏋勯�犲嚱鏁�
    	public ReqChangeBadge()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			badge = 0;
            
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
                badge = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(badge,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //修改社团类型
    public class ReqChangeGuildType : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116206;
		public int type; // 类型

    	//鏋勯�犲嚱鏁�
    	public ReqChangeGuildType()
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
    //发邮件
    public class ReqSendMail : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116207;
		public string content; // 内容

    	//鏋勯�犲嚱鏁�
    	public ReqSendMail()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			content = "";
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
                content = XBuffer.ReadString(buffer, ref offset);

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
					XBuffer.WriteString(content,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //工会信息
    public class ReqGuildInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116208;

    	//鏋勯�犲嚱鏁�
    	public ReqGuildInfo()
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
    //工会列表
    public class ReqGuildList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116209;
		public int page; // index

    	//鏋勯�犲嚱鏁�
    	public ReqGuildList()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
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
					XBuffer.WriteInt(page,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //申请加入
    public class ReqApplyJoin : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116210;
		public bool quick; // 快速加入
		public long __guildId; // 工会id
		private byte _guildId = 0; // 工会id tag
		
		public bool hasGuildId()
		{
			return this._guildId == 1;
		}
		
		public long guildId
		{
			set
			{
				_guildId = 1;
				__guildId = value;
			}
			
			get
			{
				return __guildId;
			}
		}

    	//鏋勯�犲嚱鏁�
    	public ReqApplyJoin()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			quick = false;
            quick = false;
			_guildId = 0;
			__guildId = 0L;
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
        		quick = XBuffer.ReadBool(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					guildId = XBuffer.ReadLong(buffer, ref offset);
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
					XBuffer.WriteBool(quick,buffer, ref offset);
				XBuffer.WriteByte(_guildId,buffer, ref offset);
				if (_guildId == 1)
				{
					XBuffer.WriteLong(guildId,buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //退出
    public class ReqExit : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116211;

    	//鏋勯�犲嚱鏁�
    	public ReqExit()
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
    //清空申请列表
    public class ReqClearApplyList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116212;

    	//鏋勯�犲嚱鏁�
    	public ReqClearApplyList()
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
    //操作申请列表
    public class ReqOperateApplyer : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116213;
		public long roleId; // 目标角色id
		public bool agree; // 同意

    	//鏋勯�犲嚱鏁�
    	public ReqOperateApplyer()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			agree = false;
            agree = false;
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
        		agree = XBuffer.ReadBool(buffer, ref offset);

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
					XBuffer.WriteBool(agree,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //成员列表
    public class ReqMemberList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116214;

    	//鏋勯�犲嚱鏁�
    	public ReqMemberList()
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
    //工会查找
    public class ReqFindGuildByName : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116215;
		public string name; // 名字

    	//鏋勯�犲嚱鏁�
    	public ReqFindGuildByName()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			name = "";
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
                name = XBuffer.ReadString(buffer, ref offset);

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
					XBuffer.WriteString(name,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //改变职位
    public class ReqOperateMember : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116216;
		public int oldJob; // 老职位
		public int nweJob; // 新职位 -1踢出
		public long roleId; // 角色id

    	//鏋勯�犲嚱鏁�
    	public ReqOperateMember()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			oldJob = 0;
            
			nweJob = 0;
            
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
                oldJob = XBuffer.ReadInt(buffer, ref offset);
                nweJob = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(oldJob,buffer, ref offset);
					XBuffer.WriteInt(nweJob,buffer, ref offset);
					XBuffer.WriteLong(roleId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //领奖
    public class ReqReward : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116217;

    	//鏋勯�犲嚱鏁�
    	public ReqReward()
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
    //限制设置
    public class ReqChangeLimit : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116218;
		public int limitType; // 限制类型0任何人 1申请 2禁止
		public int limitLevel; // 限制等级 0任何人 -1禁止所有 >0等级

    	//鏋勯�犲嚱鏁�
    	public ReqChangeLimit()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			limitType = 0;
            
			limitLevel = 0;
            
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
                limitType = XBuffer.ReadInt(buffer, ref offset);
                limitLevel = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(limitType,buffer, ref offset);
					XBuffer.WriteInt(limitLevel,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //请求帮派日志
    public class ReqGuildLog : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116219;

    	//鏋勯�犲嚱鏁�
    	public ReqGuildLog()
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
    //招募会员
    public class ReqZhaoMu : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116220;

    	//鏋勯�犲嚱鏁�
    	public ReqZhaoMu()
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
    //捐献
    public class ReqDonate : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116221;
		public int type; // 类型 1【金币捐献】 2【钻石捐献】 3【至尊捐献】
		public int target; // 捐献目标
		public bool all; // 捐献全部
		public int bigType; // 类型

    	//鏋勯�犲嚱鏁�
    	public ReqDonate()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			type = 0;
            
			target = 0;
            
			all = false;
            all = false;
			bigType = 0;
            
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
                target = XBuffer.ReadInt(buffer, ref offset);
        		all = XBuffer.ReadBool(buffer, ref offset);
                bigType = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(target,buffer, ref offset);
					XBuffer.WriteBool(all,buffer, ref offset);
					XBuffer.WriteInt(bigType,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //捐献
    public class ReqOpenDonate : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116222;
		public int target; // 捐献目标

    	//鏋勯�犲嚱鏁�
    	public ReqOpenDonate()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			target = 0;
            
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
                target = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(target,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //训练所加速目标信息
    public class ReqQuickRoleInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116223;
		public long roleId; // 目标角色id

    	//鏋勯�犲嚱鏁�
    	public ReqQuickRoleInfo()
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
    //训练所角色列表
    public class ReqExpHomeRoleList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116224;

    	//鏋勯�犲嚱鏁�
    	public ReqExpHomeRoleList()
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
    //随机加速
    public class ReqRandomQuick : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116225;
		public bool once; // 单次随机

    	//鏋勯�犲嚱鏁�
    	public ReqRandomQuick()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			once = false;
            once = false;
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
        		once = XBuffer.ReadBool(buffer, ref offset);

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
					XBuffer.WriteBool(once,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //为玩家加速
    public class ReqQuickRole : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116226;
		public long roleId; // 目标角色id
		public int id; // id

    	//鏋勯�犲嚱鏁�
    	public ReqQuickRole()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
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
                roleId = XBuffer.ReadLong(buffer, ref offset);
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
					XBuffer.WriteLong(roleId,buffer, ref offset);
					XBuffer.WriteInt(id,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //设置加速宠物
    public class ReqSetExpPet : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116227;
		public int id; // 位置
		public int petId; // 宠物id

    	//鏋勯�犲嚱鏁�
    	public ReqSetExpPet()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			petId = 0;
            
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
                petId = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(petId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //购买加速位置
    public class ReqBuyPos : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116228;
		public int id; // 位置

    	//鏋勯�犲嚱鏁�
    	public ReqBuyPos()
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
    //请求训练所数据
    public class ReqOpenExpHome : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116229;

    	//鏋勯�犲嚱鏁�
    	public ReqOpenExpHome()
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
    //许愿
    public class ReqWish : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116230;
		public int itemId; // 道具id
		public int type; // 资质

    	//鏋勯�犲嚱鏁�
    	public ReqWish()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			itemId = 0;
            
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
                itemId = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(itemId,buffer, ref offset);
					XBuffer.WriteInt(type,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //赠送
    public class ReqPresent : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116231;
		public long roleId; // 目标角色id

    	//鏋勯�犲嚱鏁�
    	public ReqPresent()
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
    //许愿信息
    public class ReqWishInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116232;

    	//鏋勯�犲嚱鏁�
    	public ReqWishInfo()
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
    //发红包
    public class ReqSendHongbao : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116233;
		public int id; // 红包表id

    	//鏋勯�犲嚱鏁�
    	public ReqSendHongbao()
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
    //抢红包
    public class ReqReceiveHongbao : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116234;
		public long id; // 红包id

    	//鏋勯�犲嚱鏁�
    	public ReqReceiveHongbao()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0L;
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
                id = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(id,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //星标玩家
    public class ReqStarRole : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116235;
		public long role; // 角色id

    	//鏋勯�犲嚱鏁�
    	public ReqStarRole()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			role = 0L;
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
                role = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(role,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //打开社团红包界面
    public class ReqOpenHongbaoPage : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116236;

    	//鏋勯�犲嚱鏁�
    	public ReqOpenHongbaoPage()
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
    //发红包排行
    public class ReqHongbaoRank : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116237;

    	//鏋勯�犲嚱鏁�
    	public ReqHongbaoRank()
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
    //打开抢红包列表界面
    public class ReqQiangHongBaoList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116238;

    	//鏋勯�犲嚱鏁�
    	public ReqQiangHongBaoList()
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
    //工会信息
    public class ResGuildInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116101;
		public GuildInfo info; // 工会

    	//鏋勯�犲嚱鏁�
    	public ResGuildInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//info = ClassCacheManager.New<GuildInfo>();
			info = new GuildInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			info = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //info = ClassCacheManager.New<GuildInfo>();
				info = new GuildInfo();
                info.Read(buffer, ref offset);

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
					if(info == null)
						//info = ClassCacheManager.New<GuildInfo>();
						info = new GuildInfo();
					info.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //改名
    public class ResChangeName : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116102;
		public string name; // 名字

    	//鏋勯�犲嚱鏁�
    	public ResChangeName()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			name = "";
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
                name = XBuffer.ReadString(buffer, ref offset);

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
					XBuffer.WriteString(name,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //成员
    public class ResMemberInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116103;
        public List<Member> members{get;protected set;} //成员

    	//鏋勯�犲嚱鏁�
    	public ResMemberInfo()
    	{
            members = new List<Member>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            members.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = members.Count; a < b; ++a)
            {
				members[a] = null;
                //var _value_ = members[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            members.Clear();
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
                    Member _value_ = null;
                    //_value_ = ClassCacheManager.New<Member>();
					_value_ = new Member();
                    _value_.Read(buffer, ref offset);
                    members.Add(_value_);
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

                XBuffer.WriteShort((short)members.Count, buffer, ref offset);
                for(int a = 0; a < members.Count; ++a)
                {
					if(members[a] == null)
						UnityEngine.Debug.LogError("members has nil item, idx == " + a);
					else
						members[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //改徽章
    public class ResChangeBadge : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116104;
		public int id; // 徽章

    	//鏋勯�犲嚱鏁�
    	public ResChangeBadge()
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
    //改类型
    public class ResChangeGuildType : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116105;
		public int id; // 类型

    	//鏋勯�犲嚱鏁�
    	public ResChangeGuildType()
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
    //申请者列表
    public class ResApplyerList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116106;
		public int operate; // 操作 0添加 1移除 2清空
        public List<Applyer> applyer{get;protected set;} //申请者列表

    	//鏋勯�犲嚱鏁�
    	public ResApplyerList()
    	{
            applyer = new List<Applyer>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			operate = 0;
            
            applyer.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = applyer.Count; a < b; ++a)
            {
				applyer[a] = null;
                //var _value_ = applyer[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            applyer.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                operate = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    Applyer _value_ = null;
                    //_value_ = ClassCacheManager.New<Applyer>();
					_value_ = new Applyer();
                    _value_.Read(buffer, ref offset);
                    applyer.Add(_value_);
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
					XBuffer.WriteInt(operate,buffer, ref offset);

                XBuffer.WriteShort((short)applyer.Count, buffer, ref offset);
                for(int a = 0; a < applyer.Count; ++a)
                {
					if(applyer[a] == null)
						UnityEngine.Debug.LogError("applyer has nil item, idx == " + a);
					else
						applyer[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //限制设置
    public class ResChangeLimit : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116107;
		public int limitType; // 限制类型0任何人 1申请 2禁止
		public int limitLevel; // 限制等级 0任何人 -1禁止所有 >0等级

    	//鏋勯�犲嚱鏁�
    	public ResChangeLimit()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			limitType = 0;
            
			limitLevel = 0;
            
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
                limitType = XBuffer.ReadInt(buffer, ref offset);
                limitLevel = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(limitType,buffer, ref offset);
					XBuffer.WriteInt(limitLevel,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //工会列表
    public class ResGuildList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116108;
		public int __guildNum; // 公会数量(第一次请求会推)
		private byte _guildNum = 0; // 公会数量(第一次请求会推) tag
		
		public bool hasGuildNum()
		{
			return this._guildNum == 1;
		}
		
		public int guildNum
		{
			set
			{
				_guildNum = 1;
				__guildNum = value;
			}
			
			get
			{
				return __guildNum;
			}
		}
        public List<GuildListInfo> listInfo{get;protected set;} //工会列表

    	//鏋勯�犲嚱鏁�
    	public ResGuildList()
    	{
            listInfo = new List<GuildListInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			_guildNum = 0;
			__guildNum = 0;
            
            listInfo.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = listInfo.Count; a < b; ++a)
            {
				listInfo[a] = null;
                //var _value_ = listInfo[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            listInfo.Clear();
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
					guildNum = XBuffer.ReadInt(buffer, ref offset);
				}

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    GuildListInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<GuildListInfo>();
					_value_ = new GuildListInfo();
                    _value_.Read(buffer, ref offset);
                    listInfo.Add(_value_);
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
				XBuffer.WriteByte(_guildNum,buffer, ref offset);
				if (_guildNum == 1)
				{
					XBuffer.WriteInt(guildNum,buffer, ref offset);
				}

                XBuffer.WriteShort((short)listInfo.Count, buffer, ref offset);
                for(int a = 0; a < listInfo.Count; ++a)
                {
					if(listInfo[a] == null)
						UnityEngine.Debug.LogError("listInfo has nil item, idx == " + a);
					else
						listInfo[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //工会查找
    public class ResFindGuildByName : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116109;
		public GuildListInfo info; // 工会

    	//鏋勯�犲嚱鏁�
    	public ResFindGuildByName()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//info = ClassCacheManager.New<GuildListInfo>();
			info = new GuildListInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			info = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //info = ClassCacheManager.New<GuildListInfo>();
				info = new GuildListInfo();
                info.Read(buffer, ref offset);

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
					if(info == null)
						//info = ClassCacheManager.New<GuildListInfo>();
						info = new GuildListInfo();
					info.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //工会改变
    public class ResGuildIdChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116110;
		public long id; // 工会id  -1退出

    	//鏋勯�犲嚱鏁�
    	public ResGuildIdChange()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0L;
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
                id = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(id,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //工会日志
    public class ResGuildLog : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116111;
        public List<string> logList{get;protected set;} //工会日志列表

    	//鏋勯�犲嚱鏁�
    	public ResGuildLog()
    	{
            logList = new List<string>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            logList.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            logList.Clear();
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
                    var _value_ = XBuffer.ReadString(buffer, ref offset);
                    logList.Add(_value_);
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

                XBuffer.WriteShort((short)logList.Count, buffer, ref offset);
                for(int a = 0; a < logList.Count; ++a)
                {
                    XBuffer.WriteString(logList[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //公会每日奖励领奖状态
    public class ResRewardState : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116112;
		public int state; // 状态（0 不能领 1可领 2已领）

    	//鏋勯�犲嚱鏁�
    	public ResRewardState()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
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
                TypeEnum real_type;
                state = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(state,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //公会邮件数量改变
    public class ResEMailNumChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116113;
		public int sendNum; // 已发邮件数量

    	//鏋勯�犲嚱鏁�
    	public ResEMailNumChange()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			sendNum = 0;
            
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
                sendNum = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(sendNum,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //改公告
    public class ResChangeNotice : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116114;
		public string content; // 公告

    	//鏋勯�犲嚱鏁�
    	public ResChangeNotice()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			content = "";
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
                content = XBuffer.ReadString(buffer, ref offset);

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
					XBuffer.WriteString(content,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //捐献
    public class ResDonate : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116115;
		public int bigType; // 捐献大类型
		public int num; // 捐献次数
		public int __dailyExp; // 每日经验
		private byte _dailyExp = 0; // 每日经验 tag
		
		public bool hasDailyExp()
		{
			return this._dailyExp == 1;
		}
		
		public int dailyExp
		{
			set
			{
				_dailyExp = 1;
				__dailyExp = value;
			}
			
			get
			{
				return __dailyExp;
			}
		}
        public List<DonateInfo> donateInfos{get;protected set;} //捐献信息列表

    	//鏋勯�犲嚱鏁�
    	public ResDonate()
    	{
            donateInfos = new List<DonateInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			bigType = 0;
            
			num = 0;
            
			_dailyExp = 0;
			__dailyExp = 0;
            
            donateInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = donateInfos.Count; a < b; ++a)
            {
				donateInfos[a] = null;
                //var _value_ = donateInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            donateInfos.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                bigType = XBuffer.ReadInt(buffer, ref offset);
                num = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					dailyExp = XBuffer.ReadInt(buffer, ref offset);
				}

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    DonateInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<DonateInfo>();
					_value_ = new DonateInfo();
                    _value_.Read(buffer, ref offset);
                    donateInfos.Add(_value_);
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
					XBuffer.WriteInt(bigType,buffer, ref offset);
					XBuffer.WriteInt(num,buffer, ref offset);
				XBuffer.WriteByte(_dailyExp,buffer, ref offset);
				if (_dailyExp == 1)
				{
					XBuffer.WriteInt(dailyExp,buffer, ref offset);
				}

                XBuffer.WriteShort((short)donateInfos.Count, buffer, ref offset);
                for(int a = 0; a < donateInfos.Count; ++a)
                {
					if(donateInfos[a] == null)
						UnityEngine.Debug.LogError("donateInfos has nil item, idx == " + a);
					else
						donateInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //训练所加速目标信息
    public class ResQuickRoleInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116116;
		public ExpRole role; // 目标角色

    	//鏋勯�犲嚱鏁�
    	public ResQuickRoleInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//role = ClassCacheManager.New<ExpRole>();
			role = new ExpRole();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			role = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //role = ClassCacheManager.New<ExpRole>();
				role = new ExpRole();
                role.Read(buffer, ref offset);

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
					if(role == null)
						//role = ClassCacheManager.New<ExpRole>();
						role = new ExpRole();
					role.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //训练所角色列表
    public class ResExpHomeRoleList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116117;
		public int quickNum; // 加速次数
		public int beQuickNum; // 被加速次数
		public int starNum; // 星标数
        public List<ExpHomeRole> roleList{get;protected set;} //训练所角色
        public List<string> log{get;protected set;} //加速日志

    	//鏋勯�犲嚱鏁�
    	public ResExpHomeRoleList()
    	{
            roleList = new List<ExpHomeRole>();
            log = new List<string>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			quickNum = 0;
            
			beQuickNum = 0;
            
			starNum = 0;
            
            roleList.Clear();
            log.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = roleList.Count; a < b; ++a)
            {
				roleList[a] = null;
                //var _value_ = roleList[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            roleList.Clear();
            log.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                quickNum = XBuffer.ReadInt(buffer, ref offset);
                beQuickNum = XBuffer.ReadInt(buffer, ref offset);
                starNum = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    ExpHomeRole _value_ = null;
                    //_value_ = ClassCacheManager.New<ExpHomeRole>();
					_value_ = new ExpHomeRole();
                    _value_.Read(buffer, ref offset);
                    roleList.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    var _value_ = XBuffer.ReadString(buffer, ref offset);
                    log.Add(_value_);
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
					XBuffer.WriteInt(quickNum,buffer, ref offset);
					XBuffer.WriteInt(beQuickNum,buffer, ref offset);
					XBuffer.WriteInt(starNum,buffer, ref offset);

                XBuffer.WriteShort((short)roleList.Count, buffer, ref offset);
                for(int a = 0; a < roleList.Count; ++a)
                {
					if(roleList[a] == null)
						UnityEngine.Debug.LogError("roleList has nil item, idx == " + a);
					else
						roleList[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)log.Count, buffer, ref offset);
                for(int a = 0; a < log.Count; ++a)
                {
                    XBuffer.WriteString(log[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //为玩家加速返回
    public class ResQuickRole : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116118;
		public long roleId; // 目标角色id
		public QuickPet pet; // 加速宠物

    	//鏋勯�犲嚱鏁�
    	public ResQuickRole()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			//pet = ClassCacheManager.New<QuickPet>();
			pet = new QuickPet();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			pet = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                roleId = XBuffer.ReadLong(buffer, ref offset);
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //pet = ClassCacheManager.New<QuickPet>();
				pet = new QuickPet();
                pet.Read(buffer, ref offset);

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
					if(pet == null)
						//pet = ClassCacheManager.New<QuickPet>();
						pet = new QuickPet();
					pet.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //设置加速宠物
    public class ResSetExpPet : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116119;
		public PosPet info; // 位置

    	//鏋勯�犲嚱鏁�
    	public ResSetExpPet()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//info = ClassCacheManager.New<PosPet>();
			info = new PosPet();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			info = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //info = ClassCacheManager.New<PosPet>();
				info = new PosPet();
                info.Read(buffer, ref offset);

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
					if(info == null)
						//info = ClassCacheManager.New<PosPet>();
						info = new PosPet();
					info.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //购买加速位置
    public class ResBuyPos : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116120;
		public int id; // 位置

    	//鏋勯�犲嚱鏁�
    	public ResBuyPos()
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
    //初始化我的训练所
    public class ResOpenExpHome : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116121;
        public List<PosPet> pets{get;protected set;} //宠物列表

    	//鏋勯�犲嚱鏁�
    	public ResOpenExpHome()
    	{
            pets = new List<PosPet>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
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

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    PosPet _value_ = null;
                    //_value_ = ClassCacheManager.New<PosPet>();
					_value_ = new PosPet();
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
    //许愿
    public class ResWish : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116122;
		public int itemId; // 道具id

    	//鏋勯�犲嚱鏁�
    	public ResWish()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			itemId = 0;
            
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
                itemId = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(itemId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //许愿信息
    public class ResWishInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116123;
		public int num; // 剩余需要数量
		public int presentNum; // 赠送次数
		public int itemId; // 许愿道具 -1未许愿
		public int __type; // 资质
		private byte _type = 0; // 资质 tag
		
		public bool hasType()
		{
			return this._type == 1;
		}
		
		public int type
		{
			set
			{
				_type = 1;
				__type = value;
			}
			
			get
			{
				return __type;
			}
		}
        public List<WishRole> wishRoles{get;protected set;} //许愿玩家

    	//鏋勯�犲嚱鏁�
    	public ResWishInfo()
    	{
            wishRoles = new List<WishRole>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			num = 0;
            
			presentNum = 0;
            
			itemId = 0;
            
			_type = 0;
			__type = 0;
            
            wishRoles.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = wishRoles.Count; a < b; ++a)
            {
				wishRoles[a] = null;
                //var _value_ = wishRoles[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            wishRoles.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                num = XBuffer.ReadInt(buffer, ref offset);
                presentNum = XBuffer.ReadInt(buffer, ref offset);
                itemId = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					type = XBuffer.ReadInt(buffer, ref offset);
				}

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    WishRole _value_ = null;
                    //_value_ = ClassCacheManager.New<WishRole>();
					_value_ = new WishRole();
                    _value_.Read(buffer, ref offset);
                    wishRoles.Add(_value_);
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
					XBuffer.WriteInt(presentNum,buffer, ref offset);
					XBuffer.WriteInt(itemId,buffer, ref offset);
				XBuffer.WriteByte(_type,buffer, ref offset);
				if (_type == 1)
				{
					XBuffer.WriteInt(type,buffer, ref offset);
				}

                XBuffer.WriteShort((short)wishRoles.Count, buffer, ref offset);
                for(int a = 0; a < wishRoles.Count; ++a)
                {
					if(wishRoles[a] == null)
						UnityEngine.Debug.LogError("wishRoles has nil item, idx == " + a);
					else
						wishRoles[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //有红包了
    public class ResHasHongbao : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116124;
		public long id; // 红包id

    	//鏋勯�犲嚱鏁�
    	public ResHasHongbao()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0L;
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
                id = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(id,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //抢红包列表
    public class ResHongbaoList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116125;
		public int num; // 抢红包次数
        public List<Hongbao> hongbaos{get;protected set;} //红包
        public List<string> logs{get;protected set;} //抢红包日志

    	//鏋勯�犲嚱鏁�
    	public ResHongbaoList()
    	{
            hongbaos = new List<Hongbao>();
            logs = new List<string>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			num = 0;
            
            hongbaos.Clear();
            logs.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = hongbaos.Count; a < b; ++a)
            {
				hongbaos[a] = null;
                //var _value_ = hongbaos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            hongbaos.Clear();
            logs.Clear();
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
                    Hongbao _value_ = null;
                    //_value_ = ClassCacheManager.New<Hongbao>();
					_value_ = new Hongbao();
                    _value_.Read(buffer, ref offset);
                    hongbaos.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    var _value_ = XBuffer.ReadString(buffer, ref offset);
                    logs.Add(_value_);
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

                XBuffer.WriteShort((short)hongbaos.Count, buffer, ref offset);
                for(int a = 0; a < hongbaos.Count; ++a)
                {
					if(hongbaos[a] == null)
						UnityEngine.Debug.LogError("hongbaos has nil item, idx == " + a);
					else
						hongbaos[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)logs.Count, buffer, ref offset);
                for(int a = 0; a < logs.Count; ++a)
                {
                    XBuffer.WriteString(logs[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //抢红包
    public class ResReceiveHongbao : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116126;
		public Hongbao hongbao; // 红包
		public int num; // 抢红包次数
        public List<string> logs{get;protected set;} //抢红包日志

    	//鏋勯�犲嚱鏁�
    	public ResReceiveHongbao()
    	{
            logs = new List<string>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//hongbao = ClassCacheManager.New<Hongbao>();
			hongbao = new Hongbao();
			num = 0;
            
            logs.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			hongbao = null;

            logs.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //hongbao = ClassCacheManager.New<Hongbao>();
				hongbao = new Hongbao();
                hongbao.Read(buffer, ref offset);
                num = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    var _value_ = XBuffer.ReadString(buffer, ref offset);
                    logs.Add(_value_);
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
					if(hongbao == null)
						//hongbao = ClassCacheManager.New<Hongbao>();
						hongbao = new Hongbao();
					hongbao.WriteWithType(buffer, ref offset);
					XBuffer.WriteInt(num,buffer, ref offset);

                XBuffer.WriteShort((short)logs.Count, buffer, ref offset);
                for(int a = 0; a < logs.Count; ++a)
                {
                    XBuffer.WriteString(logs[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //打开社团红包界面
    public class ResOpenHongbaoPage : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116127;
		public int num; // 已发红包次数
        public List<Hongbao> hongbaos{get;protected set;} //红包

    	//鏋勯�犲嚱鏁�
    	public ResOpenHongbaoPage()
    	{
            hongbaos = new List<Hongbao>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			num = 0;
            
            hongbaos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = hongbaos.Count; a < b; ++a)
            {
				hongbaos[a] = null;
                //var _value_ = hongbaos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            hongbaos.Clear();
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
                    Hongbao _value_ = null;
                    //_value_ = ClassCacheManager.New<Hongbao>();
					_value_ = new Hongbao();
                    _value_.Read(buffer, ref offset);
                    hongbaos.Add(_value_);
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

                XBuffer.WriteShort((short)hongbaos.Count, buffer, ref offset);
                for(int a = 0; a < hongbaos.Count; ++a)
                {
					if(hongbaos[a] == null)
						UnityEngine.Debug.LogError("hongbaos has nil item, idx == " + a);
					else
						hongbaos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //发红包次数改变
    public class ResSendHongbaoNum : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116128;
		public int num; // 已发红包次数

    	//鏋勯�犲嚱鏁�
    	public ResSendHongbaoNum()
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
    //发红包排行
    public class ResHongbaoRank : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116129;
		public int num; // 个数
		public int value; // 价值
        public List<HongbaoRankRole> rank{get;protected set;} //排行

    	//鏋勯�犲嚱鏁�
    	public ResHongbaoRank()
    	{
            rank = new List<HongbaoRankRole>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			num = 0;
            
			value = 0;
            
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
                num = XBuffer.ReadInt(buffer, ref offset);
                value = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    HongbaoRankRole _value_ = null;
                    //_value_ = ClassCacheManager.New<HongbaoRankRole>();
					_value_ = new HongbaoRankRole();
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
					XBuffer.WriteInt(num,buffer, ref offset);
					XBuffer.WriteInt(value,buffer, ref offset);

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
    //工会红包刷新
    public class ResGuildHongbaoRefresh : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 116130;

    	//鏋勯�犲嚱鏁�
    	public ResGuildHongbaoRefresh()
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