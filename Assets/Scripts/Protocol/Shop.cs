//Auto generated, do not edit it
//商店

using System;
using System.IO;
using System.Collections.Generic;
using Message.Bag;

namespace Message.Shop
{
    public enum TypeEnum
    {
        ShopInfo = 1,
    }

    //商品信息
    public class ShopInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int shopId; // 商品ID
        
		public int index; // 顺序
        
		public int num; // 数量
        
		public int discount; // 商品折扣
        
		public int buyNum; // 剩余购买次数
        
		public int price; // 价格
        
		public int currency; // 消耗道具
        
		public int __refreshCount; // 已刷新次数
		private byte _refreshCount = 0; // 已刷新次数 tag
		
		public bool hasRefreshCount()
		{
			return this._refreshCount == 1;
		}
		
		public int refreshCount
		{
			set
			{
				_refreshCount = 1;
				__refreshCount = value;
			}
			
			get
			{
				return __refreshCount;
			}
		}
        
		public long __refreshTime; // 刷新时间
		private byte _refreshTime = 0; // 刷新时间 tag
		
		public bool hasRefreshTime()
		{
			return this._refreshTime == 1;
		}
		
		public long refreshTime
		{
			set
			{
				_refreshTime = 1;
				__refreshTime = value;
			}
			
			get
			{
				return __refreshTime;
			}
		}
        

        //鏋勯�犲嚱鏁�
        public ShopInfo() : base()
        {
			
			shopId = 0;
            
			index = 0;
            
			num = 0;
            
			discount = 0;
            
			buyNum = 0;
            
			price = 0;
            
			currency = 0;
            
			_refreshCount = 0;
			__refreshCount = 0;
            
			_refreshTime = 0;
			__refreshTime = 0L;

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			shopId = 0;
            
			index = 0;
            
			num = 0;
            
			discount = 0;
            
			buyNum = 0;
            
			price = 0;
            
			currency = 0;
            
			_refreshCount = 0;
			__refreshCount = 0;
            
			_refreshTime = 0;
			__refreshTime = 0L;

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
                shopId = XBuffer.ReadInt(buffer, ref offset);
                index = XBuffer.ReadInt(buffer, ref offset);
                num = XBuffer.ReadInt(buffer, ref offset);
                discount = XBuffer.ReadInt(buffer, ref offset);
                buyNum = XBuffer.ReadInt(buffer, ref offset);
                price = XBuffer.ReadInt(buffer, ref offset);
                currency = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					refreshCount = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					refreshTime = XBuffer.ReadLong(buffer, ref offset);
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
            XBuffer.WriteByte(1, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(shopId, buffer, ref offset);
                XBuffer.WriteInt(index, buffer, ref offset);
                XBuffer.WriteInt(num, buffer, ref offset);
                XBuffer.WriteInt(discount, buffer, ref offset);
                XBuffer.WriteInt(buyNum, buffer, ref offset);
                XBuffer.WriteInt(price, buffer, ref offset);
                XBuffer.WriteInt(currency, buffer, ref offset);
				XBuffer.WriteByte(_refreshCount, buffer, ref offset);
				if (_refreshCount == 1)
				{
					XBuffer.WriteInt(refreshCount, buffer, ref offset);
				}
				XBuffer.WriteByte(_refreshTime, buffer, ref offset);
				if (_refreshTime == 1)
				{
					XBuffer.WriteLong(refreshTime, buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //商店信息
    public class ResGoodsInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 113101;
		public int shopType; // 商店类型
		public int refreshCount; // 刷新次数
		public long __refreshTime; // 刷新时间
		private byte _refreshTime = 0; // 刷新时间 tag
		
		public bool hasRefreshTime()
		{
			return this._refreshTime == 1;
		}
		
		public long refreshTime
		{
			set
			{
				_refreshTime = 1;
				__refreshTime = value;
			}
			
			get
			{
				return __refreshTime;
			}
		}
        public List<ShopInfo> infos{get;protected set;} //商品信息

    	//鏋勯�犲嚱鏁�
    	public ResGoodsInfo()
    	{
            infos = new List<ShopInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			shopType = 0;
            
			refreshCount = 0;
            
			_refreshTime = 0;
			__refreshTime = 0L;
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
                shopType = XBuffer.ReadInt(buffer, ref offset);
                refreshCount = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					refreshTime = XBuffer.ReadLong(buffer, ref offset);
				}

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    ShopInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<ShopInfo>();
					_value_ = new ShopInfo();
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
					XBuffer.WriteInt(shopType,buffer, ref offset);
					XBuffer.WriteInt(refreshCount,buffer, ref offset);
				XBuffer.WriteByte(_refreshTime,buffer, ref offset);
				if (_refreshTime == 1)
				{
					XBuffer.WriteLong(refreshTime,buffer, ref offset);
				}

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
    //商店刷新
    public class ResShopUpdate : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 113102;
        public List<int> shopType{get;protected set;} //商店类型

    	//鏋勯�犲嚱鏁�
    	public ResShopUpdate()
    	{
            shopType = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            shopType.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            shopType.Clear();
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
            		shopType.Add(_value_);
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

                XBuffer.WriteShort((short)shopType.Count, buffer, ref offset);
                for(int a = 0; a < shopType.Count; ++a)
                {
        			XBuffer.WriteInt(shopType[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //购买
    public class ResBuy : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 113103;
		public int shopType; // 商店类型
		public int index; // 顺序
		public int shopId; // 商品ID
		public int buyNum; // 剩余购买次数

    	//鏋勯�犲嚱鏁�
    	public ResBuy()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			shopType = 0;
            
			index = 0;
            
			shopId = 0;
            
			buyNum = 0;
            
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
                shopType = XBuffer.ReadInt(buffer, ref offset);
                index = XBuffer.ReadInt(buffer, ref offset);
                shopId = XBuffer.ReadInt(buffer, ref offset);
                buyNum = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(shopType,buffer, ref offset);
					XBuffer.WriteInt(index,buffer, ref offset);
					XBuffer.WriteInt(shopId,buffer, ref offset);
					XBuffer.WriteInt(buyNum,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //装备觉醒宝箱信息
    public class ResEquipBoxInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 113104;
		public int drawNum; // 还剩余抽取次数可免费
		public bool free; // 当前是否免费

    	//鏋勯�犲嚱鏁�
    	public ResEquipBoxInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			drawNum = 0;
            
			free = false;
            free = false;
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
                drawNum = XBuffer.ReadInt(buffer, ref offset);
        		free = XBuffer.ReadBool(buffer, ref offset);

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
					XBuffer.WriteInt(drawNum,buffer, ref offset);
					XBuffer.WriteBool(free,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //装备觉醒宝箱开启结果
    public class ResOpenResult : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 113105;
		public int suiPianNum; // 获得的装备觉醒碎片数量
        public List<ItemInfo> items{get;protected set;} //抽到的奖励列表

    	//鏋勯�犲嚱鏁�
    	public ResOpenResult()
    	{
            items = new List<ItemInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			suiPianNum = 0;
            
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
                suiPianNum = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(suiPianNum,buffer, ref offset);

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
    //功能开启
    public class ResOpenShop : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 113106;
		public int id; // 商店id
		public long __leaveTime; // 消失时间点
		private byte _leaveTime = 0; // 消失时间点 tag
		
		public bool hasLeaveTime()
		{
			return this._leaveTime == 1;
		}
		
		public long leaveTime
		{
			set
			{
				_leaveTime = 1;
				__leaveTime = value;
			}
			
			get
			{
				return __leaveTime;
			}
		}

    	//鏋勯�犲嚱鏁�
    	public ResOpenShop()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			_leaveTime = 0;
			__leaveTime = 0L;
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
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					leaveTime = XBuffer.ReadLong(buffer, ref offset);
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
					XBuffer.WriteInt(id,buffer, ref offset);
				XBuffer.WriteByte(_leaveTime,buffer, ref offset);
				if (_leaveTime == 1)
				{
					XBuffer.WriteLong(leaveTime,buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //关闭商店
    public class ResCloseShop : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 113107;
		public int id; // 商店id

    	//鏋勯�犲嚱鏁�
    	public ResCloseShop()
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
    //商店初始化
    public class ResShopInit : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 113108;
        public List<int> id{get;protected set;} //开启的商店

    	//鏋勯�犲嚱鏁�
    	public ResShopInit()
    	{
            id = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            id.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            id.Clear();
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
            		id.Add(_value_);
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

                XBuffer.WriteShort((short)id.Count, buffer, ref offset);
                for(int a = 0; a < id.Count; ++a)
                {
        			XBuffer.WriteInt(id[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //刷新
    public class ReqRefresh : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 113201;
		public int shopType; // 商店类型
		public int type; // 刷新类型 1普通 2特殊
		public int __index; // 刷新目标
		private byte _index = 0; // 刷新目标 tag
		
		public bool hasIndex()
		{
			return this._index == 1;
		}
		
		public int index
		{
			set
			{
				_index = 1;
				__index = value;
			}
			
			get
			{
				return __index;
			}
		}

    	//鏋勯�犲嚱鏁�
    	public ReqRefresh()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			shopType = 0;
            
			type = 0;
            
			_index = 0;
			__index = 0;
            
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
                shopType = XBuffer.ReadInt(buffer, ref offset);
                type = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					index = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(shopType,buffer, ref offset);
					XBuffer.WriteInt(type,buffer, ref offset);
				XBuffer.WriteByte(_index,buffer, ref offset);
				if (_index == 1)
				{
					XBuffer.WriteInt(index,buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //商店信息
    public class ReqGoodsInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 113202;
		public int shopType; // 商店类型

    	//鏋勯�犲嚱鏁�
    	public ReqGoodsInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			shopType = 0;
            
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
                shopType = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(shopType,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //购买
    public class ReqBuy : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 113203;
		public int shopType; // 商店类型
		public int index; // 顺序
		public int shopId; // 商品ID
		public int num; // 购买数量

    	//鏋勯�犲嚱鏁�
    	public ReqBuy()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			shopType = 0;
            
			index = 0;
            
			shopId = 0;
            
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
                shopType = XBuffer.ReadInt(buffer, ref offset);
                index = XBuffer.ReadInt(buffer, ref offset);
                shopId = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(shopType,buffer, ref offset);
					XBuffer.WriteInt(index,buffer, ref offset);
					XBuffer.WriteInt(shopId,buffer, ref offset);
					XBuffer.WriteInt(num,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //开启宝箱
    public class ReqOpenBox : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 113204;
		public int num; // 次数
		public int type; // 类型 1花钱 2免费 3道具

    	//鏋勯�犲嚱鏁�
    	public ReqOpenBox()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			num = 0;
            
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
                num = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(num,buffer, ref offset);
					XBuffer.WriteInt(type,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}