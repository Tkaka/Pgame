/**
 * Auto generated, do not edit it
 *
 * t_equip_color_up_property
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_equip_color_up_propertyBean : BaseBin
	{
				private int m_t_id; // ID（基础值ID*100+ 品阶) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID（基础值ID*100+ 品阶) 
				public string t_lv_up_property {get; set;}   // 当前品每升1级增加属性（属性id+值(放大一万倍)）
				public string t_sum_property {get; set;}   // 当前品基础总属性（属性id+类型+值(放大一万倍)）
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_lv_up_property = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_sum_property = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


