//Auto generated, do not edit it
//战斗协议

using System;
using System.IO;
using System.Collections.Generic;
using Message.Pet;
using Message.Bag;
using Message.Dungeon;
using Message.Challenge;
using Message.Replay;

namespace Message.Fight
{
    public enum TypeEnum
    {
        FightProperty = 1,
        SkillParam = 2,
        FightParam = 3,
        BaseResult = 4,
        MissionResult = 5,
        ActivityDungeonResult = 6,
        TrailResult = 7,
        TeamFightResult = 8,
        GuildBossResult = 9,
        ArenaResult = 10,
    }

    //战斗属性
    public class FightProperty : BaseMsgStruct
    {
		public int propertyId; // 属性ID
        
		public long propertyValue; // 属性值
        

        //鏋勯�犲嚱鏁�
        public FightProperty() : base()
        {
			
			propertyId = 0;
            
			propertyValue = 0L;

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			propertyId = 0;
            
			propertyValue = 0L;

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
                propertyId = XBuffer.ReadInt(buffer, ref offset);
                propertyValue = XBuffer.ReadLong(buffer, ref offset);

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
                XBuffer.WriteInt(propertyId, buffer, ref offset);
                XBuffer.WriteLong(propertyValue, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //技能或效果参数
    public class SkillParam : BaseMsgStruct
    {
		public int id; // 技能或效果id
        
		public int level; // 等级
        

        //鏋勯�犲嚱鏁�
        public SkillParam() : base()
        {
			
			id = 0;
            
			level = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
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
                id = XBuffer.ReadInt(buffer, ref offset);
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
                XBuffer.WriteInt(level, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //战斗参数
    public class FightParam : BaseMsgStruct
    {
		public int petId; // 宠物ID
        
		public int pos; // 站位
        
        public List<FightProperty> fightPropertys{get; protected set;} //宠物的属性列表
        public List<SkillParam> skills{get; protected set;} //技能列表

        //鏋勯�犲嚱鏁�
        public FightParam() : base()
        {
            fightPropertys = new List<FightProperty>(); //宠物的属性列表
            skills = new List<SkillParam>(); //技能列表
			
			petId = 0;
            
			pos = 0;
            

            fightPropertys.Clear();
            skills.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			pos = 0;
            

            fightPropertys.Clear();
            skills.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = fightPropertys.Count; a < b; ++a)
            {
                //var _value_ = fightPropertys[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				fightPropertys[a] = null;
            }
            fightPropertys.Clear();
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
                petId = XBuffer.ReadInt(buffer, ref offset);
                pos = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    FightProperty _value_ = null;
                    //_value_ = ClassCacheManager.New<FightProperty>();
					_value_ = new FightProperty();
                    _value_.Read(buffer, ref offset);
                    fightPropertys.Add(_value_);
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
            XBuffer.WriteByte(3, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(petId, buffer, ref offset);
                XBuffer.WriteInt(pos, buffer, ref offset);

                XBuffer.WriteShort((short)fightPropertys.Count,buffer, ref offset);
                for (int a = 0; a < fightPropertys.Count; ++a)
                {
					if(fightPropertys[a] == null)
						UnityEngine.Debug.LogError("fightPropertys has nil item, idx == " + a);
					else
						fightPropertys[a].WriteWithType(buffer, ref offset);
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
    //战斗属性
    public class BaseResult : BaseMsgStruct
    {
		public int __result; // 结果
		private byte _result = 0; // 结果 tag
		
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
        

        //鏋勯�犲嚱鏁�
        public BaseResult() : base()
        {
			
			_result = 0;
			__result = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			_result = 0;
			__result = 0;
            

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
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					result = XBuffer.ReadInt(buffer, ref offset);
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
            XBuffer.WriteByte(4, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
				XBuffer.WriteByte(_result, buffer, ref offset);
				if (_result == 1)
				{
					XBuffer.WriteInt(result, buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //关卡卡结果
    public class MissionResult : BaseResult
    {
		public int __chapterStar; // 章节星星数
		private byte _chapterStar = 0; // 章节星星数 tag
		
		public bool hasChapterStar()
		{
			return this._chapterStar == 1;
		}
		
		public int chapterStar
		{
			set
			{
				_chapterStar = 1;
				__chapterStar = value;
			}
			
			get
			{
				return __chapterStar;
			}
		}
        
		public int __actId; // 关卡ID
		private byte _actId = 0; // 关卡ID tag
		
		public bool hasActId()
		{
			return this._actId == 1;
		}
		
		public int actId
		{
			set
			{
				_actId = 1;
				__actId = value;
			}
			
			get
			{
				return __actId;
			}
		}
        
		public int __openNewChapterFlag; // 是否开启新章节（0:没有开启，1：开启了）
		private byte _openNewChapterFlag = 0; // 是否开启新章节（0:没有开启，1：开启了） tag
		
		public bool hasOpenNewChapterFlag()
		{
			return this._openNewChapterFlag == 1;
		}
		
		public int openNewChapterFlag
		{
			set
			{
				_openNewChapterFlag = 1;
				__openNewChapterFlag = value;
			}
			
			get
			{
				return __openNewChapterFlag;
			}
		}
        
		public int __bestRecordId; // 当前可打最新关卡ID（没有因为等级去限制）
		private byte _bestRecordId = 0; // 当前可打最新关卡ID（没有因为等级去限制） tag
		
		public bool hasBestRecordId()
		{
			return this._bestRecordId == 1;
		}
		
		public int bestRecordId
		{
			set
			{
				_bestRecordId = 1;
				__bestRecordId = value;
			}
			
			get
			{
				return __bestRecordId;
			}
		}
        
		public Award __awards; // 战斗奖励（失败时没有数据）
		private byte _awards = 0; // 战斗奖励（失败时没有数据） tag
		
		public bool hasAwards()
		{
			return this._awards == 1;
		}
		
		public Award awards
		{
			set
			{
				_awards = 1;
				__awards = value;
			}
			
			get
			{
				return __awards;
			}
		}
        
		public int __petExp; // 宠物获得的经验
		private byte _petExp = 0; // 宠物获得的经验 tag
		
		public bool hasPetExp()
		{
			return this._petExp == 1;
		}
		
		public int petExp
		{
			set
			{
				_petExp = 1;
				__petExp = value;
			}
			
			get
			{
				return __petExp;
			}
		}
        
        public List<int> boxStatus{get; protected set;} //章节宝箱状态（-1：不可领取，0：可领取未打开，1：已领取）
        public List<PetExp> petInfos{get; protected set;} //获得经验的宠物信息
        public List<int> petStatus{get; protected set;} //宠物加经验后的状态（0：只加了经验，1：升级了，2：经验加满了）

        //鏋勯�犲嚱鏁�
        public MissionResult() : base()
        {
            boxStatus = new List<int>(); //章节宝箱状态（-1：不可领取，0：可领取未打开，1：已领取）
            petInfos = new List<PetExp>(); //获得经验的宠物信息
            petStatus = new List<int>(); //宠物加经验后的状态（0：只加了经验，1：升级了，2：经验加满了）
			
			_chapterStar = 0;
			__chapterStar = 0;
            
			_actId = 0;
			__actId = 0;
            
			_openNewChapterFlag = 0;
			__openNewChapterFlag = 0;
            
			_bestRecordId = 0;
			__bestRecordId = 0;
            
			_awards = 0;
			//__awards = ClassCacheManager.New<Award>();
			__awards = new Award();
			_petExp = 0;
			__petExp = 0;
            

            boxStatus.Clear();
            petInfos.Clear();
            petStatus.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			_chapterStar = 0;
			__chapterStar = 0;
            
			_actId = 0;
			__actId = 0;
            
			_openNewChapterFlag = 0;
			__openNewChapterFlag = 0;
            
			_bestRecordId = 0;
			__bestRecordId = 0;
            
			_awards = 0;
			//__awards = ClassCacheManager.New<Award>();
			__awards = new Award();
			_petExp = 0;
			__petExp = 0;
            

            boxStatus.Clear();
            petInfos.Clear();
            petStatus.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			__awards = null;

            boxStatus.Clear();
            for (int a = 0,b = petInfos.Count; a < b; ++a)
            {
                //var _value_ = petInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				petInfos[a] = null;
            }
            petInfos.Clear();
            petStatus.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					chapterStar = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					actId = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					openNewChapterFlag = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					bestRecordId = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					_real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
					//awards = ClassCacheManager.New<Award>();
					awards = new Award();
					awards.Read(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					petExp = XBuffer.ReadInt(buffer, ref offset);
				}

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    var _value_ = XBuffer.ReadInt(buffer, ref offset);
                    boxStatus.Add(_value_);
                }
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    PetExp _value_ = null;
                    //_value_ = ClassCacheManager.New<PetExp>();
					_value_ = new PetExp();
                    _value_.Read(buffer, ref offset);
                    petInfos.Add(_value_);
                }
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    var _value_ = XBuffer.ReadInt(buffer, ref offset);
                    petStatus.Add(_value_);
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
				XBuffer.WriteByte(_chapterStar, buffer, ref offset);
				if (_chapterStar == 1)
				{
					XBuffer.WriteInt(chapterStar, buffer, ref offset);
				}
				XBuffer.WriteByte(_actId, buffer, ref offset);
				if (_actId == 1)
				{
					XBuffer.WriteInt(actId, buffer, ref offset);
				}
				XBuffer.WriteByte(_openNewChapterFlag, buffer, ref offset);
				if (_openNewChapterFlag == 1)
				{
					XBuffer.WriteInt(openNewChapterFlag, buffer, ref offset);
				}
				XBuffer.WriteByte(_bestRecordId, buffer, ref offset);
				if (_bestRecordId == 1)
				{
					XBuffer.WriteInt(bestRecordId, buffer, ref offset);
				}
				XBuffer.WriteByte(_awards, buffer, ref offset);
				if (_awards == 1)
				{
					if(awards==null)
						//awards = ClassCacheManager.New<Award>();
						awards = new Award();
					awards.WriteWithType(buffer, ref offset);
				}
				XBuffer.WriteByte(_petExp, buffer, ref offset);
				if (_petExp == 1)
				{
					XBuffer.WriteInt(petExp, buffer, ref offset);
				}

                XBuffer.WriteShort((short)boxStatus.Count,buffer, ref offset);
                for (int a = 0; a < boxStatus.Count; ++a)
                {
                    XBuffer.WriteInt(boxStatus[a], buffer, ref offset);
                }
                XBuffer.WriteShort((short)petInfos.Count,buffer, ref offset);
                for (int a = 0; a < petInfos.Count; ++a)
                {
					if(petInfos[a] == null)
						UnityEngine.Debug.LogError("petInfos has nil item, idx == " + a);
					else
						petInfos[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)petStatus.Count,buffer, ref offset);
                for (int a = 0; a < petStatus.Count; ++a)
                {
                    XBuffer.WriteInt(petStatus[a], buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //活动关卡
    public class ActivityDungeonResult : BaseResult
    {
		public int __hurtBlood; // 破坏血量
		private byte _hurtBlood = 0; // 破坏血量 tag
		
		public bool hasHurtBlood()
		{
			return this._hurtBlood == 1;
		}
		
		public int hurtBlood
		{
			set
			{
				_hurtBlood = 1;
				__hurtBlood = value;
			}
			
			get
			{
				return __hurtBlood;
			}
		}
        
		public int __score; // 评分
		private byte _score = 0; // 评分 tag
		
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
        
		public int __turnNum; // 回合数
		private byte _turnNum = 0; // 回合数 tag
		
		public bool hasTurnNum()
		{
			return this._turnNum == 1;
		}
		
		public int turnNum
		{
			set
			{
				_turnNum = 1;
				__turnNum = value;
			}
			
			get
			{
				return __turnNum;
			}
		}
        
		public int __samePetNum; // 相同的宠物数量
		private byte _samePetNum = 0; // 相同的宠物数量 tag
		
		public bool hasSamePetNum()
		{
			return this._samePetNum == 1;
		}
		
		public int samePetNum
		{
			set
			{
				_samePetNum = 1;
				__samePetNum = value;
			}
			
			get
			{
				return __samePetNum;
			}
		}
        
		public ActivityActBaseInfo activityActBaseInfo; // 相同的宠物数量
        
		public ActivityAct activityAct; // 相同的宠物数量
        
        public List<ItemInfo> items{get; protected set;} //道具列表

        //鏋勯�犲嚱鏁�
        public ActivityDungeonResult() : base()
        {
            items = new List<ItemInfo>(); //道具列表
			
			_hurtBlood = 0;
			__hurtBlood = 0;
            
			_score = 0;
			__score = 0;
            
			_turnNum = 0;
			__turnNum = 0;
            
			_samePetNum = 0;
			__samePetNum = 0;
            
			//activityActBaseInfo = ClassCacheManager.New<ActivityActBaseInfo>();
			activityActBaseInfo = new ActivityActBaseInfo();
			//activityAct = ClassCacheManager.New<ActivityAct>();
			activityAct = new ActivityAct();

            items.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			_hurtBlood = 0;
			__hurtBlood = 0;
            
			_score = 0;
			__score = 0;
            
			_turnNum = 0;
			__turnNum = 0;
            
			_samePetNum = 0;
			__samePetNum = 0;
            
			//activityActBaseInfo = ClassCacheManager.New<ActivityActBaseInfo>();
			activityActBaseInfo = new ActivityActBaseInfo();
			//activityAct = ClassCacheManager.New<ActivityAct>();
			activityAct = new ActivityAct();

            items.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			activityActBaseInfo = null;
			activityAct = null;

            for (int a = 0,b = items.Count; a < b; ++a)
            {
                //var _value_ = items[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				items[a] = null;
            }
            items.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					hurtBlood = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					score = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					turnNum = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					samePetNum = XBuffer.ReadInt(buffer, ref offset);
				}
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //activityActBaseInfo = ClassCacheManager.New<ActivityActBaseInfo>();
				activityActBaseInfo = new ActivityActBaseInfo();
                activityActBaseInfo.Read(buffer, ref offset);
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //activityAct = ClassCacheManager.New<ActivityAct>();
				activityAct = new ActivityAct();
                activityAct.Read(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    ItemInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<ItemInfo>();
					_value_ = new ItemInfo();
                    _value_.Read(buffer, ref offset);
                    items.Add(_value_);
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
				XBuffer.WriteByte(_hurtBlood, buffer, ref offset);
				if (_hurtBlood == 1)
				{
					XBuffer.WriteInt(hurtBlood, buffer, ref offset);
				}
				XBuffer.WriteByte(_score, buffer, ref offset);
				if (_score == 1)
				{
					XBuffer.WriteInt(score, buffer, ref offset);
				}
				XBuffer.WriteByte(_turnNum, buffer, ref offset);
				if (_turnNum == 1)
				{
					XBuffer.WriteInt(turnNum, buffer, ref offset);
				}
				XBuffer.WriteByte(_samePetNum, buffer, ref offset);
				if (_samePetNum == 1)
				{
					XBuffer.WriteInt(samePetNum, buffer, ref offset);
				}
                if(activityActBaseInfo==null)
                    //activityActBaseInfo = ClassCacheManager.New<ActivityActBaseInfo>();
					activityActBaseInfo = new ActivityActBaseInfo();
                activityActBaseInfo.WriteWithType(buffer, ref offset);
                if(activityAct==null)
                    //activityAct = ClassCacheManager.New<ActivityAct>();
					activityAct = new ActivityAct();
                activityAct.WriteWithType(buffer, ref offset);

                XBuffer.WriteShort((short)items.Count,buffer, ref offset);
                for (int a = 0; a < items.Count; ++a)
                {
					if(items[a] == null)
						UnityEngine.Debug.LogError("items has nil item, idx == " + a);
					else
						items[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //爬塔
    public class TrailResult : BaseResult
    {
		public TrialInfo trialInfo; // 终极试炼基础信息
        
		public int star; // 0：失败 1~3星星
        

        //鏋勯�犲嚱鏁�
        public TrailResult() : base()
        {
			
			//trialInfo = ClassCacheManager.New<TrialInfo>();
			trialInfo = new TrialInfo();
			star = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//trialInfo = ClassCacheManager.New<TrialInfo>();
			trialInfo = new TrialInfo();
			star = 0;
            

        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			trialInfo = null;

        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //trialInfo = ClassCacheManager.New<TrialInfo>();
				trialInfo = new TrialInfo();
                trialInfo.Read(buffer, ref offset);
                star = XBuffer.ReadInt(buffer, ref offset);

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
                if(trialInfo==null)
                    //trialInfo = ClassCacheManager.New<TrialInfo>();
					trialInfo = new TrialInfo();
                trialInfo.WriteWithType(buffer, ref offset);
                XBuffer.WriteInt(star, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //组队战
    public class TeamFightResult : BaseResult
    {
		public int completeTimes; // 今日已完成次数
        

        //鏋勯�犲嚱鏁�
        public TeamFightResult() : base()
        {
			
			completeTimes = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			completeTimes = 0;
            

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
                completeTimes = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(completeTimes, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //工会boss
    public class GuildBossResult : BaseResult
    {
		public long damage; // 伤害
        
        public List<ItemInfo> rewards{get; protected set;} //战斗奖励

        //鏋勯�犲嚱鏁�
        public GuildBossResult() : base()
        {
            rewards = new List<ItemInfo>(); //战斗奖励
			
			damage = 0L;

            rewards.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			damage = 0L;

            rewards.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = rewards.Count; a < b; ++a)
            {
                //var _value_ = rewards[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				rewards[a] = null;
            }
            rewards.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                damage = XBuffer.ReadLong(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    ItemInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<ItemInfo>();
					_value_ = new ItemInfo();
                    _value_.Read(buffer, ref offset);
                    rewards.Add(_value_);
                }
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
                XBuffer.WriteLong(damage, buffer, ref offset);

                XBuffer.WriteShort((short)rewards.Count,buffer, ref offset);
                for (int a = 0; a < rewards.Count; ++a)
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
    //竞技场结果
    public class ArenaResult : BaseResult
    {
		public int __winRank; // 胜利者排名
		private byte _winRank = 0; // 胜利者排名 tag
		
		public bool hasWinRank()
		{
			return this._winRank == 1;
		}
		
		public int winRank
		{
			set
			{
				_winRank = 1;
				__winRank = value;
			}
			
			get
			{
				return __winRank;
			}
		}
        
		public int __loserRank; // 失败者排名
		private byte _loserRank = 0; // 失败者排名 tag
		
		public bool hasLoserRank()
		{
			return this._loserRank == 1;
		}
		
		public int loserRank
		{
			set
			{
				_loserRank = 1;
				__loserRank = value;
			}
			
			get
			{
				return __loserRank;
			}
		}
        
		public string __winName; // 胜利者名字
		private byte _winName = 0; // 胜利者名字 tag
		
		public bool hasWinName()
		{
			return this._winName == 1;
		}
		
		public string winName
		{
			set
			{
				_winName = 1;
				__winName = value;
			}
			
			get
			{
				return __winName;
			}
		}
        
		public string __loserName; // 失败者名字
		private byte _loserName = 0; // 失败者名字 tag
		
		public bool hasLoserName()
		{
			return this._loserName == 1;
		}
		
		public string loserName
		{
			set
			{
				_loserName = 1;
				__loserName = value;
			}
			
			get
			{
				return __loserName;
			}
		}
        
		public int __upRank; // 胜利者上升排名
		private byte _upRank = 0; // 胜利者上升排名 tag
		
		public bool hasUpRank()
		{
			return this._upRank == 1;
		}
		
		public int upRank
		{
			set
			{
				_upRank = 1;
				__upRank = value;
			}
			
			get
			{
				return __upRank;
			}
		}
        
        public List<EquipedPetInfo> winPetInfos{get; protected set;} //胜利者宠物信息
        public List<EquipedPetInfo> loserPetInfos{get; protected set;} //失败者宠物信息

        //鏋勯�犲嚱鏁�
        public ArenaResult() : base()
        {
            winPetInfos = new List<EquipedPetInfo>(); //胜利者宠物信息
            loserPetInfos = new List<EquipedPetInfo>(); //失败者宠物信息
			
			_winRank = 0;
			__winRank = 0;
            
			_loserRank = 0;
			__loserRank = 0;
            
			_winName = 0;
			__winName = "";
			_loserName = 0;
			__loserName = "";
			_upRank = 0;
			__upRank = 0;
            

            winPetInfos.Clear();
            loserPetInfos.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			_winRank = 0;
			__winRank = 0;
            
			_loserRank = 0;
			__loserRank = 0;
            
			_winName = 0;
			__winName = "";
			_loserName = 0;
			__loserName = "";
			_upRank = 0;
			__upRank = 0;
            

            winPetInfos.Clear();
            loserPetInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = winPetInfos.Count; a < b; ++a)
            {
                //var _value_ = winPetInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				winPetInfos[a] = null;
            }
            winPetInfos.Clear();
            for (int a = 0,b = loserPetInfos.Count; a < b; ++a)
            {
                //var _value_ = loserPetInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				loserPetInfos[a] = null;
            }
            loserPetInfos.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					winRank = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					loserRank = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					winName = XBuffer.ReadString(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					loserName = XBuffer.ReadString(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					upRank = XBuffer.ReadInt(buffer, ref offset);
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
                    winPetInfos.Add(_value_);
                }
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    EquipedPetInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<EquipedPetInfo>();
					_value_ = new EquipedPetInfo();
                    _value_.Read(buffer, ref offset);
                    loserPetInfos.Add(_value_);
                }
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
				XBuffer.WriteByte(_winRank, buffer, ref offset);
				if (_winRank == 1)
				{
					XBuffer.WriteInt(winRank, buffer, ref offset);
				}
				XBuffer.WriteByte(_loserRank, buffer, ref offset);
				if (_loserRank == 1)
				{
					XBuffer.WriteInt(loserRank, buffer, ref offset);
				}
				XBuffer.WriteByte(_winName, buffer, ref offset);
				if (_winName == 1)
				{
					XBuffer.WriteString(winName, buffer, ref offset);
				}
				XBuffer.WriteByte(_loserName, buffer, ref offset);
				if (_loserName == 1)
				{
					XBuffer.WriteString(loserName, buffer, ref offset);
				}
				XBuffer.WriteByte(_upRank, buffer, ref offset);
				if (_upRank == 1)
				{
					XBuffer.WriteInt(upRank, buffer, ref offset);
				}

                XBuffer.WriteShort((short)winPetInfos.Count,buffer, ref offset);
                for (int a = 0; a < winPetInfos.Count; ++a)
                {
					if(winPetInfos[a] == null)
						UnityEngine.Debug.LogError("winPetInfos has nil item, idx == " + a);
					else
						winPetInfos[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)loserPetInfos.Count,buffer, ref offset);
                for (int a = 0; a < loserPetInfos.Count; ++a)
                {
					if(loserPetInfos[a] == null)
						UnityEngine.Debug.LogError("loserPetInfos has nil item, idx == " + a);
					else
						loserPetInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //请求开始战斗
    public class ReqFight : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 128101;
		public int fightType; // 战斗类型
		public int fightTypeParam; // 战斗参数

    	//鏋勯�犲嚱鏁�
    	public ReqFight()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			fightType = 0;
            
			fightTypeParam = 0;
            
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
                fightType = XBuffer.ReadInt(buffer, ref offset);
                fightTypeParam = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(fightType,buffer, ref offset);
					XBuffer.WriteInt(fightTypeParam,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //递交战斗结果
    public class ReqFightResultInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 128102;
		public long fightId; // 战斗实例ID
		public int fightResult; // 战斗结果（0：失败了，1：成功了）
		public long resultParam; // 战斗结果参数
		public ReplayInfo replayInfo; // 战斗数据

    	//鏋勯�犲嚱鏁�
    	public ReqFightResultInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			fightId = 0L;
			fightResult = 0;
            
			resultParam = 0L;
			//replayInfo = ClassCacheManager.New<ReplayInfo>();
			replayInfo = new ReplayInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			replayInfo = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                fightId = XBuffer.ReadLong(buffer, ref offset);
                fightResult = XBuffer.ReadInt(buffer, ref offset);
                resultParam = XBuffer.ReadLong(buffer, ref offset);
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //replayInfo = ClassCacheManager.New<ReplayInfo>();
				replayInfo = new ReplayInfo();
                replayInfo.Read(buffer, ref offset);

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
					XBuffer.WriteLong(fightId,buffer, ref offset);
					XBuffer.WriteInt(fightResult,buffer, ref offset);
					XBuffer.WriteLong(resultParam,buffer, ref offset);
					if(replayInfo == null)
						//replayInfo = ClassCacheManager.New<ReplayInfo>();
						replayInfo = new ReplayInfo();
					replayInfo.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //返回请求战斗的结果
    public class ResFight : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 128201;
		public long fightId; // 战斗实例ID
		public int priority; // 先手值
		public int fightType; // 战斗类型
		public int fightTypeParam; // 战斗参数
        public List<FightParam> petFightParams{get;protected set;} //宠物的战斗参数列表
        public List<FightParam> enemyFightParam{get;protected set;} //敌方宠物的战斗参数列表（可能没有）

    	//鏋勯�犲嚱鏁�
    	public ResFight()
    	{
            petFightParams = new List<FightParam>();
            enemyFightParam = new List<FightParam>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			fightId = 0L;
			priority = 0;
            
			fightType = 0;
            
			fightTypeParam = 0;
            
            petFightParams.Clear();
            enemyFightParam.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = petFightParams.Count; a < b; ++a)
            {
				petFightParams[a] = null;
                //var _value_ = petFightParams[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            petFightParams.Clear();
            for (int a = 0,b = enemyFightParam.Count; a < b; ++a)
            {
				enemyFightParam[a] = null;
                //var _value_ = enemyFightParam[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            enemyFightParam.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                fightId = XBuffer.ReadLong(buffer, ref offset);
                priority = XBuffer.ReadInt(buffer, ref offset);
                fightType = XBuffer.ReadInt(buffer, ref offset);
                fightTypeParam = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    FightParam _value_ = null;
                    //_value_ = ClassCacheManager.New<FightParam>();
					_value_ = new FightParam();
                    _value_.Read(buffer, ref offset);
                    petFightParams.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    FightParam _value_ = null;
                    //_value_ = ClassCacheManager.New<FightParam>();
					_value_ = new FightParam();
                    _value_.Read(buffer, ref offset);
                    enemyFightParam.Add(_value_);
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
					XBuffer.WriteLong(fightId,buffer, ref offset);
					XBuffer.WriteInt(priority,buffer, ref offset);
					XBuffer.WriteInt(fightType,buffer, ref offset);
					XBuffer.WriteInt(fightTypeParam,buffer, ref offset);

                XBuffer.WriteShort((short)petFightParams.Count, buffer, ref offset);
                for(int a = 0; a < petFightParams.Count; ++a)
                {
					if(petFightParams[a] == null)
						UnityEngine.Debug.LogError("petFightParams has nil item, idx == " + a);
					else
						petFightParams[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)enemyFightParam.Count, buffer, ref offset);
                for(int a = 0; a < enemyFightParam.Count; ++a)
                {
					if(enemyFightParam[a] == null)
						UnityEngine.Debug.LogError("enemyFightParam has nil item, idx == " + a);
					else
						enemyFightParam[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //返回战斗的结果
    public class ResFightResultInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 128202;
		public int fightType; // 战斗类型
		public int fightTypeParam; // 战斗参数
		public BaseResult result; // 结果

    	//鏋勯�犲嚱鏁�
    	public ResFightResultInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			fightType = 0;
            
			fightTypeParam = 0;
            
			//result = ClassCacheManager.New<BaseResult>();
			result = new BaseResult();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			result = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                fightType = XBuffer.ReadInt(buffer, ref offset);
                fightTypeParam = XBuffer.ReadInt(buffer, ref offset);
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                switch(real_type)
                {
                    //case TypeEnum.BaseResult: result = ClassCacheManager.New<BaseResult>(); break;
					case TypeEnum.BaseResult: result = new BaseResult(); break;
                    //case TypeEnum.ActivityDungeonResult : result = ClassCacheManager.New<ActivityDungeonResult>(); break;
					case TypeEnum.ActivityDungeonResult : result = new ActivityDungeonResult(); break;
                    //case TypeEnum.ArenaResult : result = ClassCacheManager.New<ArenaResult>(); break;
					case TypeEnum.ArenaResult : result = new ArenaResult(); break;
                    //case TypeEnum.GuildBossResult : result = ClassCacheManager.New<GuildBossResult>(); break;
					case TypeEnum.GuildBossResult : result = new GuildBossResult(); break;
                    //case TypeEnum.TeamFightResult : result = ClassCacheManager.New<TeamFightResult>(); break;
					case TypeEnum.TeamFightResult : result = new TeamFightResult(); break;
                    //case TypeEnum.MissionResult : result = ClassCacheManager.New<MissionResult>(); break;
					case TypeEnum.MissionResult : result = new MissionResult(); break;
                    //case TypeEnum.TrailResult : result = ClassCacheManager.New<TrailResult>(); break;
					case TypeEnum.TrailResult : result = new TrailResult(); break;
                    default:break;
                }
                result.Read(buffer, ref offset);

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
					XBuffer.WriteInt(fightType,buffer, ref offset);
					XBuffer.WriteInt(fightTypeParam,buffer, ref offset);
					if(result == null)
						//result = ClassCacheManager.New<BaseResult>();
						result = new BaseResult();
					result.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}