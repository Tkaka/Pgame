/**
 * Auto generated, do not edit it
 *
 * t_language
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_languageBean : BaseBin
	{
		private int m_t_id; // 语言包Id 
		public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 语言包Id 
		public string t_content {get; set;}   // 语言包
		
		public void LoadData(byte[] data, ref int offset)
		{
			m_t_id = XBuffer.ReadInt(data, ref offset);
			t_content = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


