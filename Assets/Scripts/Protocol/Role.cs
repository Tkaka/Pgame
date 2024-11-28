//Auto generated, do not edit it
//角色消息

using System;
using System.IO;
using System.Collections.Generic;
using Message.Bag;

namespace Message.Role
{
    public enum TypeEnum
    {
        RoleInfo = 1,
    }

    //实体信息
    public class RoleInfo : BaseMsgStruct
    {
		public long roleId; // 实体Id
        
		public int profession; // 职业100:男 200：女
        
		public string roleName; // 角色名字
        
		public int level; // 角色等级
        
		public int vip; // vip等级
        
		public long gold; // 金币
        
		public int damond; // 钻石
        
		public int energy; // 体力
        
		public int awakenFragment; // 觉醒碎片
        
		public int honorCurrency; // 荣誉币
        
		public int trailCurrency; // 试炼币
        
		public int clubCurrency; // 社团币
        
		public long curExp; // 当前经验
        
		public long aoyiJinghua; // 奥义精华
        
		public long fightPower; // 战斗力
        
		public long guildId; // 公会ID
        
		public string guildName; // 公会ID
        
		public int talent; // 天赋点
        
		public int skillPointsBuyCount; // 当日购买技能点次数
        
		public int skillPoints; // 剩余技能点
        
		public int nextPointTime; // 获得下一个技能点的时间（秒）
        
		public int headIconId; // 头像框ID
        
		public int precedeValue; // 先手值
        
		public string token; // token
        
		public long nextEnergyTime; // 获得下一个体力的时间点（体力未满时）
        
		public long exitGuildTime; // 上次退帮时间
        
		public int nicknameModifyCount; // 昵称修改次数
        
		public int energyBuyCount; // 今日体力购买次数
        
		public int totalSignIn; // 总签到
        
		public int monthSignIn; // 本月累计签到
        
		public int signInAwardIndex; // 总签到礼包领取下标
        
		public int dailySignInFlag; // 0:未领取，1：单倍，2：双倍
        
		public int signInPetIndex; // 签到宠物下标
        

        //鏋勯�犲嚱鏁�
        public RoleInfo() : base()
        {
			
			roleId = 0L;
			profession = 0;
            
			roleName = "";
			level = 0;
            
			vip = 0;
            
			gold = 0L;
			damond = 0;
            
			energy = 0;
            
			awakenFragment = 0;
            
			honorCurrency = 0;
            
			trailCurrency = 0;
            
			clubCurrency = 0;
            
			curExp = 0L;
			aoyiJinghua = 0L;
			fightPower = 0L;
			guildId = 0L;
			guildName = "";
			talent = 0;
            
			skillPointsBuyCount = 0;
            
			skillPoints = 0;
            
			nextPointTime = 0;
            
			headIconId = 0;
            
			precedeValue = 0;
            
			token = "";
			nextEnergyTime = 0L;
			exitGuildTime = 0L;
			nicknameModifyCount = 0;
            
			energyBuyCount = 0;
            
			totalSignIn = 0;
            
			monthSignIn = 0;
            
			signInAwardIndex = 0;
            
			dailySignInFlag = 0;
            
			signInPetIndex = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			profession = 0;
            
			roleName = "";
			level = 0;
            
			vip = 0;
            
			gold = 0L;
			damond = 0;
            
			energy = 0;
            
			awakenFragment = 0;
            
			honorCurrency = 0;
            
			trailCurrency = 0;
            
			clubCurrency = 0;
            
			curExp = 0L;
			aoyiJinghua = 0L;
			fightPower = 0L;
			guildId = 0L;
			guildName = "";
			talent = 0;
            
			skillPointsBuyCount = 0;
            
			skillPoints = 0;
            
			nextPointTime = 0;
            
			headIconId = 0;
            
			precedeValue = 0;
            
			token = "";
			nextEnergyTime = 0L;
			exitGuildTime = 0L;
			nicknameModifyCount = 0;
            
			energyBuyCount = 0;
            
			totalSignIn = 0;
            
			monthSignIn = 0;
            
			signInAwardIndex = 0;
            
			dailySignInFlag = 0;
            
			signInPetIndex = 0;
            

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
                profession = XBuffer.ReadInt(buffer, ref offset);
                roleName = XBuffer.ReadString(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                vip = XBuffer.ReadInt(buffer, ref offset);
                gold = XBuffer.ReadLong(buffer, ref offset);
                damond = XBuffer.ReadInt(buffer, ref offset);
                energy = XBuffer.ReadInt(buffer, ref offset);
                awakenFragment = XBuffer.ReadInt(buffer, ref offset);
                honorCurrency = XBuffer.ReadInt(buffer, ref offset);
                trailCurrency = XBuffer.ReadInt(buffer, ref offset);
                clubCurrency = XBuffer.ReadInt(buffer, ref offset);
                curExp = XBuffer.ReadLong(buffer, ref offset);
                aoyiJinghua = XBuffer.ReadLong(buffer, ref offset);
                fightPower = XBuffer.ReadLong(buffer, ref offset);
                guildId = XBuffer.ReadLong(buffer, ref offset);
                guildName = XBuffer.ReadString(buffer, ref offset);
                talent = XBuffer.ReadInt(buffer, ref offset);
                skillPointsBuyCount = XBuffer.ReadInt(buffer, ref offset);
                skillPoints = XBuffer.ReadInt(buffer, ref offset);
                nextPointTime = XBuffer.ReadInt(buffer, ref offset);
                headIconId = XBuffer.ReadInt(buffer, ref offset);
                precedeValue = XBuffer.ReadInt(buffer, ref offset);
                token = XBuffer.ReadString(buffer, ref offset);
                nextEnergyTime = XBuffer.ReadLong(buffer, ref offset);
                exitGuildTime = XBuffer.ReadLong(buffer, ref offset);
                nicknameModifyCount = XBuffer.ReadInt(buffer, ref offset);
                energyBuyCount = XBuffer.ReadInt(buffer, ref offset);
                totalSignIn = XBuffer.ReadInt(buffer, ref offset);
                monthSignIn = XBuffer.ReadInt(buffer, ref offset);
                signInAwardIndex = XBuffer.ReadInt(buffer, ref offset);
                dailySignInFlag = XBuffer.ReadInt(buffer, ref offset);
                signInPetIndex = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteLong(roleId, buffer, ref offset);
                XBuffer.WriteInt(profession, buffer, ref offset);
                XBuffer.WriteString(roleName, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteInt(vip, buffer, ref offset);
                XBuffer.WriteLong(gold, buffer, ref offset);
                XBuffer.WriteInt(damond, buffer, ref offset);
                XBuffer.WriteInt(energy, buffer, ref offset);
                XBuffer.WriteInt(awakenFragment, buffer, ref offset);
                XBuffer.WriteInt(honorCurrency, buffer, ref offset);
                XBuffer.WriteInt(trailCurrency, buffer, ref offset);
                XBuffer.WriteInt(clubCurrency, buffer, ref offset);
                XBuffer.WriteLong(curExp, buffer, ref offset);
                XBuffer.WriteLong(aoyiJinghua, buffer, ref offset);
                XBuffer.WriteLong(fightPower, buffer, ref offset);
                XBuffer.WriteLong(guildId, buffer, ref offset);
                XBuffer.WriteString(guildName, buffer, ref offset);
                XBuffer.WriteInt(talent, buffer, ref offset);
                XBuffer.WriteInt(skillPointsBuyCount, buffer, ref offset);
                XBuffer.WriteInt(skillPoints, buffer, ref offset);
                XBuffer.WriteInt(nextPointTime, buffer, ref offset);
                XBuffer.WriteInt(headIconId, buffer, ref offset);
                XBuffer.WriteInt(precedeValue, buffer, ref offset);
                XBuffer.WriteString(token, buffer, ref offset);
                XBuffer.WriteLong(nextEnergyTime, buffer, ref offset);
                XBuffer.WriteLong(exitGuildTime, buffer, ref offset);
                XBuffer.WriteInt(nicknameModifyCount, buffer, ref offset);
                XBuffer.WriteInt(energyBuyCount, buffer, ref offset);
                XBuffer.WriteInt(totalSignIn, buffer, ref offset);
                XBuffer.WriteInt(monthSignIn, buffer, ref offset);
                XBuffer.WriteInt(signInAwardIndex, buffer, ref offset);
                XBuffer.WriteInt(dailySignInFlag, buffer, ref offset);
                XBuffer.WriteInt(signInPetIndex, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //客户端初始化完成
    public class ReqClientInitOver : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105201;

    	//鏋勯�犲嚱鏁�
    	public ReqClientInitOver()
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
    //客户端主动心跳
    public class ClientHeartBeat : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105202;
		public long time; // 发送时间

    	//鏋勯�犲嚱鏁�
    	public ClientHeartBeat()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			time = 0L;
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
                time = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(time,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //购买技能点
    public class ReqBuySkillPoint : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105203;

    	//鏋勯�犲嚱鏁�
    	public ReqBuySkillPoint()
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
    //购买货币
    public class ReqBuyCurrency : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105204;
		public int type; // 货币类型
		public int num; // 请求购买的道具数量

    	//鏋勯�犲嚱鏁�
    	public ReqBuyCurrency()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			type = 0;
            
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
                type = XBuffer.ReadInt(buffer, ref offset);
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
					XBuffer.WriteInt(type,buffer, ref offset);
					XBuffer.WriteInt(num,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //更换头像
    public class ReqModifyIcon : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105206;
		public int iconId; // 新头像

    	//鏋勯�犲嚱鏁�
    	public ReqModifyIcon()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			iconId = 0;
            
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
                iconId = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(iconId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //更换昵称
    public class ReqModifyNickname : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105207;
		public string nickname; // 新昵称

    	//鏋勯�犲嚱鏁�
    	public ReqModifyNickname()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			nickname = "";
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
                nickname = XBuffer.ReadString(buffer, ref offset);

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
					XBuffer.WriteString(nickname,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //领取总签到数奖励
    public class ReqSignInBox : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105208;

    	//鏋勯�犲嚱鏁�
    	public ReqSignInBox()
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
    //领取签到奖励
    public class ReqSignIn : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105209;

    	//鏋勯�犲嚱鏁�
    	public ReqSignIn()
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
    //玩家数据
    public class ResRoleInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105101;
		public int result; // 登陆结果（0 失败，1 成功，2 仅更新数据）
		public RoleInfo roleInfo; // 角色信息

    	//鏋勯�犲嚱鏁�
    	public ResRoleInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			result = 0;
            
			//roleInfo = ClassCacheManager.New<RoleInfo>();
			roleInfo = new RoleInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			roleInfo = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                result = XBuffer.ReadInt(buffer, ref offset);
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //roleInfo = ClassCacheManager.New<RoleInfo>();
				roleInfo = new RoleInfo();
                roleInfo.Read(buffer, ref offset);

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
					if(roleInfo == null)
						//roleInfo = ClassCacheManager.New<RoleInfo>();
						roleInfo = new RoleInfo();
					roleInfo.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //服务器时间
    public class ResCurrentTime : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105102;
		public long curTime; // 当前时间

    	//鏋勯�犲嚱鏁�
    	public ResCurrentTime()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			curTime = 0L;
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
                curTime = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(curTime,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //提示信息
    public class ResPrompt : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105103;
		public int type; // 提示信息类型（1一般，2警告，3错误，4走马灯，5官方消息（插队显示即刻显示），6弹框提示）
		public int lanId; // 提示信息(语言包id -1时直接显示content)
        public List<string> content{get;protected set;} //填充字段

    	//鏋勯�犲嚱鏁�
    	public ResPrompt()
    	{
            content = new List<string>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			type = 0;
            
			lanId = 0;
            
            content.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            content.Clear();
        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                type = XBuffer.ReadInt(buffer, ref offset);
                lanId = XBuffer.ReadInt(buffer, ref offset);

    		    short _count_ = 0;
        		_count_ = XBuffer.ReadShort(buffer, ref offset);

                for (int a = 0; a < _count_; ++a)
                {
                    var _value_ = XBuffer.ReadString(buffer, ref offset);
                    content.Add(_value_);
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
					XBuffer.WriteInt(lanId,buffer, ref offset);

                XBuffer.WriteShort((short)content.Count, buffer, ref offset);
                for(int a = 0; a < content.Count; ++a)
                {
                    XBuffer.WriteString(content[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //服务器主动心跳
    public class ServerHeartBeat : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105104;
		public long time; // 发送时间

    	//鏋勯�犲嚱鏁�
    	public ServerHeartBeat()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			time = 0L;
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
                time = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(time,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //主角战斗力改变
    public class ResFightPowerChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105105;
		public long fightPower; // 战斗力

    	//鏋勯�犲嚱鏁�
    	public ResFightPowerChange()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
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
					XBuffer.WriteLong(fightPower,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //货币数量发生改变
    public class ResCurrencyChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105106;
		public int type; // 货币类型
		public long num; // 货币最终数量

    	//鏋勯�犲嚱鏁�
    	public ResCurrencyChange()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			type = 0;
            
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
                TypeEnum real_type;
                type = XBuffer.ReadInt(buffer, ref offset);
                num = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(num,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //升级
    public class ResLevelUp : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105107;
		public int level; // 等级

    	//鏋勯�犲嚱鏁�
    	public ResLevelUp()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
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
					XBuffer.WriteInt(level,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //先手值改变
    public class ResChangePrecedeValue : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105108;
		public int value; // 最终值

    	//鏋勯�犲嚱鏁�
    	public ResChangePrecedeValue()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
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
                TypeEnum real_type;
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
					XBuffer.WriteInt(value,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //更换头像
    public class ResModifyIcon : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105110;
		public int iconId; // 新头像

    	//鏋勯�犲嚱鏁�
    	public ResModifyIcon()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			iconId = 0;
            
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
                iconId = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(iconId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //更换昵称
    public class ResModifyNickname : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105111;
		public string nickname; // 新昵称

    	//鏋勯�犲嚱鏁�
    	public ResModifyNickname()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			nickname = "";
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
                nickname = XBuffer.ReadString(buffer, ref offset);

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
					XBuffer.WriteString(nickname,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //技能点改变
    public class ResSkillPointChange : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105112;
		public int num; // 最终值
		public bool __buy; // 是否购买改变
		private byte _buy = 0; // 是否购买改变 tag
		
		public bool hasBuy()
		{
			return this._buy == 1;
		}
		
		public bool buy
		{
			set
			{
				_buy = 1;
				__buy = value;
			}
			
			get
			{
				return __buy;
			}
		}

    	//鏋勯�犲嚱鏁�
    	public ResSkillPointChange()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			num = 0;
            
			_buy = 0;
			__buy = false;
            buy = false;
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
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					buy = XBuffer.ReadBool(buffer, ref offset);
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
					XBuffer.WriteInt(num,buffer, ref offset);
				XBuffer.WriteByte(_buy,buffer, ref offset);
				if (_buy == 1)
				{
					XBuffer.WriteBool(buy,buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //退帮时间重置
    public class ResResetExitGuildTime : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105113;
		public long time; // 上次退帮时间

    	//鏋勯�犲嚱鏁�
    	public ResResetExitGuildTime()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			time = 0L;
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
                time = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(time,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //开服天数
    public class ResOpenServerDays : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105114;
		public int OpenServerDays; // 开服天数

    	//鏋勯�犲嚱鏁�
    	public ResOpenServerDays()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			OpenServerDays = 0;
            
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
                OpenServerDays = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(OpenServerDays,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //体力购买次数
    public class ResEnergyBuyCount : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105115;
		public int energyBuyCount; // 体力购买次数

    	//鏋勯�犲嚱鏁�
    	public ResEnergyBuyCount()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			energyBuyCount = 0;
            
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
                energyBuyCount = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(energyBuyCount,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //总签到礼包
    public class ResSignInBox : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105116;
		public int signInAwardIndex; // 总签到礼包领取下标
        public List<ItemInfo> itemInfos{get;protected set;} //道具

    	//鏋勯�犲嚱鏁�
    	public ResSignInBox()
    	{
            itemInfos = new List<ItemInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			signInAwardIndex = 0;
            
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
                signInAwardIndex = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(signInAwardIndex,buffer, ref offset);

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
    //签到
    public class ResSignIn : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 105117;
		public int dailySignInFlag; // 0:未领取，1：单倍，2：双倍
        public List<ItemInfo> itemInfos{get;protected set;} //道具

    	//鏋勯�犲嚱鏁�
    	public ResSignIn()
    	{
            itemInfos = new List<ItemInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			dailySignInFlag = 0;
            
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
                dailySignInFlag = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteInt(dailySignInFlag,buffer, ref offset);

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
}