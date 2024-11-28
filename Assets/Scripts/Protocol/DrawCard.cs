//Auto generated, do not edit it
//抽卡消息

using System;
using System.IO;
using System.Collections.Generic;

namespace Message.DrawCard
{
    public enum TypeEnum
    {
        ItemInfo = 1,
        NumInfo = 2,
        DrawInfo = 3,
    }

    //奖励
    public class ItemInfo : BaseMsgStruct
    {
		public int id; // 道具id
        
		public int num; // 数量
        

        //鏋勯�犲嚱鏁�
        public ItemInfo() : base()
        {
			
			id = 0;
            
			num = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
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
                TypeEnum _real_type_;
                id = XBuffer.ReadInt(buffer, ref offset);
                num = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(num, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //抽取次数info
    public class NumInfo : BaseMsgStruct
    {
		public int num; // 次数类型
        
		public int buyNum; // 总抽取次数
        
		public int __freeBuyNum; // 免费抽取次数
		private byte _freeBuyNum = 0; // 免费抽取次数 tag
		
		public bool hasFreeBuyNum()
		{
			return this._freeBuyNum == 1;
		}
		
		public int freeBuyNum
		{
			set
			{
				_freeBuyNum = 1;
				__freeBuyNum = value;
			}
			
			get
			{
				return __freeBuyNum;
			}
		}
        
		public long __lastFlushTime; // 刷新时间点
		private byte _lastFlushTime = 0; // 刷新时间点 tag
		
		public bool hasLastFlushTime()
		{
			return this._lastFlushTime == 1;
		}
		
		public long lastFlushTime
		{
			set
			{
				_lastFlushTime = 1;
				__lastFlushTime = value;
			}
			
			get
			{
				return __lastFlushTime;
			}
		}
        
		public bool __halfPrice; // 是否半价
		private byte _halfPrice = 0; // 是否半价 tag
		
		public bool hasHalfPrice()
		{
			return this._halfPrice == 1;
		}
		
		public bool halfPrice
		{
			set
			{
				_halfPrice = 1;
				__halfPrice = value;
			}
			
			get
			{
				return __halfPrice;
			}
		}
        

        //鏋勯�犲嚱鏁�
        public NumInfo() : base()
        {
			
			num = 0;
            
			buyNum = 0;
            
			_freeBuyNum = 0;
			__freeBuyNum = 0;
            
			_lastFlushTime = 0;
			__lastFlushTime = 0L;
			_halfPrice = 0;
			__halfPrice = false;
            halfPrice = false;

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			num = 0;
            
			buyNum = 0;
            
			_freeBuyNum = 0;
			__freeBuyNum = 0;
            
			_lastFlushTime = 0;
			__lastFlushTime = 0L;
			_halfPrice = 0;
			__halfPrice = false;
            halfPrice = false;

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
                num = XBuffer.ReadInt(buffer, ref offset);
                buyNum = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					freeBuyNum = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					lastFlushTime = XBuffer.ReadLong(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					halfPrice = XBuffer.ReadBool(buffer, ref offset);
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
                XBuffer.WriteInt(num, buffer, ref offset);
                XBuffer.WriteInt(buyNum, buffer, ref offset);
				XBuffer.WriteByte(_freeBuyNum, buffer, ref offset);
				if (_freeBuyNum == 1)
				{
					XBuffer.WriteInt(freeBuyNum, buffer, ref offset);
				}
				XBuffer.WriteByte(_lastFlushTime, buffer, ref offset);
				if (_lastFlushTime == 1)
				{
					XBuffer.WriteLong(lastFlushTime, buffer, ref offset);
				}
				XBuffer.WriteByte(_halfPrice, buffer, ref offset);
				if (_halfPrice == 1)
				{
					XBuffer.WriteBool(halfPrice, buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //奖励
    public class DrawInfo : BaseMsgStruct
    {
		public int type; // 类型 1金币 2钻石
        
		public NumInfo numInfo; // info
        

        //鏋勯�犲嚱鏁�
        public DrawInfo() : base()
        {
			
			type = 0;
            
			//numInfo = ClassCacheManager.New<NumInfo>();
			numInfo = new NumInfo();

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			type = 0;
            
			//numInfo = ClassCacheManager.New<NumInfo>();
			numInfo = new NumInfo();

        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			numInfo = null;

        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                type = XBuffer.ReadInt(buffer, ref offset);
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //numInfo = ClassCacheManager.New<NumInfo>();
				numInfo = new NumInfo();
                numInfo.Read(buffer, ref offset);

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
                XBuffer.WriteInt(type, buffer, ref offset);
                if(numInfo==null)
                    //numInfo = ClassCacheManager.New<NumInfo>();
					numInfo = new NumInfo();
                numInfo.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //抽奖
    public class ReqDraw : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 110201;
		public int type; // 类型 1金币 2钻石
		public int num; // 抽取次数 
		public bool free; // 是否免费
		public bool __useTicket; // 是否使用替换券
		private byte _useTicket = 0; // 是否使用替换券 tag
		
		public bool hasUseTicket()
		{
			return this._useTicket == 1;
		}
		
		public bool useTicket
		{
			set
			{
				_useTicket = 1;
				__useTicket = value;
			}
			
			get
			{
				return __useTicket;
			}
		}

    	//鏋勯�犲嚱鏁�
    	public ReqDraw()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			type = 0;
            
			num = 0;
            
			free = false;
            free = false;
			_useTicket = 0;
			__useTicket = false;
            useTicket = false;
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
                num = XBuffer.ReadInt(buffer, ref offset);
        		free = XBuffer.ReadBool(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					useTicket = XBuffer.ReadBool(buffer, ref offset);
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
					XBuffer.WriteInt(type,buffer, ref offset);
					XBuffer.WriteInt(num,buffer, ref offset);
					XBuffer.WriteBool(free,buffer, ref offset);
				XBuffer.WriteByte(_useTicket,buffer, ref offset);
				if (_useTicket == 1)
				{
					XBuffer.WriteBool(useTicket,buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //抽奖
    public class ResDraw : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 110101;
		public DrawInfo info; // DrawInfo
        public List<ItemInfo> items{get;protected set;} //道具列表

    	//鏋勯�犲嚱鏁�
    	public ResDraw()
    	{
            items = new List<ItemInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//info = ClassCacheManager.New<DrawInfo>();
			info = new DrawInfo();
            items.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			info = null;

            for (int a = 0,b = items.Count; a < b; ++a)
            {
				items[a] = null;
                //var _value_ = items[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            items.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //info = ClassCacheManager.New<DrawInfo>();
				info = new DrawInfo();
                info.Read(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
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

    	//鍐欏叆鏁版嵁
    	public override void Write(byte[] buffer, ref int offset)
    	{
            try
            {
                base.Write(buffer, ref offset);
					if(info == null)
						//info = ClassCacheManager.New<DrawInfo>();
						info = new DrawInfo();
					info.WriteWithType(buffer, ref offset);

                XBuffer.WriteShort((short)items.Count, buffer, ref offset);
                for(int a = 0; a < items.Count; ++a)
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
    //跨天
    public class ResAcross : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 110102;

    	//鏋勯�犲嚱鏁�
    	public ResAcross()
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
    //抽奖
    public class ResDrawInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 110103;
        public List<DrawInfo> drawInfo{get;protected set;} //抽卡信息

    	//鏋勯�犲嚱鏁�
    	public ResDrawInfo()
    	{
            drawInfo = new List<DrawInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            drawInfo.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = drawInfo.Count; a < b; ++a)
            {
				drawInfo[a] = null;
                //var _value_ = drawInfo[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            drawInfo.Clear();
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
                    DrawInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<DrawInfo>();
					_value_ = new DrawInfo();
                    _value_.Read(buffer, ref offset);
                    drawInfo.Add(_value_);
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

                XBuffer.WriteShort((short)drawInfo.Count, buffer, ref offset);
                for(int a = 0; a < drawInfo.Count; ++a)
                {
					if(drawInfo[a] == null)
						UnityEngine.Debug.LogError("drawInfo has nil item, idx == " + a);
					else
						drawInfo[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}