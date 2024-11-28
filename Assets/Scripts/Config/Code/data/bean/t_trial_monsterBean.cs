/**
 * Auto generated, do not edit it
 *
 * t_trial_monster
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_trial_monsterBean : BaseBin
	{
				private int m_t_id; // ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID 
				private int m_t_level; // 宠物等级 
				public int t_level{get{return m_t_level;} set{m_t_level = value;}} // 宠物等级 
				private int m_t_star; // 宠物加成星级 
				public int t_star{get{return m_t_star;} set{m_t_star = value;}} // 宠物加成星级 
				private int m_t_color; // 宠物品阶 
				public int t_color{get{return m_t_color;} set{m_t_color = value;}} // 宠物品阶 
				public string t_soul {get; set;}   // 宠物战魂
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_level = XBuffer.ReadInt(data, ref offset);
					m_t_star = XBuffer.ReadInt(data, ref offset);
					m_t_color = XBuffer.ReadInt(data, ref offset);
					t_soul = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


