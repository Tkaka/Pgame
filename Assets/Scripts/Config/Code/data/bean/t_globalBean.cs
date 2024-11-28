/**
 * Auto generated, do not edit it
 *
 * t_global
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_globalBean : BaseBin
	{
				private int m_t_id; // Id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // Id 
				private int m_t_int_param; // 整型参数 
				public int t_int_param{get{return m_t_int_param;} set{m_t_int_param = value;}} // 整型参数 
				public string t_string_param {get; set;}   // 字符串参数
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_int_param = XBuffer.ReadInt(data, ref offset);
					t_string_param = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


