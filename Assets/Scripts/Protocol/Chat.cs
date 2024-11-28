//Auto generated, do not edit it
//聊天

using System;
using System.IO;
using System.Collections.Generic;

namespace Message.Chat
{
    public enum TypeEnum
    {
        ChatInfo = 1,
    }

    //聊天消息
    public class ChatInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public int channel; // 频道
        
		public string content; // 消息类容
        
		public bool __isBell; // 是否是喇叭
		private byte _isBell = 0; // 是否是喇叭 tag
		
		public bool hasIsBell()
		{
			return this._isBell == 1;
		}
		
		public bool isBell
		{
			set
			{
				_isBell = 1;
				__isBell = value;
			}
			
			get
			{
				return __isBell;
			}
		}
        
		public bool __isMarquee; // 是否是跑马灯
		private byte _isMarquee = 0; // 是否是跑马灯 tag
		
		public bool hasIsMarquee()
		{
			return this._isMarquee == 1;
		}
		
		public bool isMarquee
		{
			set
			{
				_isMarquee = 1;
				__isMarquee = value;
			}
			
			get
			{
				return __isMarquee;
			}
		}
        
		public int __jumpType; // 跳转类型
		private byte _jumpType = 0; // 跳转类型 tag
		
		public bool hasJumpType()
		{
			return this._jumpType == 1;
		}
		
		public int jumpType
		{
			set
			{
				_jumpType = 1;
				__jumpType = value;
			}
			
			get
			{
				return __jumpType;
			}
		}
        
		public string __jumpParam; // 跳转类型参数
		private byte _jumpParam = 0; // 跳转类型参数 tag
		
		public bool hasJumpParam()
		{
			return this._jumpParam == 1;
		}
		
		public string jumpParam
		{
			set
			{
				_jumpParam = 1;
				__jumpParam = value;
			}
			
			get
			{
				return __jumpParam;
			}
		}
        
		public long __roleId; // 角色ID
		private byte _roleId = 0; // 角色ID tag
		
		public bool hasRoleId()
		{
			return this._roleId == 1;
		}
		
		public long roleId
		{
			set
			{
				_roleId = 1;
				__roleId = value;
			}
			
			get
			{
				return __roleId;
			}
		}
        
		public string __playerName; // 玩家名
		private byte _playerName = 0; // 玩家名 tag
		
		public bool hasPlayerName()
		{
			return this._playerName == 1;
		}
		
		public string playerName
		{
			set
			{
				_playerName = 1;
				__playerName = value;
			}
			
			get
			{
				return __playerName;
			}
		}
        
		public int __vipLevel; // vip等级
		private byte _vipLevel = 0; // vip等级 tag
		
		public bool hasVipLevel()
		{
			return this._vipLevel == 1;
		}
		
		public int vipLevel
		{
			set
			{
				_vipLevel = 1;
				__vipLevel = value;
			}
			
			get
			{
				return __vipLevel;
			}
		}
        
		public int __iconId; // 头像框ID
		private byte _iconId = 0; // 头像框ID tag
		
		public bool hasIconId()
		{
			return this._iconId == 1;
		}
		
		public int iconId
		{
			set
			{
				_iconId = 1;
				__iconId = value;
			}
			
			get
			{
				return __iconId;
			}
		}
        
		public int __level; // 玩家等级
		private byte _level = 0; // 玩家等级 tag
		
		public bool hasLevel()
		{
			return this._level == 1;
		}
		
		public int level
		{
			set
			{
				_level = 1;
				__level = value;
			}
			
			get
			{
				return __level;
			}
		}
        
		public string __guildName; // 社团名
		private byte _guildName = 0; // 社团名 tag
		
		public bool hasGuildName()
		{
			return this._guildName == 1;
		}
		
		public string guildName
		{
			set
			{
				_guildName = 1;
				__guildName = value;
			}
			
			get
			{
				return __guildName;
			}
		}
        
		public int __bellFontType; // 喇叭字体类型
		private byte _bellFontType = 0; // 喇叭字体类型 tag
		
		public bool hasBellFontType()
		{
			return this._bellFontType == 1;
		}
		
		public int bellFontType
		{
			set
			{
				_bellFontType = 1;
				__bellFontType = value;
			}
			
			get
			{
				return __bellFontType;
			}
		}
        
		public int __bellStyle; // 喇叭样式
		private byte _bellStyle = 0; // 喇叭样式 tag
		
		public bool hasBellStyle()
		{
			return this._bellStyle == 1;
		}
		
		public int bellStyle
		{
			set
			{
				_bellStyle = 1;
				__bellStyle = value;
			}
			
			get
			{
				return __bellStyle;
			}
		}
        
		public int __bellColor; // 喇叭字体颜色
		private byte _bellColor = 0; // 喇叭字体颜色 tag
		
		public bool hasBellColor()
		{
			return this._bellColor == 1;
		}
		
		public int bellColor
		{
			set
			{
				_bellColor = 1;
				__bellColor = value;
			}
			
			get
			{
				return __bellColor;
			}
		}
        
		public long __targetRoleId; // 目标角色id
		private byte _targetRoleId = 0; // 目标角色id tag
		
		public bool hasTargetRoleId()
		{
			return this._targetRoleId == 1;
		}
		
		public long targetRoleId
		{
			set
			{
				_targetRoleId = 1;
				__targetRoleId = value;
			}
			
			get
			{
				return __targetRoleId;
			}
		}
        

        //鏋勯�犲嚱鏁�
        public ChatInfo() : base()
        {
			
			channel = 0;
            
			content = "";
			_isBell = 0;
			__isBell = false;
            isBell = false;
			_isMarquee = 0;
			__isMarquee = false;
            isMarquee = false;
			_jumpType = 0;
			__jumpType = 0;
            
			_jumpParam = 0;
			__jumpParam = "";
			_roleId = 0;
			__roleId = 0L;
			_playerName = 0;
			__playerName = "";
			_vipLevel = 0;
			__vipLevel = 0;
            
			_iconId = 0;
			__iconId = 0;
            
			_level = 0;
			__level = 0;
            
			_guildName = 0;
			__guildName = "";
			_bellFontType = 0;
			__bellFontType = 0;
            
			_bellStyle = 0;
			__bellStyle = 0;
            
			_bellColor = 0;
			__bellColor = 0;
            
			_targetRoleId = 0;
			__targetRoleId = 0L;

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			channel = 0;
            
			content = "";
			_isBell = 0;
			__isBell = false;
            isBell = false;
			_isMarquee = 0;
			__isMarquee = false;
            isMarquee = false;
			_jumpType = 0;
			__jumpType = 0;
            
			_jumpParam = 0;
			__jumpParam = "";
			_roleId = 0;
			__roleId = 0L;
			_playerName = 0;
			__playerName = "";
			_vipLevel = 0;
			__vipLevel = 0;
            
			_iconId = 0;
			__iconId = 0;
            
			_level = 0;
			__level = 0;
            
			_guildName = 0;
			__guildName = "";
			_bellFontType = 0;
			__bellFontType = 0;
            
			_bellStyle = 0;
			__bellStyle = 0;
            
			_bellColor = 0;
			__bellColor = 0;
            
			_targetRoleId = 0;
			__targetRoleId = 0L;

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
                channel = XBuffer.ReadInt(buffer, ref offset);
                content = XBuffer.ReadString(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					isBell = XBuffer.ReadBool(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					isMarquee = XBuffer.ReadBool(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					jumpType = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					jumpParam = XBuffer.ReadString(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					roleId = XBuffer.ReadLong(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					playerName = XBuffer.ReadString(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					vipLevel = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					iconId = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					level = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					guildName = XBuffer.ReadString(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					bellFontType = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					bellStyle = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					bellColor = XBuffer.ReadInt(buffer, ref offset);
				}
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					targetRoleId = XBuffer.ReadLong(buffer, ref offset);
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
                XBuffer.WriteInt(channel, buffer, ref offset);
                XBuffer.WriteString(content, buffer, ref offset);
				XBuffer.WriteByte(_isBell, buffer, ref offset);
				if (_isBell == 1)
				{
					XBuffer.WriteBool(isBell, buffer, ref offset);
				}
				XBuffer.WriteByte(_isMarquee, buffer, ref offset);
				if (_isMarquee == 1)
				{
					XBuffer.WriteBool(isMarquee, buffer, ref offset);
				}
				XBuffer.WriteByte(_jumpType, buffer, ref offset);
				if (_jumpType == 1)
				{
					XBuffer.WriteInt(jumpType, buffer, ref offset);
				}
				XBuffer.WriteByte(_jumpParam, buffer, ref offset);
				if (_jumpParam == 1)
				{
					XBuffer.WriteString(jumpParam, buffer, ref offset);
				}
				XBuffer.WriteByte(_roleId, buffer, ref offset);
				if (_roleId == 1)
				{
					XBuffer.WriteLong(roleId, buffer, ref offset);
				}
				XBuffer.WriteByte(_playerName, buffer, ref offset);
				if (_playerName == 1)
				{
					XBuffer.WriteString(playerName, buffer, ref offset);
				}
				XBuffer.WriteByte(_vipLevel, buffer, ref offset);
				if (_vipLevel == 1)
				{
					XBuffer.WriteInt(vipLevel, buffer, ref offset);
				}
				XBuffer.WriteByte(_iconId, buffer, ref offset);
				if (_iconId == 1)
				{
					XBuffer.WriteInt(iconId, buffer, ref offset);
				}
				XBuffer.WriteByte(_level, buffer, ref offset);
				if (_level == 1)
				{
					XBuffer.WriteInt(level, buffer, ref offset);
				}
				XBuffer.WriteByte(_guildName, buffer, ref offset);
				if (_guildName == 1)
				{
					XBuffer.WriteString(guildName, buffer, ref offset);
				}
				XBuffer.WriteByte(_bellFontType, buffer, ref offset);
				if (_bellFontType == 1)
				{
					XBuffer.WriteInt(bellFontType, buffer, ref offset);
				}
				XBuffer.WriteByte(_bellStyle, buffer, ref offset);
				if (_bellStyle == 1)
				{
					XBuffer.WriteInt(bellStyle, buffer, ref offset);
				}
				XBuffer.WriteByte(_bellColor, buffer, ref offset);
				if (_bellColor == 1)
				{
					XBuffer.WriteInt(bellColor, buffer, ref offset);
				}
				XBuffer.WriteByte(_targetRoleId, buffer, ref offset);
				if (_targetRoleId == 1)
				{
					XBuffer.WriteLong(targetRoleId, buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //发送聊天信息
    public class ReqSendChatInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 118201;
		public ChatInfo chatInfo; // 聊天消息
		public int __type; // 发送消息是否消耗喇叭 0钻石 1普通喇叭 2尊贵喇叭
		private byte _type = 0; // 发送消息是否消耗喇叭 0钻石 1普通喇叭 2尊贵喇叭 tag
		
		public bool hasType()
		{
			return this._type == 1;
		}
		
		public int type
		{
			set
			{
				_type = 1;
				__type = value;
			}
			
			get
			{
				return __type;
			}
		}

    	//鏋勯�犲嚱鏁�
    	public ReqSendChatInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//chatInfo = ClassCacheManager.New<ChatInfo>();
			chatInfo = new ChatInfo();
			_type = 0;
			__type = 0;
            
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			chatInfo = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //chatInfo = ClassCacheManager.New<ChatInfo>();
				chatInfo = new ChatInfo();
                chatInfo.Read(buffer, ref offset);
				if (XBuffer.ReadByte(buffer, ref offset) == 1)
				{
					type = XBuffer.ReadInt(buffer, ref offset);
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
					if(chatInfo == null)
						//chatInfo = ClassCacheManager.New<ChatInfo>();
						chatInfo = new ChatInfo();
					chatInfo.WriteWithType(buffer, ref offset);
				XBuffer.WriteByte(_type,buffer, ref offset);
				if (_type == 1)
				{
					XBuffer.WriteInt(type,buffer, ref offset);
				}

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //赠送喇叭
    public class ReqPresent : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 118202;
		public long roleId; // 赠送目标

    	//鏋勯�犲嚱鏁�
    	public ReqPresent()
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
    //接受聊天信息
    public class ResChatInfo : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 118101;
		public ChatInfo chatInfo; // 聊天消息列表

    	//鏋勯�犲嚱鏁�
    	public ResChatInfo()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			//chatInfo = ClassCacheManager.New<ChatInfo>();
			chatInfo = new ChatInfo();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();
			chatInfo = null;

        }
		
    	//璇诲彇鏁版嵁
    	public override void Read(byte[] buffer, ref int offset)
        {
            try
            {
                base.Read(buffer, ref offset);
                TypeEnum real_type;
                real_type = (TypeEnum)XBuffer.ReadByte(buffer, ref offset);
                //chatInfo = ClassCacheManager.New<ChatInfo>();
				chatInfo = new ChatInfo();
                chatInfo.Read(buffer, ref offset);

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
					if(chatInfo == null)
						//chatInfo = ClassCacheManager.New<ChatInfo>();
						chatInfo = new ChatInfo();
					chatInfo.WriteWithType(buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //初始化
    public class ResChatInit : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 118102;
        public List<ChatInfo> chatInfo{get;protected set;} //聊天消息列表

    	//鏋勯�犲嚱鏁�
    	public ResChatInit()
    	{
            chatInfo = new List<ChatInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            chatInfo.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = chatInfo.Count; a < b; ++a)
            {
				chatInfo[a] = null;
                //var _value_ = chatInfo[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            chatInfo.Clear();
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
                    ChatInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<ChatInfo>();
					_value_ = new ChatInfo();
                    _value_.Read(buffer, ref offset);
                    chatInfo.Add(_value_);
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

                XBuffer.WriteShort((short)chatInfo.Count, buffer, ref offset);
                for(int a = 0; a < chatInfo.Count; ++a)
                {
					if(chatInfo[a] == null)
						UnityEngine.Debug.LogError("chatInfo has nil item, idx == " + a);
					else
						chatInfo[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}