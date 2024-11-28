//Auto generated, do not edit it
//GM消息

using System;
using System.IO;
using System.Collections.Generic;

namespace Message.Task
{
    public enum TypeEnum
    {
        Schedule = 1,
        TaskInfo = 2,
    }

    //完成条件参数
    public class Schedule : BaseMsgStruct
    {
		public int id; // 条件id
        
		public int value; // 已完成值
        
		public int target; // 目标值
        

        //鏋勯�犲嚱鏁�
        public Schedule() : base()
        {
			
			id = 0;
            
			value = 0;
            
			target = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			value = 0;
            
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
                TypeEnum _real_type_;
                id = XBuffer.ReadInt(buffer, ref offset);
                value = XBuffer.ReadInt(buffer, ref offset);
                target = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(value, buffer, ref offset);
                XBuffer.WriteInt(target, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //任务结构
    public class TaskInfo : BaseMsgStruct
    {
		public int id; // 任务id
        
		public int state; // 状态(0进行中 1待领奖 3已完成)
        
		public long __endTime; // 过期时间点 毫秒时间戳
		private byte _endTime = 0; // 过期时间点 毫秒时间戳 tag
		
		public bool hasEndTime()
		{
			return this._endTime == 1;
		}
		
		public long endTime
		{
			set
			{
				_endTime = 1;
				__endTime = value;
			}
			
			get
			{
				return __endTime;
			}
		}
        
		public int __lastTimes; // 剩余次数
		private byte _lastTimes = 0; // 剩余次数 tag
		
		public bool hasLastTimes()
		{
			return this._lastTimes == 1;
		}
		
		public int lastTimes
		{
			set
			{
				_lastTimes = 1;
				__lastTimes = value;
			}
			
			get
			{
				return __lastTimes;
			}
		}
        
        public List<Schedule> schedule{get; protected set;} //进度

        //鏋勯�犲嚱鏁�
        public TaskInfo() : base()
        {
            schedule = new List<Schedule>(); //进度
			
			id = 0;
            
			state = 0;
            
			_endTime = 0;
			__endTime = 0L;
			_lastTimes = 0;
			__lastTimes = 0;
            

            schedule.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			state = 0;
            
			_endTime = 0;
			__endTime = 0L;
			_lastTimes = 0;
			__lastTimes = 0;
            

            schedule.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = schedule.Count; a < b; ++a)
            {
                //var _value_ = schedule[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				schedule[a] = null;
            }
            schedule.Clear();
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
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					endTime = XBuffer.ReadLong(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					lastTimes = XBuffer.ReadInt(buffer, ref offset);
				}

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    Schedule _value_ = null;
                    //_value_ = ClassCacheManager.New<Schedule>();
					_value_ = new Schedule();
                    _value_.Read(buffer, ref offset);
                    schedule.Add(_value_);
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
                XBuffer.WriteInt(state, buffer, ref offset);
				XBuffer.WriteByte(_endTime, buffer, ref offset);
				if (_endTime == 1)
				{
					XBuffer.WriteLong(endTime, buffer, ref offset);
				}
				XBuffer.WriteByte(_lastTimes, buffer, ref offset);
				if (_lastTimes == 1)
				{
					XBuffer.WriteInt(lastTimes, buffer, ref offset);
				}

                XBuffer.WriteShort((short)schedule.Count,buffer, ref offset);
                for (int a = 0; a < schedule.Count; ++a)
                {
					if(schedule[a] == null)
						UnityEngine.Debug.LogError("schedule has nil item, idx == " + a);
					else
						schedule[a].WriteWithType(buffer, ref offset);
                }
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
        public const int MsgId = 107201;
		public int id; // 任务id

    	//鏋勯�犲嚱鏁�
    	public ReqReward()
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
    //一键领奖
    public class ReqOneKeyReward : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 107202;

    	//鏋勯�犲嚱鏁�
    	public ReqOneKeyReward()
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
    //任务信息
    public class ResTaskInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 107101;
        public List<TaskInfo> taskInfos{get;protected set;} //任务列表

    	//鏋勯�犲嚱鏁�
    	public ResTaskInfo()
    	{
            taskInfos = new List<TaskInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            taskInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = taskInfos.Count; a < b; ++a)
            {
				taskInfos[a] = null;
                //var _value_ = taskInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            taskInfos.Clear();
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
                    TaskInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<TaskInfo>();
					_value_ = new TaskInfo();
                    _value_.Read(buffer, ref offset);
                    taskInfos.Add(_value_);
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

                XBuffer.WriteShort((short)taskInfos.Count, buffer, ref offset);
                for(int a = 0; a < taskInfos.Count; ++a)
                {
					if(taskInfos[a] == null)
						UnityEngine.Debug.LogError("taskInfos has nil item, idx == " + a);
					else
						taskInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //任务信息改变
    public class ResTaskInfoChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 107102;
        public List<TaskInfo> info{get;protected set;} //任务

    	//鏋勯�犲嚱鏁�
    	public ResTaskInfoChange()
    	{
            info = new List<TaskInfo>();
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
                    TaskInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<TaskInfo>();
					_value_ = new TaskInfo();
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
    //添加任务
    public class ResAddTask : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 107103;
		public bool isAdd; // 是否添加 true添加 false移除
        public List<TaskInfo> info{get;protected set;} //任务

    	//鏋勯�犲嚱鏁�
    	public ResAddTask()
    	{
            info = new List<TaskInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			isAdd = false;
            isAdd = false;
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
        		isAdd = XBuffer.ReadBool(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    TaskInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<TaskInfo>();
					_value_ = new TaskInfo();
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
					XBuffer.WriteBool(isAdd,buffer, ref offset);

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
}