/**
 * Auto generated, do not edit it
 *
 * t_integral_reward
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_integral_rewardBean : BaseBin
	{
				private int m_t_id; // ID就是积分 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID就是积分 
				public string t_reward {get; set;}   // 奖励（道具id+数量；分隔）
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_reward = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


