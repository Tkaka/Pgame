/**
 * Auto generated, do not edit it
 *
 * t_sensitive_word
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_sensitive_wordBean : BaseBin
	{
				private int m_t_id; // id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id 
				public string t_name {get; set;}   // 敏感词
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


