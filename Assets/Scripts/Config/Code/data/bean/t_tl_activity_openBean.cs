/**
 * Auto generated, do not edit it
 *
 * t_tl_activity_open
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_tl_activity_openBean : BaseBin
	{
				private int m_t_id; // 活动ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 活动ID 
				public string t_name {get; set;}   // 活动名称
				public string t_condition {get; set;}   // 是否需求活动开启前N天玩家战力和VIP等级
				private int m_t_level; // VIP等级需求 
				public int t_level{get{return m_t_level;} set{m_t_level = value;}} // VIP等级需求 
				private int m_t_loop_type; // 循环条件（0：天，1：月） 
				public int t_loop_type{get{return m_t_loop_type;} set{m_t_loop_type = value;}} // 循环条件（0：天，1：月） 
				private int m_t_loop_arg; // 循环参数 
				public int t_loop_arg{get{return m_t_loop_arg;} set{m_t_loop_arg = value;}} // 循环参数 
				public string t_open_time {get; set;}   // 开启时间(xxxx-xx-xx xx:xx:xx)
				public string t_over_time {get; set;}   // 结束时间(xxxx-xx-xx xx:xx:xx)
				public string t_close_time {get; set;}   // 活动关闭时间(xxxx-xx-xx xx:xx:xx)
				private int m_t_server_days; // 开启需求服务器开服大于等于N天 
				public int t_server_days{get{return m_t_server_days;} set{m_t_server_days = value;}} // 开启需求服务器开服大于等于N天 
				private int m_t_version; // 版本 
				public int t_version{get{return m_t_version;} set{m_t_version = value;}} // 版本 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_condition = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_level = XBuffer.ReadInt(data, ref offset);
					m_t_loop_type = XBuffer.ReadInt(data, ref offset);
					m_t_loop_arg = XBuffer.ReadInt(data, ref offset);
					t_open_time = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_over_time = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_close_time = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_server_days = XBuffer.ReadInt(data, ref offset);
					m_t_version = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


