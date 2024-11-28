/**
 * Auto generated, do not edit it
 *
 * t_activity_act
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_activity_actBean : BaseBin
	{
				private int m_t_id; // ID（类型*100+难度 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID（类型*100+难度 
				private int m_t_type; // 副本类型1=金币副本 2=经验副本 3=女格斗家限定 4=幻想挑战 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 副本类型1=金币副本 2=经验副本 3=女格斗家限定 4=幻想挑战 
				private int m_t_difficulty; // 副本难度1=简单 2=普通 3=困难 4=大师 5=深渊 6=苦痛 
				public int t_difficulty{get{return m_t_difficulty;} set{m_t_difficulty = value;}} // 副本难度1=简单 2=普通 3=困难 4=大师 5=深渊 6=苦痛 
				private int m_t_level_limit; // 限制等级 
				public int t_level_limit{get{return m_t_level_limit;} set{m_t_level_limit = value;}} // 限制等级 
				public string t_pet_limit {get; set;}   // 限定宝贝条件（宝贝特性.不填不限制）1=xx,2=xx
				public string t_scene {get; set;}   // 场景
				public string t_spawner {get; set;}   // 刷怪点
				public string t_wave_monster1 {get; set;}   // 第一波刷怪信息      900+类型1位+难度1位+怪物id2位  
				public string t_wave_monster2 {get; set;}   // 第二波刷怪信息
				public string t_wave_monster3 {get; set;}   // 第三波刷怪信息
				public string t_drop_show {get; set;}   // 可能掉落道具ID列表（展示用）
				public string t_drop_id {get; set;}   // 掉落ID
				public string t_monster_name {get; set;}   // 怪物头像名称语言ID列表
				public string t_monster_icon {get; set;}   // 怪物头像ID列表
				private int m_t_priority_value1; // 第一波怪物先手值列表 
				public int t_priority_value1{get{return m_t_priority_value1;} set{m_t_priority_value1 = value;}} // 第一波怪物先手值列表 
				private int m_t_priority_value2; // 第二波怪物先手值列表 
				public int t_priority_value2{get{return m_t_priority_value2;} set{m_t_priority_value2 = value;}} // 第二波怪物先手值列表 
				private int m_t_priority_value3; // 第三波怪物先手值列表 
				public int t_priority_value3{get{return m_t_priority_value3;} set{m_t_priority_value3 = value;}} // 第三波怪物先手值列表 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					m_t_difficulty = XBuffer.ReadInt(data, ref offset);
					m_t_level_limit = XBuffer.ReadInt(data, ref offset);
					t_pet_limit = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_scene = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_spawner = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_wave_monster1 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_wave_monster2 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_wave_monster3 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_drop_show = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_drop_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_monster_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_monster_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_priority_value1 = XBuffer.ReadInt(data, ref offset);
					m_t_priority_value2 = XBuffer.ReadInt(data, ref offset);
					m_t_priority_value3 = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


