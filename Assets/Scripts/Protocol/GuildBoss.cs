//Auto generated, do not edit it
//副本消息

using System;
using System.IO;
using System.Collections.Generic;
using Message.Challenge;

namespace Message.GuildBoss
{
    public enum TypeEnum
    {
        BossInfo = 1,
        RivalInfo = 2,
        GuildRankItem = 3,
        AllotItem = 4,
        ProgressItem = 5,
        StringVsLong = 6,
        DamageRankItem = 7,
    }

    //Boss信息
    public class BossInfo : BaseMsgStruct
    {
		public int id; // bossId
        
		public int attr; // 属性加成（百分比）
        
		public long hp; // 剩余血量
        
		public RivalInfo __rivalInfo; // 对手信息
		private byte _rivalInfo = 0; // 对手信息 tag
		
		public bool hasRivalInfo()
		{
			return this._rivalInfo == 1;
		}
		
		public RivalInfo rivalInfo
		{
			set
			{
				_rivalInfo = 1;
				__rivalInfo = value;
			}
			
			get
			{
				return __rivalInfo;
			}
		}
        

        //鏋勯�犲嚱鏁�
        public BossInfo() : base()
        {
			
			id = 0;
            
			attr = 0;
            
			hp = 0L;
			_rivalInfo = 0;
			//__rivalInfo = ClassCacheManager.New<RivalInfo>();
			__rivalInfo = new RivalInfo();

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			attr = 0;
            
			hp = 0L;
			_rivalInfo = 0;
			//__rivalInfo = ClassCacheManager.New<RivalInfo>();
			__rivalInfo = new RivalInfo();

        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			__rivalInfo = null;

        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                id = XBuffer.ReadInt(buffer, ref offset);
                attr = XBuffer.ReadInt(buffer, ref offset);
                hp = XBuffer.ReadLong(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					_real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
					//rivalInfo = ClassCacheManager.New<RivalInfo>();
					rivalInfo = new RivalInfo();
					rivalInfo.Read(buffer, ref offset);
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
                XBuffer.WriteInt(id, buffer, ref offset);
                XBuffer.WriteInt(attr, buffer, ref offset);
                XBuffer.WriteLong(hp, buffer, ref offset);
				XBuffer.WriteByte(_rivalInfo, buffer, ref offset);
				if (_rivalInfo == 1)
				{
					if(rivalInfo==null)
						//rivalInfo = ClassCacheManager.New<RivalInfo>();
						rivalInfo = new RivalInfo();
					rivalInfo.WriteWithType(buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //对手信息
    public class RivalInfo : BaseMsgStruct
    {
		public string name; // 名字
        
		public long hp; // 剩余血量
        

        //鏋勯�犲嚱鏁�
        public RivalInfo() : base()
        {
			
			name = "";
			hp = 0L;

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			name = "";
			hp = 0L;

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
                name = XBuffer.ReadString(buffer, ref offset);
                hp = XBuffer.ReadLong(buffer, ref offset);

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
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteLong(hp, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //公会排名
    public class GuildRankItem : BaseMsgStruct
    {
		public int rank; // 排名
        
		public string name; // 名字
        
		public int icon; // 图标
        
		public long completeTime; // 完成时间
        

        //鏋勯�犲嚱鏁�
        public GuildRankItem() : base()
        {
			
			rank = 0;
            
			name = "";
			icon = 0;
            
			completeTime = 0L;

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			rank = 0;
            
			name = "";
			icon = 0;
            
			completeTime = 0L;

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
                rank = XBuffer.ReadInt(buffer, ref offset);
                name = XBuffer.ReadString(buffer, ref offset);
                icon = XBuffer.ReadInt(buffer, ref offset);
                completeTime = XBuffer.ReadLong(buffer, ref offset);

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
                XBuffer.WriteInt(rank, buffer, ref offset);
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteInt(icon, buffer, ref offset);
                XBuffer.WriteLong(completeTime, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //分配记录
    public class AllotItem : BaseMsgStruct
    {
		public int itemId; // 道具名
        
		public int itemNum; // 道具数量
        
		public string name; // name
        
		public int icon; // 角色头像
        
		public int border; // 角色边框
        
		public int bossId; // bossId
        
		public long completeTime; // 完成时间
        

        //鏋勯�犲嚱鏁�
        public AllotItem() : base()
        {
			
			itemId = 0;
            
			itemNum = 0;
            
			name = "";
			icon = 0;
            
			border = 0;
            
			bossId = 0;
            
			completeTime = 0L;

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			itemId = 0;
            
			itemNum = 0;
            
			name = "";
			icon = 0;
            
			border = 0;
            
			bossId = 0;
            
			completeTime = 0L;

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
                itemId = XBuffer.ReadInt(buffer, ref offset);
                itemNum = XBuffer.ReadInt(buffer, ref offset);
                name = XBuffer.ReadString(buffer, ref offset);
                icon = XBuffer.ReadInt(buffer, ref offset);
                border = XBuffer.ReadInt(buffer, ref offset);
                bossId = XBuffer.ReadInt(buffer, ref offset);
                completeTime = XBuffer.ReadLong(buffer, ref offset);

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
                XBuffer.WriteInt(itemId, buffer, ref offset);
                XBuffer.WriteInt(itemNum, buffer, ref offset);
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteInt(icon, buffer, ref offset);
                XBuffer.WriteInt(border, buffer, ref offset);
                XBuffer.WriteInt(bossId, buffer, ref offset);
                XBuffer.WriteLong(completeTime, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //成员进度
    public class ProgressItem : BaseMsgStruct
    {
		public string name; // 名字
        
		public int level; // 等级
        
		public int icon; // 头像
        
		public int job; // 职位
        
		public int progress; // 今日已完成次数
        

        //鏋勯�犲嚱鏁�
        public ProgressItem() : base()
        {
			
			name = "";
			level = 0;
            
			icon = 0;
            
			job = 0;
            
			progress = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			name = "";
			level = 0;
            
			icon = 0;
            
			job = 0;
            
			progress = 0;
            

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
                name = XBuffer.ReadString(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                icon = XBuffer.ReadInt(buffer, ref offset);
                job = XBuffer.ReadInt(buffer, ref offset);
                progress = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteInt(icon, buffer, ref offset);
                XBuffer.WriteInt(job, buffer, ref offset);
                XBuffer.WriteInt(progress, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //字符串Vs数字
    public class StringVsLong : BaseMsgStruct
    {
		public string str; // 字符串
        
		public long num; // 数字
        

        //鏋勯�犲嚱鏁�
        public StringVsLong() : base()
        {
			
			str = "";
			num = 0L;

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			str = "";
			num = 0L;

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
                str = XBuffer.ReadString(buffer, ref offset);
                num = XBuffer.ReadLong(buffer, ref offset);

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
                XBuffer.WriteString(str, buffer, ref offset);
                XBuffer.WriteLong(num, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //伤害
    public class DamageRankItem : BaseMsgStruct
    {
		public int rank; // 排名
        
		public string name; // 名字
        
		public long damage; // 伤害
        

        //鏋勯�犲嚱鏁�
        public DamageRankItem() : base()
        {
			
			rank = 0;
            
			name = "";
			damage = 0L;

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			rank = 0;
            
			name = "";
			damage = 0L;

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
                rank = XBuffer.ReadInt(buffer, ref offset);
                name = XBuffer.ReadString(buffer, ref offset);
                damage = XBuffer.ReadLong(buffer, ref offset);

    		    short _count_ = 0;
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
                XBuffer.WriteInt(rank, buffer, ref offset);
                XBuffer.WriteString(name, buffer, ref offset);
                XBuffer.WriteLong(damage, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //获取公会副本信息
    public class ReqGuildDungeonInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119201;

    	//鏋勯�犲嚱鏁�
    	public ReqGuildDungeonInfo()
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
    //获取公会排行
    public class ReqGuildRankInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119202;
		public int id; // bossId
		public int from; // 起（0开始）
		public int end; // 止（不包括）

    	//鏋勯�犲嚱鏁�
    	public ReqGuildRankInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			from = 0;
            
			end = 0;
            
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
                from = XBuffer.ReadInt(buffer, ref offset);
                end = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(from,buffer, ref offset);
					XBuffer.WriteInt(end,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //获取分配信息
    public class ReqAllotRecordInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119203;

    	//鏋勯�犲嚱鏁�
    	public ReqAllotRecordInfo()
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
    //获取成员进度
    public class ReqProgressInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119204;
		public int from; // 起（0开始）
		public int end; // 止（不包括）

    	//鏋勯�犲嚱鏁�
    	public ReqProgressInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			from = 0;
            
			end = 0;
            
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
                from = XBuffer.ReadInt(buffer, ref offset);
                end = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(from,buffer, ref offset);
					XBuffer.WriteInt(end,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //boss信息
    public class ReqBossInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119205;
		public int bossId; // bossId

    	//鏋勯�犲嚱鏁�
    	public ReqBossInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			bossId = 0;
            
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
                bossId = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(bossId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //伤害排名
    public class ReqDamageRank : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119206;
		public int bossId; // bossId
		public int from; // 起（0开始）
		public int end; // 止（不包括）

    	//鏋勯�犲嚱鏁�
    	public ReqDamageRank()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			bossId = 0;
            
			from = 0;
            
			end = 0;
            
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
                bossId = XBuffer.ReadInt(buffer, ref offset);
                from = XBuffer.ReadInt(buffer, ref offset);
                end = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(bossId,buffer, ref offset);
					XBuffer.WriteInt(from,buffer, ref offset);
					XBuffer.WriteInt(end,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //请求战斗
    public class ReqFightStart : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119207;
		public int bossId; // bossId

    	//鏋勯�犲嚱鏁�
    	public ReqFightStart()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			bossId = 0;
            
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
                bossId = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(bossId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //发送战斗结果
    public class ReqFightEnd : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119208;
		public int bossId; // bossId
		public long damage; // 伤害

    	//鏋勯�犲嚱鏁�
    	public ReqFightEnd()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			bossId = 0;
            
			damage = 0L;
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
                bossId = XBuffer.ReadInt(buffer, ref offset);
                damage = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteInt(bossId,buffer, ref offset);
					XBuffer.WriteLong(damage,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //获取首通奖励
    public class ReqGetFirstPassAward : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119209;
		public int id; // bossId

    	//鏋勯�犲嚱鏁�
    	public ReqGetFirstPassAward()
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
    //一键获取首通奖励
    public class ReqOneKeyGetFirstPassAward : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119210;

    	//鏋勯�犲嚱鏁�
    	public ReqOneKeyGetFirstPassAward()
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
    //公会副本信息
    public class ResGuildDungeonInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119101;
		public BossInfo bossInfo; // 当前boss信息
        public List<int> canGetRewardBossIds{get;protected set;} //可以领取奖励的bossId

    	//鏋勯�犲嚱鏁�
    	public ResGuildDungeonInfo()
    	{
            canGetRewardBossIds = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//bossInfo = ClassCacheManager.New<BossInfo>();
			bossInfo = new BossInfo();
            canGetRewardBossIds.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			bossInfo = null;

            canGetRewardBossIds.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //bossInfo = ClassCacheManager.New<BossInfo>();
				bossInfo = new BossInfo();
                bossInfo.Read(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
            		var _value_ = XBuffer.ReadInt(buffer, ref offset);
            		canGetRewardBossIds.Add(_value_);
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
					if(bossInfo == null)
						//bossInfo = ClassCacheManager.New<BossInfo>();
						bossInfo = new BossInfo();
					bossInfo.WriteWithType(buffer, ref offset);

                XBuffer.WriteShort((short)canGetRewardBossIds.Count, buffer, ref offset);
                for(int a = 0; a < canGetRewardBossIds.Count; ++a)
                {
        			XBuffer.WriteInt(canGetRewardBossIds[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //公会排行
    public class ResGuildRankInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119102;
		public int bossId; // bossId
		public GuildRankItem self; // 本公会信息
        public List<GuildRankItem> ranks{get;protected set;} //排名信息

    	//鏋勯�犲嚱鏁�
    	public ResGuildRankInfo()
    	{
            ranks = new List<GuildRankItem>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			bossId = 0;
            
			//self = ClassCacheManager.New<GuildRankItem>();
			self = new GuildRankItem();
            ranks.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			self = null;

            for (int a = 0,b = ranks.Count; a < b; ++a)
            {
				ranks[a] = null;
                //var _value_ = ranks[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            ranks.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                bossId = XBuffer.ReadInt(buffer, ref offset);
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //self = ClassCacheManager.New<GuildRankItem>();
				self = new GuildRankItem();
                self.Read(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    GuildRankItem _value_ = null;
                    //_value_ = ClassCacheManager.New<GuildRankItem>();
					_value_ = new GuildRankItem();
                    _value_.Read(buffer, ref offset);
                    ranks.Add(_value_);
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
					XBuffer.WriteInt(bossId,buffer, ref offset);
					if(self == null)
						//self = ClassCacheManager.New<GuildRankItem>();
						self = new GuildRankItem();
					self.WriteWithType(buffer, ref offset);

                XBuffer.WriteShort((short)ranks.Count, buffer, ref offset);
                for(int a = 0; a < ranks.Count; ++a)
                {
					if(ranks[a] == null)
						UnityEngine.Debug.LogError("ranks has nil item, idx == " + a);
					else
						ranks[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //分配记录
    public class ResAllotRecordInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119103;
        public List<AllotItem> records{get;protected set;} //分配记录

    	//鏋勯�犲嚱鏁�
    	public ResAllotRecordInfo()
    	{
            records = new List<AllotItem>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            records.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = records.Count; a < b; ++a)
            {
				records[a] = null;
                //var _value_ = records[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            records.Clear();
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
                    AllotItem _value_ = null;
                    //_value_ = ClassCacheManager.New<AllotItem>();
					_value_ = new AllotItem();
                    _value_.Read(buffer, ref offset);
                    records.Add(_value_);
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

                XBuffer.WriteShort((short)records.Count, buffer, ref offset);
                for(int a = 0; a < records.Count; ++a)
                {
					if(records[a] == null)
						UnityEngine.Debug.LogError("records has nil item, idx == " + a);
					else
						records[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //成员进度
    public class ResProgressInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119104;
        public List<ProgressItem> progress{get;protected set;} //进度

    	//鏋勯�犲嚱鏁�
    	public ResProgressInfo()
    	{
            progress = new List<ProgressItem>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            progress.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = progress.Count; a < b; ++a)
            {
				progress[a] = null;
                //var _value_ = progress[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            progress.Clear();
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
                    ProgressItem _value_ = null;
                    //_value_ = ClassCacheManager.New<ProgressItem>();
					_value_ = new ProgressItem();
                    _value_.Read(buffer, ref offset);
                    progress.Add(_value_);
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

                XBuffer.WriteShort((short)progress.Count, buffer, ref offset);
                for(int a = 0; a < progress.Count; ++a)
                {
					if(progress[a] == null)
						UnityEngine.Debug.LogError("progress has nil item, idx == " + a);
					else
						progress[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //当前boss信息
    public class ResBossInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119105;
		public int id; // bossId
		public int progress; // 今日已完成次数
		public long hp; // 剩余血量
        public List<StringVsLong> rank{get;protected set;} //前三社团（名字，剩余血量）

    	//鏋勯�犲嚱鏁�
    	public ResBossInfo()
    	{
            rank = new List<StringVsLong>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			progress = 0;
            
			hp = 0L;
            rank.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = rank.Count; a < b; ++a)
            {
				rank[a] = null;
                //var _value_ = rank[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            rank.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                id = XBuffer.ReadInt(buffer, ref offset);
                progress = XBuffer.ReadInt(buffer, ref offset);
                hp = XBuffer.ReadLong(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    StringVsLong _value_ = null;
                    //_value_ = ClassCacheManager.New<StringVsLong>();
					_value_ = new StringVsLong();
                    _value_.Read(buffer, ref offset);
                    rank.Add(_value_);
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
					XBuffer.WriteInt(progress,buffer, ref offset);
					XBuffer.WriteLong(hp,buffer, ref offset);

                XBuffer.WriteShort((short)rank.Count, buffer, ref offset);
                for(int a = 0; a < rank.Count; ++a)
                {
					if(rank[a] == null)
						UnityEngine.Debug.LogError("rank has nil item, idx == " + a);
					else
						rank[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //伤害排名
    public class ResDamageRank : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119106;
        public List<DamageRankItem> records{get;protected set;} //伤害信息

    	//鏋勯�犲嚱鏁�
    	public ResDamageRank()
    	{
            records = new List<DamageRankItem>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            records.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = records.Count; a < b; ++a)
            {
				records[a] = null;
                //var _value_ = records[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            records.Clear();
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
                    DamageRankItem _value_ = null;
                    //_value_ = ClassCacheManager.New<DamageRankItem>();
					_value_ = new DamageRankItem();
                    _value_.Read(buffer, ref offset);
                    records.Add(_value_);
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

                XBuffer.WriteShort((short)records.Count, buffer, ref offset);
                for(int a = 0; a < records.Count; ++a)
                {
					if(records[a] == null)
						UnityEngine.Debug.LogError("records has nil item, idx == " + a);
					else
						records[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //开始战斗
    public class ResFightStart : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119107;

    	//鏋勯�犲嚱鏁�
    	public ResFightStart()
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
    //战斗结果
    public class ResFightEnd : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119108;
		public long damage; // 伤害
        public List<IntVsInt> rewards{get;protected set;} //战斗奖励

    	//鏋勯�犲嚱鏁�
    	public ResFightEnd()
    	{
            rewards = new List<IntVsInt>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			damage = 0L;
            rewards.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = rewards.Count; a < b; ++a)
            {
				rewards[a] = null;
                //var _value_ = rewards[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            rewards.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                damage = XBuffer.ReadLong(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    IntVsInt _value_ = null;
                    //_value_ = ClassCacheManager.New<IntVsInt>();
					_value_ = new IntVsInt();
                    _value_.Read(buffer, ref offset);
                    rewards.Add(_value_);
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
					XBuffer.WriteLong(damage,buffer, ref offset);

                XBuffer.WriteShort((short)rewards.Count, buffer, ref offset);
                for(int a = 0; a < rewards.Count; ++a)
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
    //领取boss首通奖励
    public class ResGetReward : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 119109;
        public List<IntVsInt> rewards{get;protected set;} //战斗奖励
        public List<int> canGetRewardBossIds{get;protected set;} //可以领取奖励的bossId

    	//鏋勯�犲嚱鏁�
    	public ResGetReward()
    	{
            rewards = new List<IntVsInt>();
            canGetRewardBossIds = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            rewards.Clear();
            canGetRewardBossIds.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = rewards.Count; a < b; ++a)
            {
				rewards[a] = null;
                //var _value_ = rewards[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            rewards.Clear();
            canGetRewardBossIds.Clear();
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
                    IntVsInt _value_ = null;
                    //_value_ = ClassCacheManager.New<IntVsInt>();
					_value_ = new IntVsInt();
                    _value_.Read(buffer, ref offset);
                    rewards.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
            		var _value_ = XBuffer.ReadInt(buffer, ref offset);
            		canGetRewardBossIds.Add(_value_);
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

                XBuffer.WriteShort((short)rewards.Count, buffer, ref offset);
                for(int a = 0; a < rewards.Count; ++a)
                {
					if(rewards[a] == null)
						UnityEngine.Debug.LogError("rewards has nil item, idx == " + a);
					else
						rewards[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)canGetRewardBossIds.Count, buffer, ref offset);
                for(int a = 0; a < canGetRewardBossIds.Count; ++a)
                {
        			XBuffer.WriteInt(canGetRewardBossIds[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}