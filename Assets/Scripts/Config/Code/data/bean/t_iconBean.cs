/**
 * Auto generated, do not edit it
 *
 * t_icon
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_iconBean : BaseBin
	{
				private int m_t_id; // id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id 
				public string t_icon {get; set;}   // 图标ui
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


