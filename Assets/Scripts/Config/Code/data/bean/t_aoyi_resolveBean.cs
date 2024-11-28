/**
 * Auto generated, do not edit it
 *
 * t_aoyi_resolve
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_aoyi_resolveBean : BaseBin
	{
				private int m_t_id; // id（品质） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id（品质） 
				public string t_resolve {get; set;}   // 分解获得（道具id+数量;...）
				private int m_t_gold_back; // 金币返还率（万分比） 
				public int t_gold_back{get{return m_t_gold_back;} set{m_t_gold_back = value;}} // 金币返还率（万分比） 
				private int m_t_item_back; // 道具返还率（万分比） 
				public int t_item_back{get{return m_t_item_back;} set{m_t_item_back = value;}} // 道具返还率（万分比） 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_resolve = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_gold_back = XBuffer.ReadInt(data, ref offset);
					m_t_item_back = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


