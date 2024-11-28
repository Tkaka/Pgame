//Auto generated, do not edit it
//战队

using System;
using System.IO;
using System.Collections.Generic;
using Message.Bag;

namespace Message.Team
{
    public enum TypeEnum
    {
        Statue = 1,
        Exhibition = 2,
        RankItem = 3,
        HofItem = 4,
        Attr = 5,
        ArtifactRandomAttr = 6,
        ArtifactAttr = 7,
        Artifact = 8,
    }

    //铜像
    public class Statue : BaseMsgStruct
    {
		public int petId; // 对应宠物ID
        
		public int value; // 铜像价值
        
		public int currentStatueId; // 当前使用的铜像（petId*100+材质（1-6）*10+品质（0-3））没有就是0
        
		public int starAdd; // 伙伴星级加成（万分比）
        
		public int colorAdd; // 伙伴品质加成（万分比）
        
		public int quantityAdd; // 铜像数量加成（万分比）
        
        public List<int> statueUnitId{get; protected set;} //拥有的该宠物铜像ID列表（petId*100+材质（1-6）*10+品质（0-3））

        //鏋勯�犲嚱鏁�
        public Statue() : base()
        {
            statueUnitId = new List<int>(); //拥有的该宠物铜像ID列表（petId*100+材质（1-6）*10+品质（0-3））
			
			petId = 0;
            
			value = 0;
            
			currentStatueId = 0;
            
			starAdd = 0;
            
			colorAdd = 0;
            
			quantityAdd = 0;
            

            statueUnitId.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			value = 0;
            
			currentStatueId = 0;
            
			starAdd = 0;
            
			colorAdd = 0;
            
			quantityAdd = 0;
            

            statueUnitId.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            statueUnitId.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                petId = XBuffer.ReadInt(buffer, ref offset);
                value = XBuffer.ReadInt(buffer, ref offset);
                currentStatueId = XBuffer.ReadInt(buffer, ref offset);
                starAdd = XBuffer.ReadInt(buffer, ref offset);
                colorAdd = XBuffer.ReadInt(buffer, ref offset);
                quantityAdd = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    var _value_ = XBuffer.ReadInt(buffer, ref offset);
                    statueUnitId.Add(_value_);
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
                XBuffer.WriteInt(petId, buffer, ref offset);
                XBuffer.WriteInt(value, buffer, ref offset);
                XBuffer.WriteInt(currentStatueId, buffer, ref offset);
                XBuffer.WriteInt(starAdd, buffer, ref offset);
                XBuffer.WriteInt(colorAdd, buffer, ref offset);
                XBuffer.WriteInt(quantityAdd, buffer, ref offset);

                XBuffer.WriteShort((short)statueUnitId.Count,buffer, ref offset);
                for (int a = 0; a < statueUnitId.Count; ++a)
                {
                    XBuffer.WriteInt(statueUnitId[a], buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //展馆
    public class Exhibition : BaseMsgStruct
    {
		public int exhibitionId; // 展馆序号
        
		public int exhibitionAtk; // 展馆加成的攻击
        
		public int exhibitionDef; // 展馆铜像加成的防御
        
		public int exhibitionHp; // 展馆铜像加成的生命
        
		public int value; // 铜像总价值
        
        public List<int> currentStatueIds{get; protected set;} //当前使用的铜像列表（petId*100+材质（1-6）*10+品质（0-3））没有就是0

        //鏋勯�犲嚱鏁�
        public Exhibition() : base()
        {
            currentStatueIds = new List<int>(); //当前使用的铜像列表（petId*100+材质（1-6）*10+品质（0-3））没有就是0
			
			exhibitionId = 0;
            
			exhibitionAtk = 0;
            
			exhibitionDef = 0;
            
			exhibitionHp = 0;
            
			value = 0;
            

            currentStatueIds.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			exhibitionId = 0;
            
			exhibitionAtk = 0;
            
			exhibitionDef = 0;
            
			exhibitionHp = 0;
            
			value = 0;
            

            currentStatueIds.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            currentStatueIds.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                exhibitionId = XBuffer.ReadInt(buffer, ref offset);
                exhibitionAtk = XBuffer.ReadInt(buffer, ref offset);
                exhibitionDef = XBuffer.ReadInt(buffer, ref offset);
                exhibitionHp = XBuffer.ReadInt(buffer, ref offset);
                value = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    var _value_ = XBuffer.ReadInt(buffer, ref offset);
                    currentStatueIds.Add(_value_);
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
                XBuffer.WriteInt(exhibitionId, buffer, ref offset);
                XBuffer.WriteInt(exhibitionAtk, buffer, ref offset);
                XBuffer.WriteInt(exhibitionDef, buffer, ref offset);
                XBuffer.WriteInt(exhibitionHp, buffer, ref offset);
                XBuffer.WriteInt(value, buffer, ref offset);

                XBuffer.WriteShort((short)currentStatueIds.Count,buffer, ref offset);
                for (int a = 0; a < currentStatueIds.Count; ++a)
                {
                    XBuffer.WriteInt(currentStatueIds[a], buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //铜像排行榜
    public class RankItem : BaseMsgStruct
    {
		public long roleId; // 角色ID
        
		public string roleName; // 角色名
        
		public int statueTotalValue; // 铜像总价值
        
		public int statueTotalNumber; // 铜像总数量
        
		public int level; // 玩家等级
        
		public string clubName; // 公会名
        

        //鏋勯�犲嚱鏁�
        public RankItem() : base()
        {
			
			roleId = 0L;
			roleName = "";
			statueTotalValue = 0;
            
			statueTotalNumber = 0;
            
			level = 0;
            
			clubName = "";

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			roleName = "";
			statueTotalValue = 0;
            
			statueTotalNumber = 0;
            
			level = 0;
            
			clubName = "";

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
                roleName = XBuffer.ReadString(buffer, ref offset);
                statueTotalValue = XBuffer.ReadInt(buffer, ref offset);
                statueTotalNumber = XBuffer.ReadInt(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                clubName = XBuffer.ReadString(buffer, ref offset);

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
                XBuffer.WriteLong(roleId, buffer, ref offset);
                XBuffer.WriteString(roleName, buffer, ref offset);
                XBuffer.WriteInt(statueTotalValue, buffer, ref offset);
                XBuffer.WriteInt(statueTotalNumber, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteString(clubName, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //名人堂人物
    public class HofItem : BaseMsgStruct
    {
		public int petId; // 宠物ID
        
		public int level; // 等级
        
		public int exp; // 经验
        
		public int color; // 品阶
        
		public int priority; // 先手值
        
        public List<Attr> attrs{get; protected set;} //属性加成

        //鏋勯�犲嚱鏁�
        public HofItem() : base()
        {
            attrs = new List<Attr>(); //属性加成
			
			petId = 0;
            
			level = 0;
            
			exp = 0;
            
			color = 0;
            
			priority = 0;
            

            attrs.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			level = 0;
            
			exp = 0;
            
			color = 0;
            
			priority = 0;
            

            attrs.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = attrs.Count; a < b; ++a)
            {
                //var _value_ = attrs[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				attrs[a] = null;
            }
            attrs.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                petId = XBuffer.ReadInt(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                exp = XBuffer.ReadInt(buffer, ref offset);
                color = XBuffer.ReadInt(buffer, ref offset);
                priority = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    Attr _value_ = null;
                    //_value_ = ClassCacheManager.New<Attr>();
					_value_ = new Attr();
                    _value_.Read(buffer, ref offset);
                    attrs.Add(_value_);
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
                XBuffer.WriteInt(petId, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteInt(exp, buffer, ref offset);
                XBuffer.WriteInt(color, buffer, ref offset);
                XBuffer.WriteInt(priority, buffer, ref offset);

                XBuffer.WriteShort((short)attrs.Count,buffer, ref offset);
                for (int a = 0; a < attrs.Count; ++a)
                {
					if(attrs[a] == null)
						UnityEngine.Debug.LogError("attrs has nil item, idx == " + a);
					else
						attrs[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //属性
    public class Attr : BaseMsgStruct
    {
		public int id; // 属性id
        
		public int value; // 属性值
        

        //鏋勯�犲嚱鏁�
        public Attr() : base()
        {
			
			id = 0;
            
			value = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			value = 0;
            

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
                XBuffer.WriteInt(value, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //随机属性
    public class ArtifactRandomAttr : BaseMsgStruct
    {
		public int recommend; // 推荐（1：是，0：否）
        
        public List<Attr> attr{get; protected set;} //属性

        //鏋勯�犲嚱鏁�
        public ArtifactRandomAttr() : base()
        {
            attr = new List<Attr>(); //属性
			
			recommend = 0;
            

            attr.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			recommend = 0;
            

            attr.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = attr.Count; a < b; ++a)
            {
                //var _value_ = attr[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				attr[a] = null;
            }
            attr.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                recommend = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    Attr _value_ = null;
                    //_value_ = ClassCacheManager.New<Attr>();
					_value_ = new Attr();
                    _value_.Read(buffer, ref offset);
                    attr.Add(_value_);
                }
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
                XBuffer.WriteInt(recommend, buffer, ref offset);

                XBuffer.WriteShort((short)attr.Count,buffer, ref offset);
                for (int a = 0; a < attr.Count; ++a)
                {
					if(attr[a] == null)
						UnityEngine.Debug.LogError("attr has nil item, idx == " + a);
					else
						attr[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //神器属性
    public class ArtifactAttr : BaseMsgStruct
    {
		public int id; // id
        
		public int value; // 值
        
		public int max; // 上限
        

        //鏋勯�犲嚱鏁�
        public ArtifactAttr() : base()
        {
			
			id = 0;
            
			value = 0;
            
			max = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			value = 0;
            
			max = 0;
            

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
                max = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(id, buffer, ref offset);
                XBuffer.WriteInt(value, buffer, ref offset);
                XBuffer.WriteInt(max, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //神器
    public class Artifact : BaseMsgStruct
    {
		public int id; // 神器ID
        
		public int singleTimesOdd; // 单次剩余
        
		public int tenTimesOdd; // 十次剩余
        
        public List<ArtifactAttr> artifactAttrs{get; protected set;} //属性

        //鏋勯�犲嚱鏁�
        public Artifact() : base()
        {
            artifactAttrs = new List<ArtifactAttr>(); //属性
			
			id = 0;
            
			singleTimesOdd = 0;
            
			tenTimesOdd = 0;
            

            artifactAttrs.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			singleTimesOdd = 0;
            
			tenTimesOdd = 0;
            

            artifactAttrs.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = artifactAttrs.Count; a < b; ++a)
            {
                //var _value_ = artifactAttrs[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				artifactAttrs[a] = null;
            }
            artifactAttrs.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                id = XBuffer.ReadInt(buffer, ref offset);
                singleTimesOdd = XBuffer.ReadInt(buffer, ref offset);
                tenTimesOdd = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    ArtifactAttr _value_ = null;
                    //_value_ = ClassCacheManager.New<ArtifactAttr>();
					_value_ = new ArtifactAttr();
                    _value_.Read(buffer, ref offset);
                    artifactAttrs.Add(_value_);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(8, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(id, buffer, ref offset);
                XBuffer.WriteInt(singleTimesOdd, buffer, ref offset);
                XBuffer.WriteInt(tenTimesOdd, buffer, ref offset);

                XBuffer.WriteShort((short)artifactAttrs.Count,buffer, ref offset);
                for (int a = 0; a < artifactAttrs.Count; ++a)
                {
					if(artifactAttrs[a] == null)
						UnityEngine.Debug.LogError("artifactAttrs has nil item, idx == " + a);
					else
						artifactAttrs[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //获取铜像馆信息
    public class ReqExhibitionInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111201;

    	//鏋勯�犲嚱鏁�
    	public ReqExhibitionInfo()
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
    //获取展厅信息
    public class ReqExhibitionRoomInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111202;
		public int exhibitionId; // 展厅ID（1开始）

    	//鏋勯�犲嚱鏁�
    	public ReqExhibitionRoomInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			exhibitionId = 0;
            
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
                exhibitionId = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(exhibitionId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //获取雕塑信息
    public class ReqStatueInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111203;
		public int exhibitionId; // 展厅ID（1开始）
		public int statueId; // 宠物ID

    	//鏋勯�犲嚱鏁�
    	public ReqStatueInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			exhibitionId = 0;
            
			statueId = 0;
            
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
                exhibitionId = XBuffer.ReadInt(buffer, ref offset);
                statueId = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(exhibitionId,buffer, ref offset);
					XBuffer.WriteInt(statueId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //购买雕塑
    public class ReqStatueBuy : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111204;
		public int exhibitionId; // 展厅ID（1开始）
		public int statueId; // 雕塑ID（petId*100+材质（1-6）*10+品质（0-3））

    	//鏋勯�犲嚱鏁�
    	public ReqStatueBuy()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			exhibitionId = 0;
            
			statueId = 0;
            
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
                exhibitionId = XBuffer.ReadInt(buffer, ref offset);
                statueId = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(exhibitionId,buffer, ref offset);
					XBuffer.WriteInt(statueId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //更换雕塑
    public class ReqStatueExchange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111205;
		public int exhibitionId; // 展厅ID（1开始）
		public int oldStatueId; // 被替换雕塑ID
		public int newStatueId; // 新的雕塑ID

    	//鏋勯�犲嚱鏁�
    	public ReqStatueExchange()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			exhibitionId = 0;
            
			oldStatueId = 0;
            
			newStatueId = 0;
            
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
                exhibitionId = XBuffer.ReadInt(buffer, ref offset);
                oldStatueId = XBuffer.ReadInt(buffer, ref offset);
                newStatueId = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(exhibitionId,buffer, ref offset);
					XBuffer.WriteInt(oldStatueId,buffer, ref offset);
					XBuffer.WriteInt(newStatueId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //查看排行榜
    public class ReqStatueRankList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111206;
		public int from; // 从多少名
		public int end; // 到多少名

    	//鏋勯�犲嚱鏁�
    	public ReqStatueRankList()
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
    //名人堂增加经验
    public class ReqHofAddExp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111207;
		public int teamId; // 队伍ID
		public int petId; // 宠物ID
		public int gridId; // 格子ID
		public int number; // 使用数量

    	//鏋勯�犲嚱鏁�
    	public ReqHofAddExp()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			teamId = 0;
            
			petId = 0;
            
			gridId = 0;
            
			number = 0;
            
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
                teamId = XBuffer.ReadInt(buffer, ref offset);
                petId = XBuffer.ReadInt(buffer, ref offset);
                gridId = XBuffer.ReadInt(buffer, ref offset);
                number = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(teamId,buffer, ref offset);
					XBuffer.WriteInt(petId,buffer, ref offset);
					XBuffer.WriteInt(gridId,buffer, ref offset);
					XBuffer.WriteInt(number,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //名人堂升品
    public class ReqHofColorUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111208;
		public int petId; // 宠物ID

    	//鏋勯�犲嚱鏁�
    	public ReqHofColorUp()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
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
					XBuffer.WriteInt(petId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //获取名人堂信息
    public class ReqHofInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111209;

    	//鏋勯�犲嚱鏁�
    	public ReqHofInfo()
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
    //获取神器信息
    public class ReqArtifactInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111210;

    	//鏋勯�犲嚱鏁�
    	public ReqArtifactInfo()
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
    //神器训练
    public class ReqArtifactTrain : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111211;
		public int artifactId; // 神器ID
		public int type; // 训练类型（1：免费，2：金币，3：钻石）
		public int isSingle; // 是否单次培养（1：是，0：否）
		public int number; // 训练次数

    	//鏋勯�犲嚱鏁�
    	public ReqArtifactTrain()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			artifactId = 0;
            
			type = 0;
            
			isSingle = 0;
            
			number = 0;
            
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
                artifactId = XBuffer.ReadInt(buffer, ref offset);
                type = XBuffer.ReadInt(buffer, ref offset);
                isSingle = XBuffer.ReadInt(buffer, ref offset);
                number = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(artifactId,buffer, ref offset);
					XBuffer.WriteInt(type,buffer, ref offset);
					XBuffer.WriteInt(isSingle,buffer, ref offset);
					XBuffer.WriteInt(number,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //神器训练保存
    public class ReqArtifactTrainSave : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111212;
		public int id; // 神器ID
        public List<int> save{get;protected set;} //是否保存

    	//鏋勯�犲嚱鏁�
    	public ReqArtifactTrainSave()
    	{
            save = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
            save.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            save.Clear();
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
            		var _value_ = XBuffer.ReadInt(buffer, ref offset);
            		save.Add(_value_);
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

                XBuffer.WriteShort((short)save.Count, buffer, ref offset);
                for(int a = 0; a < save.Count; ++a)
                {
        			XBuffer.WriteInt(save[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //神器解锁
    public class ReqArtifactUnlock : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111213;
		public int id; // 神器ID

    	//鏋勯�犲嚱鏁�
    	public ReqArtifactUnlock()
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
    //铜像馆信息
    public class ResExhibitionInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111101;
		public int value; // 总价值
        public List<int> owns{get;protected set;} //已开放展厅拥有了雕塑的数量

    	//鏋勯�犲嚱鏁�
    	public ResExhibitionInfo()
    	{
            owns = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			value = 0;
            
            owns.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            owns.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                value = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
            		var _value_ = XBuffer.ReadInt(buffer, ref offset);
            		owns.Add(_value_);
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
					XBuffer.WriteInt(value,buffer, ref offset);

                XBuffer.WriteShort((short)owns.Count, buffer, ref offset);
                for(int a = 0; a < owns.Count; ++a)
                {
        			XBuffer.WriteInt(owns[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //展厅信息
    public class ResExhibitionRoomInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111102;
		public Exhibition exhibition; // 展馆

    	//鏋勯�犲嚱鏁�
    	public ResExhibitionRoomInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//exhibition = ClassCacheManager.New<Exhibition>();
			exhibition = new Exhibition();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			exhibition = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //exhibition = ClassCacheManager.New<Exhibition>();
				exhibition = new Exhibition();
                exhibition.Read(buffer, ref offset);

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
					if(exhibition == null)
						//exhibition = ClassCacheManager.New<Exhibition>();
						exhibition = new Exhibition();
					exhibition.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //雕塑信息
    public class ResStatueInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111103;
		public Statue statue; // 雕塑信息

    	//鏋勯�犲嚱鏁�
    	public ResStatueInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//statue = ClassCacheManager.New<Statue>();
			statue = new Statue();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			statue = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //statue = ClassCacheManager.New<Statue>();
				statue = new Statue();
                statue.Read(buffer, ref offset);

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
					if(statue == null)
						//statue = ClassCacheManager.New<Statue>();
						statue = new Statue();
					statue.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //购买铜像
    public class ResStatueBuy : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111104;
		public Exhibition exhibition; // 展馆信息
		public Statue statue; // 雕塑信息
		public int value; // 总价值

    	//鏋勯�犲嚱鏁�
    	public ResStatueBuy()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//exhibition = ClassCacheManager.New<Exhibition>();
			exhibition = new Exhibition();
			//statue = ClassCacheManager.New<Statue>();
			statue = new Statue();
			value = 0;
            
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			exhibition = null;
			statue = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //exhibition = ClassCacheManager.New<Exhibition>();
				exhibition = new Exhibition();
                exhibition.Read(buffer, ref offset);
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //statue = ClassCacheManager.New<Statue>();
				statue = new Statue();
                statue.Read(buffer, ref offset);
                value = XBuffer.ReadInt(buffer, ref offset);

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
					if(exhibition == null)
						//exhibition = ClassCacheManager.New<Exhibition>();
						exhibition = new Exhibition();
					exhibition.WriteWithType(buffer, ref offset);
					if(statue == null)
						//statue = ClassCacheManager.New<Statue>();
						statue = new Statue();
					statue.WriteWithType(buffer, ref offset);
					XBuffer.WriteInt(value,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //更换铜像
    public class ResExchangeStatue : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111105;
		public int exhibitionId; // 展馆ID
		public int exhibitionAtk; // 展馆加成的攻击
		public int exhibitionDef; // 展馆铜像加成的防御
		public int exhibitionHp; // 展馆铜像加成的生命
		public int petId; // 宠物ID
		public int currentStatueId; // 当前使用的铜像（petId*100+材质（1-6）*10+品质（0-3））

    	//鏋勯�犲嚱鏁�
    	public ResExchangeStatue()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			exhibitionId = 0;
            
			exhibitionAtk = 0;
            
			exhibitionDef = 0;
            
			exhibitionHp = 0;
            
			petId = 0;
            
			currentStatueId = 0;
            
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
                exhibitionId = XBuffer.ReadInt(buffer, ref offset);
                exhibitionAtk = XBuffer.ReadInt(buffer, ref offset);
                exhibitionDef = XBuffer.ReadInt(buffer, ref offset);
                exhibitionHp = XBuffer.ReadInt(buffer, ref offset);
                petId = XBuffer.ReadInt(buffer, ref offset);
                currentStatueId = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(exhibitionId,buffer, ref offset);
					XBuffer.WriteInt(exhibitionAtk,buffer, ref offset);
					XBuffer.WriteInt(exhibitionDef,buffer, ref offset);
					XBuffer.WriteInt(exhibitionHp,buffer, ref offset);
					XBuffer.WriteInt(petId,buffer, ref offset);
					XBuffer.WriteInt(currentStatueId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //排行榜
    public class ResStatueRankList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111105;
        public List<RankItem> rankList{get;protected set;} //排行榜信息

    	//鏋勯�犲嚱鏁�
    	public ResStatueRankList()
    	{
            rankList = new List<RankItem>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            rankList.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = rankList.Count; a < b; ++a)
            {
				rankList[a] = null;
                //var _value_ = rankList[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            rankList.Clear();
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
                    RankItem _value_ = null;
                    //_value_ = ClassCacheManager.New<RankItem>();
					_value_ = new RankItem();
                    _value_.Read(buffer, ref offset);
                    rankList.Add(_value_);
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

                XBuffer.WriteShort((short)rankList.Count, buffer, ref offset);
                for(int a = 0; a < rankList.Count; ++a)
                {
					if(rankList[a] == null)
						UnityEngine.Debug.LogError("rankList has nil item, idx == " + a);
					else
						rankList[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //名人堂信息
    public class ResHofInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111106;
		public int totalPriority; // 总先手值
        public List<HofItem> hofItems{get;protected set;} //名人堂信息

    	//鏋勯�犲嚱鏁�
    	public ResHofInfo()
    	{
            hofItems = new List<HofItem>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			totalPriority = 0;
            
            hofItems.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = hofItems.Count; a < b; ++a)
            {
				hofItems[a] = null;
                //var _value_ = hofItems[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            hofItems.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                totalPriority = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    HofItem _value_ = null;
                    //_value_ = ClassCacheManager.New<HofItem>();
					_value_ = new HofItem();
                    _value_.Read(buffer, ref offset);
                    hofItems.Add(_value_);
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
					XBuffer.WriteInt(totalPriority,buffer, ref offset);

                XBuffer.WriteShort((short)hofItems.Count, buffer, ref offset);
                for(int a = 0; a < hofItems.Count; ++a)
                {
					if(hofItems[a] == null)
						UnityEngine.Debug.LogError("hofItems has nil item, idx == " + a);
					else
						hofItems[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //名人堂增加经验
    public class ResHofAddExp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111107;
		public HofItem hofItem; // 名人堂信息

    	//鏋勯�犲嚱鏁�
    	public ResHofAddExp()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//hofItem = ClassCacheManager.New<HofItem>();
			hofItem = new HofItem();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			hofItem = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //hofItem = ClassCacheManager.New<HofItem>();
				hofItem = new HofItem();
                hofItem.Read(buffer, ref offset);

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
					if(hofItem == null)
						//hofItem = ClassCacheManager.New<HofItem>();
						hofItem = new HofItem();
					hofItem.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //名人堂升品
    public class ResHofColorUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111108;
		public HofItem hofItem; // 名人堂信息
        public List<ItemInfo> itemInfos{get;protected set;} //奖励列表

    	//鏋勯�犲嚱鏁�
    	public ResHofColorUp()
    	{
            itemInfos = new List<ItemInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//hofItem = ClassCacheManager.New<HofItem>();
			hofItem = new HofItem();
            itemInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			hofItem = null;

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
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //hofItem = ClassCacheManager.New<HofItem>();
				hofItem = new HofItem();
                hofItem.Read(buffer, ref offset);

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
					if(hofItem == null)
						//hofItem = ClassCacheManager.New<HofItem>();
						hofItem = new HofItem();
					hofItem.WriteWithType(buffer, ref offset);

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
    //神器信息
    public class ResArtifactInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111109;
		public int artifactId; // 将要开放神器ID
        public List<Artifact> artifacts{get;protected set;} //已开神器列表
        public List<int> conditions{get;protected set;} //开启条件完成情况（-1代表已完成）

    	//鏋勯�犲嚱鏁�
    	public ResArtifactInfo()
    	{
            artifacts = new List<Artifact>();
            conditions = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			artifactId = 0;
            
            artifacts.Clear();
            conditions.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = artifacts.Count; a < b; ++a)
            {
				artifacts[a] = null;
                //var _value_ = artifacts[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            artifacts.Clear();
            conditions.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                artifactId = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    Artifact _value_ = null;
                    //_value_ = ClassCacheManager.New<Artifact>();
					_value_ = new Artifact();
                    _value_.Read(buffer, ref offset);
                    artifacts.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
            		var _value_ = XBuffer.ReadInt(buffer, ref offset);
            		conditions.Add(_value_);
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
					XBuffer.WriteInt(artifactId,buffer, ref offset);

                XBuffer.WriteShort((short)artifacts.Count, buffer, ref offset);
                for(int a = 0; a < artifacts.Count; ++a)
                {
					if(artifacts[a] == null)
						UnityEngine.Debug.LogError("artifacts has nil item, idx == " + a);
					else
						artifacts[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)conditions.Count, buffer, ref offset);
                for(int a = 0; a < conditions.Count; ++a)
                {
        			XBuffer.WriteInt(conditions[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //神器训练
    public class ResArtifactTrain : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111110;
		public int artifactId; // 神器ID
        public List<ArtifactRandomAttr> attrs{get;protected set;} //属性

    	//鏋勯�犲嚱鏁�
    	public ResArtifactTrain()
    	{
            attrs = new List<ArtifactRandomAttr>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			artifactId = 0;
            
            attrs.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = attrs.Count; a < b; ++a)
            {
				attrs[a] = null;
                //var _value_ = attrs[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            attrs.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                artifactId = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    ArtifactRandomAttr _value_ = null;
                    //_value_ = ClassCacheManager.New<ArtifactRandomAttr>();
					_value_ = new ArtifactRandomAttr();
                    _value_.Read(buffer, ref offset);
                    attrs.Add(_value_);
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
					XBuffer.WriteInt(artifactId,buffer, ref offset);

                XBuffer.WriteShort((short)attrs.Count, buffer, ref offset);
                for(int a = 0; a < attrs.Count; ++a)
                {
					if(attrs[a] == null)
						UnityEngine.Debug.LogError("attrs has nil item, idx == " + a);
					else
						attrs[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //神器训练保存
    public class ResArtifactTrainSave : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111111;
		public Artifact artifact; // 神器信息

    	//鏋勯�犲嚱鏁�
    	public ResArtifactTrainSave()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//artifact = ClassCacheManager.New<Artifact>();
			artifact = new Artifact();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			artifact = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //artifact = ClassCacheManager.New<Artifact>();
				artifact = new Artifact();
                artifact.Read(buffer, ref offset);

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
					if(artifact == null)
						//artifact = ClassCacheManager.New<Artifact>();
						artifact = new Artifact();
					artifact.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //神器解锁
    public class ResArtifactUnlock : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 111112;
		public Artifact artifact; // 解锁的神器
		public int artifactId; // 将要开放神器ID
        public List<int> conditions{get;protected set;} //开启条件完成情况（1：完成，0:不满足）

    	//鏋勯�犲嚱鏁�
    	public ResArtifactUnlock()
    	{
            conditions = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//artifact = ClassCacheManager.New<Artifact>();
			artifact = new Artifact();
			artifactId = 0;
            
            conditions.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			artifact = null;

            conditions.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //artifact = ClassCacheManager.New<Artifact>();
				artifact = new Artifact();
                artifact.Read(buffer, ref offset);
                artifactId = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
            		var _value_ = XBuffer.ReadInt(buffer, ref offset);
            		conditions.Add(_value_);
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
					if(artifact == null)
						//artifact = ClassCacheManager.New<Artifact>();
						artifact = new Artifact();
					artifact.WriteWithType(buffer, ref offset);
					XBuffer.WriteInt(artifactId,buffer, ref offset);

                XBuffer.WriteShort((short)conditions.Count, buffer, ref offset);
                for(int a = 0; a < conditions.Count; ++a)
                {
        			XBuffer.WriteInt(conditions[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}