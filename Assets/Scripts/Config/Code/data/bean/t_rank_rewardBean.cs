/**
 * Auto generated, do not edit it
 *
 * t_rank_reward
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_rank_rewardBean : BaseBin
	{
				private int m_t_id; // ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID 
				private int m_t_begin; // 奖励区间起始 
				public int t_begin{get{return m_t_begin;} set{m_t_begin = value;}} // 奖励区间起始 
				private int m_t_end; // 奖励区间结束 
				public int t_end{get{return m_t_end;} set{m_t_end = value;}} // 奖励区间结束 
				public string t_reward {get; set;}   // 奖励道具id+数量；分隔
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_begin = XBuffer.ReadInt(data, ref offset);
					m_t_end = XBuffer.ReadInt(data, ref offset);
					t_reward = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


