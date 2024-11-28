//Auto generated, do not edit it
//奥义

using System;
using System.IO;
using System.Collections.Generic;
using Message.Bag;

namespace Message.Profound
{
    public enum TypeEnum
    {
        StoneInfo = 1,
        StonePage = 2,
        PartGridInfo = 3,
        Reward = 4,
        DrawCountInfo = 5,
        EquipInfo = 6,
    }

    //奥义石
    public class StoneInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int id; // 格子id 在背包为背包格子id 在部位为部位格子id
        
		public int itemId; // 道具ID
        
		public int minLevel; // 小等级
        
		public int bigLevel; // 大等级
        
		public int exp; // 经验
        

        //鏋勯�犲嚱鏁�
        public StoneInfo() : base()
        {
			
			id = 0;
            
			itemId = 0;
            
			minLevel = 0;
            
			bigLevel = 0;
            
			exp = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			itemId = 0;
            
			minLevel = 0;
            
			bigLevel = 0;
            
			exp = 0;
            

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
                itemId = XBuffer.ReadInt(buffer, ref offset);
                minLevel = XBuffer.ReadInt(buffer, ref offset);
                bigLevel = XBuffer.ReadInt(buffer, ref offset);
                exp = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(itemId, buffer, ref offset);
                XBuffer.WriteInt(minLevel, buffer, ref offset);
                XBuffer.WriteInt(bigLevel, buffer, ref offset);
                XBuffer.WriteInt(exp, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //页签
    public class StonePage : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int girdLevel; // 奥义等级 1初级 2中级 3究极
        
        public List<StoneInfo> stones{get; protected set;} //装备的石头

        //鏋勯�犲嚱鏁�
        public StonePage() : base()
        {
            stones = new List<StoneInfo>(); //装备的石头
			
			girdLevel = 0;
            

            stones.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			girdLevel = 0;
            

            stones.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = stones.Count; a < b; ++a)
            {
                //var _value_ = stones[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				stones[a] = null;
            }
            stones.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                girdLevel = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    StoneInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<StoneInfo>();
					_value_ = new StoneInfo();
                    _value_.Read(buffer, ref offset);
                    stones.Add(_value_);
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
                XBuffer.WriteInt(girdLevel, buffer, ref offset);

                XBuffer.WriteShort((short)stones.Count,buffer, ref offset);
                for (int a = 0; a < stones.Count; ++a)
                {
					if(stones[a] == null)
						UnityEngine.Debug.LogError("stones has nil item, idx == " + a);
					else
						stones[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //奥义石格子部位信息
    public class PartGridInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int petId; // 宠物ID
        
        public List<StonePage> pages{get; protected set;} //页签列表
        public List<Reward> rewards{get; protected set;} //奥义奖励

        //鏋勯�犲嚱鏁�
        public PartGridInfo() : base()
        {
            pages = new List<StonePage>(); //页签列表
            rewards = new List<Reward>(); //奥义奖励
			
			petId = 0;
            

            pages.Clear();
            rewards.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            

            pages.Clear();
            rewards.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = pages.Count; a < b; ++a)
            {
                //var _value_ = pages[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				pages[a] = null;
            }
            pages.Clear();
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
                petId = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    StonePage _value_ = null;
                    //_value_ = ClassCacheManager.New<StonePage>();
					_value_ = new StonePage();
                    _value_.Read(buffer, ref offset);
                    pages.Add(_value_);
                }
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    Reward _value_ = null;
                    //_value_ = ClassCacheManager.New<Reward>();
					_value_ = new Reward();
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

                XBuffer.WriteShort((short)pages.Count,buffer, ref offset);
                for (int a = 0; a < pages.Count; ++a)
                {
					if(pages[a] == null)
						UnityEngine.Debug.LogError("pages has nil item, idx == " + a);
					else
						pages[a].WriteWithType(buffer, ref offset);
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
    //奥义奖励
    public class Reward : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int id; // 奖励id
        
		public int state; // 状态 1可领奖未领 2已领奖
        

        //鏋勯�犲嚱鏁�
        public Reward() : base()
        {
			
			id = 0;
            
			state = 0;
            

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
                TypeEnum _real_type_;
                id = XBuffer.ReadInt(buffer, ref offset);
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
                XBuffer.WriteInt(state, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //奥义石抽奖次数信息
    public class DrawCountInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int id; // 抽取id 1 钻石单抽  2钻石十连抽 3金币10连抽 4金币单抽
        
		public int drawCount; // 已抽取的次数
        
		public int freeCount; // 剩余免费次数
        

        //鏋勯�犲嚱鏁�
        public DrawCountInfo() : base()
        {
			
			id = 0;
            
			drawCount = 0;
            
			freeCount = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			drawCount = 0;
            
			freeCount = 0;
            

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
                drawCount = XBuffer.ReadInt(buffer, ref offset);
                freeCount = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(drawCount, buffer, ref offset);
                XBuffer.WriteInt(freeCount, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //石头装备信息
    public class EquipInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int equipId; // 装备部位id
        
		public int gridId; // 奥义石在包裹中的id
        
		public int source; // 来源 1背包 2装备部位
        

        //鏋勯�犲嚱鏁�
        public EquipInfo() : base()
        {
			
			equipId = 0;
            
			gridId = 0;
            
			source = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			equipId = 0;
            
			gridId = 0;
            
			source = 0;
            

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
                equipId = XBuffer.ReadInt(buffer, ref offset);
                gridId = XBuffer.ReadInt(buffer, ref offset);
                source = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
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
                XBuffer.WriteInt(equipId, buffer, ref offset);
                XBuffer.WriteInt(gridId, buffer, ref offset);
                XBuffer.WriteInt(source, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //奥义石背包数据(初始化 添加)
    public class ResBagStoneList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122101;
        public List<StoneInfo> stoneInfos{get;protected set;} //奥义石列表

    	//鏋勯�犲嚱鏁�
    	public ResBagStoneList()
    	{
            stoneInfos = new List<StoneInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            stoneInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = stoneInfos.Count; a < b; ++a)
            {
				stoneInfos[a] = null;
                //var _value_ = stoneInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            stoneInfos.Clear();
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
                    StoneInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<StoneInfo>();
					_value_ = new StoneInfo();
                    _value_.Read(buffer, ref offset);
                    stoneInfos.Add(_value_);
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

                XBuffer.WriteShort((short)stoneInfos.Count, buffer, ref offset);
                for(int a = 0; a < stoneInfos.Count; ++a)
                {
					if(stoneInfos[a] == null)
						UnityEngine.Debug.LogError("stoneInfos has nil item, idx == " + a);
					else
						stoneInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //奥义石背包删除
    public class ResBagStoneDel : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122102;
        public List<int> gridId{get;protected set;} //格子id

    	//鏋勯�犲嚱鏁�
    	public ResBagStoneDel()
    	{
            gridId = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            gridId.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            gridId.Clear();
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
            		gridId.Add(_value_);
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

                XBuffer.WriteShort((short)gridId.Count, buffer, ref offset);
                for(int a = 0; a < gridId.Count; ++a)
                {
        			XBuffer.WriteInt(gridId[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //宠物奥义数据初始化
    public class ResStoneGridInfos : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122103;
        public List<PartGridInfo> gridInfos{get;protected set;} //所有等级的奥义石列表

    	//鏋勯�犲嚱鏁�
    	public ResStoneGridInfos()
    	{
            gridInfos = new List<PartGridInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            gridInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = gridInfos.Count; a < b; ++a)
            {
				gridInfos[a] = null;
                //var _value_ = gridInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            gridInfos.Clear();
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
                    PartGridInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<PartGridInfo>();
					_value_ = new PartGridInfo();
                    _value_.Read(buffer, ref offset);
                    gridInfos.Add(_value_);
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

                XBuffer.WriteShort((short)gridInfos.Count, buffer, ref offset);
                for(int a = 0; a < gridInfos.Count; ++a)
                {
					if(gridInfos[a] == null)
						UnityEngine.Debug.LogError("gridInfos has nil item, idx == " + a);
					else
						gridInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //奥义石单次突破结果
    public class ResStoneBreak : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122105;
		public int petId; // 宠物id
		public int girdLevel; // 奥义等级 1初级 2中级 3究极
		public int id; // 格子id 在背包为背包格子id 在部位为部位格子id
		public int bigLevel; // 大等级

    	//鏋勯�犲嚱鏁�
    	public ResStoneBreak()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			girdLevel = 0;
            
			id = 0;
            
			bigLevel = 0;
            
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
                girdLevel = XBuffer.ReadInt(buffer, ref offset);
                id = XBuffer.ReadInt(buffer, ref offset);
                bigLevel = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(girdLevel,buffer, ref offset);
					XBuffer.WriteInt(id,buffer, ref offset);
					XBuffer.WriteInt(bigLevel,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //奥义石单次升级结果
    public class ResStoneLevel : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122106;
		public int petId; // 宠物id
		public int girdLevel; // 奥义等级 1初级 2中级 3究极
		public int id; // 格子id 在背包为背包格子id 在部位为部位格子id
		public int minLevel; // 小等级
		public int exp; // 经验

    	//鏋勯�犲嚱鏁�
    	public ResStoneLevel()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			girdLevel = 0;
            
			id = 0;
            
			minLevel = 0;
            
			exp = 0;
            
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
                girdLevel = XBuffer.ReadInt(buffer, ref offset);
                id = XBuffer.ReadInt(buffer, ref offset);
                minLevel = XBuffer.ReadInt(buffer, ref offset);
                exp = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(girdLevel,buffer, ref offset);
					XBuffer.WriteInt(id,buffer, ref offset);
					XBuffer.WriteInt(minLevel,buffer, ref offset);
					XBuffer.WriteInt(exp,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //单个石头改变
    public class ResSingleStoneChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122107;
		public int petId; // 宠物id
		public int girdLevel; // 奥义等级 1初级 2中级 3究极
		public int id; // 格子id 在背包为背包格子id 在部位为部位格子id
		public int minLevel; // 小等级
		public int exp; // 经验
		public int bigLevel; // 大等级

    	//鏋勯�犲嚱鏁�
    	public ResSingleStoneChange()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			girdLevel = 0;
            
			id = 0;
            
			minLevel = 0;
            
			exp = 0;
            
			bigLevel = 0;
            
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
                girdLevel = XBuffer.ReadInt(buffer, ref offset);
                id = XBuffer.ReadInt(buffer, ref offset);
                minLevel = XBuffer.ReadInt(buffer, ref offset);
                exp = XBuffer.ReadInt(buffer, ref offset);
                bigLevel = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(girdLevel,buffer, ref offset);
					XBuffer.WriteInt(id,buffer, ref offset);
					XBuffer.WriteInt(minLevel,buffer, ref offset);
					XBuffer.WriteInt(exp,buffer, ref offset);
					XBuffer.WriteInt(bigLevel,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //奥义链奖励领取结果
    public class ResGetedReward : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122108;
		public int petId; // 宠物id
        public List<int> profoundIds{get;protected set;} //奥义链ID

    	//鏋勯�犲嚱鏁�
    	public ResGetedReward()
    	{
            profoundIds = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
            profoundIds.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            profoundIds.Clear();
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
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
            		var _value_ = XBuffer.ReadInt(buffer, ref offset);
            		profoundIds.Add(_value_);
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
					XBuffer.WriteInt(petId,buffer, ref offset);

                XBuffer.WriteShort((short)profoundIds.Count, buffer, ref offset);
                for(int a = 0; a < profoundIds.Count; ++a)
                {
        			XBuffer.WriteInt(profoundIds[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //单页石头列表信息改变
    public class ResStoneListChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122109;
		public int petId; // 宠物id
		public int girdLevel; // 奥义等级 1初级 2中级 3究极
        public List<StoneInfo> stoneInfos{get;protected set;} //奥义石列表

    	//鏋勯�犲嚱鏁�
    	public ResStoneListChange()
    	{
            stoneInfos = new List<StoneInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			girdLevel = 0;
            
            stoneInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = stoneInfos.Count; a < b; ++a)
            {
				stoneInfos[a] = null;
                //var _value_ = stoneInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            stoneInfos.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                petId = XBuffer.ReadInt(buffer, ref offset);
                girdLevel = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    StoneInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<StoneInfo>();
					_value_ = new StoneInfo();
                    _value_.Read(buffer, ref offset);
                    stoneInfos.Add(_value_);
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
					XBuffer.WriteInt(petId,buffer, ref offset);
					XBuffer.WriteInt(girdLevel,buffer, ref offset);

                XBuffer.WriteShort((short)stoneInfos.Count, buffer, ref offset);
                for(int a = 0; a < stoneInfos.Count; ++a)
                {
					if(stoneInfos[a] == null)
						UnityEngine.Debug.LogError("stoneInfos has nil item, idx == " + a);
					else
						stoneInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //奥义链激活
    public class ResAoyiActive : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122110;
		public int petId; // 宠物id
		public int profoundId; // 奥义链ID

    	//鏋勯�犲嚱鏁�
    	public ResAoyiActive()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			profoundId = 0;
            
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
                profoundId = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(profoundId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //奥义抽取
    public class ResAoyiDraw : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122111;
		public int id; // 抽取id 1 钻石单抽  2钻石十连抽 3金币10连抽 4金币单抽
        public List<ItemInfo> itemInfos{get;protected set;} //道具

    	//鏋勯�犲嚱鏁�
    	public ResAoyiDraw()
    	{
            itemInfos = new List<ItemInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
            itemInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = itemInfos.Count; a < b; ++a)
            {
				itemInfos[a] = null;
                //var _value_ = itemInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            itemInfos.Clear();
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
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    ItemInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<ItemInfo>();
					_value_ = new ItemInfo();
                    _value_.Read(buffer, ref offset);
                    itemInfos.Add(_value_);
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
					XBuffer.WriteInt(id,buffer, ref offset);

                XBuffer.WriteShort((short)itemInfos.Count, buffer, ref offset);
                for(int a = 0; a < itemInfos.Count; ++a)
                {
					if(itemInfos[a] == null)
						UnityEngine.Debug.LogError("itemInfos has nil item, idx == " + a);
					else
						itemInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //奥义抽取的次数信息
    public class ResDrawCountInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122112;
        public List<DrawCountInfo> countInfos{get;protected set;} //道具

    	//鏋勯�犲嚱鏁�
    	public ResDrawCountInfo()
    	{
            countInfos = new List<DrawCountInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            countInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = countInfos.Count; a < b; ++a)
            {
				countInfos[a] = null;
                //var _value_ = countInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            countInfos.Clear();
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
                    DrawCountInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<DrawCountInfo>();
					_value_ = new DrawCountInfo();
                    _value_.Read(buffer, ref offset);
                    countInfos.Add(_value_);
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

                XBuffer.WriteShort((short)countInfos.Count, buffer, ref offset);
                for(int a = 0; a < countInfos.Count; ++a)
                {
					if(countInfos[a] == null)
						UnityEngine.Debug.LogError("countInfos has nil item, idx == " + a);
					else
						countInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //单个石头升级
    public class ReqLevelUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122201;
		public int gridId; // 部位id
		public int petId; // 宠物id
		public int girdLevel; // 奥义等级 1初级 2中级 3究极
		public int type; // 强化类型（1升级 2一键强化  3一键50）

    	//鏋勯�犲嚱鏁�
    	public ReqLevelUp()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			gridId = 0;
            
			petId = 0;
            
			girdLevel = 0;
            
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
                gridId = XBuffer.ReadInt(buffer, ref offset);
                petId = XBuffer.ReadInt(buffer, ref offset);
                girdLevel = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(gridId,buffer, ref offset);
					XBuffer.WriteInt(petId,buffer, ref offset);
					XBuffer.WriteInt(girdLevel,buffer, ref offset);
					XBuffer.WriteInt(type,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //突破
    public class ReqLevelBreak : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122202;
		public int gridId; // 部位id
		public int petId; // 宠物id
		public int girdLevel; // 奥义等级 1初级 2中级 3究极

    	//鏋勯�犲嚱鏁�
    	public ReqLevelBreak()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			gridId = 0;
            
			petId = 0;
            
			girdLevel = 0;
            
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
                petId = XBuffer.ReadInt(buffer, ref offset);
                girdLevel = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(petId,buffer, ref offset);
					XBuffer.WriteInt(girdLevel,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //请求装备
    public class ReqEquip : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122203;
		public int petId; // 宠物id
		public int girdLevel; // 奥义等级 1初级 2中级 3究极
        public List<EquipInfo> equipInfos{get;protected set;} //奥义石列表

    	//鏋勯�犲嚱鏁�
    	public ReqEquip()
    	{
            equipInfos = new List<EquipInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			girdLevel = 0;
            
            equipInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = equipInfos.Count; a < b; ++a)
            {
				equipInfos[a] = null;
                //var _value_ = equipInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            equipInfos.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                petId = XBuffer.ReadInt(buffer, ref offset);
                girdLevel = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    EquipInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<EquipInfo>();
					_value_ = new EquipInfo();
                    _value_.Read(buffer, ref offset);
                    equipInfos.Add(_value_);
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
					XBuffer.WriteInt(petId,buffer, ref offset);
					XBuffer.WriteInt(girdLevel,buffer, ref offset);

                XBuffer.WriteShort((short)equipInfos.Count, buffer, ref offset);
                for(int a = 0; a < equipInfos.Count; ++a)
                {
					if(equipInfos[a] == null)
						UnityEngine.Debug.LogError("equipInfos has nil item, idx == " + a);
					else
						equipInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //请求一键卸下
    public class ReqUnEquip : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122204;
		public int petId; // 宠物ID
		public int girdLevel; // 奥义等级 1初级 2中级 3究极

    	//鏋勯�犲嚱鏁�
    	public ReqUnEquip()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			girdLevel = 0;
            
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
                girdLevel = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(girdLevel,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //请求分解
    public class ReqResolve : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122205;
        public List<int> gridIds{get;protected set;} //背包格子id

    	//鏋勯�犲嚱鏁�
    	public ReqResolve()
    	{
            gridIds = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            gridIds.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            gridIds.Clear();
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
            		gridIds.Add(_value_);
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

                XBuffer.WriteShort((short)gridIds.Count, buffer, ref offset);
                for(int a = 0; a < gridIds.Count; ++a)
                {
        			XBuffer.WriteInt(gridIds[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //请求领取奥义链奖励
    public class ReqGetReward : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122206;
		public int __profoundId; // 奥义id
		private byte _profoundId = 0; // 奥义id tag
		
		public bool hasProfoundId()
		{
			return this._profoundId == 1;
		}
		
		public int profoundId
		{
			set
			{
				_profoundId = 1;
				__profoundId = value;
			}
			
			get
			{
				return __profoundId;
			}
		}
		public bool oneKey; // 一键
		public int petId; // 宠物id

    	//鏋勯�犲嚱鏁�
    	public ReqGetReward()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			_profoundId = 0;
			__profoundId = 0;
            
			oneKey = false;
            oneKey = false;
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
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					profoundId = XBuffer.ReadInt(buffer, ref offset);
				}
        		oneKey = XBuffer.ReadBool(buffer, ref offset);
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
				XBuffer.WriteByte(_profoundId,buffer, ref offset);
				if (_profoundId == 1)
				{
					XBuffer.WriteInt(profoundId,buffer, ref offset);
				}
					XBuffer.WriteBool(oneKey,buffer, ref offset);
					XBuffer.WriteInt(petId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //一键强化多个
    public class ReqBigOneKeyStrength : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122207;
		public int petId; // 宠物id
		public int girdLevel; // 奥义等级 1初级 2中级 3究极
		public int targetLevel; // 目标等级
        public List<int> id{get;protected set;} //部位id

    	//鏋勯�犲嚱鏁�
    	public ReqBigOneKeyStrength()
    	{
            id = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			girdLevel = 0;
            
			targetLevel = 0;
            
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
                petId = XBuffer.ReadInt(buffer, ref offset);
                girdLevel = XBuffer.ReadInt(buffer, ref offset);
                targetLevel = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(petId,buffer, ref offset);
					XBuffer.WriteInt(girdLevel,buffer, ref offset);
					XBuffer.WriteInt(targetLevel,buffer, ref offset);

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
    //奥义抽取
    public class ReqAoyiDraw : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 122208;
		public int id; // 奥义抽取表id

    	//鏋勯�犲嚱鏁�
    	public ReqAoyiDraw()
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
}