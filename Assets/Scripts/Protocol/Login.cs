//Auto generated, do not edit it
//登陆消息

using System;
using System.IO;
using System.Collections.Generic;

namespace Message.Login
{
    public enum TypeEnum
    {
        LoginRoleInfo = 1,
    }

    //实体信息
    public class LoginRoleInfo : BaseMsgStruct
    {
		public override bool doCache { get { return true; } }
		public long roleId; // 实体Id
        
		public int profession; // 职业1:男2：女
        
		public string roleName; // 角色名字
        
		public int showId; // 显示Id
        
		public int level; // 角色等级
        
		public int curMainCityId; // 当前所在主城
        
		public int equipsId; // 身上的装备id
        

        //鏋勯�犲嚱鏁�
        public LoginRoleInfo() : base()
        {
			
			roleId = 0L;
			profession = 0;
            
			roleName = "";
			showId = 0;
            
			level = 0;
            
			curMainCityId = 0;
            
			equipsId = 0;
            

        }

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleId = 0L;
			profession = 0;
            
			roleName = "";
			showId = 0;
            
			level = 0;
            
			curMainCityId = 0;
            
			equipsId = 0;
            

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
                showId = XBuffer.ReadInt(buffer, ref offset);
                level = XBuffer.ReadInt(buffer, ref offset);
                curMainCityId = XBuffer.ReadInt(buffer, ref offset);
                equipsId = XBuffer.ReadInt(buffer, ref offset);

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
                XBuffer.WriteInt(showId, buffer, ref offset);
                XBuffer.WriteInt(level, buffer, ref offset);
                XBuffer.WriteInt(curMainCityId, buffer, ref offset);
                XBuffer.WriteInt(equipsId, buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    //登陆
    public class ReqLogin : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 103201;
		public string userName; // 登陆用户名
		public int serverId; // 登陆服务器Id
		public int platformId; // 平台
		public long loginTime; // 登陆时间

    	//鏋勯�犲嚱鏁�
    	public ReqLogin()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			userName = "";
			serverId = 0;
            
			platformId = 0;
            
			loginTime = 0L;
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
                userName = XBuffer.ReadString(buffer, ref offset);
                serverId = XBuffer.ReadInt(buffer, ref offset);
                platformId = XBuffer.ReadInt(buffer, ref offset);
                loginTime = XBuffer.ReadLong(buffer, ref offset);

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
					XBuffer.WriteString(userName,buffer, ref offset);
					XBuffer.WriteInt(serverId,buffer, ref offset);
					XBuffer.WriteInt(platformId,buffer, ref offset);
					XBuffer.WriteLong(loginTime,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //请求角色列表
    public class ReqRoleList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 103202;

    	//鏋勯�犲嚱鏁�
    	public ReqRoleList()
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
    //选择角色
    public class ReqSelectRole : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 103203;
		public long roleId; // 角色id

    	//鏋勯�犲嚱鏁�
    	public ReqSelectRole()
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
    //创建角色
    public class ReqCreateRole : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 103204;
		public string roleName; // 角色名字
		public int profession; // 职业

    	//鏋勯�犲嚱鏁�
    	public ReqCreateRole()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			roleName = "";
			profession = 0;
            
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
                roleName = XBuffer.ReadString(buffer, ref offset);
                profession = XBuffer.ReadInt(buffer, ref offset);

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
					XBuffer.WriteString(roleName,buffer, ref offset);
					XBuffer.WriteInt(profession,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //登陆
    public class ResLogin : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 103101;
		public int loginResult; // 登陆结果（1 = 成功，other = 失败）
		public int failedReason; // 登陆失败的原因 (1 = 服务器未开启, 2= ip地址被屏蔽 3 = 登陆超时 4 登陆Key验证失败（非客户端登陆）5 账户未激活 6 激活失败 7 激活码过期
		public string username; // 登陆用户名

    	//鏋勯�犲嚱鏁�
    	public ResLogin()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			loginResult = 0;
            
			failedReason = 0;
            
			username = "";
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
                loginResult = XBuffer.ReadInt(buffer, ref offset);
                failedReason = XBuffer.ReadInt(buffer, ref offset);
                username = XBuffer.ReadString(buffer, ref offset);

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
					XBuffer.WriteInt(loginResult,buffer, ref offset);
					XBuffer.WriteInt(failedReason,buffer, ref offset);
					XBuffer.WriteString(username,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //请求角色列表
    public class ResRoleList : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 103102;
        public List<LoginRoleInfo> roles{get;protected set;} //角色列表

    	//鏋勯�犲嚱鏁�
    	public ResRoleList()
    	{
            roles = new List<LoginRoleInfo>();
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
            roles.Clear();
        }

        public override void FakeDtr()
        {
            base.FakeDtr();

            for (int a = 0,b = roles.Count; a < b; ++a)
            {
				roles[a] = null;
                //var _value_ = roles[a];
                //if(_value_ != null)
                //    ClassCacheManager.Delete(ref _value_);
            }
            roles.Clear();
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
                    LoginRoleInfo _value_ = null;
                    //_value_ = ClassCacheManager.New<LoginRoleInfo>();
					_value_ = new LoginRoleInfo();
                    _value_.Read(buffer, ref offset);
                    roles.Add(_value_);
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

                XBuffer.WriteShort((short)roles.Count, buffer, ref offset);
                for(int a = 0; a < roles.Count; ++a)
                {
					if(roles[a] == null)
						UnityEngine.Debug.LogError("roles has nil item, idx == " + a);
					else
						roles[a].WriteWithType(buffer, ref offset);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //创建角色
    public class ResCreateRole : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 103104;
		public bool success; // 是否创建成功

    	//鏋勯�犲嚱鏁�
    	public ResCreateRole()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			success = false;
            success = false;
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
        		success = XBuffer.ReadBool(buffer, ref offset);

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
					XBuffer.WriteBool(success,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //请求重连
    public class ReqReLogin : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 103205;
		public string token; // token

    	//鏋勯�犲嚱鏁�
    	public ReqReLogin()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			token = "";
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
                token = XBuffer.ReadString(buffer, ref offset);

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
					XBuffer.WriteString(token,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
    //重连连结果
    public class ResReLogin : BaseMessage
    {
        public override int GetMsgId()
        {
            return MsgId;
        }
        public const int MsgId = 103105;
		public bool success; // 登陆结果

    	//鏋勯�犲嚱鏁�
    	public ResReLogin()
    	{
    	}

        public override void FakeCtr(IParam param)
        {
            base.FakeCtr(param);
			success = false;
            success = false;
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
        		success = XBuffer.ReadBool(buffer, ref offset);

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
					XBuffer.WriteBool(success,buffer, ref offset);

            }
            catch(Exception ex)
            {
                throw ex;
            }
    	}
    }
}