/**
 * Auto generated, do not edit it
 *
 * t_teamfight
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_teamfightBean : BaseBin
	{
				private int m_t_id; // ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID 
				private int m_t_level; // 等级上限（小于该等级对应该配置） 
				public int t_level{get{return m_t_level;} set{m_t_level = value;}} // 等级上限（小于该等级对应该配置） 
				public string t_monster_1 {get; set;}   // 组队刷新怪物ID及权重（出2个）
				public string t_monster_2 {get; set;}   // 组队出现怪物ID及权重（出4个）
				public string t_rewards {get; set;}   // 奖励道具ID+概率（万分比）+权重1+数量1+权重2+数量2
				private int m_t_fragment; // 碎片掉率（万分比） 
				public int t_fragment{get{return m_t_fragment;} set{m_t_fragment = value;}} // 碎片掉率（万分比） 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_level = XBuffer.ReadInt(data, ref offset);
					t_monster_1 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_monster_2 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_rewards = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_fragment = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


