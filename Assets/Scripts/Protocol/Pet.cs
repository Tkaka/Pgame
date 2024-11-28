//Auto generated, do not edit it
//宠物消息

using System;
using System.IO;
using System.Collections.Generic;

namespace Message.Pet
{
    public enum TypeEnum
    {
        PetEquip = 11,
        FormationInfo = 12,
        EquipedPetInfo = 13,
        PetExp = 14,
        ConditionProperty = 15,
        PetInfo = 1,
        PetBaseInfo = 2,
        PetSkillInfo = 3,
        SkillInfo = 4,
        Property = 5,
        PetFightInfo = 6,
        PetSoulInfo = 7,
        GridInfo = 8,
        SoulInfo = 9,
        PetEquipInfo = 10,
    }

    //宠物信息
    public class PetInfo : BaseMsgStruct
    {
		public int petId; // 宠物Id
        
		public int priority; // 先手值
        
		public PetBaseInfo basInfo; // 宠物基础信息
        
		public PetSkillInfo skillInfo; // 宠物技能信息
        
		public PetFightInfo fightInfo; // 宠物战斗信息
        
		public PetSoulInfo soulInfo; // 宠物战魂信息
        
		public PetEquipInfo equipInfo; // 宠物装备信息
        
        public List<Property> property{get; protected set;} //额外属性列表

        //鏋勯�犲嚱鏁�
        public PetInfo() : base()
        {
            property = new List<Property>(); //额外属性列表
			
			petId = 0;
            
			priority = 0;
            
			//basInfo = ClassCacheManager.New<PetBaseInfo>();
			basInfo = new PetBaseInfo();
			//skillInfo = ClassCacheManager.New<PetSkillInfo>();
			skillInfo = new PetSkillInfo();
			//fightInfo = ClassCacheManager.New<PetFightInfo>();
			fightInfo = new PetFightInfo();
			//soulInfo = ClassCacheManager.New<PetSoulInfo>();
			soulInfo = new PetSoulInfo();
			//equipInfo = ClassCacheManager.New<PetEquipInfo>();
			equipInfo = new PetEquipInfo();

            property.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			priority = 0;
            
			//basInfo = ClassCacheManager.New<PetBaseInfo>();
			basInfo = new PetBaseInfo();
			//skillInfo = ClassCacheManager.New<PetSkillInfo>();
			skillInfo = new PetSkillInfo();
			//fightInfo = ClassCacheManager.New<PetFightInfo>();
			fightInfo = new PetFightInfo();
			//soulInfo = ClassCacheManager.New<PetSoulInfo>();
			soulInfo = new PetSoulInfo();
			//equipInfo = ClassCacheManager.New<PetEquipInfo>();
			equipInfo = new PetEquipInfo();

            property.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			basInfo = null;
			skillInfo = null;
			fightInfo = null;
			soulInfo = null;
			equipInfo = null;

            for (int a = 0,b = property.Count; a < b; ++a)
            {
                //var _value_ = property[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				property[a] = null;
            }
            property.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                petId = XBuffer.ReadInt(buffer, ref offset);
                priority = XBuffer.ReadInt(buffer, ref offset);
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //basInfo = ClassCacheManager.New<PetBaseInfo>();
				basInfo = new PetBaseInfo();
                basInfo.Read(buffer, ref offset);
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //skillInfo = ClassCacheManager.New<PetSkillInfo>();
				skillInfo = new PetSkillInfo();
                skillInfo.Read(buffer, ref offset);
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //fightInfo = ClassCacheManager.New<PetFightInfo>();
				fightInfo = new PetFightInfo();
                fightInfo.Read(buffer, ref offset);
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //soulInfo = ClassCacheManager.New<PetSoulInfo>();
				soulInfo = new PetSoulInfo();
                soulInfo.Read(buffer, ref offset);
                _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //equipInfo = ClassCacheManager.New<PetEquipInfo>();
				equipInfo = new PetEquipInfo();
                equipInfo.Read(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    Property _value_ = null;
                    //_value_ = ClassCacheManager.New<Property>();
					_value_ = new Property();
                    _value_.Read(buffer, ref offset);
                    property.Add(_value_);
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
                XBuffer.WriteInt(priority, buffer, ref offset);
                if(basInfo==null)
                    //basInfo = ClassCacheManager.New<PetBaseInfo>();
					basInfo = new PetBaseInfo();
                basInfo.WriteWithType(buffer, ref offset);
                if(skillInfo==null)
                    //skillInfo = ClassCacheManager.New<PetSkillInfo>();
					skillInfo = new PetSkillInfo();
                skillInfo.WriteWithType(buffer, ref offset);
                if(fightInfo==null)
                    //fightInfo = ClassCacheManager.New<PetFightInfo>();
					fightInfo = new PetFightInfo();
                fightInfo.WriteWithType(buffer, ref offset);
                if(soulInfo==null)
                    //soulInfo = ClassCacheManager.New<PetSoulInfo>();
					soulInfo = new PetSoulInfo();
                soulInfo.WriteWithType(buffer, ref offset);
                if(equipInfo==null)
                    //equipInfo = ClassCacheManager.New<PetEquipInfo>();
					equipInfo = new PetEquipInfo();
                equipInfo.WriteWithType(buffer, ref offset);

                XBuffer.WriteShort((short)property.Count,buffer, ref offset);
                for (int a = 0; a < property.Count; ++a)
                {
					if(property[a] == null)
						UnityEngine.Debug.LogError("property has nil item, idx == " + a);
					else
						property[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //宠物基础信息
    public class PetBaseInfo : BaseMsgStruct
    {
		public int level; // 等级
        
		public long totalExp; // 总经验
        
		public int expRemain; // 剩余经验
        
		public int star; // 星级
        
		public int color; // 品阶
        

        //鏋勯�犲嚱鏁�
        public PetBaseInfo() : base()
        {
			
			level = 0;
            
			totalExp = 0L;
			expRemain = 0;
            
			star = 0;
            
			color = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			level = 0;
            
			totalExp = 0L;
			expRemain = 0;
            
			star = 0;
            
			color = 0;
            

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
                totalExp = XBuffer.ReadLong(buffer, ref offset);
                expRemain = XBuffer.ReadInt(buffer, ref offset);
                star = XBuffer.ReadInt(buffer, ref offset);
                color = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteLong(totalExp, buffer, ref offset);
                XBuffer.WriteInt(expRemain, buffer, ref offset);
                XBuffer.WriteInt(star, buffer, ref offset);
                XBuffer.WriteInt(color, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //宠物技能列表
    public class PetSkillInfo : BaseMsgStruct
    {
        public List<SkillInfo> skillInfos{get; protected set;} //技能列表

        //鏋勯�犲嚱鏁�
        public PetSkillInfo() : base()
        {
            skillInfos = new List<SkillInfo>(); //技能列表
			

            skillInfos.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);

            skillInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = skillInfos.Count; a < b; ++a)
            {
                //var _value_ = skillInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				skillInfos[a] = null;
            }
            skillInfos.Clear();
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
                    SkillInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<SkillInfo>();
					_value_ = new SkillInfo();
                    _value_.Read(buffer, ref offset);
                    skillInfos.Add(_value_);
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

                XBuffer.WriteShort((short)skillInfos.Count,buffer, ref offset);
                for (int a = 0; a < skillInfos.Count; ++a)
                {
					if(skillInfos[a] == null)
						UnityEngine.Debug.LogError("skillInfos has nil item, idx == " + a);
					else
						skillInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //宠物技能
    public class SkillInfo : BaseMsgStruct
    {
		public int level; // 技能等级
        
		public int id; // 技能id
        
		public int percent; // 百分比
        
		public int fixedValue; // 固定值
        

        //鏋勯�犲嚱鏁�
        public SkillInfo() : base()
        {
			
			level = 0;
            
			id = 0;
            
			percent = 0;
            
			fixedValue = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			level = 0;
            
			id = 0;
            
			percent = 0;
            
			fixedValue = 0;
            

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
                id = XBuffer.ReadInt(buffer, ref offset);
                percent = XBuffer.ReadInt(buffer, ref offset);
                fixedValue = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteInt(id, buffer, ref offset);
                XBuffer.WriteInt(percent, buffer, ref offset);
                XBuffer.WriteInt(fixedValue, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //属性结构
    public class Property : BaseMsgStruct
    {
		public int id; // 属性id
        
		public int flag; // 类型
        
		public float value; // 值
        

        //鏋勯�犲嚱鏁�
        public Property() : base()
        {
			
			id = 0;
            
			flag = 0;
            
			value = 0f;

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			flag = 0;
            
			value = 0f;

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
                flag = XBuffer.ReadInt(buffer, ref offset);
                value = XBuffer.ReadFloat(buffer, ref offset);

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
                XBuffer.WriteInt(flag, buffer, ref offset);
                XBuffer.WriteFloat(value, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //宠物战斗信息
    public class PetFightInfo : BaseMsgStruct
    {
		public int atk; // 攻击
        
		public int def; // 防御
        
		public int hp; // 血量
        
		public int critical; // 暴击
        
		public int antiCritical; // 抗暴
        
		public int criticalDamage; // 暴伤
        
		public int block; // 格挡
        
		public int antiBlock; // 破击
        
		public int blockValue; // 格挡强度
        
		public int damageDeppen; // 伤害加深
        
		public int damageAvoid; // 伤害减免
        
		public int fightPower; // 战力
        

        //鏋勯�犲嚱鏁�
        public PetFightInfo() : base()
        {
			
			atk = 0;
            
			def = 0;
            
			hp = 0;
            
			critical = 0;
            
			antiCritical = 0;
            
			criticalDamage = 0;
            
			block = 0;
            
			antiBlock = 0;
            
			blockValue = 0;
            
			damageDeppen = 0;
            
			damageAvoid = 0;
            
			fightPower = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			atk = 0;
            
			def = 0;
            
			hp = 0;
            
			critical = 0;
            
			antiCritical = 0;
            
			criticalDamage = 0;
            
			block = 0;
            
			antiBlock = 0;
            
			blockValue = 0;
            
			damageDeppen = 0;
            
			damageAvoid = 0;
            
			fightPower = 0;
            

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
                atk = XBuffer.ReadInt(buffer, ref offset);
                def = XBuffer.ReadInt(buffer, ref offset);
                hp = XBuffer.ReadInt(buffer, ref offset);
                critical = XBuffer.ReadInt(buffer, ref offset);
                antiCritical = XBuffer.ReadInt(buffer, ref offset);
                criticalDamage = XBuffer.ReadInt(buffer, ref offset);
                block = XBuffer.ReadInt(buffer, ref offset);
                antiBlock = XBuffer.ReadInt(buffer, ref offset);
                blockValue = XBuffer.ReadInt(buffer, ref offset);
                damageDeppen = XBuffer.ReadInt(buffer, ref offset);
                damageAvoid = XBuffer.ReadInt(buffer, ref offset);
                fightPower = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(atk, buffer, ref offset);
                XBuffer.WriteInt(def, buffer, ref offset);
                XBuffer.WriteInt(hp, buffer, ref offset);
                XBuffer.WriteInt(critical, buffer, ref offset);
                XBuffer.WriteInt(antiCritical, buffer, ref offset);
                XBuffer.WriteInt(criticalDamage, buffer, ref offset);
                XBuffer.WriteInt(block, buffer, ref offset);
                XBuffer.WriteInt(antiBlock, buffer, ref offset);
                XBuffer.WriteInt(blockValue, buffer, ref offset);
                XBuffer.WriteInt(damageDeppen, buffer, ref offset);
                XBuffer.WriteInt(damageAvoid, buffer, ref offset);
                XBuffer.WriteInt(fightPower, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //宠物战魂信息
    public class PetSoulInfo : BaseMsgStruct
    {
        public List<SoulInfo> souls{get; protected set;} //战魂信息

        //鏋勯�犲嚱鏁�
        public PetSoulInfo() : base()
        {
            souls = new List<SoulInfo>(); //战魂信息
			

            souls.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);

            souls.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = souls.Count; a < b; ++a)
            {
                //var _value_ = souls[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				souls[a] = null;
            }
            souls.Clear();
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
                    SoulInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<SoulInfo>();
					_value_ = new SoulInfo();
                    _value_.Read(buffer, ref offset);
                    souls.Add(_value_);
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

                XBuffer.WriteShort((short)souls.Count,buffer, ref offset);
                for (int a = 0; a < souls.Count; ++a)
                {
					if(souls[a] == null)
						UnityEngine.Debug.LogError("souls has nil item, idx == " + a);
					else
						souls[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //背包格子
    public class GridInfo : BaseMsgStruct
    {
		public int gridId; // 格子id
        
		public int num; // 消耗数量
        

        //鏋勯�犲嚱鏁�
        public GridInfo() : base()
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
            XBuffer.WriteByte(8, buffer, ref offset);
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
    //战魂
    public class SoulInfo : BaseMsgStruct
    {
		public int index; // 战魂index
        
		public int level; // 战魂等级
        
		public int remainExp; // 战魂经验
        

        //鏋勯�犲嚱鏁�
        public SoulInfo() : base()
        {
			
			index = 0;
            
			level = 0;
            
			remainExp = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			index = 0;
            
			level = 0;
            
			remainExp = 0;
            

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
                index = XBuffer.ReadInt(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                remainExp = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(9, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(index, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteInt(remainExp, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //宠物装备信息
    public class PetEquipInfo : BaseMsgStruct
    {
        public List<PetEquip> equips{get; protected set;} //装备

        //鏋勯�犲嚱鏁�
        public PetEquipInfo() : base()
        {
            equips = new List<PetEquip>(); //装备
			

            equips.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);

            equips.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = equips.Count; a < b; ++a)
            {
                //var _value_ = equips[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				equips[a] = null;
            }
            equips.Clear();
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
                    PetEquip _value_ = null;
                    //_value_ = ClassCacheManager.New<PetEquip>();
					_value_ = new PetEquip();
                    _value_.Read(buffer, ref offset);
                    equips.Add(_value_);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(10, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);

                XBuffer.WriteShort((short)equips.Count,buffer, ref offset);
                for (int a = 0; a < equips.Count; ++a)
                {
					if(equips[a] == null)
						UnityEngine.Debug.LogError("equips has nil item, idx == " + a);
					else
						equips[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //宠物装备信息
    public class PetEquip : BaseMsgStruct
    {
		public int id; // 部位index
        
		public int equipTypeId; // 装备类型id
        
		public int level; // 等级
        
		public int star; // 星级
        
		public int color; // 品阶
        
		public int __exp; // 当前经验
		private byte _exp = 0; // 当前经验 tag
		
		public bool hasExp()
		{
			return this._exp == 1;
		}
		
		public int exp
		{
			set
			{
				_exp = 1;
				__exp = value;
			}
			
			get
			{
				return __exp;
			}
		}
        

        //鏋勯�犲嚱鏁�
        public PetEquip() : base()
        {
			
			id = 0;
            
			equipTypeId = 0;
            
			level = 0;
            
			star = 0;
            
			color = 0;
            
			_exp = 0;
			__exp = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			id = 0;
            
			equipTypeId = 0;
            
			level = 0;
            
			star = 0;
            
			color = 0;
            
			_exp = 0;
			__exp = 0;
            

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
                equipTypeId = XBuffer.ReadInt(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                star = XBuffer.ReadInt(buffer, ref offset);
                color = XBuffer.ReadInt(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					exp = XBuffer.ReadInt(buffer, ref offset);
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
            XBuffer.WriteByte(11, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(id, buffer, ref offset);
                XBuffer.WriteInt(equipTypeId, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteInt(star, buffer, ref offset);
                XBuffer.WriteInt(color, buffer, ref offset);
				XBuffer.WriteByte(_exp, buffer, ref offset);
				if (_exp == 1)
				{
					XBuffer.WriteInt(exp, buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //阵型信息
    public class FormationInfo : BaseMsgStruct
    {
		public int type; // 阵型类型
        
        public List<int> formation{get; protected set;} //阵型

        //鏋勯�犲嚱鏁�
        public FormationInfo() : base()
        {
            formation = new List<int>(); //阵型
			
			type = 0;
            

            formation.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			type = 0;
            

            formation.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            formation.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                type = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    var _value_ = XBuffer.ReadInt(buffer, ref offset);
                    formation.Add(_value_);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(12, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(type, buffer, ref offset);

                XBuffer.WriteShort((short)formation.Count,buffer, ref offset);
                for (int a = 0; a < formation.Count; ++a)
                {
                    XBuffer.WriteInt(formation[a], buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //上阵宠物信息
    public class EquipedPetInfo : BaseMsgStruct
    {
		public int level; // 等级
        
		public int star; // 星级
        
		public int color; // 品阶
        
		public int id; // 宠物id
        

        //鏋勯�犲嚱鏁�
        public EquipedPetInfo() : base()
        {
			
			level = 0;
            
			star = 0;
            
			color = 0;
            
			id = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			level = 0;
            
			star = 0;
            
			color = 0;
            
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
                TypeEnum _real_type_;
                level = XBuffer.ReadInt(buffer, ref offset);
                star = XBuffer.ReadInt(buffer, ref offset);
                color = XBuffer.ReadInt(buffer, ref offset);
                id = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(13, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteInt(star, buffer, ref offset);
                XBuffer.WriteInt(color, buffer, ref offset);
                XBuffer.WriteInt(id, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //宠物经验
    public class PetExp : BaseMsgStruct
    {
		public int petId; // 宠物id
        
		public int level; // 等级
        
		public long totalExp; // 总经验
        
		public int expRemain; // 剩余经验
        

        //鏋勯�犲嚱鏁�
        public PetExp() : base()
        {
			
			petId = 0;
            
			level = 0;
            
			totalExp = 0L;
			expRemain = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			level = 0;
            
			totalExp = 0L;
			expRemain = 0;
            

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
                petId = XBuffer.ReadInt(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                totalExp = XBuffer.ReadLong(buffer, ref offset);
                expRemain = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(14, buffer, ref offset);
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
                XBuffer.WriteLong(totalExp, buffer, ref offset);
                XBuffer.WriteInt(expRemain, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //条件属性结构
    public class ConditionProperty : BaseMsgStruct
    {
		public int petType; // 宠物类型
        
		public int pos; // 站位
        
        public List<Property> property{get; protected set;} //属性列表

        //鏋勯�犲嚱鏁�
        public ConditionProperty() : base()
        {
            property = new List<Property>(); //属性列表
			
			petType = 0;
            
			pos = 0;
            

            property.Clear();
        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petType = 0;
            
			pos = 0;
            

            property.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = property.Count; a < b; ++a)
            {
                //var _value_ = property[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
				property[a] = null;
            }
            property.Clear();
        }
		
        //璇诲彇鏁版嵁
        public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum _real_type_;
                petType = XBuffer.ReadInt(buffer, ref offset);
                pos = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
            	_count_ = XBuffer.ReadShort(buffer, ref offset);
                for(int a = 0; a < _count_; ++a)
                {
                    _real_type_ = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    Property _value_ = null;
                    //_value_ = ClassCacheManager.New<Property>();
					_value_ = new Property();
                    _value_.Read(buffer, ref offset);
                    property.Add(_value_);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void WriteWithType(byte[] buffer, ref int offset)
        {
            XBuffer.WriteByte(15, buffer, ref offset);
            Write(buffer, ref offset);
        }

        //鍐欏叆鏁版嵁
        public override void Write(byte[] buffer, ref int offset)
        {
            try
            {
                base.Write(buffer, ref offset);
                XBuffer.WriteInt(petType, buffer, ref offset);
                XBuffer.WriteInt(pos, buffer, ref offset);

                XBuffer.WriteShort((short)property.Count,buffer, ref offset);
                for (int a = 0; a < property.Count; ++a)
                {
					if(property[a] == null)
						UnityEngine.Debug.LogError("property has nil item, idx == " + a);
					else
						property[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //宠物升级请求
    public class ReqPetAddExp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104201;
		public int petId; // 宠物id
		public GridInfo gridInfo; // 背包格子信息

    	//鏋勯�犲嚱鏁�
    	public ReqPetAddExp()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			//gridInfo = ClassCacheManager.New<GridInfo>();
			gridInfo = new GridInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			gridInfo = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                petId = XBuffer.ReadInt(buffer, ref offset);
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //gridInfo = ClassCacheManager.New<GridInfo>();
				gridInfo = new GridInfo();
                gridInfo.Read(buffer, ref offset);

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
					if(gridInfo == null)
						//gridInfo = ClassCacheManager.New<GridInfo>();
						gridInfo = new GridInfo();
					gridInfo.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //更新宠物上阵信息
    public class ReqPetResetFormation : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104202;
		public FormationInfo formationInfo; // 阵型信息

    	//鏋勯�犲嚱鏁�
    	public ReqPetResetFormation()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//formationInfo = ClassCacheManager.New<FormationInfo>();
			formationInfo = new FormationInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			formationInfo = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //formationInfo = ClassCacheManager.New<FormationInfo>();
				formationInfo = new FormationInfo();
                formationInfo.Read(buffer, ref offset);

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
					if(formationInfo == null)
						//formationInfo = ClassCacheManager.New<FormationInfo>();
						formationInfo = new FormationInfo();
					formationInfo.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //宠物升品
    public class ReqPetColorUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104203;
		public int petId; // 宠物id
        public List<GridInfo> gridInfo{get;protected set;} //升品材料

    	//鏋勯�犲嚱鏁�
    	public ReqPetColorUp()
    	{
            gridInfo = new List<GridInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
            gridInfo.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = gridInfo.Count; a < b; ++a)
            {
				gridInfo[a] = null;
                //var _value_ = gridInfo[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            gridInfo.Clear();
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
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    GridInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<GridInfo>();
					_value_ = new GridInfo();
                    _value_.Read(buffer, ref offset);
                    gridInfo.Add(_value_);
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

                XBuffer.WriteShort((short)gridInfo.Count, buffer, ref offset);
                for(int a = 0; a < gridInfo.Count; ++a)
                {
					if(gridInfo[a] == null)
						UnityEngine.Debug.LogError("gridInfo has nil item, idx == " + a);
					else
						gridInfo[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //宠物技能升级
    public class ReqPetSkillLevelUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104204;
		public int petId; // 宠物id
		public int idx; // index

    	//鏋勯�犲嚱鏁�
    	public ReqPetSkillLevelUp()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			idx = 0;
            
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
                idx = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(idx,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //宠物升星
    public class ReqPetStarUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104205;
		public int petId; // 宠物id
		public GridInfo gridInfo; // 碎片所在格子信息

    	//鏋勯�犲嚱鏁�
    	public ReqPetStarUp()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			//gridInfo = ClassCacheManager.New<GridInfo>();
			gridInfo = new GridInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			gridInfo = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                petId = XBuffer.ReadInt(buffer, ref offset);
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //gridInfo = ClassCacheManager.New<GridInfo>();
				gridInfo = new GridInfo();
                gridInfo.Read(buffer, ref offset);

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
					if(gridInfo == null)
						//gridInfo = ClassCacheManager.New<GridInfo>();
						gridInfo = new GridInfo();
					gridInfo.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //战魂强化-消耗钻石
    public class ReqPetSoulUpUseDiamond : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104206;
		public int petId; // 宠物id
		public int idx; // 战魂index

    	//鏋勯�犲嚱鏁�
    	public ReqPetSoulUpUseDiamond()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			idx = 0;
            
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
                idx = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(idx,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //战魂强化-消耗道具
    public class ReqPetSoulUpUseItem : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104207;
		public int petId; // 宠物id
		public int idx; // 战魂index
        public List<GridInfo> grids{get;protected set;} //消耗道具信息

    	//鏋勯�犲嚱鏁�
    	public ReqPetSoulUpUseItem()
    	{
            grids = new List<GridInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			idx = 0;
            
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
                petId = XBuffer.ReadInt(buffer, ref offset);
                idx = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(petId,buffer, ref offset);
					XBuffer.WriteInt(idx,buffer, ref offset);

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
    //装备升级
    public class ReqPetEquipLvUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104208;
		public int petId; // 宠物Id
		public int idx; // 装备部位index
		public int targetLevel; // 目标等级

    	//鏋勯�犲嚱鏁�
    	public ReqPetEquipLvUp()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			idx = 0;
            
			targetLevel = 0;
            
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
                idx = XBuffer.ReadInt(buffer, ref offset);
                targetLevel = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(idx,buffer, ref offset);
					XBuffer.WriteInt(targetLevel,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //装备升品
    public class ReqPetEquipColorUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104209;
		public int petId; // 宠物Id
		public int idx; // 装备部位index
        public List<GridInfo> grids{get;protected set;} //材料列表（格子id和数量）

    	//鏋勯�犲嚱鏁�
    	public ReqPetEquipColorUp()
    	{
            grids = new List<GridInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			idx = 0;
            
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
                petId = XBuffer.ReadInt(buffer, ref offset);
                idx = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(petId,buffer, ref offset);
					XBuffer.WriteInt(idx,buffer, ref offset);

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
    //装备快速升级
    public class ReqPetEquipFastLvUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104210;
		public int petId; // 宠物Id
		public int idx; // 装备部位index
		public int targetLevel; // 目标等级
        public List<GridInfo> grids{get;protected set;} //材料列表（格子id和数量）

    	//鏋勯�犲嚱鏁�
    	public ReqPetEquipFastLvUp()
    	{
            grids = new List<GridInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			idx = 0;
            
			targetLevel = 0;
            
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
                petId = XBuffer.ReadInt(buffer, ref offset);
                idx = XBuffer.ReadInt(buffer, ref offset);
                targetLevel = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(petId,buffer, ref offset);
					XBuffer.WriteInt(idx,buffer, ref offset);
					XBuffer.WriteInt(targetLevel,buffer, ref offset);

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
    //徽章或秘籍升级
    public class ReqPetSpecialEquipLvUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104211;
		public int petId; // 宠物Id
		public int idx; // 装备部位index
        public List<GridInfo> grids{get;protected set;} //材料列表（格子id和数量）

    	//鏋勯�犲嚱鏁�
    	public ReqPetSpecialEquipLvUp()
    	{
            grids = new List<GridInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			idx = 0;
            
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
                petId = XBuffer.ReadInt(buffer, ref offset);
                idx = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(petId,buffer, ref offset);
					XBuffer.WriteInt(idx,buffer, ref offset);

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
    //徽章或秘籍升品
    public class ReqPetSpecialEquipColorUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104212;
		public int petId; // 宠物Id
		public int idx; // 装备部位index

    	//鏋勯�犲嚱鏁�
    	public ReqPetSpecialEquipColorUp()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			idx = 0;
            
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
                idx = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(idx,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //装备觉醒
    public class ReqPetEquipAwaken : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104213;
		public int petId; // 宠物Id
		public int idx; // 装备部位index
        public List<GridInfo> grids{get;protected set;} //材料列表（格子id和数量）

    	//鏋勯�犲嚱鏁�
    	public ReqPetEquipAwaken()
    	{
            grids = new List<GridInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			idx = 0;
            
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
                petId = XBuffer.ReadInt(buffer, ref offset);
                idx = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(petId,buffer, ref offset);
					XBuffer.WriteInt(idx,buffer, ref offset);

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
    //徽章或秘籍快速升级
    public class ReqPetSpecialEquipFastLvUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104214;
		public int petId; // 宠物Id
		public int idx; // 装备部位index
		public int targetLevel; // 目标等级，到该时等级不升品
        public List<GridInfo> grids{get;protected set;} //材料列表（格子id和数量）

    	//鏋勯�犲嚱鏁�
    	public ReqPetSpecialEquipFastLvUp()
    	{
            grids = new List<GridInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			idx = 0;
            
			targetLevel = 0;
            
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
                petId = XBuffer.ReadInt(buffer, ref offset);
                idx = XBuffer.ReadInt(buffer, ref offset);
                targetLevel = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(petId,buffer, ref offset);
					XBuffer.WriteInt(idx,buffer, ref offset);
					XBuffer.WriteInt(targetLevel,buffer, ref offset);

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
    //装备降星
    public class ReqPetEquipStarDown : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104215;
		public int petId; // 宠物Id
		public int idx; // 装备部位index

    	//鏋勯�犲嚱鏁�
    	public ReqPetEquipStarDown()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			idx = 0;
            
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
                idx = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(idx,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //宠物合成
    public class ReqPetCompose : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104216;
		public int petId; // 宠物Id

    	//鏋勯�犲嚱鏁�
    	public ReqPetCompose()
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
    //万能碎片转换
    public class ReqFragmentTransform : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104217;
		public int petId; // 宠物Id
		public int num; // 数量

    	//鏋勯�犲嚱鏁�
    	public ReqFragmentTransform()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
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
                petId = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(petId,buffer, ref offset);
					XBuffer.WriteInt(num,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //所有宠物信息
    public class ResPetInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104101;
        public List<PetInfo> petsInfo{get;protected set;} //宠物列表
        public List<FormationInfo> formationInfos{get;protected set;} //阵型列表

    	//鏋勯�犲嚱鏁�
    	public ResPetInfo()
    	{
            petsInfo = new List<PetInfo>();
            formationInfos = new List<FormationInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            petsInfo.Clear();
            formationInfos.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = petsInfo.Count; a < b; ++a)
            {
				petsInfo[a] = null;
                //var _value_ = petsInfo[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            petsInfo.Clear();
            for (int a = 0,b = formationInfos.Count; a < b; ++a)
            {
				formationInfos[a] = null;
                //var _value_ = formationInfos[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            formationInfos.Clear();
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
                    PetInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<PetInfo>();
					_value_ = new PetInfo();
                    _value_.Read(buffer, ref offset);
                    petsInfo.Add(_value_);
                }
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    FormationInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<FormationInfo>();
					_value_ = new FormationInfo();
                    _value_.Read(buffer, ref offset);
                    formationInfos.Add(_value_);
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

                XBuffer.WriteShort((short)petsInfo.Count, buffer, ref offset);
                for(int a = 0; a < petsInfo.Count; ++a)
                {
					if(petsInfo[a] == null)
						UnityEngine.Debug.LogError("petsInfo has nil item, idx == " + a);
					else
						petsInfo[a].WriteWithType(buffer, ref offset);
                }
                XBuffer.WriteShort((short)formationInfos.Count, buffer, ref offset);
                for(int a = 0; a < formationInfos.Count; ++a)
                {
					if(formationInfos[a] == null)
						UnityEngine.Debug.LogError("formationInfos has nil item, idx == " + a);
					else
						formationInfos[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //单个宠物信息更新
    public class ResPetInfoUpdate : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104102;
		public PetInfo petInfo; // 宠物信息

    	//鏋勯�犲嚱鏁�
    	public ResPetInfoUpdate()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//petInfo = ClassCacheManager.New<PetInfo>();
			petInfo = new PetInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			petInfo = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //petInfo = ClassCacheManager.New<PetInfo>();
				petInfo = new PetInfo();
                petInfo.Read(buffer, ref offset);

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
					if(petInfo == null)
						//petInfo = ClassCacheManager.New<PetInfo>();
						petInfo = new PetInfo();
					petInfo.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //宠物阵型信息
    public class ResPetResetFormation : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104103;
		public FormationInfo formationInfo; // 上阵信息（必须为6个， 没有写0）

    	//鏋勯�犲嚱鏁�
    	public ResPetResetFormation()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//formationInfo = ClassCacheManager.New<FormationInfo>();
			formationInfo = new FormationInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			formationInfo = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //formationInfo = ClassCacheManager.New<FormationInfo>();
				formationInfo = new FormationInfo();
                formationInfo.Read(buffer, ref offset);

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
					if(formationInfo == null)
						//formationInfo = ClassCacheManager.New<FormationInfo>();
						formationInfo = new FormationInfo();
					formationInfo.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //装备觉醒结果
    public class ResPetEquipAwaken : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104104;
		public int result; // 结果（1表示成功））

    	//鏋勯�犲嚱鏁�
    	public ResPetEquipAwaken()
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
    //羁绊激活
    public class ResActivePetSolder : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104105;
		public bool isActive; // 是否激活 true激活 false失效
        public List<int> tableIds{get;protected set;} //激活表id

    	//鏋勯�犲嚱鏁�
    	public ResActivePetSolder()
    	{
            tableIds = new List<int>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			isActive = false;
            isActive = false;
            tableIds.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            tableIds.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
        		isActive = XBuffer.ReadBool(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
            		var _value_ = XBuffer.ReadInt(buffer, ref offset);
            		tableIds.Add(_value_);
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
					XBuffer.WriteBool(isActive,buffer, ref offset);

                XBuffer.WriteShort((short)tableIds.Count, buffer, ref offset);
                for(int a = 0; a < tableIds.Count; ++a)
                {
        			XBuffer.WriteInt(tableIds[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //宠物合成
    public class ResPetCompose : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104106;
		public PetInfo tableIds; // 合成后的宠物

    	//鏋勯�犲嚱鏁�
    	public ResPetCompose()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//tableIds = ClassCacheManager.New<PetInfo>();
			tableIds = new PetInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			tableIds = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //tableIds = ClassCacheManager.New<PetInfo>();
				tableIds = new PetInfo();
                tableIds.Read(buffer, ref offset);

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
					if(tableIds == null)
						//tableIds = ClassCacheManager.New<PetInfo>();
						tableIds = new PetInfo();
					tableIds.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //宠物碎片合成
    public class ResPetFragmentCompose : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104107;
		public PetInfo petInfo; // 合成的宠物信息

    	//鏋勯�犲嚱鏁�
    	public ResPetFragmentCompose()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//petInfo = ClassCacheManager.New<PetInfo>();
			petInfo = new PetInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			petInfo = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //petInfo = ClassCacheManager.New<PetInfo>();
				petInfo = new PetInfo();
                petInfo.Read(buffer, ref offset);

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
					if(petInfo == null)
						//petInfo = ClassCacheManager.New<PetInfo>();
						petInfo = new PetInfo();
					petInfo.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //羁绊替换技能
    public class ResPetSkillReplace : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104108;
		public int petId; // 宠物id
		public int oldSkill; // 老技能
		public int newSkill; // 新技能

    	//鏋勯�犲嚱鏁�
    	public ResPetSkillReplace()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			oldSkill = 0;
            
			newSkill = 0;
            
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
                oldSkill = XBuffer.ReadInt(buffer, ref offset);
                newSkill = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(oldSkill,buffer, ref offset);
					XBuffer.WriteInt(newSkill,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //技能升级
    public class ResPetSkillLevelUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104109;
		public int petId; // 宠物id
		public int index; // index
		public int skillId; // 技能id
		public int level; // 技能等级

    	//鏋勯�犲嚱鏁�
    	public ResPetSkillLevelUp()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			index = 0;
            
			skillId = 0;
            
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
                TypeEnum real_type;
                petId = XBuffer.ReadInt(buffer, ref offset);
                index = XBuffer.ReadInt(buffer, ref offset);
                skillId = XBuffer.ReadInt(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(index,buffer, ref offset);
					XBuffer.WriteInt(skillId,buffer, ref offset);
					XBuffer.WriteInt(level,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //经验改变
    public class ResPetAddExp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104110;
		public int petId; // 宠物id
		public int exp; // 改变后的值

    	//鏋勯�犲嚱鏁�
    	public ResPetAddExp()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
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
					XBuffer.WriteInt(exp,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //升级
    public class ResPetLevelUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104111;
		public int petId; // 宠物id
		public int level; // 等级
		public int exp; // 升级剩余经验

    	//鏋勯�犲嚱鏁�
    	public ResPetLevelUp()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
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
                TypeEnum real_type;
                petId = XBuffer.ReadInt(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(level,buffer, ref offset);
					XBuffer.WriteInt(exp,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //升品
    public class ResPetColorUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104112;
		public int petId; // 宠物id
		public int color; // 品阶
		public int priority; // 先手值

    	//鏋勯�犲嚱鏁�
    	public ResPetColorUp()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			color = 0;
            
			priority = 0;
            
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
                color = XBuffer.ReadInt(buffer, ref offset);
                priority = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(color,buffer, ref offset);
					XBuffer.WriteInt(priority,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //升星
    public class ResPetStarUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104113;
		public int petId; // 宠物id
		public int star; // 星级
		public int priority; // 先手值

    	//鏋勯�犲嚱鏁�
    	public ResPetStarUp()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			star = 0;
            
			priority = 0;
            
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
                star = XBuffer.ReadInt(buffer, ref offset);
                priority = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(star,buffer, ref offset);
					XBuffer.WriteInt(priority,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //战魂强化
    public class ResPetSoulUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104114;
		public int petId; // 宠物id
		public SoulInfo soul; // 战魂信息

    	//鏋勯�犲嚱鏁�
    	public ResPetSoulUp()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			//soul = ClassCacheManager.New<SoulInfo>();
			soul = new SoulInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			soul = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                petId = XBuffer.ReadInt(buffer, ref offset);
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //soul = ClassCacheManager.New<SoulInfo>();
				soul = new SoulInfo();
                soul.Read(buffer, ref offset);

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
					if(soul == null)
						//soul = ClassCacheManager.New<SoulInfo>();
						soul = new SoulInfo();
					soul.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //装备信息改变
    public class ResPetEquipInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104115;
		public int petId; // 宠物id
		public PetEquip equip; // 装备信息

    	//鏋勯�犲嚱鏁�
    	public ResPetEquipInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			//equip = ClassCacheManager.New<PetEquip>();
			equip = new PetEquip();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			equip = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                petId = XBuffer.ReadInt(buffer, ref offset);
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //equip = ClassCacheManager.New<PetEquip>();
				equip = new PetEquip();
                equip.Read(buffer, ref offset);

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
					if(equip == null)
						//equip = ClassCacheManager.New<PetEquip>();
						equip = new PetEquip();
					equip.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //宠物额外属性改变
    public class ResPetExtPropertyChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104116;
		public int petId; // 宠物id
        public List<Property> property{get;protected set;} //属性列表

    	//鏋勯�犲嚱鏁�
    	public ResPetExtPropertyChange()
    	{
            property = new List<Property>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
            property.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = property.Count; a < b; ++a)
            {
				property[a] = null;
                //var _value_ = property[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            property.Clear();
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
                    real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                    Property _value_ = null;
                    //_value_ = ClassCacheManager.New<Property>();
					_value_ = new Property();
                    _value_.Read(buffer, ref offset);
                    property.Add(_value_);
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

                XBuffer.WriteShort((short)property.Count, buffer, ref offset);
                for(int a = 0; a < property.Count; ++a)
                {
					if(property[a] == null)
						UnityEngine.Debug.LogError("property has nil item, idx == " + a);
					else
						property[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //额外系统条件属性改变
    public class ResExtConditionPropertyChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104117;
        public List<ConditionProperty> property{get;protected set;} //属性列表

    	//鏋勯�犲嚱鏁�
    	public ResExtConditionPropertyChange()
    	{
            property = new List<ConditionProperty>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            property.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = property.Count; a < b; ++a)
            {
				property[a] = null;
                //var _value_ = property[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            property.Clear();
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
                    ConditionProperty _value_ = null;
                    //_value_ = ClassCacheManager.New<ConditionProperty>();
					_value_ = new ConditionProperty();
                    _value_.Read(buffer, ref offset);
                    property.Add(_value_);
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

                XBuffer.WriteShort((short)property.Count, buffer, ref offset);
                for(int a = 0; a < property.Count; ++a)
                {
					if(property[a] == null)
						UnityEngine.Debug.LogError("property has nil item, idx == " + a);
					else
						property[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //宠物战力变化
    public class ResFightPowerChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 104118;
		public int petId; // 宠物id
		public long fightPower; // 战力

    	//鏋勯�犲嚱鏁�
    	public ResFightPowerChange()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			petId = 0;
            
			fightPower = 0L;
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
                fightPower = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(fightPower,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}