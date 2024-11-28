//Auto generated, do not edit it
//邮件系统

using System;
using System.IO;
using System.Collections.Generic;
using Message.Bag;

namespace Message.Mail
{
    public enum TypeEnum
    {
        MailInfo = 1,
        MailState = 2,
    }

    //邮件结构
    public class MailInfo : BaseMsgStruct
    {
		public long id; // 邮件id
        
		public string topic; // 主题
        
		public string sender; // 发送者
        
		public string content; // 内容
        
		public long time; // 时间
        
		public int state; // 状态(0未读 1已读(未领取) 2已领取(已读))
        
        public List<ItemInfo> items{get; protected set;} //道具列表

        //鏋勯�犲嚱鏁�
        public MailInfo() : base()
        {
            items = new List<ItemInfo>(); //道具列表
			
			id = 0L;
			topic = "";
			sender = "";
			content = "";
			time = 0L;
			state = 0;
            

            items.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0L;
			topic = "";
			sender = "";
			content = "";
			time = 0L;
			state = 0;
            

            items.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

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
                id = XBuffer.ReadLong(buffer, ref offset);
                topic = XBuffer.ReadString(buffer, ref offset);
                sender = XBuffer.ReadString(buffer, ref offset);
                content = XBuffer.ReadString(buffer, ref offset);
                time = XBuffer.ReadLong(buffer, ref offset);
                state = XBuffer.ReadInt(buffer, ref offset);

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
            XBuffer.WriteByte(1, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteLong(id, buffer, ref offset);
                XBuffer.WriteString(topic, buffer, ref offset);
                XBuffer.WriteString(sender, buffer, ref offset);
                XBuffer.WriteString(content, buffer, ref offset);
                XBuffer.WriteLong(time, buffer, ref offset);
                XBuffer.WriteInt(state, buffer, ref offset);

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
    //邮件结构
    public class MailState : BaseMsgStruct
    {
		public long id; // 邮件id
        
		public int state; // 状态(0未读 1已读(未领取) 2已领取(已读))
        

        //鏋勯�犲嚱鏁�
        public MailState() : base()
        {
			
			id = 0L;
			state = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0L;
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
                id = XBuffer.ReadLong(buffer, ref offset);
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
                XBuffer.WriteInt(state, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //领取邮件
    public class ReqReceive : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 112201;
        public List<long> ids{get;protected set;} //邮件id

    	//鏋勯�犲嚱鏁�
    	public ReqReceive()
    	{
            ids = new List<long>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            ids.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            ids.Clear();
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
                    var _value_ = XBuffer.ReadLong(buffer, ref offset);
                    ids.Add(_value_);
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

                XBuffer.WriteShort((short)ids.Count, buffer, ref offset);
                for(int a = 0; a < ids.Count; ++a)
                {
        			XBuffer.WriteLong(ids[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //删除邮件
    public class ReqDelMail : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 112202;
		public long id; // 邮件id

    	//鏋勯�犲嚱鏁�
    	public ReqDelMail()
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
    //读邮件
    public class ReqReadMail : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 112203;
		public long id; // 邮件id

    	//鏋勯�犲嚱鏁�
    	public ReqReadMail()
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
    //邮件列表
    public class ResMailList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 112101;
        public List<MailInfo> mails{get;protected set;} //邮件

    	//鏋勯�犲嚱鏁�
    	public ResMailList()
    	{
            mails = new List<MailInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            mails.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = mails.Count; a < b; ++a)
            {
				mails[a] = null;
                //var _value_ = mails[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            mails.Clear();
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
                    MailInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<MailInfo>();
					_value_ = new MailInfo();
                    _value_.Read(buffer, ref offset);
                    mails.Add(_value_);
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

                XBuffer.WriteShort((short)mails.Count, buffer, ref offset);
                for(int a = 0; a < mails.Count; ++a)
                {
					if(mails[a] == null)
						UnityEngine.Debug.LogError("mails has nil item, idx == " + a);
					else
						mails[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //添加邮件
    public class ResAddMail : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 112102;
		public MailInfo mail; // 邮件

    	//鏋勯�犲嚱鏁�
    	public ResAddMail()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//mail = ClassCacheManager.New<MailInfo>();
			mail = new MailInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			mail = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //mail = ClassCacheManager.New<MailInfo>();
				mail = new MailInfo();
                mail.Read(buffer, ref offset);

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
					if(mail == null)
						//mail = ClassCacheManager.New<MailInfo>();
						mail = new MailInfo();
					mail.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //状态改变
    public class ResMailStateChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 112103;
        public List<MailState> mails{get;protected set;} //邮件

    	//鏋勯�犲嚱鏁�
    	public ResMailStateChange()
    	{
            mails = new List<MailState>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            mails.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = mails.Count; a < b; ++a)
            {
				mails[a] = null;
                //var _value_ = mails[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            mails.Clear();
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
                    MailState _value_ = null;
                    //_value_ = ClassCacheManager.New<MailState>();
					_value_ = new MailState();
                    _value_.Read(buffer, ref offset);
                    mails.Add(_value_);
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

                XBuffer.WriteShort((short)mails.Count, buffer, ref offset);
                for(int a = 0; a < mails.Count; ++a)
                {
					if(mails[a] == null)
						UnityEngine.Debug.LogError("mails has nil item, idx == " + a);
					else
						mails[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //删除邮件
    public class ResDelMail : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 112104;
        public List<long> ids{get;protected set;} //邮件

    	//鏋勯�犲嚱鏁�
    	public ResDelMail()
    	{
            ids = new List<long>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            ids.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            ids.Clear();
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
                    var _value_ = XBuffer.ReadLong(buffer, ref offset);
                    ids.Add(_value_);
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

                XBuffer.WriteShort((short)ids.Count, buffer, ref offset);
                for(int a = 0; a < ids.Count; ++a)
                {
        			XBuffer.WriteLong(ids[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}