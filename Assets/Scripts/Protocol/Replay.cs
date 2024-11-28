//Auto generated, do not edit it
//回放

using System;
using System.IO;
using System.Collections.Generic;
using Message.Fight;

namespace Message.Replay
{
    public enum TypeEnum
    {
        MsgActor = 1,
        FightCMD = 2,
        TurnInfo = 3,
        WaveInfo = 4,
        ReplayInfo = 5,
    }

    //实体数据
    public class MsgActor : BaseMsgStruct
    {
		public int tmpId; // 模板id
        
		public int type; // 实体类型
        
		public int camp; // 实体阵营
        
		public long actorId; // 实体id
        
		public int gridId; // 实体站位
        
		public int __star; // 宠物星级
		private byte _star = 0; // 宠物星级 tag
		
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
        
		public int __color; // 宠物品级
		private byte _color = 0; // 宠物品级 tag
		
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
        
        public List<int> propertyTypes{get; protected set;} //所有属性类型
        public List<long> basePropertyVals{get; protected set;} //基础属性
        public List<long> propertyVals{get; protected set;} //属性
        public List<SkillParam> skills{get; protected set;} //技能id

        //鏋勯�犲嚱鏁�
        public MsgActor() : base()
        {
            propertyTypes = new List<int>(); //所有属性类型
            basePropertyVals = new List<long>(); //基础属性
            propertyVals = new List<long>(); //属性
            skills = new List<SkillParam>(); //技能id
			
			tmpId = 0;
            
			type = 0;
            
			camp = 0;
            
			actorId = 0L;
			gridId = 0;
            
			_star = 0;
			__star = 0;
            
			_color = 0;
			__color = 0;
            

            propertyTypes.Clear();
            basePropertyVals.Clear();
            propertyVals.Clear();
            skills.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			tmpId = 0;
            
			type = 0;
            
			camp = 0;
            
			actorId = 0L;
			gridId = 0;
            
			_star = 0;
			__star = 0;
            
			_color = 0;
			__color = 0;
            

            propertyTypes.Clear();
            basePropertyVals.Clear();
            propertyVals.Clear();
            skills.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            propertyTypes.Clear();
            basePropertyVals.Clear();
            propertyVals.Clear();
            for (int a = 0,b = skills.Count; a < b; ++a)
            {
                //var _value_ = skills[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				skills[a] = null;
            }
            skills.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                tmpId = XBuffer.ReadInt(buffer, ref offset);
                type = XBuffer.ReadInt(buffer, ref offset);
                camp = XBuffer.ReadInt(buffer, ref offset);
                actorId = XBuffer.ReadLong(buffer, ref offset);
                gridId = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					star = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					color = XBuffer.ReadInt(buffer, ref offset);
				}

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    var _value_ = XBuffer.ReadInt(buffer, ref offset);
                    propertyTypes.Add(_value_);
                }
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    var _value_ = XBuffer.ReadLong(buffer, ref offset);
                    basePropertyVals.Add(_value_);
                }
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    var _value_ = XBuffer.ReadLong(buffer, ref offset);
                    propertyVals.Add(_value_);
                }
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    SkillParam _value_ = null;
                    //_value_ = ClassCacheManager.New<SkillParam>();
					_value_ = new SkillParam();
                    _value_.Read(buffer, ref offset);
                    skills.Add(_value_);
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
                XBuffer.WriteInt(tmpId, buffer, ref offset);
                XBuffer.WriteInt(type, buffer, ref offset);
                XBuffer.WriteInt(camp, buffer, ref offset);
                XBuffer.WriteLong(actorId, buffer, ref offset);
                XBuffer.WriteInt(gridId, buffer, ref offset);
				XBuffer.WriteByte(_star, buffer, ref offset);
				if (_star == 1)
				{
					XBuffer.WriteInt(star, buffer, ref offset);
				}
				XBuffer.WriteByte(_color, buffer, ref offset);
				if (_color == 1)
				{
					XBuffer.WriteInt(color, buffer, ref offset);
				}

                XBuffer.WriteShort((short)propertyTypes.Count,buffer, ref offset);
                for (int a = 0; a < propertyTypes.Count; ++a)
                {
                    XBuffer.WriteInt(propertyTypes[a], buffer, ref offset);
                }
                XBuffer.WriteShort((short)basePropertyVals.Count,buffer, ref offset);
                for (int a = 0; a < basePropertyVals.Count; ++a)
                {
                    XBuffer.WriteLong(basePropertyVals[a], buffer, ref offset);
                }
                XBuffer.WriteShort((short)propertyVals.Count,buffer, ref offset);
                for (int a = 0; a < propertyVals.Count; ++a)
                {
                    XBuffer.WriteLong(propertyVals[a], buffer, ref offset);
                }
                XBuffer.WriteShort((short)skills.Count,buffer, ref offset);
                for (int a = 0; a < skills.Count; ++a)
                {
					if(skills[a] == null)
						UnityEngine.Debug.LogError("skills has nil item, idx == " + a);
					else
						skills[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //一次战斗指令
    public class FightCMD : BaseMsgStruct
    {
		public long actorId; // roleid
        
		public long targetId; // 目标id
        
		public int skillId; // 技能id
        
		public int comboType; // 操作评分
        
		public bool isMasterSkill; // 是不是大招
        

        //鏋勯�犲嚱鏁�
        public FightCMD() : base()
        {
			
			actorId = 0L;
			targetId = 0L;
			skillId = 0;
            
			comboType = 0;
            
			isMasterSkill = false;
            isMasterSkill = false;

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			actorId = 0L;
			targetId = 0L;
			skillId = 0;
            
			comboType = 0;
            
			isMasterSkill = false;
            isMasterSkill = false;

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
                actorId = XBuffer.ReadLong(buffer, ref offset);
                targetId = XBuffer.ReadLong(buffer, ref offset);
                skillId = XBuffer.ReadInt(buffer, ref offset);
                comboType = XBuffer.ReadInt(buffer, ref offset);
                isMasterSkill = XBuffer.ReadBool(buffer, ref offset);

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
                XBuffer.WriteLong(actorId, buffer, ref offset);
                XBuffer.WriteLong(targetId, buffer, ref offset);
                XBuffer.WriteInt(skillId, buffer, ref offset);
                XBuffer.WriteInt(comboType, buffer, ref offset);
                XBuffer.WriteBool(isMasterSkill, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //一个回合的战斗指令
    public class TurnInfo : BaseMsgStruct
    {
        public List<FightCMD> turns{get; protected set;} //指令集合

        //鏋勯�犲嚱鏁�
        public TurnInfo() : base()
        {
            turns = new List<FightCMD>(); //指令集合
			

            turns.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);

            turns.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = turns.Count; a < b; ++a)
            {
                //var _value_ = turns[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				turns[a] = null;
            }
            turns.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    FightCMD _value_ = null;
                    //_value_ = ClassCacheManager.New<FightCMD>();
					_value_ = new FightCMD();
                    _value_.Read(buffer, ref offset);
                    turns.Add(_value_);
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

                XBuffer.WriteShort((short)turns.Count,buffer, ref offset);
                for (int a = 0; a < turns.Count; ++a)
                {
					if(turns[a] == null)
						UnityEngine.Debug.LogError("turns has nil item, idx == " + a);
					else
						turns[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //一局战斗
    public class WaveInfo : BaseMsgStruct
    {
		public int firstVal; // 主方视角先手值
        
		public int enemyFirstVal; // 敌方视角先手值
        
        public List<MsgActor> actorList{get; protected set;} //参战实体
        public List<TurnInfo> turnList{get; protected set;} //所有回合

        //鏋勯�犲嚱鏁�
        public WaveInfo() : base()
        {
            actorList = new List<MsgActor>(); //参战实体
            turnList = new List<TurnInfo>(); //所有回合
			
			firstVal = 0;
            
			enemyFirstVal = 0;
            

            actorList.Clear();
            turnList.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			firstVal = 0;
            
			enemyFirstVal = 0;
            

            actorList.Clear();
            turnList.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = actorList.Count; a < b; ++a)
            {
                //var _value_ = actorList[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				actorList[a] = null;
            }
            actorList.Clear();
            for (int a = 0,b = turnList.Count; a < b; ++a)
            {
                //var _value_ = turnList[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				turnList[a] = null;
            }
            turnList.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                firstVal = XBuffer.ReadInt(buffer, ref offset);
                enemyFirstVal = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    MsgActor _value_ = null;
                    //_value_ = ClassCacheManager.New<MsgActor>();
					_value_ = new MsgActor();
                    _value_.Read(buffer, ref offset);
                    actorList.Add(_value_);
                }
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    TurnInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<TurnInfo>();
					_value_ = new TurnInfo();
                    _value_.Read(buffer, ref offset);
                    turnList.Add(_value_);
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
                XBuffer.WriteInt(firstVal, buffer, ref offset);
                XBuffer.WriteInt(enemyFirstVal, buffer, ref offset);

                XBuffer.WriteShort((short)actorList.Count,buffer, ref offset);
                for (int a = 0; a < actorList.Count; ++a)
                {
					if(actorList[a] == null)
						UnityEngine.Debug.LogError("actorList has nil item, idx == " + a);
					else
						actorList[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)turnList.Count,buffer, ref offset);
                for (int a = 0; a < turnList.Count; ++a)
                {
					if(turnList[a] == null)
						UnityEngine.Debug.LogError("turnList has nil item, idx == " + a);
					else
						turnList[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //一场战斗
    public class ReplayInfo : BaseMsgStruct
    {
		public int version; // 战斗版本
        
		public int randomNum; // 战斗随机数
        
		public bool firstWin; // 战斗结果（主方胜利true）
        
		public string firstName; // 主视角方的名字
        
		public string enemyName; // 敌方视角的名字
        
        public List<WaveInfo> waveList{get; protected set;} //所有局数

        //鏋勯�犲嚱鏁�
        public ReplayInfo() : base()
        {
            waveList = new List<WaveInfo>(); //所有局数
			
			version = 0;
            
			randomNum = 0;
            
			firstWin = false;
            firstWin = false;
			firstName = "";
			enemyName = "";

            waveList.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			version = 0;
            
			randomNum = 0;
            
			firstWin = false;
            firstWin = false;
			firstName = "";
			enemyName = "";

            waveList.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = waveList.Count; a < b; ++a)
            {
                //var _value_ = waveList[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				waveList[a] = null;
            }
            waveList.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                version = XBuffer.ReadInt(buffer, ref offset);
                randomNum = XBuffer.ReadInt(buffer, ref offset);
                firstWin = XBuffer.ReadBool(buffer, ref offset);
                firstName = XBuffer.ReadString(buffer, ref offset);
                enemyName = XBuffer.ReadString(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    WaveInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<WaveInfo>();
					_value_ = new WaveInfo();
                    _value_.Read(buffer, ref offset);
                    waveList.Add(_value_);
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
                XBuffer.WriteInt(version, buffer, ref offset);
                XBuffer.WriteInt(randomNum, buffer, ref offset);
                XBuffer.WriteBool(firstWin, buffer, ref offset);
                XBuffer.WriteString(firstName, buffer, ref offset);
                XBuffer.WriteString(enemyName, buffer, ref offset);

                XBuffer.WriteShort((short)waveList.Count,buffer, ref offset);
                for (int a = 0; a < waveList.Count; ++a)
                {
					if(waveList[a] == null)
						UnityEngine.Debug.LogError("waveList has nil item, idx == " + a);
					else
						waveList[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //初始化功能解锁列表
    public class ResReplay : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 129201;
		public long replayId; // 战斗标示
		public ReplayInfo replay; // 战斗信息

    	//鏋勯�犲嚱鏁�
    	public ResReplay()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			replayId = 0L;
			//replay = ClassCacheManager.New<ReplayInfo>();
			replay = new ReplayInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			replay = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                replayId = XBuffer.ReadLong(buffer, ref offset);
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //replay = ClassCacheManager.New<ReplayInfo>();
				replay = new ReplayInfo();
                replay.Read(buffer, ref offset);

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
					XBuffer.WriteLong(replayId,buffer, ref offset);
					if(replay == null)
						//replay = ClassCacheManager.New<ReplayInfo>();
						replay = new ReplayInfo();
					replay.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //初始化功能解锁列表
    public class ReqReplay : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 129101;
		public long replayId; // 战斗标示
		public ReplayInfo replay; // 战斗信息

    	//鏋勯�犲嚱鏁�
    	public ReqReplay()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			replayId = 0L;
			//replay = ClassCacheManager.New<ReplayInfo>();
			replay = new ReplayInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			replay = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                replayId = XBuffer.ReadLong(buffer, ref offset);
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //replay = ClassCacheManager.New<ReplayInfo>();
				replay = new ReplayInfo();
                replay.Read(buffer, ref offset);

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
					XBuffer.WriteLong(replayId,buffer, ref offset);
					if(replay == null)
						//replay = ClassCacheManager.New<ReplayInfo>();
						replay = new ReplayInfo();
					replay.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}