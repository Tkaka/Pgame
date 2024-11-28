/**
 * Auto generated, do not edit it
 *
 * t_profession
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_professionBean : BaseBin
	{
				private int m_t_id; // Id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // Id 
				public string t_name {get; set;}   // 职业名字
				public string t_avatar {get; set;}   // 默认头像Icon
				public string t_city_prefab {get; set;}   // 主城模型
				public string t_battle_prefab {get; set;}   // 战斗模型
				public string t_team {get; set;}   // 初始阵容
				private int m_t_gold; // 初始金币 
				public int t_gold{get{return m_t_gold;} set{m_t_gold = value;}} // 初始金币 
				private int m_t_damond; // 初始钻石 
				public int t_damond{get{return m_t_damond;} set{m_t_damond = value;}} // 初始钻石 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_avatar = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_city_prefab = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_battle_prefab = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_team = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_gold = XBuffer.ReadInt(data, ref offset);
					m_t_damond = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


