/**
 * Auto generated, do not edit it
 *
 * t_star_attach_precent
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_star_attach_precentBean : BaseBin
	{
				private int m_t_id; // ID（装备类别) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID（装备类别) 
				private int m_t_base_id; // 缩放基础值ID 
				public int t_base_id{get{return m_t_base_id;} set{m_t_base_id = value;}} // 缩放基础值ID 
				private int m_t_rate; // 缩放比例（万分比） 
				public int t_rate{get{return m_t_rate;} set{m_t_rate = value;}} // 缩放比例（万分比） 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_base_id = XBuffer.ReadInt(data, ref offset);
					m_t_rate = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


