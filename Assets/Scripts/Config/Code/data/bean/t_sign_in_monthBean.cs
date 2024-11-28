/**
 * Auto generated, do not edit it
 *
 * t_sign_in_month
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_sign_in_monthBean : BaseBin
	{
				private int m_t_id; // id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id 
				private int m_t_fix_id; // 固定的道具id 
				public int t_fix_id{get{return m_t_fix_id;} set{m_t_fix_id = value;}} // 固定的道具id 
				private int m_t_fix_num; // 固定的道具数量 
				public int t_fix_num{get{return m_t_fix_num;} set{m_t_fix_num = value;}} // 固定的道具数量 
				private int m_t_vip_double; // 双倍的vip等级 
				public int t_vip_double{get{return m_t_vip_double;} set{m_t_vip_double = value;}} // 双倍的vip等级 
				private int m_t_replace; // 是否要被替换(1:是) 
				public int t_replace{get{return m_t_replace;} set{m_t_replace = value;}} // 是否要被替换(1:是) 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_fix_id = XBuffer.ReadInt(data, ref offset);
					m_t_fix_num = XBuffer.ReadInt(data, ref offset);
					m_t_vip_double = XBuffer.ReadInt(data, ref offset);
					m_t_replace = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


