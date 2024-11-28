//Auto generated, do not edit it
//副本消息

using System;
using System.IO;
using System.Collections.Generic;
using Message.Bag;
using Message.Pet;

namespace Message.Dungeon
{
    public enum TypeEnum
    {
        ActInfo = 1,
        ChapterInfo = 2,
        DungeonInfo = 3,
        Award = 4,
        PlayerExpAndLevel = 5,
        SweepReqInfo = 6,
        SweepItem = 7,
    }

    //关卡信息
    public class ActInfo : BaseMsgStruct
    {
		public int actId; // 关卡Id
        
		public int star; // 完成星级
        
		public int boxStatus; // -2：没有宝箱，-1：不可领取，0：可领取未打开，1：已领取
        
		public int __attackNum; // 已攻打次数
		private byte _attackNum = 0; // 已攻打次数 tag
		
		public bool hasAttackNum()
		{
			return this._attackNum == 1;
		}
		
		public int attackNum
		{
			set
			{
				_attackNum = 1;
				__attackNum = value;
			}
			
			get
			{
				return __attackNum;
			}
		}
        
		public int __refreshNum; // 已购买刷新次数
		private byte _refreshNum = 0; // 已购买刷新次数 tag
		
		public bool hasRefreshNum()
		{
			return this._refreshNum == 1;
		}
		
		public int refreshNum
		{
			set
			{
				_refreshNum = 1;
				__refreshNum = value;
			}
			
			get
			{
				return __refreshNum;
			}
		}
        

        //鏋勯�犲嚱鏁�
        public ActInfo() : base()
        {
			
			actId = 0;
            
			star = 0;
            
			boxStatus = 0;
            
			_attackNum = 0;
			__attackNum = 0;
            
			_refreshNum = 0;
			__refreshNum = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			actId = 0;
            
			star = 0;
            
			boxStatus = 0;
            
			_attackNum = 0;
			__attackNum = 0;
            
			_refreshNum = 0;
			__refreshNum = 0;
            

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
                actId = XBuffer.ReadInt(buffer, ref offset);
                star = XBuffer.ReadInt(buffer, ref offset);
                boxStatus = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					attackNum = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					refreshNum = XBuffer.ReadInt(buffer, ref offset);
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
                XBuffer.WriteInt(actId, buffer, ref offset);
                XBuffer.WriteInt(star, buffer, ref offset);
                XBuffer.WriteInt(boxStatus, buffer, ref offset);
				XBuffer.WriteByte(_attackNum, buffer, ref offset);
				if (_attackNum == 1)
				{
					XBuffer.WriteInt(attackNum, buffer, ref offset);
				}
				XBuffer.WriteByte(_refreshNum, buffer, ref offset);
				if (_refreshNum == 1)
				{
					XBuffer.WriteInt(refreshNum, buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //章节信息
    public class ChapterInfo : BaseMsgStruct
    {
		public int chapterId; // 章节Id
        
		public int star; // 星星数
        
        public List<ActInfo> actInfos{get; protected set;} //关卡信息列表
        public List<int> boxStatus{get; protected set;} //章节宝箱状态（-1：不可领取，0：可领取未打开，1：已领取）

        //鏋勯�犲嚱鏁�
        public ChapterInfo() : base()
        {
            actInfos = new List<ActInfo>(); //关卡信息列表
            boxStatus = new List<int>(); //章节宝箱状态（-1：不可领取，0：可领取未打开，1：已领取）
			
			chapterId = 0;
            
			star = 0;
            

            actInfos.Clear();
            boxStatus.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			chapterId = 0;
            
			star = 0;
            

            actInfos.Clear();
            boxStatus.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = actInfos.Count; a < b; ++a)
            {
                //var _value_ = actInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				actInfos[a] = null;
            }
            actInfos.Clear();
            boxStatus.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                chapterId = XBuffer.ReadInt(buffer, ref offset);
                star = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    ActInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<ActInfo>();
					_value_ = new ActInfo();
                    _value_.Read(buffer, ref offset);
                    actInfos.Add(_value_);
                }
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    var _value_ = XBuffer.ReadInt(buffer, ref offset);
                    boxStatus.Add(_value_);
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
                XBuffer.WriteInt(chapterId, buffer, ref offset);
                XBuffer.WriteInt(star, buffer, ref offset);

                XBuffer.WriteShort((short)actInfos.Count,buffer, ref offset);
                for (int a = 0; a < actInfos.Count; ++a)
                {
					if(actInfos[a] == null)
						UnityEngine.Debug.LogError("actInfos has nil item, idx == " + a);
					else
						actInfos[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)boxStatus.Count,buffer, ref offset);
                for (int a = 0; a < boxStatus.Count; ++a)
                {
                    XBuffer.WriteInt(boxStatus[a], buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //副本信息
    public class DungeonInfo : BaseMsgStruct
    {
		public int dungeonId; // 副本类型
        
		public int bestRecordId; // 可攻打最新关卡Id
        
        public List<ChapterInfo> chapterInfos{get; protected set;} //章节信息列表

        //鏋勯�犲嚱鏁�
        public DungeonInfo() : base()
        {
            chapterInfos = new List<ChapterInfo>(); //章节信息列表
			
			dungeonId = 0;
            
			bestRecordId = 0;
            

            chapterInfos.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			dungeonId = 0;
            
			bestRecordId = 0;
            

            chapterInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = chapterInfos.Count; a < b; ++a)
            {
                //var _value_ = chapterInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				chapterInfos[a] = null;
            }
            chapterInfos.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                dungeonId = XBuffer.ReadInt(buffer, ref offset);
                bestRecordId = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    ChapterInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<ChapterInfo>();
					_value_ = new ChapterInfo();
                    _value_.Read(buffer, ref offset);
                    chapterInfos.Add(_value_);
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
                XBuffer.WriteInt(dungeonId, buffer, ref offset);
                XBuffer.WriteInt(bestRecordId, buffer, ref offset);

                XBuffer.WriteShort((short)chapterInfos.Count,buffer, ref offset);
                for (int a = 0; a < chapterInfos.Count; ++a)
                {
					if(chapterInfos[a] == null)
						UnityEngine.Debug.LogError("chapterInfos has nil item, idx == " + a);
					else
						chapterInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //关卡战斗奖励
    public class Award : BaseMsgStruct
    {
        public List<ItemInfo> items{get; protected set;} //道具列表

        //鏋勯�犲嚱鏁�
        public Award() : base()
        {
            items = new List<ItemInfo>(); //道具列表
			

            items.Clear();
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
            XBuffer.WriteByte(4, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);

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
    //玩家等级经验信息
    public class PlayerExpAndLevel : BaseMsgStruct
    {
		public int level; // 等级
        
		public int exp; // 经验
        

        //鏋勯�犲嚱鏁�
        public PlayerExpAndLevel() : base()
        {
			
			level = 0;
            
			exp = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			level = 0;
            
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
                level = XBuffer.ReadInt(buffer, ref offset);
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
            XBuffer.WriteByte(5, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteInt(exp, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //扫荡请求信息
    public class SweepReqInfo : BaseMsgStruct
    {
		public int actId; // 关卡Id
        
		public int num; // 扫荡次数（选择多少就是多少）
        

        //鏋勯�犲嚱鏁�
        public SweepReqInfo() : base()
        {
			
			actId = 0;
            
			num = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			actId = 0;
            
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
                actId = XBuffer.ReadInt(buffer, ref offset);
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
            XBuffer.WriteByte(6, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(actId, buffer, ref offset);
                XBuffer.WriteInt(num, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //扫荡请求信息
    public class SweepItem : BaseMsgStruct
    {
		public int actId; // 关卡Id
        
		public int reqNum; // 请求扫荡次数
        
        public List<Award> award{get; protected set;} //扫荡奖励

        //鏋勯�犲嚱鏁�
        public SweepItem() : base()
        {
            award = new List<Award>(); //扫荡奖励
			
			actId = 0;
            
			reqNum = 0;
            

            award.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			actId = 0;
            
			reqNum = 0;
            

            award.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = award.Count; a < b; ++a)
            {
                //var _value_ = award[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				award[a] = null;
            }
            award.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                actId = XBuffer.ReadInt(buffer, ref offset);
                reqNum = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    Award _value_ = null;
                    //_value_ = ClassCacheManager.New<Award>();
					_value_ = new Award();
                    _value_.Read(buffer, ref offset);
                    award.Add(_value_);
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
                XBuffer.WriteInt(actId, buffer, ref offset);
                XBuffer.WriteInt(reqNum, buffer, ref offset);

                XBuffer.WriteShort((short)award.Count,buffer, ref offset);
                for (int a = 0; a < award.Count; ++a)
                {
					if(award[a] == null)
						UnityEngine.Debug.LogError("award has nil item, idx == " + a);
					else
						award[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //获取副本信息
    public class ReqDungeonInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 106201;

    	//鏋勯�犲嚱鏁�
    	public ReqDungeonInfo()
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
    //请求开始战斗
    public class ReqFightStart : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 106202;
		public int actId; // 关卡Id

    	//鏋勯�犲嚱鏁�
    	public ReqFightStart()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			actId = 0;
            
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
                actId = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(actId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //递交战斗结果
    public class ReqFightResult : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 106203;
		public int actId; // 关卡Id
		public int fightResult; // 战斗结果（0：失败了，1：成功了）
		public int star; // 星星数

    	//鏋勯�犲嚱鏁�
    	public ReqFightResult()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			actId = 0;
            
			fightResult = 0;
            
			star = 0;
            
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
                actId = XBuffer.ReadInt(buffer, ref offset);
                fightResult = XBuffer.ReadInt(buffer, ref offset);
                star = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(actId,buffer, ref offset);
					XBuffer.WriteInt(fightResult,buffer, ref offset);
					XBuffer.WriteInt(star,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //请求开启关卡宝箱
    public class ReqOpenActBox : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 106204;
		public int actId; // 关卡Id

    	//鏋勯�犲嚱鏁�
    	public ReqOpenActBox()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			actId = 0;
            
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
                actId = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(actId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //请求开启章节宝箱
    public class ReqOpenChapterBox : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 106205;
		public int chapterId; // 章节Id
		public int serialNum; // 宝箱序号

    	//鏋勯�犲嚱鏁�
    	public ReqOpenChapterBox()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			chapterId = 0;
            
			serialNum = 0;
            
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
                chapterId = XBuffer.ReadInt(buffer, ref offset);
                serialNum = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(chapterId,buffer, ref offset);
					XBuffer.WriteInt(serialNum,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //请求一键开启宝箱（包括关卡宝箱和章节宝箱）
    public class ReqFastOpenBox : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 106206;
		public int dungeonId; // 副本Id（1：普通，2：困难，3：噩梦，4：轮回）

    	//鏋勯�犲嚱鏁�
    	public ReqFastOpenBox()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			dungeonId = 0;
            
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
                dungeonId = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(dungeonId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //请求扫荡通用接口
    public class ReqSweepAct : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 106207;
        public List<SweepReqInfo> sweepReqInfos{get;protected set;} //扫荡信息列表

    	//鏋勯�犲嚱鏁�
    	public ReqSweepAct()
    	{
            sweepReqInfos = new List<SweepReqInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            sweepReqInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = sweepReqInfos.Count; a < b; ++a)
            {
				sweepReqInfos[a] = null;
                //var _value_ = sweepReqInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            sweepReqInfos.Clear();
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
                    SweepReqInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<SweepReqInfo>();
					_value_ = new SweepReqInfo();
                    _value_.Read(buffer, ref offset);
                    sweepReqInfos.Add(_value_);
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

                XBuffer.WriteShort((short)sweepReqInfos.Count, buffer, ref offset);
                for(int a = 0; a < sweepReqInfos.Count; ++a)
                {
					if(sweepReqInfos[a] == null)
						UnityEngine.Debug.LogError("sweepReqInfos has nil item, idx == " + a);
					else
						sweepReqInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //重置精英关卡攻打次数
    public class ReqResetAttackNum : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 106209;
		public int actId; // 关卡Id

    	//鏋勯�犲嚱鏁�
    	public ReqResetAttackNum()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			actId = 0;
            
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
                actId = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(actId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //返回副本信息
    public class ResDungeonInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 106101;
        public List<DungeonInfo> dungeonInfos{get;protected set;} //副本信息

    	//鏋勯�犲嚱鏁�
    	public ResDungeonInfo()
    	{
            dungeonInfos = new List<DungeonInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            dungeonInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = dungeonInfos.Count; a < b; ++a)
            {
				dungeonInfos[a] = null;
                //var _value_ = dungeonInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            dungeonInfos.Clear();
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
                    DungeonInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<DungeonInfo>();
					_value_ = new DungeonInfo();
                    _value_.Read(buffer, ref offset);
                    dungeonInfos.Add(_value_);
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

                XBuffer.WriteShort((short)dungeonInfos.Count, buffer, ref offset);
                for(int a = 0; a < dungeonInfos.Count; ++a)
                {
					if(dungeonInfos[a] == null)
						UnityEngine.Debug.LogError("dungeonInfos has nil item, idx == " + a);
					else
						dungeonInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //返回请求战斗的结果
    public class ResFightStart : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 106102;
		public int result; // 结果（1：可以战斗，-1：体力不足，-2：未开启）

    	//鏋勯�犲嚱鏁�
    	public ResFightStart()
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
    //返回请求开启宝箱的结果
    public class ResOpenBox : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 106103;
		public int type; // 箱子类型（0：关卡ID，1：章节ID，2：一键开箱）
		public int __chapterOrActId; // 章节或关卡的ID
		private byte _chapterOrActId = 0; // 章节或关卡的ID tag
		
		public bool hasChapterOrActId()
		{
			return this._chapterOrActId == 1;
		}
		
		public int chapterOrActId
		{
			set
			{
				_chapterOrActId = 1;
				__chapterOrActId = value;
			}
			
			get
			{
				return __chapterOrActId;
			}
		}
		public int __chapterBoxSerialNum; // 章节箱子序号
		private byte _chapterBoxSerialNum = 0; // 章节箱子序号 tag
		
		public bool hasChapterBoxSerialNum()
		{
			return this._chapterBoxSerialNum == 1;
		}
		
		public int chapterBoxSerialNum
		{
			set
			{
				_chapterBoxSerialNum = 1;
				__chapterBoxSerialNum = value;
			}
			
			get
			{
				return __chapterBoxSerialNum;
			}
		}
        public List<ItemInfo> items{get;protected set;} //开箱结果

    	//鏋勯�犲嚱鏁�
    	public ResOpenBox()
    	{
            items = new List<ItemInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			type = 0;
            
			_chapterOrActId = 0;
			__chapterOrActId = 0;
            
			_chapterBoxSerialNum = 0;
			__chapterBoxSerialNum = 0;
            
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
                type = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					chapterOrActId = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					chapterBoxSerialNum = XBuffer.ReadInt(buffer, ref offset);
				}

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
					XBuffer.WriteInt(type,buffer, ref offset);
				XBuffer.WriteByte(_chapterOrActId,buffer, ref offset);
				if (_chapterOrActId == 1)
				{
					XBuffer.WriteInt(chapterOrActId,buffer, ref offset);
				}
				XBuffer.WriteByte(_chapterBoxSerialNum,buffer, ref offset);
				if (_chapterBoxSerialNum == 1)
				{
					XBuffer.WriteInt(chapterBoxSerialNum,buffer, ref offset);
				}

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
    //返回关卡扫荡结果
    public class ResFastSweepAct : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 106105;
        public List<SweepItem> awards{get;protected set;} //战斗奖励（失败时没有数据）
        public List<ItemInfo> extraAward{get;protected set;} //额外奖励（经验药水）

    	//鏋勯�犲嚱鏁�
    	public ResFastSweepAct()
    	{
            awards = new List<SweepItem>();
            extraAward = new List<ItemInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            awards.Clear();
            extraAward.Clear();
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
            for (int a = 0,b = extraAward.Count; a < b; ++a)
            {
				extraAward[a] = null;
                //var _value_ = extraAward[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            extraAward.Clear();
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
                    SweepItem _value_ = null;
                    //_value_ = ClassCacheManager.New<SweepItem>();
					_value_ = new SweepItem();
                    _value_.Read(buffer, ref offset);
                    awards.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    ItemInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<ItemInfo>();
					_value_ = new ItemInfo();
                    _value_.Read(buffer, ref offset);
                    extraAward.Add(_value_);
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

                XBuffer.WriteShort((short)awards.Count, buffer, ref offset);
                for(int a = 0; a < awards.Count; ++a)
                {
					if(awards[a] == null)
						UnityEngine.Debug.LogError("awards has nil item, idx == " + a);
					else
						awards[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)extraAward.Count, buffer, ref offset);
                for(int a = 0; a < extraAward.Count; ++a)
                {
					if(extraAward[a] == null)
						UnityEngine.Debug.LogError("extraAward has nil item, idx == " + a);
					else
						extraAward[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //返回战斗结果
    public class ResFightResult : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 106106;
		public int result; // 结果（0：失败，1：成功
		public int __chapterStar; // 章节星星数
		private byte _chapterStar = 0; // 章节星星数 tag
		
		public bool hasChapterStar()
		{
			return this._chapterStar == 1;
		}
		
		public int chapterStar
		{
			set
			{
				_chapterStar = 1;
				__chapterStar = value;
			}
			
			get
			{
				return __chapterStar;
			}
		}
		public int __actId; // 关卡ID
		private byte _actId = 0; // 关卡ID tag
		
		public bool hasActId()
		{
			return this._actId == 1;
		}
		
		public int actId
		{
			set
			{
				_actId = 1;
				__actId = value;
			}
			
			get
			{
				return __actId;
			}
		}
		public int __openNewChapterFlag; // 是否开启新章节（0:没有开启，1：开启了）
		private byte _openNewChapterFlag = 0; // 是否开启新章节（0:没有开启，1：开启了） tag
		
		public bool hasOpenNewChapterFlag()
		{
			return this._openNewChapterFlag == 1;
		}
		
		public int openNewChapterFlag
		{
			set
			{
				_openNewChapterFlag = 1;
				__openNewChapterFlag = value;
			}
			
			get
			{
				return __openNewChapterFlag;
			}
		}
		public int __bestRecordId; // 当前可打最新关卡ID（没有因为等级去限制）
		private byte _bestRecordId = 0; // 当前可打最新关卡ID（没有因为等级去限制） tag
		
		public bool hasBestRecordId()
		{
			return this._bestRecordId == 1;
		}
		
		public int bestRecordId
		{
			set
			{
				_bestRecordId = 1;
				__bestRecordId = value;
			}
			
			get
			{
				return __bestRecordId;
			}
		}
		public Award __awards; // 战斗奖励（失败时没有数据）
		private byte _awards = 0; // 战斗奖励（失败时没有数据） tag
		
		public bool hasAwards()
		{
			return this._awards == 1;
		}
		
		public Award awards
		{
			set
			{
				_awards = 1;
				__awards = value;
			}
			
			get
			{
				return __awards;
			}
		}
		public int __petExp; // 宠物获得的经验
		private byte _petExp = 0; // 宠物获得的经验 tag
		
		public bool hasPetExp()
		{
			return this._petExp == 1;
		}
		
		public int petExp
		{
			set
			{
				_petExp = 1;
				__petExp = value;
			}
			
			get
			{
				return __petExp;
			}
		}
        public List<int> boxStatus{get;protected set;} //章节宝箱状态（-1：不可领取，0：可领取未打开，1：已领取）
        public List<PetExp> petInfos{get;protected set;} //获得经验的宠物信息
        public List<int> petStatus{get;protected set;} //宠物加经验后的状态（0：只加了经验，1：升级了，2：经验加满了）

    	//鏋勯�犲嚱鏁�
    	public ResFightResult()
    	{
            boxStatus = new List<int>();
            petInfos = new List<PetExp>();
            petStatus = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			result = 0;
            
			_chapterStar = 0;
			__chapterStar = 0;
            
			_actId = 0;
			__actId = 0;
            
			_openNewChapterFlag = 0;
			__openNewChapterFlag = 0;
            
			_bestRecordId = 0;
			__bestRecordId = 0;
            
			_awards = 0;
			//__awards = ClassCacheManager.New<Award>();
			__awards = new Award();
			_petExp = 0;
			__petExp = 0;
            
            boxStatus.Clear();
            petInfos.Clear();
            petStatus.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			__awards = null;

            boxStatus.Clear();
            for (int a = 0,b = petInfos.Count; a < b; ++a)
            {
				petInfos[a] = null;
                //var _value_ = petInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            petInfos.Clear();
            petStatus.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                result = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					chapterStar = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					actId = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					openNewChapterFlag = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					bestRecordId = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
					//awards = ClassCacheManager.New<Award>();
					awards = new Award();
					awards.Read(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					petExp = XBuffer.ReadInt(buffer, ref offset);
				}

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
            		var _value_ = XBuffer.ReadInt(buffer, ref offset);
            		boxStatus.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    PetExp _value_ = null;
                    //_value_ = ClassCacheManager.New<PetExp>();
					_value_ = new PetExp();
                    _value_.Read(buffer, ref offset);
                    petInfos.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
            		var _value_ = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(result,buffer, ref offset);
				XBuffer.WriteByte(_chapterStar,buffer, ref offset);
				if (_chapterStar == 1)
				{
					XBuffer.WriteInt(chapterStar,buffer, ref offset);
				}
				XBuffer.WriteByte(_actId,buffer, ref offset);
				if (_actId == 1)
				{
					XBuffer.WriteInt(actId,buffer, ref offset);
				}
				XBuffer.WriteByte(_openNewChapterFlag,buffer, ref offset);
				if (_openNewChapterFlag == 1)
				{
					XBuffer.WriteInt(openNewChapterFlag,buffer, ref offset);
				}
				XBuffer.WriteByte(_bestRecordId,buffer, ref offset);
				if (_bestRecordId == 1)
				{
					XBuffer.WriteInt(bestRecordId,buffer, ref offset);
				}
				XBuffer.WriteByte(_awards,buffer, ref offset);
				if (_awards == 1)
				{
					if(awards == null)
						//awards = ClassCacheManager.New<Award>();
						awards = new Award();
					awards.WriteWithType(buffer, ref offset);
				}
				XBuffer.WriteByte(_petExp,buffer, ref offset);
				if (_petExp == 1)
				{
					XBuffer.WriteInt(petExp,buffer, ref offset);
				}

                XBuffer.WriteShort((short)boxStatus.Count, buffer, ref offset);
                for(int a = 0; a < boxStatus.Count; ++a)
                {
        			XBuffer.WriteInt(boxStatus[a],buffer, ref offset);
                }
                XBuffer.WriteShort((short)petInfos.Count, buffer, ref offset);
                for(int a = 0; a < petInfos.Count; ++a)
                {
					if(petInfos[a] == null)
						UnityEngine.Debug.LogError("petInfos has nil item, idx == " + a);
					else
						petInfos[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)petStatus.Count, buffer, ref offset);
                for(int a = 0; a < petStatus.Count; ++a)
                {
        			XBuffer.WriteInt(petStatus[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //关卡刷新
    public class ResActInfoUpdate : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 106107;
        public List<ActInfo> actInfos{get;protected set;} //关卡刷新

    	//鏋勯�犲嚱鏁�
    	public ResActInfoUpdate()
    	{
            actInfos = new List<ActInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            actInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = actInfos.Count; a < b; ++a)
            {
				actInfos[a] = null;
                //var _value_ = actInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            actInfos.Clear();
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
                    ActInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<ActInfo>();
					_value_ = new ActInfo();
                    _value_.Read(buffer, ref offset);
                    actInfos.Add(_value_);
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

                XBuffer.WriteShort((short)actInfos.Count, buffer, ref offset);
                for(int a = 0; a < actInfos.Count; ++a)
                {
					if(actInfos[a] == null)
						UnityEngine.Debug.LogError("actInfos has nil item, idx == " + a);
					else
						actInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}