/**
 * Auto generated, do not edit it
 *
 * t_formation
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_formationBean : BaseBin
	{
				private int m_t_id; // id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id 
				public string t_condition {get; set;}   // 满足条件（1：等级，2：战力，3：种族4：类型）
				public string t_num {get; set;}   // 上阵人数限制
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_condition = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_num = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


