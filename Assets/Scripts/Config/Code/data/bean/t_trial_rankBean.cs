/**
 * Auto generated, do not edit it
 *
 * t_trial_rank
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_trial_rankBean : BaseBin
	{
				private int m_t_id; // ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID 
				private int m_t_rank_from; // 排名起 
				public int t_rank_from{get{return m_t_rank_from;} set{m_t_rank_from = value;}} // 排名起 
				private int m_t_rank_end; // 排名止 
				public int t_rank_end{get{return m_t_rank_end;} set{m_t_rank_end = value;}} // 排名止 
				public string t_award {get; set;}   // 奖励
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_rank_from = XBuffer.ReadInt(data, ref offset);
					m_t_rank_end = XBuffer.ReadInt(data, ref offset);
					t_award = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


