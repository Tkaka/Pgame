//Auto generated, do not edit it
//争霸赛

using System;
using System.IO;
using System.Collections.Generic;
using Message.Pet;
using Message.Bag;

namespace Message.KingFight
{
    public enum TypeEnum
    {
        BaseInfo = 1,
        BetInfo = 2,
        MainInfo = 3,
        FightInfo = 4,
        EightRoleInfo = 5,
        EightFightInfo = 6,
        EightInfo = 7,
        TeamInfo = 8,
    }

    //基本信息
    public class BaseInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public EquipedPetInfo petBaseInfo; // 宠物基本信息
        
		public int precedeValue; // 先手值
        
		public int fightPower; // 战斗力
        
		public int index; // 出场顺序 -1替补
        
		public int __state; // 0胜利 1失败
		private byte _state = 0; // 0胜利 1失败 tag
		
		public bool hasState()
		{
			return this._state == 1;
		}
		
		public int state
		{
			set
			{
				_state = 1;
				__state = value;
			}
			
			get
			{
				return __state;
			}
		}
        

        //鏋勯�犲嚱鏁�
        public BaseInfo() : base()
        {
			
			//petBaseInfo = ClassCacheManager.New<EquipedPetInfo>();
			petBaseInfo = new EquipedPetInfo();
			precedeValue = 0;
            
			fightPower = 0;
            
			index = 0;
            
			_state = 0;
			__state = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//petBaseInfo = ClassCacheManager.New<EquipedPetInfo>();
			petBaseInfo = new EquipedPetInfo();
			precedeValue = 0;
            
			fightPower = 0;
            
			index = 0;
            
			_state = 0;
			__state = 0;
            

        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			petBaseInfo = null;

        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //petBaseInfo = ClassCacheManager.New<EquipedPetInfo>();
				petBaseInfo = new EquipedPetInfo();
                petBaseInfo.Read(buffer, ref offset);
                precedeValue = XBuffer.ReadInt(buffer, ref offset);
                fightPower = XBuffer.ReadInt(buffer, ref offset);
                index = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					state = XBuffer.ReadInt(buffer, ref offset);
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
                if(petBaseInfo==null)
                    //petBaseInfo = ClassCacheManager.New<EquipedPetInfo>();
					petBaseInfo = new EquipedPetInfo();
                petBaseInfo.WriteWithType(buffer, ref offset);
                XBuffer.WriteInt(precedeValue, buffer, ref offset);
                XBuffer.WriteInt(fightPower, buffer, ref offset);
                XBuffer.WriteInt(index, buffer, ref offset);
				XBuffer.WriteByte(_state, buffer, ref offset);
				if (_state == 1)
				{
					XBuffer.WriteInt(state, buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //下注信息
    public class BetInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public string name; // 名字
        
		public int level; // 等级
        
		public int rank; // 排名
        
		public int model; // 头像模型
        
		public int title; // 称号
        
		public float odds; // 赔率
        
		public long roleId; // 角色id
        

        //鏋勯�犲嚱鏁�
        public BetInfo() : base()
        {
			
			name = "";
			level = 0;
            
			rank = 0;
            
			model = 0;
            
			title = 0;
            
			odds = 0f;
			roleId = 0L;

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			name = "";
			level = 0;
            
			rank = 0;
            
			model = 0;
            
			title = 0;
            
			odds = 0f;
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
                TypeEnum _real_type_;
                name = XBuffer.ReadString(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                rank = XBuffer.ReadInt(buffer, ref offset);
                model = XBuffer.ReadInt(buffer, ref offset);
                title = XBuffer.ReadInt(buffer, ref offset);
                odds = XBuffer.ReadFloat(buffer, ref offset);
                roleId = XBuffer.ReadLong(buffer, ref offset);

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
                XBuffer.WriteInt(rank, buffer, ref offset);
                XBuffer.WriteInt(model, buffer, ref offset);
                XBuffer.WriteInt(title, buffer, ref offset);
                XBuffer.WriteFloat(odds, buffer, ref offset);
                XBuffer.WriteLong(roleId, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //主赛场人物信息
    public class MainInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public long roleId; // 角色id
        
		public int rank; // 排名
        
		public string name; // 名字
        
		public string guildName; // 公会名
        
        public List<EquipedPetInfo> petBaseInfos{get; protected set;} //宠物信息

        //鏋勯�犲嚱鏁�
        public MainInfo() : base()
        {
            petBaseInfos = new List<EquipedPetInfo>(); //宠物信息
			
			roleId = 0L;
			rank = 0;
            
			name = "";
			guildName = "";

            petBaseInfos.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			rank = 0;
            
			name = "";
			guildName = "";

            petBaseInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = petBaseInfos.Count; a < b; ++a)
            {
                //var _value_ = petBaseInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				petBaseInfos[a] = null;
            }
            petBaseInfos.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                roleId = XBuffer.ReadLong(buffer, ref offset);
                rank = XBuffer.ReadInt(buffer, ref offset);
                name = XBuffer.ReadString(buffer, ref offset);
                guildName = XBuffer.ReadString(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    EquipedPetInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<EquipedPetInfo>();
					_value_ = new EquipedPetInfo();
                    _value_.Read(buffer, ref offset);
                    petBaseInfos.Add(_value_);
                }
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
                XBuffer.WriteLong(roleId, buffer, ref offset);
                XBuffer.WriteInt(rank, buffer, ref offset);
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteString(guildName, buffer, ref offset);

                XBuffer.WriteShort((short)petBaseInfos.Count,buffer, ref offset);
                for (int a = 0; a < petBaseInfos.Count; ++a)
                {
					if(petBaseInfos[a] == null)
						UnityEngine.Debug.LogError("petBaseInfos has nil item, idx == " + a);
					else
						petBaseInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //赛程战斗信息
    public class FightInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int index; // 场次
        
		public int __boxstate; // 宝箱状态 0已开 1未开
		private byte _boxstate = 0; // 宝箱状态 0已开 1未开 tag
		
		public bool hasBoxstate()
		{
			return this._boxstate == 1;
		}
		
		public int boxstate
		{
			set
			{
				_boxstate = 1;
				__boxstate = value;
			}
			
			get
			{
				return __boxstate;
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
        
		public string __name; // 名字
		private byte _name = 0; // 名字 tag
		
		public bool hasName()
		{
			return this._name == 1;
		}
		
		public string name
		{
			set
			{
				_name = 1;
				__name = value;
			}
			
			get
			{
				return __name;
			}
		}
        
		public long __roleId; // 角色id
		private byte _roleId = 0; // 角色id tag
		
		public bool hasRoleId()
		{
			return this._roleId == 1;
		}
		
		public long roleId
		{
			set
			{
				_roleId = 1;
				__roleId = value;
			}
			
			get
			{
				return __roleId;
			}
		}
        
		public int __time; // 倒计时 -1战斗中
		private byte _time = 0; // 倒计时 -1战斗中 tag
		
		public bool hasTime()
		{
			return this._time == 1;
		}
		
		public int time
		{
			set
			{
				_time = 1;
				__time = value;
			}
			
			get
			{
				return __time;
			}
		}
        
		public int __result; // 0胜利 1失败
		private byte _result = 0; // 0胜利 1失败 tag
		
		public bool hasResult()
		{
			return this._result == 1;
		}
		
		public int result
		{
			set
			{
				_result = 1;
				__result = value;
			}
			
			get
			{
				return __result;
			}
		}
        
        public List<EquipedPetInfo> petBaseInfo{get; protected set;} //对战宠物

        //鏋勯�犲嚱鏁�
        public FightInfo() : base()
        {
            petBaseInfo = new List<EquipedPetInfo>(); //对战宠物
			
			index = 0;
            
			_boxstate = 0;
			__boxstate = 0;
            
			_level = 0;
			__level = 0;
            
			_name = 0;
			__name = "";
			_roleId = 0;
			__roleId = 0L;
			_time = 0;
			__time = 0;
            
			_result = 0;
			__result = 0;
            

            petBaseInfo.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			index = 0;
            
			_boxstate = 0;
			__boxstate = 0;
            
			_level = 0;
			__level = 0;
            
			_name = 0;
			__name = "";
			_roleId = 0;
			__roleId = 0L;
			_time = 0;
			__time = 0;
            
			_result = 0;
			__result = 0;
            

            petBaseInfo.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = petBaseInfo.Count; a < b; ++a)
            {
                //var _value_ = petBaseInfo[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				petBaseInfo[a] = null;
            }
            petBaseInfo.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                index = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					boxstate = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					level = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					name = XBuffer.ReadString(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					roleId = XBuffer.ReadLong(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					time = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					result = XBuffer.ReadInt(buffer, ref offset);
				}

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    EquipedPetInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<EquipedPetInfo>();
					_value_ = new EquipedPetInfo();
                    _value_.Read(buffer, ref offset);
                    petBaseInfo.Add(_value_);
                }
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
                XBuffer.WriteInt(index, buffer, ref offset);
				XBuffer.WriteByte(_boxstate, buffer, ref offset);
				if (_boxstate == 1)
				{
					XBuffer.WriteInt(boxstate, buffer, ref offset);
				}
				XBuffer.WriteByte(_level, buffer, ref offset);
				if (_level == 1)
				{
					XBuffer.WriteInt(level, buffer, ref offset);
				}
				XBuffer.WriteByte(_name, buffer, ref offset);
				if (_name == 1)
				{
					XBuffer.WriteString(name, buffer, ref offset);
				}
				XBuffer.WriteByte(_roleId, buffer, ref offset);
				if (_roleId == 1)
				{
					XBuffer.WriteLong(roleId, buffer, ref offset);
				}
				XBuffer.WriteByte(_time, buffer, ref offset);
				if (_time == 1)
				{
					XBuffer.WriteInt(time, buffer, ref offset);
				}
				XBuffer.WriteByte(_result, buffer, ref offset);
				if (_result == 1)
				{
					XBuffer.WriteInt(result, buffer, ref offset);
				}

                XBuffer.WriteShort((short)petBaseInfo.Count,buffer, ref offset);
                for (int a = 0; a < petBaseInfo.Count; ++a)
                {
					if(petBaseInfo[a] == null)
						UnityEngine.Debug.LogError("petBaseInfo has nil item, idx == " + a);
					else
						petBaseInfo[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //8强赛玩家信息
    public class EightRoleInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public long roleId; // 角色id
        
		public string name; // 名字
        
		public string guildName; // 公会名
        
		public int fightPower; // 战斗力
        
		public int model; // 头像模型
        

        //鏋勯�犲嚱鏁�
        public EightRoleInfo() : base()
        {
			
			roleId = 0L;
			name = "";
			guildName = "";
			fightPower = 0;
            
			model = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			name = "";
			guildName = "";
			fightPower = 0;
            
			model = 0;
            

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
                guildName = XBuffer.ReadString(buffer, ref offset);
                fightPower = XBuffer.ReadInt(buffer, ref offset);
                model = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteString(guildName, buffer, ref offset);
                XBuffer.WriteInt(fightPower, buffer, ref offset);
                XBuffer.WriteInt(model, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //8强赛玩家信息
    public class EightFightInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int index; // 场次8 4 2
        
		public long winer; // 赢家 -1刚匹配
        
        public List<EightRoleInfo> roles{get; protected set;} //对战角色

        //鏋勯�犲嚱鏁�
        public EightFightInfo() : base()
        {
            roles = new List<EightRoleInfo>(); //对战角色
			
			index = 0;
            
			winer = 0L;

            roles.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			index = 0;
            
			winer = 0L;

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
                index = XBuffer.ReadInt(buffer, ref offset);
                winer = XBuffer.ReadLong(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    EightRoleInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<EightRoleInfo>();
					_value_ = new EightRoleInfo();
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
            XBuffer.WriteByte(6, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(index, buffer, ref offset);
                XBuffer.WriteLong(winer, buffer, ref offset);

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
    //8强赛信息
    public class EightInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public EightRoleInfo winer; // 赢家
        
		public EightRoleInfo loser; // 输家
        
		public int index; // 场次8 4 2
        

        //鏋勯�犲嚱鏁�
        public EightInfo() : base()
        {
			
			//winer = ClassCacheManager.New<EightRoleInfo>();
			winer = new EightRoleInfo();
			//loser = ClassCacheManager.New<EightRoleInfo>();
			loser = new EightRoleInfo();
			index = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//winer = ClassCacheManager.New<EightRoleInfo>();
			winer = new EightRoleInfo();
			//loser = ClassCacheManager.New<EightRoleInfo>();
			loser = new EightRoleInfo();
			index = 0;
            

        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			winer = null;
			loser = null;

        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //winer = ClassCacheManager.New<EightRoleInfo>();
				winer = new EightRoleInfo();
                winer.Read(buffer, ref offset);
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //loser = ClassCacheManager.New<EightRoleInfo>();
				loser = new EightRoleInfo();
                loser.Read(buffer, ref offset);
                index = XBuffer.ReadInt(buffer, ref offset);

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
                if(winer==null)
                    //winer = ClassCacheManager.New<EightRoleInfo>();
					winer = new EightRoleInfo();
                winer.WriteWithType(buffer, ref offset);
                if(loser==null)
                    //loser = ClassCacheManager.New<EightRoleInfo>();
					loser = new EightRoleInfo();
                loser.WriteWithType(buffer, ref offset);
                XBuffer.WriteInt(index, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //队伍信息
    public class TeamInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int petId; // 宠物id
        
		public int index; // 顺序
        

        //鏋勯�犲嚱鏁�
        public TeamInfo() : base()
        {
			
			petId = 0;
            
			index = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
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
                TypeEnum _real_type_;
                petId = XBuffer.ReadInt(buffer, ref offset);
                index = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
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
                XBuffer.WriteInt(petId, buffer, ref offset);
                XBuffer.WriteInt(index, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //请求参赛信息
    public class ReqFightInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117201;
		public long roleId; // 角色id

    	//鏋勯�犲嚱鏁�
    	public ReqFightInfo()
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
    //设置阵容
    public class ReqSetTeam : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117202;
        public List<TeamInfo> team{get;protected set;} //宠物id列表 顺序即为参赛顺序

    	//鏋勯�犲嚱鏁�
    	public ReqSetTeam()
    	{
            team = new List<TeamInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            team.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = team.Count; a < b; ++a)
            {
				team[a] = null;
                //var _value_ = team[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            team.Clear();
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
                    TeamInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<TeamInfo>();
					_value_ = new TeamInfo();
                    _value_.Read(buffer, ref offset);
                    team.Add(_value_);
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

                XBuffer.WriteShort((short)team.Count, buffer, ref offset);
                for(int a = 0; a < team.Count; ++a)
                {
					if(team[a] == null)
						UnityEngine.Debug.LogError("team has nil item, idx == " + a);
					else
						team[a].WriteWithType(buffer, ref offset);
                }
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
        public const int MsgId = 117203;
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
    //下注信息
    public class ReqBetInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117204;

    	//鏋勯�犲嚱鏁�
    	public ReqBetInfo()
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
    //下注
    public class ReqBet : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117205;
		public int type; // 0普通 1高级
		public long roleId; // 角色id

    	//鏋勯�犲嚱鏁�
    	public ReqBet()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			type = 0;
            
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
                type = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(type,buffer, ref offset);
					XBuffer.WriteLong(roleId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //打开宝箱
    public class ReqOpenBox : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117211;
		public int index; // 场次下标

    	//鏋勯�犲嚱鏁�
    	public ReqOpenBox()
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
    //打开主赛场界面
    public class ReqOpenMainInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117206;

    	//鏋勯�犲嚱鏁�
    	public ReqOpenMainInfo()
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
    //关闭主赛场信息
    public class ReqCloseMainInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117207;

    	//鏋勯�犲嚱鏁�
    	public ReqCloseMainInfo()
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
    //打开我的赛程界面
    public class ReqOpenSelfInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117208;

    	//鏋勯�犲嚱鏁�
    	public ReqOpenSelfInfo()
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
    //关闭我的赛程界面
    public class ReqCloseSelfInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117209;

    	//鏋勯�犲嚱鏁�
    	public ReqCloseSelfInfo()
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
    //昨日回顾
    public class ReqYesterday : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117210;

    	//鏋勯�犲嚱鏁�
    	public ReqYesterday()
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
    //开启活动报名
    public class ResOpenSignUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117101;
		public bool join; // 是否报名

    	//鏋勯�犲嚱鏁�
    	public ResOpenSignUp()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			join = false;
            join = false;
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
        		join = XBuffer.ReadBool(buffer, ref offset);

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
					XBuffer.WriteBool(join,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //开始前倒计时
    public class ResCountDown : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117102;
		public int time; // 倒计时时间 秒

    	//鏋勯�犲嚱鏁�
    	public ResCountDown()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			time = 0;
            
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
                time = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(time,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //开启活动
    public class ResOpen : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117103;

    	//鏋勯�犲嚱鏁�
    	public ResOpen()
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
    //下注信息
    public class ResBetInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117204;
		public long __roleId; // 下注对象
		private byte _roleId = 0; // 下注对象 tag
		
		public bool hasRoleId()
		{
			return this._roleId == 1;
		}
		
		public long roleId
		{
			set
			{
				_roleId = 1;
				__roleId = value;
			}
			
			get
			{
				return __roleId;
			}
		}
		public int __type; // 0普通 1高级
		private byte _type = 0; // 0普通 1高级 tag
		
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
        public List<BetInfo> info{get;protected set;} //下注列表

    	//鏋勯�犲嚱鏁�
    	public ResBetInfo()
    	{
            info = new List<BetInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			_roleId = 0;
			__roleId = 0L;
			_type = 0;
			__type = 0;
            
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
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					roleId = XBuffer.ReadLong(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					type = XBuffer.ReadInt(buffer, ref offset);
				}

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    BetInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<BetInfo>();
					_value_ = new BetInfo();
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
				XBuffer.WriteByte(_roleId,buffer, ref offset);
				if (_roleId == 1)
				{
					XBuffer.WriteLong(roleId,buffer, ref offset);
				}
				XBuffer.WriteByte(_type,buffer, ref offset);
				if (_type == 1)
				{
					XBuffer.WriteInt(type,buffer, ref offset);
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
    //主赛场信息
    public class ResMainInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117105;
		public int num; // 已报名人数
		public bool join; // 是否已报名
		public int playId; // 玩法类型
        public List<MainInfo> mainInfo{get;protected set;} //前3信息

    	//鏋勯�犲嚱鏁�
    	public ResMainInfo()
    	{
            mainInfo = new List<MainInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			num = 0;
            
			join = false;
            join = false;
			playId = 0;
            
            mainInfo.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = mainInfo.Count; a < b; ++a)
            {
				mainInfo[a] = null;
                //var _value_ = mainInfo[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            mainInfo.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                num = XBuffer.ReadInt(buffer, ref offset);
        		join = XBuffer.ReadBool(buffer, ref offset);
                playId = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    MainInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<MainInfo>();
					_value_ = new MainInfo();
                    _value_.Read(buffer, ref offset);
                    mainInfo.Add(_value_);
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
					XBuffer.WriteBool(join,buffer, ref offset);
					XBuffer.WriteInt(playId,buffer, ref offset);

                XBuffer.WriteShort((short)mainInfo.Count, buffer, ref offset);
                for(int a = 0; a < mainInfo.Count; ++a)
                {
					if(mainInfo[a] == null)
						UnityEngine.Debug.LogError("mainInfo has nil item, idx == " + a);
					else
						mainInfo[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //设置阵容
    public class ResSetTeam : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117106;
        public List<TeamInfo> team{get;protected set;} //宠物id列表 顺序即为参赛顺序

    	//鏋勯�犲嚱鏁�
    	public ResSetTeam()
    	{
            team = new List<TeamInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            team.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = team.Count; a < b; ++a)
            {
				team[a] = null;
                //var _value_ = team[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            team.Clear();
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
                    TeamInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<TeamInfo>();
					_value_ = new TeamInfo();
                    _value_.Read(buffer, ref offset);
                    team.Add(_value_);
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

                XBuffer.WriteShort((short)team.Count, buffer, ref offset);
                for(int a = 0; a < team.Count; ++a)
                {
					if(team[a] == null)
						UnityEngine.Debug.LogError("team has nil item, idx == " + a);
					else
						team[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //匹配对手
    public class ResMatchTarget : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117107;
		public FightInfo fightInfo; // 对战信息

    	//鏋勯�犲嚱鏁�
    	public ResMatchTarget()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//fightInfo = ClassCacheManager.New<FightInfo>();
			fightInfo = new FightInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			fightInfo = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //fightInfo = ClassCacheManager.New<FightInfo>();
				fightInfo = new FightInfo();
                fightInfo.Read(buffer, ref offset);

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
					if(fightInfo == null)
						//fightInfo = ClassCacheManager.New<FightInfo>();
						fightInfo = new FightInfo();
					fightInfo.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //参赛目标信息
    public class ResTargetFightInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117108;
		public string name; // 名字
		public int level; // 等级
		public int online; // 0在线 1离线
		public int winNum; // 胜利次数
		public int failedNum; // 失败次数
        public List<BaseInfo> baseInfo{get;protected set;} //基本信息

    	//鏋勯�犲嚱鏁�
    	public ResTargetFightInfo()
    	{
            baseInfo = new List<BaseInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			name = "";
			level = 0;
            
			online = 0;
            
			winNum = 0;
            
			failedNum = 0;
            
            baseInfo.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = baseInfo.Count; a < b; ++a)
            {
				baseInfo[a] = null;
                //var _value_ = baseInfo[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            baseInfo.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                name = XBuffer.ReadString(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                online = XBuffer.ReadInt(buffer, ref offset);
                winNum = XBuffer.ReadInt(buffer, ref offset);
                failedNum = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    BaseInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<BaseInfo>();
					_value_ = new BaseInfo();
                    _value_.Read(buffer, ref offset);
                    baseInfo.Add(_value_);
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
					XBuffer.WriteInt(level,buffer, ref offset);
					XBuffer.WriteInt(online,buffer, ref offset);
					XBuffer.WriteInt(winNum,buffer, ref offset);
					XBuffer.WriteInt(failedNum,buffer, ref offset);

                XBuffer.WriteShort((short)baseInfo.Count, buffer, ref offset);
                for(int a = 0; a < baseInfo.Count; ++a)
                {
					if(baseInfo[a] == null)
						UnityEngine.Debug.LogError("baseInfo has nil item, idx == " + a);
					else
						baseInfo[a].WriteWithType(buffer, ref offset);
                }
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
        public const int MsgId = 117109;
		public int id; // 道具Id
		public int num; // 数量
		public bool crit; // 暴击

    	//鏋勯�犲嚱鏁�
    	public ResExchange()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			num = 0;
            
			crit = false;
            crit = false;
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
                num = XBuffer.ReadInt(buffer, ref offset);
        		crit = XBuffer.ReadBool(buffer, ref offset);

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
					XBuffer.WriteInt(num,buffer, ref offset);
					XBuffer.WriteBool(crit,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //主赛场赛程
    public class ResCourseInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117110;
		public int index; // 场次
        public List<string> info{get;protected set;} //对战信息

    	//鏋勯�犲嚱鏁�
    	public ResCourseInfo()
    	{
            info = new List<string>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			index = 0;
            
            info.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            info.Clear();
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
                    var _value_ = XBuffer.ReadString(buffer, ref offset);
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
					XBuffer.WriteInt(index,buffer, ref offset);

                XBuffer.WriteShort((short)info.Count, buffer, ref offset);
                for(int a = 0; a < info.Count; ++a)
                {
                    XBuffer.WriteString(info[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //我的赛程
    public class ResSelfInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117112;
		public int core; // 积分
		public int winNum; // 胜利次数
		public int failedNum; // 失败次数
		public int state; // 0进行中 1已结束未进 2已结束进8强
        public List<FightInfo> infos{get;protected set;} //赛程战斗信息

    	//鏋勯�犲嚱鏁�
    	public ResSelfInfo()
    	{
            infos = new List<FightInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			core = 0;
            
			winNum = 0;
            
			failedNum = 0;
            
			state = 0;
            
            infos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = infos.Count; a < b; ++a)
            {
				infos[a] = null;
                //var _value_ = infos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            infos.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                core = XBuffer.ReadInt(buffer, ref offset);
                winNum = XBuffer.ReadInt(buffer, ref offset);
                failedNum = XBuffer.ReadInt(buffer, ref offset);
                state = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    FightInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<FightInfo>();
					_value_ = new FightInfo();
                    _value_.Read(buffer, ref offset);
                    infos.Add(_value_);
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
					XBuffer.WriteInt(core,buffer, ref offset);
					XBuffer.WriteInt(winNum,buffer, ref offset);
					XBuffer.WriteInt(failedNum,buffer, ref offset);
					XBuffer.WriteInt(state,buffer, ref offset);

                XBuffer.WriteShort((short)infos.Count, buffer, ref offset);
                for(int a = 0; a < infos.Count; ++a)
                {
					if(infos[a] == null)
						UnityEngine.Debug.LogError("infos has nil item, idx == " + a);
					else
						infos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //活动结束
    public class ResOver : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117113;

    	//鏋勯�犲嚱鏁�
    	public ResOver()
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
    //昨日回顾
    public class ResYesterday : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117114;
		public int core; // 积分
		public int winNum; // 胜利次数
		public int failedNum; // 失败次数
		public int rank; // 我的昨日排名
		public int playId; // 昨日玩法id
        public List<EightInfo> infos{get;protected set;} //8强信息
        public List<FightInfo> myinfos{get;protected set;} //我的赛程战斗信息

    	//鏋勯�犲嚱鏁�
    	public ResYesterday()
    	{
            infos = new List<EightInfo>();
            myinfos = new List<FightInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			core = 0;
            
			winNum = 0;
            
			failedNum = 0;
            
			rank = 0;
            
			playId = 0;
            
            infos.Clear();
            myinfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = infos.Count; a < b; ++a)
            {
				infos[a] = null;
                //var _value_ = infos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            infos.Clear();
            for (int a = 0,b = myinfos.Count; a < b; ++a)
            {
				myinfos[a] = null;
                //var _value_ = myinfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            myinfos.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                core = XBuffer.ReadInt(buffer, ref offset);
                winNum = XBuffer.ReadInt(buffer, ref offset);
                failedNum = XBuffer.ReadInt(buffer, ref offset);
                rank = XBuffer.ReadInt(buffer, ref offset);
                playId = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    EightInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<EightInfo>();
					_value_ = new EightInfo();
                    _value_.Read(buffer, ref offset);
                    infos.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    FightInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<FightInfo>();
					_value_ = new FightInfo();
                    _value_.Read(buffer, ref offset);
                    myinfos.Add(_value_);
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
					XBuffer.WriteInt(core,buffer, ref offset);
					XBuffer.WriteInt(winNum,buffer, ref offset);
					XBuffer.WriteInt(failedNum,buffer, ref offset);
					XBuffer.WriteInt(rank,buffer, ref offset);
					XBuffer.WriteInt(playId,buffer, ref offset);

                XBuffer.WriteShort((short)infos.Count, buffer, ref offset);
                for(int a = 0; a < infos.Count; ++a)
                {
					if(infos[a] == null)
						UnityEngine.Debug.LogError("infos has nil item, idx == " + a);
					else
						infos[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)myinfos.Count, buffer, ref offset);
                for(int a = 0; a < myinfos.Count; ++a)
                {
					if(myinfos[a] == null)
						UnityEngine.Debug.LogError("myinfos has nil item, idx == " + a);
					else
						myinfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //8强匹配信息
    public class ResEightMatchInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117115;
        public List<EightFightInfo> info{get;protected set;} //8强信息

    	//鏋勯�犲嚱鏁�
    	public ResEightMatchInfo()
    	{
            info = new List<EightFightInfo>();
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
                    EightFightInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<EightFightInfo>();
					_value_ = new EightFightInfo();
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
    //打开宝箱
    public class ResOpenBox : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 117216;
		public int index; // 场次下标
		public int state; // 状态

    	//鏋勯�犲嚱鏁�
    	public ResOpenBox()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			index = 0;
            
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
                index = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(index,buffer, ref offset);
					XBuffer.WriteInt(state,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}