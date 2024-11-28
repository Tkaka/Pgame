/**
 * Auto generated, do not edit it
 *
 * t_special_equip_attr
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_special_equip_attrBean : BaseBin
	{
				private int m_t_id; // ID（品阶*100+类别） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID（品阶*100+类别） 
				private int m_t_add_v_list; // 当前品每级增加属性值列表 
				public int t_add_v_list{get{return m_t_add_v_list;} set{m_t_add_v_list = value;}} // 当前品每级增加属性值列表 
				private int m_t_base_v_list; // 当前品本身属性值列表 
				public int t_base_v_list{get{return m_t_base_v_list;} set{m_t_base_v_list = value;}} // 当前品本身属性值列表 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_add_v_list = XBuffer.ReadInt(data, ref offset);
					m_t_base_v_list = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


