/**
 * Auto generated, do not edit it
 *
 * t_trial_score
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_trial_scoreBean : BaseBin
	{
				private int m_t_id; // ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID 
				private int m_t_score; // 积分 
				public int t_score{get{return m_t_score;} set{m_t_score = value;}} // 积分 
				public string t_award {get; set;}   // 奖励
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_score = XBuffer.ReadInt(data, ref offset);
					t_award = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


