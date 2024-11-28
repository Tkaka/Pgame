/**
 * Auto generated, do not edit it
 *
 * t_aoyi_page
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_aoyi_pageBean : BaseBin
	{
				private int m_t_id; // id(页*10+格子id) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id(页*10+格子id) 
				private int m_t_page; // 页(1初级 2中级 3究极) 
				public int t_page{get{return m_t_page;} set{m_t_page = value;}} // 页(1初级 2中级 3究极) 
				private int m_t_grid; // 格子id 
				public int t_grid{get{return m_t_grid;} set{m_t_grid = value;}} // 格子id 
				private int m_t_level_limit; // 等级限制 
				public int t_level_limit{get{return m_t_level_limit;} set{m_t_level_limit = value;}} // 等级限制 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_page = XBuffer.ReadInt(data, ref offset);
					m_t_grid = XBuffer.ReadInt(data, ref offset);
					m_t_level_limit = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


