//Auto generated, do not edit it
//挑战

using System;
using System.IO;
using System.Collections.Generic;
using Message.Bag;

namespace Message.Challenge
{
    public enum TypeEnum
    {
        TrialPetStatus = 11,
        TrialRankItem = 12,
        TeamFightRoleInfo = 13,
        ActivityActBaseInfo = 1,
        ActivityActInfo = 2,
        ActivityAct = 3,
        IntVsInt = 4,
        TrialSingleAttr = 5,
        TrialFloorAttr = 6,
        TrialInfo = 7,
        TrialMonsterSimpleInfo = 8,
        TrialMonster = 9,
        TrialScoreAwardInfo = 10,
    }

    //活动副本信息
    public class ActivityActBaseInfo : BaseMsgStruct
    {
		public int activityId; // 活动id（1：金币，2：女格斗家，3：幻象挑战，4,：经验挑战））
        
		public int record; // 开启的最高难度（1~6）
        
		public int sweepId; // 扫荡难度
        
		public int completeTimes; // 已经完成次数
        
		public int seconds; // 剩余时间（秒）
        

        //鏋勯�犲嚱鏁�
        public ActivityActBaseInfo() : base()
        {
			
			activityId = 0;
            
			record = 0;
            
			sweepId = 0;
            
			completeTimes = 0;
            
			seconds = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			activityId = 0;
            
			record = 0;
            
			sweepId = 0;
            
			completeTimes = 0;
            
			seconds = 0;
            

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
                activityId = XBuffer.ReadInt(buffer, ref offset);
                record = XBuffer.ReadInt(buffer, ref offset);
                sweepId = XBuffer.ReadInt(buffer, ref offset);
                completeTimes = XBuffer.ReadInt(buffer, ref offset);
                seconds = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(activityId, buffer, ref offset);
                XBuffer.WriteInt(record, buffer, ref offset);
                XBuffer.WriteInt(sweepId, buffer, ref offset);
                XBuffer.WriteInt(completeTimes, buffer, ref offset);
                XBuffer.WriteInt(seconds, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //活动副本信息
    public class ActivityActInfo : BaseMsgStruct
    {
		public ActivityActBaseInfo baseInfo; // 活动副本基础信息
        
		public int isOpen; // 是否已开启（0：未开启，1：已开启）
        
		public int maxTimes; // 最大次数
        
		public int __phantomIndex; // 今日幻象序号（幻象挑战专用）
		private byte _phantomIndex = 0; // 今日幻象序号（幻象挑战专用） tag
		
		public bool hasPhantomIndex()
		{
			return this._phantomIndex == 1;
		}
		
		public int phantomIndex
		{
			set
			{
				_phantomIndex = 1;
				__phantomIndex = value;
			}
			
			get
			{
				return __phantomIndex;
			}
		}
        

        //鏋勯�犲嚱鏁�
        public ActivityActInfo() : base()
        {
			
			//baseInfo = ClassCacheManager.New<ActivityActBaseInfo>();
			baseInfo = new ActivityActBaseInfo();
			isOpen = 0;
            
			maxTimes = 0;
            
			_phantomIndex = 0;
			__phantomIndex = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//baseInfo = ClassCacheManager.New<ActivityActBaseInfo>();
			baseInfo = new ActivityActBaseInfo();
			isOpen = 0;
            
			maxTimes = 0;
            
			_phantomIndex = 0;
			__phantomIndex = 0;
            

        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			baseInfo = null;

        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //baseInfo = ClassCacheManager.New<ActivityActBaseInfo>();
				baseInfo = new ActivityActBaseInfo();
                baseInfo.Read(buffer, ref offset);
                isOpen = XBuffer.ReadInt(buffer, ref offset);
                maxTimes = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					phantomIndex = XBuffer.ReadInt(buffer, ref offset);
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
            XBuffer.WriteByte(2, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                if(baseInfo==null)
                    //baseInfo = ClassCacheManager.New<ActivityActBaseInfo>();
					baseInfo = new ActivityActBaseInfo();
                baseInfo.WriteWithType(buffer, ref offset);
                XBuffer.WriteInt(isOpen, buffer, ref offset);
                XBuffer.WriteInt(maxTimes, buffer, ref offset);
				XBuffer.WriteByte(_phantomIndex, buffer, ref offset);
				if (_phantomIndex == 1)
				{
					XBuffer.WriteInt(phantomIndex, buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //活动关卡
    public class ActivityAct : BaseMsgStruct
    {
		public int activityId; // 活动id（1：金币，2：女格斗家，3：幻象挑战，4：经验挑战）
        
		public int difficulty; // 难度
        

        //鏋勯�犲嚱鏁�
        public ActivityAct() : base()
        {
			
			activityId = 0;
            
			difficulty = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			activityId = 0;
            
			difficulty = 0;
            

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
                activityId = XBuffer.ReadInt(buffer, ref offset);
                difficulty = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(activityId, buffer, ref offset);
                XBuffer.WriteInt(difficulty, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //两个整数
    public class IntVsInt : BaseMsgStruct
    {
		public int int1; // 第一个整数
        
		public int int2; // 第二个整数
        

        //鏋勯�犲嚱鏁�
        public IntVsInt() : base()
        {
			
			int1 = 0;
            
			int2 = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			int1 = 0;
            
			int2 = 0;
            

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
                int1 = XBuffer.ReadInt(buffer, ref offset);
                int2 = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(int1, buffer, ref offset);
                XBuffer.WriteInt(int2, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //单个属性
    public class TrialSingleAttr : BaseMsgStruct
    {
		public int attrId; // 属性id
        
		public int attrValue; // 属性值
        
		public int costStar; // 兑换需要的星星
        
		public int floor; // 所在层数
        
		public int isExchange; // 是否已被兑换（1：已被兑换，0：未被兑换）
        

        //鏋勯�犲嚱鏁�
        public TrialSingleAttr() : base()
        {
			
			attrId = 0;
            
			attrValue = 0;
            
			costStar = 0;
            
			floor = 0;
            
			isExchange = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			attrId = 0;
            
			attrValue = 0;
            
			costStar = 0;
            
			floor = 0;
            
			isExchange = 0;
            

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
                attrId = XBuffer.ReadInt(buffer, ref offset);
                attrValue = XBuffer.ReadInt(buffer, ref offset);
                costStar = XBuffer.ReadInt(buffer, ref offset);
                floor = XBuffer.ReadInt(buffer, ref offset);
                isExchange = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(attrId, buffer, ref offset);
                XBuffer.WriteInt(attrValue, buffer, ref offset);
                XBuffer.WriteInt(costStar, buffer, ref offset);
                XBuffer.WriteInt(floor, buffer, ref offset);
                XBuffer.WriteInt(isExchange, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //单层属性
    public class TrialFloorAttr : BaseMsgStruct
    {
        public List<TrialSingleAttr> attrs{get; protected set;} //属性

        //鏋勯�犲嚱鏁�
        public TrialFloorAttr() : base()
        {
            attrs = new List<TrialSingleAttr>(); //属性
			

            attrs.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);

            attrs.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = attrs.Count; a < b; ++a)
            {
                //var _value_ = attrs[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				attrs[a] = null;
            }
            attrs.Clear();
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
                    TrialSingleAttr _value_ = null;
                    //_value_ = ClassCacheManager.New<TrialSingleAttr>();
					_value_ = new TrialSingleAttr();
                    _value_.Read(buffer, ref offset);
                    attrs.Add(_value_);
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

                XBuffer.WriteShort((short)attrs.Count,buffer, ref offset);
                for (int a = 0; a < attrs.Count; ++a)
                {
					if(attrs[a] == null)
						UnityEngine.Debug.LogError("attrs has nil item, idx == " + a);
					else
						attrs[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //试炼信息
    public class TrialInfo : BaseMsgStruct
    {
		public int floor; // 当前层
        
		public int score; // 今日积分
        
		public int star; // 当前剩余星
        
        public List<TrialPetStatus> petStatus{get; protected set;} //宠物状态

        //鏋勯�犲嚱鏁�
        public TrialInfo() : base()
        {
            petStatus = new List<TrialPetStatus>(); //宠物状态
			
			floor = 0;
            
			score = 0;
            
			star = 0;
            

            petStatus.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			floor = 0;
            
			score = 0;
            
			star = 0;
            

            petStatus.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = petStatus.Count; a < b; ++a)
            {
                //var _value_ = petStatus[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				petStatus[a] = null;
            }
            petStatus.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                floor = XBuffer.ReadInt(buffer, ref offset);
                score = XBuffer.ReadInt(buffer, ref offset);
                star = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    TrialPetStatus _value_ = null;
                    //_value_ = ClassCacheManager.New<TrialPetStatus>();
					_value_ = new TrialPetStatus();
                    _value_.Read(buffer, ref offset);
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
            XBuffer.WriteByte(7, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(floor, buffer, ref offset);
                XBuffer.WriteInt(score, buffer, ref offset);
                XBuffer.WriteInt(star, buffer, ref offset);

                XBuffer.WriteShort((short)petStatus.Count,buffer, ref offset);
                for (int a = 0; a < petStatus.Count; ++a)
                {
					if(petStatus[a] == null)
						UnityEngine.Debug.LogError("petStatus has nil item, idx == " + a);
					else
						petStatus[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //试炼怪
    public class TrialMonsterSimpleInfo : BaseMsgStruct
    {
		public int petId; // ID
        
		public int level; // 等级
        
		public int star; // 星级
        
		public int color; // 品阶
        

        //鏋勯�犲嚱鏁�
        public TrialMonsterSimpleInfo() : base()
        {
			
			petId = 0;
            
			level = 0;
            
			star = 0;
            
			color = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			level = 0;
            
			star = 0;
            
			color = 0;
            

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
                level = XBuffer.ReadInt(buffer, ref offset);
                star = XBuffer.ReadInt(buffer, ref offset);
                color = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteInt(star, buffer, ref offset);
                XBuffer.WriteInt(color, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //试炼怪阵型
    public class TrialMonster : BaseMsgStruct
    {
		public int fightPower; // 战力
        
		public int canFight; // 是否可打（1：可，0：不可）
        
        public List<TrialMonsterSimpleInfo> monster{get; protected set;} //怪

        //鏋勯�犲嚱鏁�
        public TrialMonster() : base()
        {
            monster = new List<TrialMonsterSimpleInfo>(); //怪
			
			fightPower = 0;
            
			canFight = 0;
            

            monster.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			fightPower = 0;
            
			canFight = 0;
            

            monster.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = monster.Count; a < b; ++a)
            {
                //var _value_ = monster[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				monster[a] = null;
            }
            monster.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                fightPower = XBuffer.ReadInt(buffer, ref offset);
                canFight = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    TrialMonsterSimpleInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<TrialMonsterSimpleInfo>();
					_value_ = new TrialMonsterSimpleInfo();
                    _value_.Read(buffer, ref offset);
                    monster.Add(_value_);
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
                XBuffer.WriteInt(fightPower, buffer, ref offset);
                XBuffer.WriteInt(canFight, buffer, ref offset);

                XBuffer.WriteShort((short)monster.Count,buffer, ref offset);
                for (int a = 0; a < monster.Count; ++a)
                {
					if(monster[a] == null)
						UnityEngine.Debug.LogError("monster has nil item, idx == " + a);
					else
						monster[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //试炼积分奖励信息
    public class TrialScoreAwardInfo : BaseMsgStruct
    {
		public int score; // 历史总积分的8%
        
        public List<int> num{get; protected set;} //是否领取（1：已领取，0：未领取）
        public List<IntVsInt> rewards{get; protected set;} //累计获得奖励

        //鏋勯�犲嚱鏁�
        public TrialScoreAwardInfo() : base()
        {
            num = new List<int>(); //是否领取（1：已领取，0：未领取）
            rewards = new List<IntVsInt>(); //累计获得奖励
			
			score = 0;
            

            num.Clear();
            rewards.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			score = 0;
            

            num.Clear();
            rewards.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            num.Clear();
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
                score = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    var _value_ = XBuffer.ReadInt(buffer, ref offset);
                    num.Add(_value_);
                }
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    IntVsInt _value_ = null;
                    //_value_ = ClassCacheManager.New<IntVsInt>();
					_value_ = new IntVsInt();
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
            XBuffer.WriteByte(10, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(score, buffer, ref offset);

                XBuffer.WriteShort((short)num.Count,buffer, ref offset);
                for (int a = 0; a < num.Count; ++a)
                {
                    XBuffer.WriteInt(num[a], buffer, ref offset);
                }
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
    //试炼宠物状态
    public class TrialPetStatus : BaseMsgStruct
    {
		public int petId; // 宠物ID
        
		public int hpLoss; // 损失生命
        
		public int anger; // 怒气
        
		public int dead; // 是否死亡（1：是，0：否）
        

        //鏋勯�犲嚱鏁�
        public TrialPetStatus() : base()
        {
			
			petId = 0;
            
			hpLoss = 0;
            
			anger = 0;
            
			dead = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			hpLoss = 0;
            
			anger = 0;
            
			dead = 0;
            

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
                hpLoss = XBuffer.ReadInt(buffer, ref offset);
                anger = XBuffer.ReadInt(buffer, ref offset);
                dead = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(petId, buffer, ref offset);
                XBuffer.WriteInt(hpLoss, buffer, ref offset);
                XBuffer.WriteInt(anger, buffer, ref offset);
                XBuffer.WriteInt(dead, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //试炼排行
    public class TrialRankItem : BaseMsgStruct
    {
		public long roleId; // 角色ID
        
		public string roleName; // 角色名
        
		public int rank; // 排名
        
		public int score; // 总积分
        
		public int floor; // 今日最高层
        

        //鏋勯�犲嚱鏁�
        public TrialRankItem() : base()
        {
			
			roleId = 0L;
			roleName = "";
			rank = 0;
            
			score = 0;
            
			floor = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			roleName = "";
			rank = 0;
            
			score = 0;
            
			floor = 0;
            

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
                roleName = XBuffer.ReadString(buffer, ref offset);
                rank = XBuffer.ReadInt(buffer, ref offset);
                score = XBuffer.ReadInt(buffer, ref offset);
                floor = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
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
                XBuffer.WriteLong(roleId, buffer, ref offset);
                XBuffer.WriteString(roleName, buffer, ref offset);
                XBuffer.WriteInt(rank, buffer, ref offset);
                XBuffer.WriteInt(score, buffer, ref offset);
                XBuffer.WriteInt(floor, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //组队战玩家信息
    public class TeamFightRoleInfo : BaseMsgStruct
    {
		public long roleId; // 角色ID
        
		public string roleName; // 角色名
        
		public int level; // 等级
        
		public int petId; // 头像宠物ID
        
		public int color; // 头像宠物品阶
        
		public int star; // 头像宠物星级
        
		public int count; // 已经完成次数
        
		public int index; // 位置
        

        //鏋勯�犲嚱鏁�
        public TeamFightRoleInfo() : base()
        {
			
			roleId = 0L;
			roleName = "";
			level = 0;
            
			petId = 0;
            
			color = 0;
            
			star = 0;
            
			count = 0;
            
			index = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			roleName = "";
			level = 0;
            
			petId = 0;
            
			color = 0;
            
			star = 0;
            
			count = 0;
            
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
                roleId = XBuffer.ReadLong(buffer, ref offset);
                roleName = XBuffer.ReadString(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                petId = XBuffer.ReadInt(buffer, ref offset);
                color = XBuffer.ReadInt(buffer, ref offset);
                star = XBuffer.ReadInt(buffer, ref offset);
                count = XBuffer.ReadInt(buffer, ref offset);
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
            XBuffer.WriteByte(13, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteLong(roleId, buffer, ref offset);
                XBuffer.WriteString(roleName, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteInt(petId, buffer, ref offset);
                XBuffer.WriteInt(color, buffer, ref offset);
                XBuffer.WriteInt(star, buffer, ref offset);
                XBuffer.WriteInt(count, buffer, ref offset);
                XBuffer.WriteInt(index, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //获取活动副本信息
    public class ReqActivityActInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108201;

    	//鏋勯�犲嚱鏁�
    	public ReqActivityActInfo()
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
    //活动副本请求开始战斗
    public class ReqActivityFightStart : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108202;
		public ActivityAct act; // 活动关卡

    	//鏋勯�犲嚱鏁�
    	public ReqActivityFightStart()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//act = ClassCacheManager.New<ActivityAct>();
			act = new ActivityAct();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			act = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //act = ClassCacheManager.New<ActivityAct>();
				act = new ActivityAct();
                act.Read(buffer, ref offset);

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
					if(act == null)
						//act = ClassCacheManager.New<ActivityAct>();
						act = new ActivityAct();
					act.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //活动副本提交战斗结果
    public class ReqActivityFightEnd : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108203;
		public ActivityAct act; // 活动关卡
		public int result; // 结果（0：失败，1：成功）
		public int __bloodPer; // 损血百分比（金币、经验本）
		private byte _bloodPer = 0; // 损血百分比（金币、经验本） tag
		
		public bool hasBloodPer()
		{
			return this._bloodPer == 1;
		}
		
		public int bloodPer
		{
			set
			{
				_bloodPer = 1;
				__bloodPer = value;
			}
			
			get
			{
				return __bloodPer;
			}
		}

    	//鏋勯�犲嚱鏁�
    	public ReqActivityFightEnd()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//act = ClassCacheManager.New<ActivityAct>();
			act = new ActivityAct();
			result = 0;
            
			_bloodPer = 0;
			__bloodPer = 0;
            
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			act = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //act = ClassCacheManager.New<ActivityAct>();
				act = new ActivityAct();
                act.Read(buffer, ref offset);
                result = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					bloodPer = XBuffer.ReadInt(buffer, ref offset);
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
					if(act == null)
						//act = ClassCacheManager.New<ActivityAct>();
						act = new ActivityAct();
					act.WriteWithType(buffer, ref offset);
					XBuffer.WriteInt(result,buffer, ref offset);
				XBuffer.WriteByte(_bloodPer,buffer, ref offset);
				if (_bloodPer == 1)
				{
					XBuffer.WriteInt(bloodPer,buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //活动副本请求扫荡
    public class ReqActivitySweep : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108204;
		public ActivityAct act; // 活动关卡

    	//鏋勯�犲嚱鏁�
    	public ReqActivitySweep()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//act = ClassCacheManager.New<ActivityAct>();
			act = new ActivityAct();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			act = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //act = ClassCacheManager.New<ActivityAct>();
				act = new ActivityAct();
                act.Read(buffer, ref offset);

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
					if(act == null)
						//act = ClassCacheManager.New<ActivityAct>();
						act = new ActivityAct();
					act.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //获取挑战信息
    public class ReqChallengeInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108205;

    	//鏋勯�犲嚱鏁�
    	public ReqChallengeInfo()
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
    //获取终极试炼信息
    public class ReqTrialInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108206;

    	//鏋勯�犲嚱鏁�
    	public ReqTrialInfo()
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
    //终极试炼跳过
    public class ReqTrialSkip : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108208;

    	//鏋勯�犲嚱鏁�
    	public ReqTrialSkip()
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
    //终极试炼属性兑换
    public class ReqTrialAttrExchange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108209;
		public int floor; // 层数
		public int index; // 属性下标（0~2）
		public int __petId; // 选择的宠物ID
		private byte _petId = 0; // 选择的宠物ID tag
		
		public bool hasPetId()
		{
			return this._petId == 1;
		}
		
		public int petId
		{
			set
			{
				_petId = 1;
				__petId = value;
			}
			
			get
			{
				return __petId;
			}
		}

    	//鏋勯�犲嚱鏁�
    	public ReqTrialAttrExchange()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			floor = 0;
            
			index = 0;
            
			_petId = 0;
			__petId = 0;
            
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
                floor = XBuffer.ReadInt(buffer, ref offset);
                index = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					petId = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(floor,buffer, ref offset);
					XBuffer.WriteInt(index,buffer, ref offset);
				XBuffer.WriteByte(_petId,buffer, ref offset);
				if (_petId == 1)
				{
					XBuffer.WriteInt(petId,buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //终极试炼打开钻石宝箱
    public class ReqTrialBoxOpen : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108210;
		public int floor; // 层数
		public int number; // 开箱数量

    	//鏋勯�犲嚱鏁�
    	public ReqTrialBoxOpen()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			floor = 0;
            
			number = 0;
            
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
                floor = XBuffer.ReadInt(buffer, ref offset);
                number = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(floor,buffer, ref offset);
					XBuffer.WriteInt(number,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //终极试炼批量打开钻石宝箱
    public class ReqTrialBatchBoxOpen : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108211;
		public int number; // 开箱数量
        public List<int> floors{get;protected set;} //层数

    	//鏋勯�犲嚱鏁�
    	public ReqTrialBatchBoxOpen()
    	{
            floors = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			number = 0;
            
            floors.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            floors.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                number = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
            		var _value_ = XBuffer.ReadInt(buffer, ref offset);
            		floors.Add(_value_);
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
					XBuffer.WriteInt(number,buffer, ref offset);

                XBuffer.WriteShort((short)floors.Count, buffer, ref offset);
                for(int a = 0; a < floors.Count; ++a)
                {
        			XBuffer.WriteInt(floors[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //终极试炼获取当前层信息
    public class ReqTrialFloorInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108212;
		public int floor; // 层数

    	//鏋勯�犲嚱鏁�
    	public ReqTrialFloorInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			floor = 0;
            
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
                floor = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(floor,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //获取终极试炼积分奖励信息
    public class ReqTrialScoreAwardInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108213;

    	//鏋勯�犲嚱鏁�
    	public ReqTrialScoreAwardInfo()
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
    //领取终极试炼积分奖励
    public class ReqTrialScoreAwardGet : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108214;
		public int index; // 奖励下标（0开头）

    	//鏋勯�犲嚱鏁�
    	public ReqTrialScoreAwardGet()
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
    //自动购买buff
    public class ReqTrialAutoBuyBuff : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108215;

    	//鏋勯�犲嚱鏁�
    	public ReqTrialAutoBuyBuff()
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
    //终极试炼战斗开始
    public class ReqTrialFightStart : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108216;
		public int floor; // 层
		public int index; // 难度（0、1、2）

    	//鏋勯�犲嚱鏁�
    	public ReqTrialFightStart()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			floor = 0;
            
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
                floor = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(floor,buffer, ref offset);
					XBuffer.WriteInt(index,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //终极试炼战斗结果
    public class ReqTrialFightEnd : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108217;
		public int floor; // 层
		public int result; // 0：失败 1~3星星
        public List<TrialPetStatus> status{get;protected set;} //上阵宠物状态

    	//鏋勯�犲嚱鏁�
    	public ReqTrialFightEnd()
    	{
            status = new List<TrialPetStatus>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			floor = 0;
            
			result = 0;
            
            status.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = status.Count; a < b; ++a)
            {
				status[a] = null;
                //var _value_ = status[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            status.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                floor = XBuffer.ReadInt(buffer, ref offset);
                result = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    TrialPetStatus _value_ = null;
                    //_value_ = ClassCacheManager.New<TrialPetStatus>();
					_value_ = new TrialPetStatus();
                    _value_.Read(buffer, ref offset);
                    status.Add(_value_);
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
					XBuffer.WriteInt(floor,buffer, ref offset);
					XBuffer.WriteInt(result,buffer, ref offset);

                XBuffer.WriteShort((short)status.Count, buffer, ref offset);
                for(int a = 0; a < status.Count; ++a)
                {
					if(status[a] == null)
						UnityEngine.Debug.LogError("status has nil item, idx == " + a);
					else
						status[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //终极试炼排行榜
    public class ReqTrialRank : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108218;
		public int from; // 起
		public int end; // 止

    	//鏋勯�犲嚱鏁�
    	public ReqTrialRank()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			from = 0;
            
			end = 0;
            
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
                from = XBuffer.ReadInt(buffer, ref offset);
                end = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(from,buffer, ref offset);
					XBuffer.WriteInt(end,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //获取组队战信息
    public class ReqTeamFightInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108219;

    	//鏋勯�犲嚱鏁�
    	public ReqTeamFightInfo()
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
    //组队战加入队伍
    public class ReqTeamFightJoinTeam : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108220;
		public bool fastEnter; // 快速加入
		public int petId; // 宠物ID
		public int bestPetId; // 上阵宠物ID

    	//鏋勯�犲嚱鏁�
    	public ReqTeamFightJoinTeam()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			fastEnter = false;
            fastEnter = false;
			petId = 0;
            
			bestPetId = 0;
            
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
        		fastEnter = XBuffer.ReadBool(buffer, ref offset);
                petId = XBuffer.ReadInt(buffer, ref offset);
                bestPetId = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteBool(fastEnter,buffer, ref offset);
					XBuffer.WriteInt(petId,buffer, ref offset);
					XBuffer.WriteInt(bestPetId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //组队战离开队伍
    public class ReqTeamFightLeaveTeam : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108221;

    	//鏋勯�犲嚱鏁�
    	public ReqTeamFightLeaveTeam()
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
    //组队战邀请好友
    public class ReqTeamFightFriendInvite : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108222;
		public long roleId; // 被邀请者角色Id

    	//鏋勯�犲嚱鏁�
    	public ReqTeamFightFriendInvite()
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
    //组队战发送邀请消息
    public class ReqTeamFightSendInviteMessage : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108223;
		public int channel; // 频道（1：世界频道，2：社团频道）

    	//鏋勯�犲嚱鏁�
    	public ReqTeamFightSendInviteMessage()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			channel = 0;
            
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
                channel = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(channel,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //组队战设置禁止快速加入
    public class ReqTeamFightForbidFastEnter : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108224;
		public bool forbid; // 禁止

    	//鏋勯�犲嚱鏁�
    	public ReqTeamFightForbidFastEnter()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			forbid = false;
            forbid = false;
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
        		forbid = XBuffer.ReadBool(buffer, ref offset);

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
					XBuffer.WriteBool(forbid,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //组队战战斗开始
    public class ReqTeamFightStart : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108225;

    	//鏋勯�犲嚱鏁�
    	public ReqTeamFightStart()
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
    //组队战战斗结果
    public class ReqTeamFightEnd : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108226;
		public int result; // 结果（0：失败，1：成功）

    	//鏋勯�犲嚱鏁�
    	public ReqTeamFightEnd()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			result = 0;
            
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
                result = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(result,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //组队战通知队友
    public class ReqTeamFightNotifyTeammates : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108227;

    	//鏋勯�犲嚱鏁�
    	public ReqTeamFightNotifyTeammates()
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
    //组队战接受邀请
    public class ReqTeamFightAgreeInvite : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108228;
		public long roleId; // 邀请者角色ID

    	//鏋勯�犲嚱鏁�
    	public ReqTeamFightAgreeInvite()
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
    //组队战换上阵
    public class ReqTeamFightChangePet : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108229;
		public int petId; // 想要上阵的宠物Id

    	//鏋勯�犲嚱鏁�
    	public ReqTeamFightChangePet()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
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
					XBuffer.WriteInt(petId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //组队战开启宝箱
    public class ReqTeamFightOpenBox : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108230;
		public int index; // 箱子下标

    	//鏋勯�犲嚱鏁�
    	public ReqTeamFightOpenBox()
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
    //返回活动关卡信息
    public class ResActivityActInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108101;
        public List<ActivityActInfo> activityActInfo{get;protected set;} //副本信息

    	//鏋勯�犲嚱鏁�
    	public ResActivityActInfo()
    	{
            activityActInfo = new List<ActivityActInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            activityActInfo.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = activityActInfo.Count; a < b; ++a)
            {
				activityActInfo[a] = null;
                //var _value_ = activityActInfo[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            activityActInfo.Clear();
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
                    ActivityActInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<ActivityActInfo>();
					_value_ = new ActivityActInfo();
                    _value_.Read(buffer, ref offset);
                    activityActInfo.Add(_value_);
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

                XBuffer.WriteShort((short)activityActInfo.Count, buffer, ref offset);
                for(int a = 0; a < activityActInfo.Count; ++a)
                {
					if(activityActInfo[a] == null)
						UnityEngine.Debug.LogError("activityActInfo has nil item, idx == " + a);
					else
						activityActInfo[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //返回活动副本开始战斗请求
    public class ResActivityFightStart : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108102;
		public ActivityAct act; // 活动关卡
		public int result; // 是否可打（0：不可打，1：可打）

    	//鏋勯�犲嚱鏁�
    	public ResActivityFightStart()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//act = ClassCacheManager.New<ActivityAct>();
			act = new ActivityAct();
			result = 0;
            
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			act = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //act = ClassCacheManager.New<ActivityAct>();
				act = new ActivityAct();
                act.Read(buffer, ref offset);
                result = XBuffer.ReadInt(buffer, ref offset);

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
					if(act == null)
						//act = ClassCacheManager.New<ActivityAct>();
						act = new ActivityAct();
					act.WriteWithType(buffer, ref offset);
					XBuffer.WriteInt(result,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //返回活动副本关卡信息
    public class ResActivitySingle : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108104;
		public ActivityActBaseInfo act; // 活动关卡

    	//鏋勯�犲嚱鏁�
    	public ResActivitySingle()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//act = ClassCacheManager.New<ActivityActBaseInfo>();
			act = new ActivityActBaseInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			act = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //act = ClassCacheManager.New<ActivityActBaseInfo>();
				act = new ActivityActBaseInfo();
                act.Read(buffer, ref offset);

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
					if(act == null)
						//act = ClassCacheManager.New<ActivityActBaseInfo>();
						act = new ActivityActBaseInfo();
					act.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //挑战信息
    public class ResChallengeInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108105;
        public List<int> infos{get;protected set;} //挑战信息（1：终极试炼，2：活动关卡，3：克隆模式）

    	//鏋勯�犲嚱鏁�
    	public ResChallengeInfo()
    	{
            infos = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            infos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            infos.Clear();
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

                XBuffer.WriteShort((short)infos.Count, buffer, ref offset);
                for(int a = 0; a < infos.Count; ++a)
                {
        			XBuffer.WriteInt(infos[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //终极试炼信息
    public class ResTrialInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108106;
		public TrialInfo trialInfo; // 试炼信息
		public int skipFloor; // 今日可跳过层数
        public List<IntVsInt> buffs{get;protected set;} //buff列表（属性id：值）

    	//鏋勯�犲嚱鏁�
    	public ResTrialInfo()
    	{
            buffs = new List<IntVsInt>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//trialInfo = ClassCacheManager.New<TrialInfo>();
			trialInfo = new TrialInfo();
			skipFloor = 0;
            
            buffs.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			trialInfo = null;

            for (int a = 0,b = buffs.Count; a < b; ++a)
            {
				buffs[a] = null;
                //var _value_ = buffs[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            buffs.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //trialInfo = ClassCacheManager.New<TrialInfo>();
				trialInfo = new TrialInfo();
                trialInfo.Read(buffer, ref offset);
                skipFloor = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    IntVsInt _value_ = null;
                    //_value_ = ClassCacheManager.New<IntVsInt>();
					_value_ = new IntVsInt();
                    _value_.Read(buffer, ref offset);
                    buffs.Add(_value_);
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
					if(trialInfo == null)
						//trialInfo = ClassCacheManager.New<TrialInfo>();
						trialInfo = new TrialInfo();
					trialInfo.WriteWithType(buffer, ref offset);
					XBuffer.WriteInt(skipFloor,buffer, ref offset);

                XBuffer.WriteShort((short)buffs.Count, buffer, ref offset);
                for(int a = 0; a < buffs.Count; ++a)
                {
					if(buffs[a] == null)
						UnityEngine.Debug.LogError("buffs has nil item, idx == " + a);
					else
						buffs[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //终极试炼跳过
    public class ResTrialSkip : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108108;
		public TrialInfo trialInfo; // 试炼信息
        public List<IntVsInt> rewards{get;protected set;} //奖励（道具ID：道具数量）
        public List<TrialFloorAttr> attrs{get;protected set;} //可兑换属性
        public List<IntVsInt> diamondBoxInfos{get;protected set;} //钻石宝箱信息（层数 -> 打开次数）

    	//鏋勯�犲嚱鏁�
    	public ResTrialSkip()
    	{
            rewards = new List<IntVsInt>();
            attrs = new List<TrialFloorAttr>();
            diamondBoxInfos = new List<IntVsInt>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//trialInfo = ClassCacheManager.New<TrialInfo>();
			trialInfo = new TrialInfo();
            rewards.Clear();
            attrs.Clear();
            diamondBoxInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			trialInfo = null;

            for (int a = 0,b = rewards.Count; a < b; ++a)
            {
				rewards[a] = null;
                //var _value_ = rewards[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            rewards.Clear();
            for (int a = 0,b = attrs.Count; a < b; ++a)
            {
				attrs[a] = null;
                //var _value_ = attrs[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            attrs.Clear();
            for (int a = 0,b = diamondBoxInfos.Count; a < b; ++a)
            {
				diamondBoxInfos[a] = null;
                //var _value_ = diamondBoxInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            diamondBoxInfos.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //trialInfo = ClassCacheManager.New<TrialInfo>();
				trialInfo = new TrialInfo();
                trialInfo.Read(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    IntVsInt _value_ = null;
                    //_value_ = ClassCacheManager.New<IntVsInt>();
					_value_ = new IntVsInt();
                    _value_.Read(buffer, ref offset);
                    rewards.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    TrialFloorAttr _value_ = null;
                    //_value_ = ClassCacheManager.New<TrialFloorAttr>();
					_value_ = new TrialFloorAttr();
                    _value_.Read(buffer, ref offset);
                    attrs.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    IntVsInt _value_ = null;
                    //_value_ = ClassCacheManager.New<IntVsInt>();
					_value_ = new IntVsInt();
                    _value_.Read(buffer, ref offset);
                    diamondBoxInfos.Add(_value_);
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
					if(trialInfo == null)
						//trialInfo = ClassCacheManager.New<TrialInfo>();
						trialInfo = new TrialInfo();
					trialInfo.WriteWithType(buffer, ref offset);

                XBuffer.WriteShort((short)rewards.Count, buffer, ref offset);
                for(int a = 0; a < rewards.Count; ++a)
                {
					if(rewards[a] == null)
						UnityEngine.Debug.LogError("rewards has nil item, idx == " + a);
					else
						rewards[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)attrs.Count, buffer, ref offset);
                for(int a = 0; a < attrs.Count; ++a)
                {
					if(attrs[a] == null)
						UnityEngine.Debug.LogError("attrs has nil item, idx == " + a);
					else
						attrs[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)diamondBoxInfos.Count, buffer, ref offset);
                for(int a = 0; a < diamondBoxInfos.Count; ++a)
                {
					if(diamondBoxInfos[a] == null)
						UnityEngine.Debug.LogError("diamondBoxInfos has nil item, idx == " + a);
					else
						diamondBoxInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //终极试炼怪物层信息
    public class ResTrialMonsterFloor : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108109;
        public List<TrialMonster> monsters{get;protected set;} //三种难度的怪物数据

    	//鏋勯�犲嚱鏁�
    	public ResTrialMonsterFloor()
    	{
            monsters = new List<TrialMonster>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            monsters.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = monsters.Count; a < b; ++a)
            {
				monsters[a] = null;
                //var _value_ = monsters[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            monsters.Clear();
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
                    TrialMonster _value_ = null;
                    //_value_ = ClassCacheManager.New<TrialMonster>();
					_value_ = new TrialMonster();
                    _value_.Read(buffer, ref offset);
                    monsters.Add(_value_);
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

                XBuffer.WriteShort((short)monsters.Count, buffer, ref offset);
                for(int a = 0; a < monsters.Count; ++a)
                {
					if(monsters[a] == null)
						UnityEngine.Debug.LogError("monsters has nil item, idx == " + a);
					else
						monsters[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //终极试炼属性层信息
    public class ResTrialAttrFloor : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108110;
		public TrialFloorAttr __trialFloorAttr; // 可兑换属性
		private byte _trialFloorAttr = 0; // 可兑换属性 tag
		
		public bool hasTrialFloorAttr()
		{
			return this._trialFloorAttr == 1;
		}
		
		public TrialFloorAttr trialFloorAttr
		{
			set
			{
				_trialFloorAttr = 1;
				__trialFloorAttr = value;
			}
			
			get
			{
				return __trialFloorAttr;
			}
		}
		public int star; // 现有星星
		public int type; // 1:单层，2：自动购买
		public int floor; // 当前层
        public List<IntVsInt> buffs{get;protected set;} //buff列表（属性id：值）
        public List<TrialPetStatus> petStatus{get;protected set;} //宠物状态

    	//鏋勯�犲嚱鏁�
    	public ResTrialAttrFloor()
    	{
            buffs = new List<IntVsInt>();
            petStatus = new List<TrialPetStatus>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			_trialFloorAttr = 0;
			//__trialFloorAttr = ClassCacheManager.New<TrialFloorAttr>();
			__trialFloorAttr = new TrialFloorAttr();
			star = 0;
            
			type = 0;
            
			floor = 0;
            
            buffs.Clear();
            petStatus.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			__trialFloorAttr = null;

            for (int a = 0,b = buffs.Count; a < b; ++a)
            {
				buffs[a] = null;
                //var _value_ = buffs[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            buffs.Clear();
            for (int a = 0,b = petStatus.Count; a < b; ++a)
            {
				petStatus[a] = null;
                //var _value_ = petStatus[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            petStatus.Clear();
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
					//trialFloorAttr = ClassCacheManager.New<TrialFloorAttr>();
					trialFloorAttr = new TrialFloorAttr();
					trialFloorAttr.Read(buffer, ref offset);
				}
                star = XBuffer.ReadInt(buffer, ref offset);
                type = XBuffer.ReadInt(buffer, ref offset);
                floor = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    IntVsInt _value_ = null;
                    //_value_ = ClassCacheManager.New<IntVsInt>();
					_value_ = new IntVsInt();
                    _value_.Read(buffer, ref offset);
                    buffs.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    TrialPetStatus _value_ = null;
                    //_value_ = ClassCacheManager.New<TrialPetStatus>();
					_value_ = new TrialPetStatus();
                    _value_.Read(buffer, ref offset);
                    petStatus.Add(_value_);
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
				XBuffer.WriteByte(_trialFloorAttr,buffer, ref offset);
				if (_trialFloorAttr == 1)
				{
					if(trialFloorAttr == null)
						//trialFloorAttr = ClassCacheManager.New<TrialFloorAttr>();
						trialFloorAttr = new TrialFloorAttr();
					trialFloorAttr.WriteWithType(buffer, ref offset);
				}
					XBuffer.WriteInt(star,buffer, ref offset);
					XBuffer.WriteInt(type,buffer, ref offset);
					XBuffer.WriteInt(floor,buffer, ref offset);

                XBuffer.WriteShort((short)buffs.Count, buffer, ref offset);
                for(int a = 0; a < buffs.Count; ++a)
                {
					if(buffs[a] == null)
						UnityEngine.Debug.LogError("buffs has nil item, idx == " + a);
					else
						buffs[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)petStatus.Count, buffer, ref offset);
                for(int a = 0; a < petStatus.Count; ++a)
                {
					if(petStatus[a] == null)
						UnityEngine.Debug.LogError("petStatus has nil item, idx == " + a);
					else
						petStatus[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //终极试炼宝箱层奖励
    public class ResTrialBoxFloor : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108111;
		public int floor; // 当前层
        public List<IntVsInt> rewards{get;protected set;} //奖励列表

    	//鏋勯�犲嚱鏁�
    	public ResTrialBoxFloor()
    	{
            rewards = new List<IntVsInt>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			floor = 0;
            
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
                floor = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    IntVsInt _value_ = null;
                    //_value_ = ClassCacheManager.New<IntVsInt>();
					_value_ = new IntVsInt();
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
					XBuffer.WriteInt(floor,buffer, ref offset);

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
    //终极试炼钻石宝箱信息
    public class ResTrialSingleBoxOpen : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108112;
		public IntVsInt diamondBoxInfo; // 钻石宝箱信息（层数 -> 打开次数
        public List<IntVsInt> rewards{get;protected set;} //奖励列表

    	//鏋勯�犲嚱鏁�
    	public ResTrialSingleBoxOpen()
    	{
            rewards = new List<IntVsInt>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//diamondBoxInfo = ClassCacheManager.New<IntVsInt>();
			diamondBoxInfo = new IntVsInt();
            rewards.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			diamondBoxInfo = null;

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
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //diamondBoxInfo = ClassCacheManager.New<IntVsInt>();
				diamondBoxInfo = new IntVsInt();
                diamondBoxInfo.Read(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    IntVsInt _value_ = null;
                    //_value_ = ClassCacheManager.New<IntVsInt>();
					_value_ = new IntVsInt();
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
					if(diamondBoxInfo == null)
						//diamondBoxInfo = ClassCacheManager.New<IntVsInt>();
						diamondBoxInfo = new IntVsInt();
					diamondBoxInfo.WriteWithType(buffer, ref offset);

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
    //终极试炼积分奖励信息
    public class ResTrialScoreAwardInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108113;
		public TrialScoreAwardInfo trialScoreAwardInfo; // 终极试炼积分奖励信息

    	//鏋勯�犲嚱鏁�
    	public ResTrialScoreAwardInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//trialScoreAwardInfo = ClassCacheManager.New<TrialScoreAwardInfo>();
			trialScoreAwardInfo = new TrialScoreAwardInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			trialScoreAwardInfo = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //trialScoreAwardInfo = ClassCacheManager.New<TrialScoreAwardInfo>();
				trialScoreAwardInfo = new TrialScoreAwardInfo();
                trialScoreAwardInfo.Read(buffer, ref offset);

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
					if(trialScoreAwardInfo == null)
						//trialScoreAwardInfo = ClassCacheManager.New<TrialScoreAwardInfo>();
						trialScoreAwardInfo = new TrialScoreAwardInfo();
					trialScoreAwardInfo.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //领取终极试炼积分奖励
    public class ResTrialScoreAwardGet : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108114;
		public TrialScoreAwardInfo trialScoreAwardInfo; // 终极试炼积分奖励信息
        public List<IntVsInt> rewards{get;protected set;} //奖励列表

    	//鏋勯�犲嚱鏁�
    	public ResTrialScoreAwardGet()
    	{
            rewards = new List<IntVsInt>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//trialScoreAwardInfo = ClassCacheManager.New<TrialScoreAwardInfo>();
			trialScoreAwardInfo = new TrialScoreAwardInfo();
            rewards.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			trialScoreAwardInfo = null;

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
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //trialScoreAwardInfo = ClassCacheManager.New<TrialScoreAwardInfo>();
				trialScoreAwardInfo = new TrialScoreAwardInfo();
                trialScoreAwardInfo.Read(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    IntVsInt _value_ = null;
                    //_value_ = ClassCacheManager.New<IntVsInt>();
					_value_ = new IntVsInt();
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
					if(trialScoreAwardInfo == null)
						//trialScoreAwardInfo = ClassCacheManager.New<TrialScoreAwardInfo>();
						trialScoreAwardInfo = new TrialScoreAwardInfo();
					trialScoreAwardInfo.WriteWithType(buffer, ref offset);

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
    //终极试炼开始战斗
    public class ResTrialFightStart : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108115;
		public int floor; // 层
		public int index; // 难度

    	//鏋勯�犲嚱鏁�
    	public ResTrialFightStart()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			floor = 0;
            
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
                floor = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(floor,buffer, ref offset);
					XBuffer.WriteInt(index,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //终极试炼战斗结束
    public class ResTrialFightEnd : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108116;
		public TrialInfo trialInfo; // 终极试炼基础信息
		public int result; // 0：失败 1~3星星

    	//鏋勯�犲嚱鏁�
    	public ResTrialFightEnd()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//trialInfo = ClassCacheManager.New<TrialInfo>();
			trialInfo = new TrialInfo();
			result = 0;
            
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
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //trialInfo = ClassCacheManager.New<TrialInfo>();
				trialInfo = new TrialInfo();
                trialInfo.Read(buffer, ref offset);
                result = XBuffer.ReadInt(buffer, ref offset);

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
					if(trialInfo == null)
						//trialInfo = ClassCacheManager.New<TrialInfo>();
						trialInfo = new TrialInfo();
					trialInfo.WriteWithType(buffer, ref offset);
					XBuffer.WriteInt(result,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //终极试炼排行榜
    public class ResTrialRank : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108117;
		public TrialRankItem rank; // 自己的排名
        public List<TrialRankItem> ranks{get;protected set;} //排名

    	//鏋勯�犲嚱鏁�
    	public ResTrialRank()
    	{
            ranks = new List<TrialRankItem>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//rank = ClassCacheManager.New<TrialRankItem>();
			rank = new TrialRankItem();
            ranks.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			rank = null;

            for (int a = 0,b = ranks.Count; a < b; ++a)
            {
				ranks[a] = null;
                //var _value_ = ranks[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            ranks.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //rank = ClassCacheManager.New<TrialRankItem>();
				rank = new TrialRankItem();
                rank.Read(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    TrialRankItem _value_ = null;
                    //_value_ = ClassCacheManager.New<TrialRankItem>();
					_value_ = new TrialRankItem();
                    _value_.Read(buffer, ref offset);
                    ranks.Add(_value_);
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
					if(rank == null)
						//rank = ClassCacheManager.New<TrialRankItem>();
						rank = new TrialRankItem();
					rank.WriteWithType(buffer, ref offset);

                XBuffer.WriteShort((short)ranks.Count, buffer, ref offset);
                for(int a = 0; a < ranks.Count; ++a)
                {
					if(ranks[a] == null)
						UnityEngine.Debug.LogError("ranks has nil item, idx == " + a);
					else
						ranks[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //终极试炼钻石宝箱信息
    public class ResTrialBatchBoxOpen : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108118;
        public List<IntVsInt> diamondBoxInfo{get;protected set;} //钻石宝箱信息（层数 -> 打开次数
        public List<IntVsInt> rewards{get;protected set;} //奖励列表

    	//鏋勯�犲嚱鏁�
    	public ResTrialBatchBoxOpen()
    	{
            diamondBoxInfo = new List<IntVsInt>();
            rewards = new List<IntVsInt>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            diamondBoxInfo.Clear();
            rewards.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = diamondBoxInfo.Count; a < b; ++a)
            {
				diamondBoxInfo[a] = null;
                //var _value_ = diamondBoxInfo[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            diamondBoxInfo.Clear();
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

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    IntVsInt _value_ = null;
                    //_value_ = ClassCacheManager.New<IntVsInt>();
					_value_ = new IntVsInt();
                    _value_.Read(buffer, ref offset);
                    diamondBoxInfo.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    IntVsInt _value_ = null;
                    //_value_ = ClassCacheManager.New<IntVsInt>();
					_value_ = new IntVsInt();
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

                XBuffer.WriteShort((short)diamondBoxInfo.Count, buffer, ref offset);
                for(int a = 0; a < diamondBoxInfo.Count; ++a)
                {
					if(diamondBoxInfo[a] == null)
						UnityEngine.Debug.LogError("diamondBoxInfo has nil item, idx == " + a);
					else
						diamondBoxInfo[a].WriteWithType(buffer, ref offset);
                }
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
    //组队战怪物信息
    public class ResTeamFightMonsterInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108119;
        public List<int> petIds{get;protected set;} //今日宠物

    	//鏋勯�犲嚱鏁�
    	public ResTeamFightMonsterInfo()
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
    //组队战队伍信息
    public class ResTeamFightTeamInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108120;
		public long roleId; // 队长角色ID
		public bool forbidFastEnter; // 禁止快速加入
		public int petId; // 宠物ID
        public List<TeamFightRoleInfo> teammates{get;protected set;} //队员信息

    	//鏋勯�犲嚱鏁�
    	public ResTeamFightTeamInfo()
    	{
            teammates = new List<TeamFightRoleInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			forbidFastEnter = false;
            forbidFastEnter = false;
			petId = 0;
            
            teammates.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = teammates.Count; a < b; ++a)
            {
				teammates[a] = null;
                //var _value_ = teammates[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            teammates.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                roleId = XBuffer.ReadLong(buffer, ref offset);
        		forbidFastEnter = XBuffer.ReadBool(buffer, ref offset);
                petId = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    TeamFightRoleInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<TeamFightRoleInfo>();
					_value_ = new TeamFightRoleInfo();
                    _value_.Read(buffer, ref offset);
                    teammates.Add(_value_);
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
					XBuffer.WriteLong(roleId,buffer, ref offset);
					XBuffer.WriteBool(forbidFastEnter,buffer, ref offset);
					XBuffer.WriteInt(petId,buffer, ref offset);

                XBuffer.WriteShort((short)teammates.Count, buffer, ref offset);
                for(int a = 0; a < teammates.Count; ++a)
                {
					if(teammates[a] == null)
						UnityEngine.Debug.LogError("teammates has nil item, idx == " + a);
					else
						teammates[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //组队战队员变动
    public class ResTeamFightTeammateChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108121;
		public bool enter; // 是否加入
		public long roleId; // 队长角色ID
		public TeamFightRoleInfo __teammate; // 队员信息
		private byte _teammate = 0; // 队员信息 tag
		
		public bool hasTeammate()
		{
			return this._teammate == 1;
		}
		
		public TeamFightRoleInfo teammate
		{
			set
			{
				_teammate = 1;
				__teammate = value;
			}
			
			get
			{
				return __teammate;
			}
		}

    	//鏋勯�犲嚱鏁�
    	public ResTeamFightTeammateChange()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			enter = false;
            enter = false;
			roleId = 0L;
			_teammate = 0;
			//__teammate = ClassCacheManager.New<TeamFightRoleInfo>();
			__teammate = new TeamFightRoleInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			__teammate = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
        		enter = XBuffer.ReadBool(buffer, ref offset);
                roleId = XBuffer.ReadLong(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
					//teammate = ClassCacheManager.New<TeamFightRoleInfo>();
					teammate = new TeamFightRoleInfo();
					teammate.Read(buffer, ref offset);
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
					XBuffer.WriteBool(enter,buffer, ref offset);
					XBuffer.WriteLong(roleId,buffer, ref offset);
				XBuffer.WriteByte(_teammate,buffer, ref offset);
				if (_teammate == 1)
				{
					if(teammate == null)
						//teammate = ClassCacheManager.New<TeamFightRoleInfo>();
						teammate = new TeamFightRoleInfo();
					teammate.WriteWithType(buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //组队战是否禁止自动加入
    public class ResTeamFightForbibFastEnter : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108123;
		public bool enter; // 是否自动加入

    	//鏋勯�犲嚱鏁�
    	public ResTeamFightForbibFastEnter()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			enter = false;
            enter = false;
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
        		enter = XBuffer.ReadBool(buffer, ref offset);

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
					XBuffer.WriteBool(enter,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //组队战请求战斗
    public class ResTeamFightStart : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108124;

    	//鏋勯�犲嚱鏁�
    	public ResTeamFightStart()
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
    //组队战战斗结果
    public class ResTeamFightEnd : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108125;
		public int result; // 结果（0：失败，1：成功）
		public int completeTimes; // 今日已完成次数

    	//鏋勯�犲嚱鏁�
    	public ResTeamFightEnd()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			result = 0;
            
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
                TypeEnum real_type;
                result = XBuffer.ReadInt(buffer, ref offset);
                completeTimes = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(result,buffer, ref offset);
					XBuffer.WriteInt(completeTimes,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //组队战改变上阵宠物
    public class ResTeamFightChangePet : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108126;
		public TeamFightRoleInfo teamFightRoleInfo; // 队员信息

    	//鏋勯�犲嚱鏁�
    	public ResTeamFightChangePet()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//teamFightRoleInfo = ClassCacheManager.New<TeamFightRoleInfo>();
			teamFightRoleInfo = new TeamFightRoleInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			teamFightRoleInfo = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //teamFightRoleInfo = ClassCacheManager.New<TeamFightRoleInfo>();
				teamFightRoleInfo = new TeamFightRoleInfo();
                teamFightRoleInfo.Read(buffer, ref offset);

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
					if(teamFightRoleInfo == null)
						//teamFightRoleInfo = ClassCacheManager.New<TeamFightRoleInfo>();
						teamFightRoleInfo = new TeamFightRoleInfo();
					teamFightRoleInfo.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //组队战开箱
    public class ResTeamFightOpenBox : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108127;
		public int index; // 下标
        public List<IntVsInt> awards{get;protected set;} //奖励

    	//鏋勯�犲嚱鏁�
    	public ResTeamFightOpenBox()
    	{
            awards = new List<IntVsInt>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			index = 0;
            
            awards.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = awards.Count; a < b; ++a)
            {
				awards[a] = null;
                //var _value_ = awards[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            awards.Clear();
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
                    IntVsInt _value_ = null;
                    //_value_ = ClassCacheManager.New<IntVsInt>();
					_value_ = new IntVsInt();
                    _value_.Read(buffer, ref offset);
                    awards.Add(_value_);
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

                XBuffer.WriteShort((short)awards.Count, buffer, ref offset);
                for(int a = 0; a < awards.Count; ++a)
                {
					if(awards[a] == null)
						UnityEngine.Debug.LogError("awards has nil item, idx == " + a);
					else
						awards[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //组队战通知队友成功
    public class ResTeamFightNoticeSuccess : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108128;

    	//鏋勯�犲嚱鏁�
    	public ResTeamFightNoticeSuccess()
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
    //组队战邀请好友发送成功
    public class ResTeamFightFriendInviteSuccess : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108129;

    	//鏋勯�犲嚱鏁�
    	public ResTeamFightFriendInviteSuccess()
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
    //组队战邀请发送成功
    public class ResTeamFightInviteSuccess : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 108129;
		public int type; // 1：世界 2：社团

    	//鏋勯�犲嚱鏁�
    	public ResTeamFightInviteSuccess()
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
}