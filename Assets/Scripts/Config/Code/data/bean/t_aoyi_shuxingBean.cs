/**
 * Auto generated, do not edit it
 *
 * t_aoyi_shuxing
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_aoyi_shuxingBean : BaseBin
	{
				private int m_t_id; // id(type*10+品质) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id(type*10+品质) 
				private int m_t_type; // 类型（1攻防 2攻血 3防血 4伤 5免） 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 类型（1攻防 2攻血 3防血 4伤 5免） 
				public string t_property {get; set;}   // 基本属性（属性id+值;属性id+值;...）
				public string t_rate {get; set;}   // 成长值（1;2;...(和属性个数对应)）
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					t_property = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_rate = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


