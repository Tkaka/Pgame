//Auto generated, do not edit it
//天赋

using System;
using System.IO;
using System.Collections.Generic;

namespace Message.Talent
{
    public enum TypeEnum
    {
        TalentInfo = 1,
        TalentPage = 2,
    }

    //天赋
    public class TalentInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int id; // ID
        
		public int level; // 等级 -1未开启
        

        //鏋勯�犲嚱鏁�
        public TalentInfo() : base()
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
                XBuffer.WriteInt(level, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //天赋页
    public class TalentPage : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int num; // 消耗天赋点
        
		public int id; // 天赋页ID
        
        public List<TalentInfo> talents{get; protected set;} //天赋

        //鏋勯�犲嚱鏁�
        public TalentPage() : base()
        {
            talents = new List<TalentInfo>(); //天赋
			
			num = 0;
            
			id = 0;
            

            talents.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			num = 0;
            
			id = 0;
            

            talents.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = talents.Count; a < b; ++a)
            {
                //var _value_ = talents[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				talents[a] = null;
            }
            talents.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                num = XBuffer.ReadInt(buffer, ref offset);
                id = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    TalentInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<TalentInfo>();
					_value_ = new TalentInfo();
                    _value_.Read(buffer, ref offset);
                    talents.Add(_value_);
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
                XBuffer.WriteInt(num, buffer, ref offset);
                XBuffer.WriteInt(id, buffer, ref offset);

                XBuffer.WriteShort((short)talents.Count,buffer, ref offset);
                for (int a = 0; a < talents.Count; ++a)
                {
					if(talents[a] == null)
						UnityEngine.Debug.LogError("talents has nil item, idx == " + a);
					else
						talents[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //信息
    public class ResTalentInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 115101;
        public List<TalentPage> talents{get;protected set;} //天赋

    	//鏋勯�犲嚱鏁�
    	public ResTalentInfo()
    	{
            talents = new List<TalentPage>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            talents.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = talents.Count; a < b; ++a)
            {
				talents[a] = null;
                //var _value_ = talents[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            talents.Clear();
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
                    TalentPage _value_ = null;
                    //_value_ = ClassCacheManager.New<TalentPage>();
					_value_ = new TalentPage();
                    _value_.Read(buffer, ref offset);
                    talents.Add(_value_);
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

                XBuffer.WriteShort((short)talents.Count, buffer, ref offset);
                for(int a = 0; a < talents.Count; ++a)
                {
					if(talents[a] == null)
						UnityEngine.Debug.LogError("talents has nil item, idx == " + a);
					else
						talents[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //信息改变
    public class ResChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 115102;
		public int num; // 消耗天赋点
		public int id; // 天赋页ID
		public TalentInfo talent; // 天赋

    	//鏋勯�犲嚱鏁�
    	public ResChange()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			num = 0;
            
			id = 0;
            
			//talent = ClassCacheManager.New<TalentInfo>();
			talent = new TalentInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			talent = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                num = XBuffer.ReadInt(buffer, ref offset);
                id = XBuffer.ReadInt(buffer, ref offset);
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //talent = ClassCacheManager.New<TalentInfo>();
				talent = new TalentInfo();
                talent.Read(buffer, ref offset);

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
					XBuffer.WriteInt(id,buffer, ref offset);
					if(talent == null)
						//talent = ClassCacheManager.New<TalentInfo>();
						talent = new TalentInfo();
					talent.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //解锁天赋
    public class ResUnlockTalent : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 115103;
		public TalentInfo info; // 天赋

    	//鏋勯�犲嚱鏁�
    	public ResUnlockTalent()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//info = ClassCacheManager.New<TalentInfo>();
			info = new TalentInfo();
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
                //info = ClassCacheManager.New<TalentInfo>();
				info = new TalentInfo();
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
						//info = ClassCacheManager.New<TalentInfo>();
						info = new TalentInfo();
					info.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //解锁天赋页
    public class ResUnlockTalentPage : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 115104;
		public TalentPage page; // 天赋页

    	//鏋勯�犲嚱鏁�
    	public ResUnlockTalentPage()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//page = ClassCacheManager.New<TalentPage>();
			page = new TalentPage();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			page = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //page = ClassCacheManager.New<TalentPage>();
				page = new TalentPage();
                page.Read(buffer, ref offset);

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
					if(page == null)
						//page = ClassCacheManager.New<TalentPage>();
						page = new TalentPage();
					page.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //升级
    public class ReqLevel : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 115201;
		public int id; // 天赋id

    	//鏋勯�犲嚱鏁�
    	public ReqLevel()
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
    //重置
    public class ReqReset : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 115202;

    	//鏋勯�犲嚱鏁�
    	public ReqReset()
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