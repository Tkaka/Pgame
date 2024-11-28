/**
 * Auto generated, do not edit it
 *
 * t_open_fight_reward
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_open_fight_rewardBean : BaseBin
	{
				private int m_t_id; // ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID 
				private int m_t_finish_condition; // 完成条件(战力大于N） 
				public int t_finish_condition{get{return m_t_finish_condition;} set{m_t_finish_condition = value;}} // 完成条件(战力大于N） 
				public string t_reward {get; set;}   // 道具奖励
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_finish_condition = XBuffer.ReadInt(data, ref offset);
					t_reward = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


