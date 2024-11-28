/**
 * Auto generated, do not edit it
 *
 * t_guild_battle_exchange
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_guild_battle_exchangeBean : BaseBin
	{
				private int m_t_id; // id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id 
				private int m_t_item_id; // 道具id 
				public int t_item_id{get{return m_t_item_id;} set{m_t_item_id = value;}} // 道具id 
				private int m_t_item_num; // 道具数量 
				public int t_item_num{get{return m_t_item_num;} set{m_t_item_num = value;}} // 道具数量 
				private int m_t_cost_id; // 消耗道具id 
				public int t_cost_id{get{return m_t_cost_id;} set{m_t_cost_id = value;}} // 消耗道具id 
				private int m_t_cost_num; // 消耗道具数量 
				public int t_cost_num{get{return m_t_cost_num;} set{m_t_cost_num = value;}} // 消耗道具数量 
				public string t_critical_rate {get; set;}   // 暴击率（倍数+权重），分号隔开
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_item_id = XBuffer.ReadInt(data, ref offset);
					m_t_item_num = XBuffer.ReadInt(data, ref offset);
					m_t_cost_id = XBuffer.ReadInt(data, ref offset);
					m_t_cost_num = XBuffer.ReadInt(data, ref offset);
					t_critical_rate = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


