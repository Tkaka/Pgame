/**
 * Auto generated, do not edit it
 *
 * t_open_carnival_score
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_open_carnival_scoreBean : BaseBin
	{
				private int m_t_id; // id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id 
				private int m_t_score; // 需求积分 
				public int t_score{get{return m_t_score;} set{m_t_score = value;}} // 需求积分 
				public string t_reward {get; set;}   // 奖励
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_score = XBuffer.ReadInt(data, ref offset);
					t_reward = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


