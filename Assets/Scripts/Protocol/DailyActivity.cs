//Auto generated, do not edit it
//日常活动

using System;
using System.IO;
using System.Collections.Generic;

namespace Message.DailyActivity
{
    public enum TypeEnum
    {
        SubActivityData = 1,
        ActivityData = 2,
        SubActivity = 3,
        Activity = 4,
        ActivityState = 5,
    }

    //活动小项数据
    public class SubActivityData : BaseMsgStruct
    {
		public int id; // 活动id（表id）
        
		public long __startTime; // 活动开始时间戳
		private byte _startTime = 0; // 活动开始时间戳 tag
		
		public bool hasStartTime()
		{
			return this._startTime == 1;
		}
		
		public long startTime
		{
			set
			{
				_startTime = 1;
				__startTime = value;
			}
			
			get
			{
				return __startTime;
			}
		}
        
		public int progress; // 进度
        
		public int totalCount; // 总数
        
		public string reward; // 奖励
        
		public string condition; // 完成条件
        
		public int state; // 状态（1可领取/可兑换，2不可领取/不可兑换/未激活，3已领取/已兑换/已激活）
        
		public string __cost; // 兑换消耗
		private byte _cost = 0; // 兑换消耗 tag
		
		public bool hasCost()
		{
			return this._cost == 1;
		}
		
		public string cost
		{
			set
			{
				_cost = 1;
				__cost = value;
			}
			
			get
			{
				return __cost;
			}
		}
        
		public string desc; // 描述
        
		public int jump; // 跳转类型
        

        //鏋勯�犲嚱鏁�
        public SubActivityData() : base()
        {
			
			id = 0;
            
			_startTime = 0;
			__startTime = 0L;
			progress = 0;
            
			totalCount = 0;
            
			reward = "";
			condition = "";
			state = 0;
            
			_cost = 0;
			__cost = "";
			desc = "";
			jump = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			_startTime = 0;
			__startTime = 0L;
			progress = 0;
            
			totalCount = 0;
            
			reward = "";
			condition = "";
			state = 0;
            
			_cost = 0;
			__cost = "";
			desc = "";
			jump = 0;
            

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
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					startTime = XBuffer.ReadLong(buffer, ref offset);
				}
                progress = XBuffer.ReadInt(buffer, ref offset);
                totalCount = XBuffer.ReadInt(buffer, ref offset);
                reward = XBuffer.ReadString(buffer, ref offset);
                condition = XBuffer.ReadString(buffer, ref offset);
                state = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					cost = XBuffer.ReadString(buffer, ref offset);
				}
                desc = XBuffer.ReadString(buffer, ref offset);
                jump = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(id, buffer, ref offset);
				XBuffer.WriteByte(_startTime, buffer, ref offset);
				if (_startTime == 1)
				{
					XBuffer.WriteLong(startTime, buffer, ref offset);
				}
                XBuffer.WriteInt(progress, buffer, ref offset);
                XBuffer.WriteInt(totalCount, buffer, ref offset);
                XBuffer.WriteString(reward, buffer, ref offset);
                XBuffer.WriteString(condition, buffer, ref offset);
                XBuffer.WriteInt(state, buffer, ref offset);
				XBuffer.WriteByte(_cost, buffer, ref offset);
				if (_cost == 1)
				{
					XBuffer.WriteString(cost, buffer, ref offset);
				}
                XBuffer.WriteString(desc, buffer, ref offset);
                XBuffer.WriteInt(jump, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //活动数据
    public class ActivityData : BaseMsgStruct
    {
		public int id; // 活动id（表id）
        
		public long startTime; // 活动开始时间戳
        
		public long endTime; // 活动结束时间戳
        
		public string title; // 标题
        
		public string leftTitle; // 左侧标题
        
		public string desc; // 描述
        
		public int type; // 类型
        
		public int sort; // 显示优先级
        
		public string icon; // 图标
        
		public string __mark; // 角标
		private byte _mark = 0; // 角标 tag
		
		public bool hasMark()
		{
			return this._mark == 1;
		}
		
		public string mark
		{
			set
			{
				_mark = 1;
				__mark = value;
			}
			
			get
			{
				return __mark;
			}
		}
        
        public List<SubActivityData> child{get; protected set;} //小项数据

        //鏋勯�犲嚱鏁�
        public ActivityData() : base()
        {
            child = new List<SubActivityData>(); //小项数据
			
			id = 0;
            
			startTime = 0L;
			endTime = 0L;
			title = "";
			leftTitle = "";
			desc = "";
			type = 0;
            
			sort = 0;
            
			icon = "";
			_mark = 0;
			__mark = "";

            child.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			startTime = 0L;
			endTime = 0L;
			title = "";
			leftTitle = "";
			desc = "";
			type = 0;
            
			sort = 0;
            
			icon = "";
			_mark = 0;
			__mark = "";

            child.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = child.Count; a < b; ++a)
            {
                //var _value_ = child[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				child[a] = null;
            }
            child.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                id = XBuffer.ReadInt(buffer, ref offset);
                startTime = XBuffer.ReadLong(buffer, ref offset);
                endTime = XBuffer.ReadLong(buffer, ref offset);
                title = XBuffer.ReadString(buffer, ref offset);
                leftTitle = XBuffer.ReadString(buffer, ref offset);
                desc = XBuffer.ReadString(buffer, ref offset);
                type = XBuffer.ReadInt(buffer, ref offset);
                sort = XBuffer.ReadInt(buffer, ref offset);
                icon = XBuffer.ReadString(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					mark = XBuffer.ReadString(buffer, ref offset);
				}

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    SubActivityData _value_ = null;
                    //_value_ = ClassCacheManager.New<SubActivityData>();
					_value_ = new SubActivityData();
                    _value_.Read(buffer, ref offset);
                    child.Add(_value_);
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
                XBuffer.WriteInt(id, buffer, ref offset);
                XBuffer.WriteLong(startTime, buffer, ref offset);
                XBuffer.WriteLong(endTime, buffer, ref offset);
                XBuffer.WriteString(title, buffer, ref offset);
                XBuffer.WriteString(leftTitle, buffer, ref offset);
                XBuffer.WriteString(desc, buffer, ref offset);
                XBuffer.WriteInt(type, buffer, ref offset);
                XBuffer.WriteInt(sort, buffer, ref offset);
                XBuffer.WriteString(icon, buffer, ref offset);
				XBuffer.WriteByte(_mark, buffer, ref offset);
				if (_mark == 1)
				{
					XBuffer.WriteString(mark, buffer, ref offset);
				}

                XBuffer.WriteShort((short)child.Count,buffer, ref offset);
                for (int a = 0; a < child.Count; ++a)
                {
					if(child[a] == null)
						UnityEngine.Debug.LogError("child has nil item, idx == " + a);
					else
						child[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //活动小项
    public class SubActivity : BaseMsgStruct
    {
		public int id; // 活动id（表id）
        
		public int progress; // 进度
        
		public int totalCount; // 总数
        
		public int version; // 版本号
        
		public long __startTime; // 活动开始时间戳
		private byte _startTime = 0; // 活动开始时间戳 tag
		
		public bool hasStartTime()
		{
			return this._startTime == 1;
		}
		
		public long startTime
		{
			set
			{
				_startTime = 1;
				__startTime = value;
			}
			
			get
			{
				return __startTime;
			}
		}
        
		public int state; // 状态（1可领取，2不可领取）
        

        //鏋勯�犲嚱鏁�
        public SubActivity() : base()
        {
			
			id = 0;
            
			progress = 0;
            
			totalCount = 0;
            
			version = 0;
            
			_startTime = 0;
			__startTime = 0L;
			state = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			progress = 0;
            
			totalCount = 0;
            
			version = 0;
            
			_startTime = 0;
			__startTime = 0L;
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
                progress = XBuffer.ReadInt(buffer, ref offset);
                totalCount = XBuffer.ReadInt(buffer, ref offset);
                version = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					startTime = XBuffer.ReadLong(buffer, ref offset);
				}
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
            XBuffer.WriteByte(3, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(id, buffer, ref offset);
                XBuffer.WriteInt(progress, buffer, ref offset);
                XBuffer.WriteInt(totalCount, buffer, ref offset);
                XBuffer.WriteInt(version, buffer, ref offset);
				XBuffer.WriteByte(_startTime, buffer, ref offset);
				if (_startTime == 1)
				{
					XBuffer.WriteLong(startTime, buffer, ref offset);
				}
                XBuffer.WriteInt(state, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //活动数据
    public class Activity : BaseMsgStruct
    {
		public int id; // 活动id（表id）
        
		public long startTime; // 活动开始时间戳
        
		public long endTime; // 活动结束时间戳
        
		public int version; // 活动版本号
        
        public List<SubActivity> child{get; protected set;} //小项数据

        //鏋勯�犲嚱鏁�
        public Activity() : base()
        {
            child = new List<SubActivity>(); //小项数据
			
			id = 0;
            
			startTime = 0L;
			endTime = 0L;
			version = 0;
            

            child.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			startTime = 0L;
			endTime = 0L;
			version = 0;
            

            child.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = child.Count; a < b; ++a)
            {
                //var _value_ = child[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				child[a] = null;
            }
            child.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                id = XBuffer.ReadInt(buffer, ref offset);
                startTime = XBuffer.ReadLong(buffer, ref offset);
                endTime = XBuffer.ReadLong(buffer, ref offset);
                version = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    SubActivity _value_ = null;
                    //_value_ = ClassCacheManager.New<SubActivity>();
					_value_ = new SubActivity();
                    _value_.Read(buffer, ref offset);
                    child.Add(_value_);
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
                XBuffer.WriteInt(id, buffer, ref offset);
                XBuffer.WriteLong(startTime, buffer, ref offset);
                XBuffer.WriteLong(endTime, buffer, ref offset);
                XBuffer.WriteInt(version, buffer, ref offset);

                XBuffer.WriteShort((short)child.Count,buffer, ref offset);
                for (int a = 0; a < child.Count; ++a)
                {
					if(child[a] == null)
						UnityEngine.Debug.LogError("child has nil item, idx == " + a);
					else
						child[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //活动状态
    public class ActivityState : BaseMsgStruct
    {
		public int id; // 活动id
        
		public int subId; // 活动小项id
        
		public int progress; // 进度
        
		public int state; // 状态
        

        //鏋勯�犲嚱鏁�
        public ActivityState() : base()
        {
			
			id = 0;
            
			subId = 0;
            
			progress = 0;
            
			state = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			subId = 0;
            
			progress = 0;
            
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
                subId = XBuffer.ReadInt(buffer, ref offset);
                progress = XBuffer.ReadInt(buffer, ref offset);
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
            XBuffer.WriteByte(5, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(id, buffer, ref offset);
                XBuffer.WriteInt(subId, buffer, ref offset);
                XBuffer.WriteInt(progress, buffer, ref offset);
                XBuffer.WriteInt(state, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //活动数据
    public class ReqActivityInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 131201;
        public List<int> actList{get;protected set;} //请求的活动列表（如果长度为0，那么全部下发）

    	//鏋勯�犲嚱鏁�
    	public ReqActivityInfo()
    	{
            actList = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            actList.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            actList.Clear();
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
            		actList.Add(_value_);
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

                XBuffer.WriteShort((short)actList.Count, buffer, ref offset);
                for(int a = 0; a < actList.Count; ++a)
                {
        			XBuffer.WriteInt(actList[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //领奖励/兑换
    public class ReqTaskReward : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 131202;
		public int id; // 活动id
		public int subId; // 活动小项id
		public int num; // 数量

    	//鏋勯�犲嚱鏁�
    	public ReqTaskReward()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			subId = 0;
            
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
                id = XBuffer.ReadInt(buffer, ref offset);
                subId = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(id,buffer, ref offset);
					XBuffer.WriteInt(subId,buffer, ref offset);
					XBuffer.WriteInt(num,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //活动数据
    public class ResActivityInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 131101;
        public List<Activity> actList{get;protected set;} //活动列表
        public List<ActivityData> actDataList{get;protected set;} //活动列表

    	//鏋勯�犲嚱鏁�
    	public ResActivityInfo()
    	{
            actList = new List<Activity>();
            actDataList = new List<ActivityData>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            actList.Clear();
            actDataList.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = actList.Count; a < b; ++a)
            {
				actList[a] = null;
                //var _value_ = actList[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            actList.Clear();
            for (int a = 0,b = actDataList.Count; a < b; ++a)
            {
				actDataList[a] = null;
                //var _value_ = actDataList[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            actDataList.Clear();
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
                    Activity _value_ = null;
                    //_value_ = ClassCacheManager.New<Activity>();
					_value_ = new Activity();
                    _value_.Read(buffer, ref offset);
                    actList.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    ActivityData _value_ = null;
                    //_value_ = ClassCacheManager.New<ActivityData>();
					_value_ = new ActivityData();
                    _value_.Read(buffer, ref offset);
                    actDataList.Add(_value_);
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

                XBuffer.WriteShort((short)actList.Count, buffer, ref offset);
                for(int a = 0; a < actList.Count; ++a)
                {
					if(actList[a] == null)
						UnityEngine.Debug.LogError("actList has nil item, idx == " + a);
					else
						actList[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)actDataList.Count, buffer, ref offset);
                for(int a = 0; a < actDataList.Count; ++a)
                {
					if(actDataList[a] == null)
						UnityEngine.Debug.LogError("actDataList has nil item, idx == " + a);
					else
						actDataList[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //活动数据
    public class ResChangeSubActivityState : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 131102;
		public int id; // 活动id
		public int subId; // 活动小项id
		public int progress; // 进度
		public int state; // 状态

    	//鏋勯�犲嚱鏁�
    	public ResChangeSubActivityState()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			subId = 0;
            
			progress = 0;
            
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
                id = XBuffer.ReadInt(buffer, ref offset);
                subId = XBuffer.ReadInt(buffer, ref offset);
                progress = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(id,buffer, ref offset);
					XBuffer.WriteInt(subId,buffer, ref offset);
					XBuffer.WriteInt(progress,buffer, ref offset);
					XBuffer.WriteInt(state,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //活动状态改变
    public class ResActivityChangeState : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 131103;
		public int id; // 活动id
		public int state; // 状态 2结束 3关闭

    	//鏋勯�犲嚱鏁�
    	public ResActivityChangeState()
    	{
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
                TypeEnum real_type;
                id = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(id,buffer, ref offset);
					XBuffer.WriteInt(state,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //活动进度改变
    public class ResActivityProgressChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 131104;
        public List<ActivityState> activitys{get;protected set;} //活动

    	//鏋勯�犲嚱鏁�
    	public ResActivityProgressChange()
    	{
            activitys = new List<ActivityState>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            activitys.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = activitys.Count; a < b; ++a)
            {
				activitys[a] = null;
                //var _value_ = activitys[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            activitys.Clear();
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
                    ActivityState _value_ = null;
                    //_value_ = ClassCacheManager.New<ActivityState>();
					_value_ = new ActivityState();
                    _value_.Read(buffer, ref offset);
                    activitys.Add(_value_);
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

                XBuffer.WriteShort((short)activitys.Count, buffer, ref offset);
                for(int a = 0; a < activitys.Count; ++a)
                {
					if(activitys[a] == null)
						UnityEngine.Debug.LogError("activitys has nil item, idx == " + a);
					else
						activitys[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}