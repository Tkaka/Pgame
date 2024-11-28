/**
 * Auto generated, do not edit it
 *
 * t_aoyi_list
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_aoyi_listBean : BaseBin
	{
				private int m_t_id; // id（品阶） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id（品阶） 
				public string t_id_list {get; set;}   // id列表
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_id_list = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


