/**
 * Auto generated, do not edit it
 *
 * t_player_filter
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_player_filterBean : BaseBin
	{
				private int m_t_id; // Id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // Id 
				public string t_content {get; set;}   // 过滤内容
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_content = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


