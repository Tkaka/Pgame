//Auto generated, do not edit it
//好友

using System;
using System.IO;
using System.Collections.Generic;

namespace Message.Friend
{
    public enum TypeEnum
    {
        PlayerInfo = 1,
        ApplyResult = 2,
    }

    //好友
    public class PlayerInfo : BaseMsgStruct
    {
		public string roleName; // 角色昵称
        
		public long roleId; // 角色id
        
		public int level; // 等级
        
		public long fightPower; // 战力
        
		public long timeInMinutes; // 离线时间(-1：在线)
        
		public bool __receiveEnergy; // 领取体力
		private byte _receiveEnergy = 0; // 领取体力 tag
		
		public bool hasReceiveEnergy()
		{
			return this._receiveEnergy == 1;
		}
		
		public bool receiveEnergy
		{
			set
			{
				_receiveEnergy = 1;
				__receiveEnergy = value;
			}
			
			get
			{
				return __receiveEnergy;
			}
		}
        
		public bool __giveEnergy; // 赠送体力
		private byte _giveEnergy = 0; // 赠送体力 tag
		
		public bool hasGiveEnergy()
		{
			return this._giveEnergy == 1;
		}
		
		public bool giveEnergy
		{
			set
			{
				_giveEnergy = 1;
				__giveEnergy = value;
			}
			
			get
			{
				return __giveEnergy;
			}
		}
        
		public bool __friend; // 是否好友
		private byte _friend = 0; // 是否好友 tag
		
		public bool hasFriend()
		{
			return this._friend == 1;
		}
		
		public bool friend
		{
			set
			{
				_friend = 1;
				__friend = value;
			}
			
			get
			{
				return __friend;
			}
		}
        

        //鏋勯�犲嚱鏁�
        public PlayerInfo() : base()
        {
			
			roleName = "";
			roleId = 0L;
			level = 0;
            
			fightPower = 0L;
			timeInMinutes = 0L;
			_receiveEnergy = 0;
			__receiveEnergy = false;
            receiveEnergy = false;
			_giveEnergy = 0;
			__giveEnergy = false;
            giveEnergy = false;
			_friend = 0;
			__friend = false;
            friend = false;

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleName = "";
			roleId = 0L;
			level = 0;
            
			fightPower = 0L;
			timeInMinutes = 0L;
			_receiveEnergy = 0;
			__receiveEnergy = false;
            receiveEnergy = false;
			_giveEnergy = 0;
			__giveEnergy = false;
            giveEnergy = false;
			_friend = 0;
			__friend = false;
            friend = false;

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
                roleName = XBuffer.ReadString(buffer, ref offset);
                roleId = XBuffer.ReadLong(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                fightPower = XBuffer.ReadLong(buffer, ref offset);
                timeInMinutes = XBuffer.ReadLong(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					receiveEnergy = XBuffer.ReadBool(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					giveEnergy = XBuffer.ReadBool(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					friend = XBuffer.ReadBool(buffer, ref offset);
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
                XBuffer.WriteString(roleName, buffer, ref offset);
                XBuffer.WriteLong(roleId, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteLong(fightPower, buffer, ref offset);
                XBuffer.WriteLong(timeInMinutes, buffer, ref offset);
				XBuffer.WriteByte(_receiveEnergy, buffer, ref offset);
				if (_receiveEnergy == 1)
				{
					XBuffer.WriteBool(receiveEnergy, buffer, ref offset);
				}
				XBuffer.WriteByte(_giveEnergy, buffer, ref offset);
				if (_giveEnergy == 1)
				{
					XBuffer.WriteBool(giveEnergy, buffer, ref offset);
				}
				XBuffer.WriteByte(_friend, buffer, ref offset);
				if (_friend == 1)
				{
					XBuffer.WriteBool(friend, buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    //好友申请结果
    public class ApplyResult : BaseMsgStruct
    {
		public long roleId; // 角色id
        
		public int result; // 0:成功，1：对方好友已满，2：自己好友已满
        

        //鏋勯�犲嚱鏁�
        public ApplyResult() : base()
        {
			
			roleId = 0L;
			result = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
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
                TypeEnum _real_type_;
                roleId = XBuffer.ReadLong(buffer, ref offset);
                result = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteLong(roleId, buffer, ref offset);
                XBuffer.WriteInt(result, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //赠送体力
    public class ReqGiveEnergy : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127202;
		public long roleId; // 角色id

    	//鏋勯�犲嚱鏁�
    	public ReqGiveEnergy()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
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
                roleId = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(roleId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //一键赠送体力
    public class ReqOneKeyGiveEnergy : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127203;

    	//鏋勯�犲嚱鏁�
    	public ReqOneKeyGiveEnergy()
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
    //领取体力
    public class ReqReceiveEnergy : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127204;
		public long roleId; // 角色id

    	//鏋勯�犲嚱鏁�
    	public ReqReceiveEnergy()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
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
                roleId = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(roleId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //一键领取体力
    public class ReqOneKeyReceiveEnergy : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127205;

    	//鏋勯�犲嚱鏁�
    	public ReqOneKeyReceiveEnergy()
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
    //删除好友
    public class ReqDeleteFriend : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127206;
        public List<long> roleIdList{get;protected set;} //角色id列表

    	//鏋勯�犲嚱鏁�
    	public ReqDeleteFriend()
    	{
            roleIdList = new List<long>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            roleIdList.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            roleIdList.Clear();
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
                    roleIdList.Add(_value_);
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

                XBuffer.WriteShort((short)roleIdList.Count, buffer, ref offset);
                for(int a = 0; a < roleIdList.Count; ++a)
                {
        			XBuffer.WriteLong(roleIdList[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //添加好友
    public class ReqApplyFriend : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127207;
		public long roleId; // 角色id

    	//鏋勯�犲嚱鏁�
    	public ReqApplyFriend()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
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
                roleId = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(roleId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //申请列表
    public class ReqApplyList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127208;

    	//鏋勯�犲嚱鏁�
    	public ReqApplyList()
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
    //同意好友申请
    public class ReqAgreeApply : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127209;
		public long roleId; // 角色id

    	//鏋勯�犲嚱鏁�
    	public ReqAgreeApply()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
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
                roleId = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(roleId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //一键同意好友申请
    public class ReqOneKeyAgreeApply : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127210;

    	//鏋勯�犲嚱鏁�
    	public ReqOneKeyAgreeApply()
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
    //随机玩家列表
    public class ReqRandomPlayerList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127211;

    	//鏋勯�犲嚱鏁�
    	public ReqRandomPlayerList()
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
    //搜索玩家
    public class ReqSearchPlayer : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127212;
		public string keyWord; // 关键字（完整昵称或编号）

    	//鏋勯�犲嚱鏁�
    	public ReqSearchPlayer()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			keyWord = "";
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
                keyWord = XBuffer.ReadString(buffer, ref offset);

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
					XBuffer.WriteString(keyWord,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //拒绝好友申请
    public class ReqRefuseApply : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127213;
		public long roleId; // 角色id

    	//鏋勯�犲嚱鏁�
    	public ReqRefuseApply()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
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
                roleId = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(roleId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //一键拒绝好友申请
    public class ReqOneKeyRefuseApply : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127214;

    	//鏋勯�犲嚱鏁�
    	public ReqOneKeyRefuseApply()
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
    //一键申请
    public class ReqOneKeyApply : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127215;
        public List<long> roleIdList{get;protected set;} //角色id列表

    	//鏋勯�犲嚱鏁�
    	public ReqOneKeyApply()
    	{
            roleIdList = new List<long>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            roleIdList.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            roleIdList.Clear();
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
                    roleIdList.Add(_value_);
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

                XBuffer.WriteShort((short)roleIdList.Count, buffer, ref offset);
                for(int a = 0; a < roleIdList.Count; ++a)
                {
        			XBuffer.WriteLong(roleIdList[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //添加到黑名单
    public class ReqAddToBlackList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127216;
		public long roleId; // 角色ID

    	//鏋勯�犲嚱鏁�
    	public ReqAddToBlackList()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
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
                roleId = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(roleId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //从黑名单中移除
    public class ReqRemoveFromBlackList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127217;
		public long roleId; // 角色ID

    	//鏋勯�犲嚱鏁�
    	public ReqRemoveFromBlackList()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
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
                roleId = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(roleId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //好友列表
    public class ResFriends : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127101;
        public List<PlayerInfo> friends{get;protected set;} //好友列表

    	//鏋勯�犲嚱鏁�
    	public ResFriends()
    	{
            friends = new List<PlayerInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            friends.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = friends.Count; a < b; ++a)
            {
				friends[a] = null;
                //var _value_ = friends[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            friends.Clear();
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
                    PlayerInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<PlayerInfo>();
					_value_ = new PlayerInfo();
                    _value_.Read(buffer, ref offset);
                    friends.Add(_value_);
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

                XBuffer.WriteShort((short)friends.Count, buffer, ref offset);
                for(int a = 0; a < friends.Count; ++a)
                {
					if(friends[a] == null)
						UnityEngine.Debug.LogError("friends has nil item, idx == " + a);
					else
						friends[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //赠送体力
    public class ResGiveEnergy : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127102;
		public long roleId; // 角色id

    	//鏋勯�犲嚱鏁�
    	public ResGiveEnergy()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
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
                roleId = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(roleId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //一键赠送体力
    public class ResOneKeyGiveEnergy : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127103;

    	//鏋勯�犲嚱鏁�
    	public ResOneKeyGiveEnergy()
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
    //领取体力
    public class ResReceiveEnergy : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127104;
		public long roleId; // 角色id
		public int energyAdd; // 获得的体力
		public bool full; // 领取达上限

    	//鏋勯�犲嚱鏁�
    	public ResReceiveEnergy()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			energyAdd = 0;
            
			full = false;
            full = false;
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
                roleId = XBuffer.ReadLong(buffer, ref offset);
                energyAdd = XBuffer.ReadInt(buffer, ref offset);
        		full = XBuffer.ReadBool(buffer, ref offset);

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
					XBuffer.WriteLong(roleId,buffer, ref offset);
					XBuffer.WriteInt(energyAdd,buffer, ref offset);
					XBuffer.WriteBool(full,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //一键领取体力
    public class ResOneKeyReceiveEnergy : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127105;
		public int energyAdd; // 获得的体力
		public bool full; // 领取达上限

    	//鏋勯�犲嚱鏁�
    	public ResOneKeyReceiveEnergy()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			energyAdd = 0;
            
			full = false;
            full = false;
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
                energyAdd = XBuffer.ReadInt(buffer, ref offset);
        		full = XBuffer.ReadBool(buffer, ref offset);

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
					XBuffer.WriteInt(energyAdd,buffer, ref offset);
					XBuffer.WriteBool(full,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //删除好友成功
    public class ResDeleteFriend : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127106;
        public List<long> roleIdList{get;protected set;} //角色id列表

    	//鏋勯�犲嚱鏁�
    	public ResDeleteFriend()
    	{
            roleIdList = new List<long>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            roleIdList.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            roleIdList.Clear();
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
                    roleIdList.Add(_value_);
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

                XBuffer.WriteShort((short)roleIdList.Count, buffer, ref offset);
                for(int a = 0; a < roleIdList.Count; ++a)
                {
        			XBuffer.WriteLong(roleIdList[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //添加好友发送成功
    public class ResApplyFriend : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127107;
		public long roleId; // 角色id
		public int result; // 0:成功，1：对方好友已满，2：自己好友已满

    	//鏋勯�犲嚱鏁�
    	public ResApplyFriend()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
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
                roleId = XBuffer.ReadLong(buffer, ref offset);
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
					XBuffer.WriteLong(roleId,buffer, ref offset);
					XBuffer.WriteInt(result,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //申请列表
    public class ResApplyList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127108;
        public List<PlayerInfo> applicants{get;protected set;} //申请者列表

    	//鏋勯�犲嚱鏁�
    	public ResApplyList()
    	{
            applicants = new List<PlayerInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            applicants.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = applicants.Count; a < b; ++a)
            {
				applicants[a] = null;
                //var _value_ = applicants[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            applicants.Clear();
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
                    PlayerInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<PlayerInfo>();
					_value_ = new PlayerInfo();
                    _value_.Read(buffer, ref offset);
                    applicants.Add(_value_);
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

                XBuffer.WriteShort((short)applicants.Count, buffer, ref offset);
                for(int a = 0; a < applicants.Count; ++a)
                {
					if(applicants[a] == null)
						UnityEngine.Debug.LogError("applicants has nil item, idx == " + a);
					else
						applicants[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //同意好友申请
    public class ResAgreeApply : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127109;
		public ApplyResult applyResult; // 同意好友申请结果

    	//鏋勯�犲嚱鏁�
    	public ResAgreeApply()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//applyResult = ClassCacheManager.New<ApplyResult>();
			applyResult = new ApplyResult();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			applyResult = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //applyResult = ClassCacheManager.New<ApplyResult>();
				applyResult = new ApplyResult();
                applyResult.Read(buffer, ref offset);

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
					if(applyResult == null)
						//applyResult = ClassCacheManager.New<ApplyResult>();
						applyResult = new ApplyResult();
					applyResult.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //一键同意好友申请
    public class ResOneKeyAgreeApply : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127110;

    	//鏋勯�犲嚱鏁�
    	public ResOneKeyAgreeApply()
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
    //随机玩家列表
    public class ResRandomPlayerList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127111;
        public List<PlayerInfo> players{get;protected set;} //玩家列表

    	//鏋勯�犲嚱鏁�
    	public ResRandomPlayerList()
    	{
            players = new List<PlayerInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            players.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = players.Count; a < b; ++a)
            {
				players[a] = null;
                //var _value_ = players[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            players.Clear();
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
                    PlayerInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<PlayerInfo>();
					_value_ = new PlayerInfo();
                    _value_.Read(buffer, ref offset);
                    players.Add(_value_);
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

                XBuffer.WriteShort((short)players.Count, buffer, ref offset);
                for(int a = 0; a < players.Count; ++a)
                {
					if(players[a] == null)
						UnityEngine.Debug.LogError("players has nil item, idx == " + a);
					else
						players[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //搜索玩家
    public class ResSearchPlayer : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127112;
		public PlayerInfo __player; // 玩家
		private byte _player = 0; // 玩家 tag
		
		public bool hasPlayer()
		{
			return this._player == 1;
		}
		
		public PlayerInfo player
		{
			set
			{
				_player = 1;
				__player = value;
			}
			
			get
			{
				return __player;
			}
		}

    	//鏋勯�犲嚱鏁�
    	public ResSearchPlayer()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			_player = 0;
			//__player = ClassCacheManager.New<PlayerInfo>();
			__player = new PlayerInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			__player = null;

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
					real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
					//player = ClassCacheManager.New<PlayerInfo>();
					player = new PlayerInfo();
					player.Read(buffer, ref offset);
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
				XBuffer.WriteByte(_player,buffer, ref offset);
				if (_player == 1)
				{
					if(player == null)
						//player = ClassCacheManager.New<PlayerInfo>();
						player = new PlayerInfo();
					player.WriteWithType(buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //拒绝好友申请
    public class ResRefuseApply : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127113;
		public long roleId; // 角色id

    	//鏋勯�犲嚱鏁�
    	public ResRefuseApply()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
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
                roleId = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(roleId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //一键拒绝好友申请
    public class ResOneKeyRefuseApply : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127114;

    	//鏋勯�犲嚱鏁�
    	public ResOneKeyRefuseApply()
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
    //一键申请
    public class ResOneKeyApply : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127115;

    	//鏋勯�犲嚱鏁�
    	public ResOneKeyApply()
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
    //黑名单
    public class ResBlackList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127116;
        public List<long> roleIdList{get;protected set;} //角色id列表

    	//鏋勯�犲嚱鏁�
    	public ResBlackList()
    	{
            roleIdList = new List<long>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            roleIdList.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            roleIdList.Clear();
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
                    roleIdList.Add(_value_);
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

                XBuffer.WriteShort((short)roleIdList.Count, buffer, ref offset);
                for(int a = 0; a < roleIdList.Count; ++a)
                {
        			XBuffer.WriteLong(roleIdList[a],buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //添加到黑名单
    public class ResAddToBlackList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127117;
		public long roleId; // 角色id

    	//鏋勯�犲嚱鏁�
    	public ResAddToBlackList()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
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
                roleId = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(roleId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //从黑名单中移除
    public class ResRemoveFromBlackList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 127118;
		public long roleId; // 角色id

    	//鏋勯�犲嚱鏁�
    	public ResRemoveFromBlackList()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
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
                roleId = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteLong(roleId,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}