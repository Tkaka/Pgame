/**
 * Auto generated, do not edit it
 *
 * t_open_fight_rank
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_open_fight_rankBean : BaseBin
	{
				private int m_t_id; // id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id 
				private int m_t_rank_low; // 排名低 
				public int t_rank_low{get{return m_t_rank_low;} set{m_t_rank_low = value;}} // 排名低 
				private int m_t_rank_high; // 排名高 
				public int t_rank_high{get{return m_t_rank_high;} set{m_t_rank_high = value;}} // 排名高 
				private int m_t_fight_power; // 战力需求 
				public int t_fight_power{get{return m_t_fight_power;} set{m_t_fight_power = value;}} // 战力需求 
				public string t_reward {get; set;}   // 奖励
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_rank_low = XBuffer.ReadInt(data, ref offset);
					m_t_rank_high = XBuffer.ReadInt(data, ref offset);
					m_t_fight_power = XBuffer.ReadInt(data, ref offset);
					t_reward = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


