/**
 * Auto generated, do not edit it
 *
 * t_pet_soul_open
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_pet_soul_openBean : BaseBin
	{
				private int m_t_id; // ID(战魂位) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID(战魂位) 
				private int m_t_color; // 宠物品阶 
				public int t_color{get{return m_t_color;} set{m_t_color = value;}} // 宠物品阶 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_color = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


