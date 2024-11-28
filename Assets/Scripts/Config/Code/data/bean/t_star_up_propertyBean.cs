/**
 * Auto generated, do not edit it
 *
 * t_star_up_property
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_star_up_propertyBean : BaseBin
	{
				private int m_t_id; // ID（装备类别*100+星级) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID（装备类别*100+星级) 
				public string t_sum_property {get; set;}   // 当前星总属性（属性id+类型+值）
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_sum_property = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


