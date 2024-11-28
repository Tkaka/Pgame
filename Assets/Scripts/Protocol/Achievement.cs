//Auto generated, do not edit it
//成就

using System;
using System.IO;
using System.Collections.Generic;

namespace Message.Achievement
{
    public enum TypeEnum
    {
        AchievementInfo = 1,
        AchievementRankInfo = 2,
    }

    //成就信息
    public class AchievementInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int id; // ID
        
		public long schedule; // 进度
        
		public int star; // 星级
        
		public int state; // 状态 (0进行中 1可领奖 2已完成)
        

        //鏋勯�犲嚱鏁�
        public AchievementInfo() : base()
        {
			
			id = 0;
            
			schedule = 0L;
			star = 0;
            
			state = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			schedule = 0L;
			star = 0;
            
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
                schedule = XBuffer.ReadLong(buffer, ref offset);
                star = XBuffer.ReadInt(buffer, ref offset);
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
                XBuffer.WriteLong(schedule, buffer, ref offset);
                XBuffer.WriteInt(star, buffer, ref offset);
                XBuffer.WriteInt(state, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //成就排行
    public class AchievementRankInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public long roleId; // 角色id
        
		public int core; // 分数
        
		public int rank; // 排名
        
		public int level; // 等级
        
		public int photo; // 头像
        
		public int title; // 称号
        

        //鏋勯�犲嚱鏁�
        public AchievementRankInfo() : base()
        {
			
			roleId = 0L;
			core = 0;
            
			rank = 0;
            
			level = 0;
            
			photo = 0;
            
			title = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			core = 0;
            
			rank = 0;
            
			level = 0;
            
			photo = 0;
            
			title = 0;
            

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
                core = XBuffer.ReadInt(buffer, ref offset);
                rank = XBuffer.ReadInt(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                photo = XBuffer.ReadInt(buffer, ref offset);
                title = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteLong(roleId, buffer, ref offset);
                XBuffer.WriteInt(core, buffer, ref offset);
                XBuffer.WriteInt(rank, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteInt(photo, buffer, ref offset);
                XBuffer.WriteInt(title, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //成就信息
    public class ResAchievementInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 114101;
		public int core; // 当前成就分数
		public int precedeValue; // 先手值
		public int title; // 称号id
        public List<AchievementInfo> infos{get;protected set;} //成就列表

    	//鏋勯�犲嚱鏁�
    	public ResAchievementInfo()
    	{
            infos = new List<AchievementInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			core = 0;
            
			precedeValue = 0;
            
			title = 0;
            
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
                precedeValue = XBuffer.ReadInt(buffer, ref offset);
                title = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    AchievementInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<AchievementInfo>();
					_value_ = new AchievementInfo();
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
					XBuffer.WriteInt(precedeValue,buffer, ref offset);
					XBuffer.WriteInt(title,buffer, ref offset);

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
    //成就改变
    public class ResAchievementChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 114102;
        public List<AchievementInfo> info{get;protected set;} //成就

    	//鏋勯�犲嚱鏁�
    	public ResAchievementChange()
    	{
            info = new List<AchievementInfo>();
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
                    AchievementInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<AchievementInfo>();
					_value_ = new AchievementInfo();
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
    //领取
    public class ResFinish : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 114103;
		public AchievementInfo info; // 成就
		public int core; // 当前成就分数
		public int precedeValue; // 先手值
		public int title; // 称号id

    	//鏋勯�犲嚱鏁�
    	public ResFinish()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//info = ClassCacheManager.New<AchievementInfo>();
			info = new AchievementInfo();
			core = 0;
            
			precedeValue = 0;
            
			title = 0;
            
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
                //info = ClassCacheManager.New<AchievementInfo>();
				info = new AchievementInfo();
                info.Read(buffer, ref offset);
                core = XBuffer.ReadInt(buffer, ref offset);
                precedeValue = XBuffer.ReadInt(buffer, ref offset);
                title = XBuffer.ReadInt(buffer, ref offset);

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
						//info = ClassCacheManager.New<AchievementInfo>();
						info = new AchievementInfo();
					info.WriteWithType(buffer, ref offset);
					XBuffer.WriteInt(core,buffer, ref offset);
					XBuffer.WriteInt(precedeValue,buffer, ref offset);
					XBuffer.WriteInt(title,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //成就排行
    public class ResAchievementRank : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 114104;
		public int rank; // 自己排名
        public List<AchievementRankInfo> info{get;protected set;} //成就

    	//鏋勯�犲嚱鏁�
    	public ResAchievementRank()
    	{
            info = new List<AchievementRankInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			rank = 0;
            
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
                rank = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    AchievementRankInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<AchievementRankInfo>();
					_value_ = new AchievementRankInfo();
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
					XBuffer.WriteInt(rank,buffer, ref offset);

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
    //领取
    public class ReqFinish : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 114201;
		public int id; // 成就id

    	//鏋勯�犲嚱鏁�
    	public ReqFinish()
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
    //排行榜
    public class ReqAchievementRank : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 114202;
		public int begin; // 开始index

    	//鏋勯�犲嚱鏁�
    	public ReqAchievementRank()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			begin = 0;
            
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
                begin = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(begin,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}