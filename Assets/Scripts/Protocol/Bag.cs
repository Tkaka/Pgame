//Auto generated, do not edit it
//背包消息

using System;
using System.IO;
using System.Collections.Generic;

namespace Message.Bag
{
    public enum TypeEnum
    {
        ItemInfo = 1,
        GridInfo = 2,
        SellInfo = 3,
    }

    //道具信息
    public class ItemInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int id; // 道具Id
        
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
    //格子信息
    public class GridInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int id; // 格子Id
        
		public ItemInfo __itemInfo; // 道具信息
		private byte _itemInfo = 0; // 道具信息 tag
		
		public bool hasItemInfo()
		{
			return this._itemInfo == 1;
		}
		
		public ItemInfo itemInfo
		{
			set
			{
				_itemInfo = 1;
				__itemInfo = value;
			}
			
			get
			{
				return __itemInfo;
			}
		}
        

        //鏋勯�犲嚱鏁�
        public GridInfo() : base()
        {
			
			id = 0;
            
			_itemInfo = 0;
			//__itemInfo = ClassCacheManager.New<ItemInfo>();
			__itemInfo = new ItemInfo();

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			_itemInfo = 0;
			//__itemInfo = ClassCacheManager.New<ItemInfo>();
			__itemInfo = new ItemInfo();

        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			__itemInfo = null;

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
					_real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
					//itemInfo = ClassCacheManager.New<ItemInfo>();
					itemInfo = new ItemInfo();
					itemInfo.Read(buffer, ref offset);
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
                XBuffer.WriteInt(id, buffer, ref offset);
				XBuffer.WriteByte(_itemInfo, buffer, ref offset);
				if (_itemInfo == 1)
				{
					if(itemInfo==null)
						//itemInfo = ClassCacheManager.New<ItemInfo>();
						itemInfo = new ItemInfo();
					itemInfo.WriteWithType(buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //出售道具信息
    public class SellInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int gridId; // 格子id
        
		public int num; // 道具数量
        

        //鏋勯�犲嚱鏁�
        public SellInfo() : base()
        {
			
			gridId = 0;
            
			num = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			gridId = 0;
            
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
                gridId = XBuffer.ReadInt(buffer, ref offset);
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
            XBuffer.WriteByte(3, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(gridId, buffer, ref offset);
                XBuffer.WriteInt(num, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //出售道具
    public class ReqItemSell : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 101201;
        public List<SellInfo> sellInfo{get;protected set;} //出售道具信息

    	//鏋勯�犲嚱鏁�
    	public ReqItemSell()
    	{
            sellInfo = new List<SellInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            sellInfo.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = sellInfo.Count; a < b; ++a)
            {
				sellInfo[a] = null;
                //var _value_ = sellInfo[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            sellInfo.Clear();
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
                    SellInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<SellInfo>();
					_value_ = new SellInfo();
                    _value_.Read(buffer, ref offset);
                    sellInfo.Add(_value_);
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

                XBuffer.WriteShort((short)sellInfo.Count, buffer, ref offset);
                for(int a = 0; a < sellInfo.Count; ++a)
                {
					if(sellInfo[a] == null)
						UnityEngine.Debug.LogError("sellInfo has nil item, idx == " + a);
					else
						sellInfo[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //使用道具
    public class ReqItemUse : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 101203;
		public int gridId; // 格子id
		public int num; // 使用数量
		public string arg; // 参数

    	//鏋勯�犲嚱鏁�
    	public ReqItemUse()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			gridId = 0;
            
			num = 0;
            
			arg = "";
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
                gridId = XBuffer.ReadInt(buffer, ref offset);
                num = XBuffer.ReadInt(buffer, ref offset);
                arg = XBuffer.ReadString(buffer, ref offset);

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
					XBuffer.WriteInt(gridId,buffer, ref offset);
					XBuffer.WriteInt(num,buffer, ref offset);
					XBuffer.WriteString(arg,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //钻石购买道具
    public class ReqItemBuyCostDiamond : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 101204;
        public List<ItemInfo> items{get;protected set;} //想要购买的道具列表

    	//鏋勯�犲嚱鏁�
    	public ReqItemBuyCostDiamond()
    	{
            items = new List<ItemInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            items.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

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
    //道具合成
    public class ReqItemCompose : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 101205;
		public int itemId; // 目标ID
		public int num; // 数量

    	//鏋勯�犲嚱鏁�
    	public ReqItemCompose()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			itemId = 0;
            
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
                itemId = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(itemId,buffer, ref offset);
					XBuffer.WriteInt(num,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //背包消息
    public class ResBagUpdate : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 101101;
        public List<GridInfo> grids{get;protected set;} //格子信息

    	//鏋勯�犲嚱鏁�
    	public ResBagUpdate()
    	{
            grids = new List<GridInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            grids.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = grids.Count; a < b; ++a)
            {
				grids[a] = null;
                //var _value_ = grids[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            grids.Clear();
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
                    GridInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<GridInfo>();
					_value_ = new GridInfo();
                    _value_.Read(buffer, ref offset);
                    grids.Add(_value_);
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

                XBuffer.WriteShort((short)grids.Count, buffer, ref offset);
                for(int a = 0; a < grids.Count; ++a)
                {
					if(grids[a] == null)
						UnityEngine.Debug.LogError("grids has nil item, idx == " + a);
					else
						grids[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //背包消息
    public class ResBagInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 101102;
        public List<GridInfo> grids{get;protected set;} //格子信息

    	//鏋勯�犲嚱鏁�
    	public ResBagInfo()
    	{
            grids = new List<GridInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            grids.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = grids.Count; a < b; ++a)
            {
				grids[a] = null;
                //var _value_ = grids[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            grids.Clear();
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
                    GridInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<GridInfo>();
					_value_ = new GridInfo();
                    _value_.Read(buffer, ref offset);
                    grids.Add(_value_);
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

                XBuffer.WriteShort((short)grids.Count, buffer, ref offset);
                for(int a = 0; a < grids.Count; ++a)
                {
					if(grids[a] == null)
						UnityEngine.Debug.LogError("grids has nil item, idx == " + a);
					else
						grids[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //出售道具
    public class ResItemSell : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 101103;
		public int gridId; // 格子id
		public int sellNum; // 是否售空
		public int gold; // 获得金币

    	//鏋勯�犲嚱鏁�
    	public ResItemSell()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			gridId = 0;
            
			sellNum = 0;
            
			gold = 0;
            
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
                gridId = XBuffer.ReadInt(buffer, ref offset);
                sellNum = XBuffer.ReadInt(buffer, ref offset);
                gold = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(gridId,buffer, ref offset);
					XBuffer.WriteInt(sellNum,buffer, ref offset);
					XBuffer.WriteInt(gold,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //宝箱开启结果
    public class ResBoxItemUse : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 101104;
		public int result; // 0使用不成功
        public List<ItemInfo> items{get;protected set;} //道具信息

    	//鏋勯�犲嚱鏁�
    	public ResBoxItemUse()
    	{
            items = new List<ItemInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			result = 0;
            
            items.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

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
                result = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(result,buffer, ref offset);

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
}