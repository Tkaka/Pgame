/**
 * Auto generated, do not edit it
 *
 * t_donate_reward
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_donate_rewardBean : BaseBin
	{
				private int m_t_id; // id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id 
				public string t_comsume_item {get; set;}   // 道具ID+数量
				public string t_reward {get; set;}   // 奖励（经验+社团币数量+贡献）
				public string t_bgIocn {get; set;}   // 背景图
				private int m_t_openCondition; // 开启条件（训练师等级） 
				public int t_openCondition{get{return m_t_openCondition;} set{m_t_openCondition = value;}} // 开启条件（训练师等级） 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_comsume_item = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_reward = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_bgIocn = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_openCondition = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


