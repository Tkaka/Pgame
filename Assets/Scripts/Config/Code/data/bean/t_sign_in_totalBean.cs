/**
 * Auto generated, do not edit it
 *
 * t_sign_in_total
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_sign_in_totalBean : BaseBin
	{
				private int m_t_id; // id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id 
				public string t_items {get; set;}   // 道具
				private int m_t_day; // 需求天数 
				public int t_day{get{return m_t_day;} set{m_t_day = value;}} // 需求天数 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_items = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_day = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


