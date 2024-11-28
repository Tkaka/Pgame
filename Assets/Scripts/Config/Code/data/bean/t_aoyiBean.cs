/**
 * Auto generated, do not edit it
 *
 * t_aoyi
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_aoyiBean : BaseBin
	{
				private int m_t_id; // id(道具ID) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id(道具ID) 
				private int m_t_type; // 类型(1攻防 2攻血 3防血 4伤 5免) 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 类型(1攻防 2攻血 3防血 4伤 5免) 
				private int m_t_dic; // -1=拳，-2=脚，3=右，4=右下，5=下，6=左下，7=左 
				public int t_dic{get{return m_t_dic;} set{m_t_dic = value;}} // -1=拳，-2=脚，3=右，4=右下，5=下，6=左下，7=左 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					m_t_dic = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


