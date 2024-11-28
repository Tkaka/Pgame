/**
 * Auto generated, do not edit it
 *
 * t_library
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_libraryBean : BaseBin
	{
				private int m_t_id; // id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id 
				private int m_t_level; // 宝贝等级 
				public int t_level{get{return m_t_level;} set{m_t_level = value;}} // 宝贝等级 
				private int m_t_color; // 品阶id 
				public int t_color{get{return m_t_color;} set{m_t_color = value;}} // 品阶id 
				private int m_t_star; // 星级加成 
				public int t_star{get{return m_t_star;} set{m_t_star = value;}} // 星级加成 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_level = XBuffer.ReadInt(data, ref offset);
					m_t_color = XBuffer.ReadInt(data, ref offset);
					m_t_star = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


