/**
 * Auto generated, do not edit it
 *
 * t_dungeon_act
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_dungeon_actBean : BaseBin
	{
				private int m_t_id; // 关卡ID（3位章节ID，2位序号） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 关卡ID（3位章节ID，2位序号） 
				private int m_t_next_id; // 下一个关卡ID 
				public int t_next_id{get{return m_t_next_id;} set{m_t_next_id = value;}} // 下一个关卡ID 
				private int m_t_pre_id; // 前置关卡ID 
				public int t_pre_id{get{return m_t_pre_id;} set{m_t_pre_id = value;}} // 前置关卡ID 
				public string t_scene {get; set;}   // 场景
				private int m_t_start_wave; // 第几个刷挂点刷怪 
				public int t_start_wave{get{return m_t_start_wave;} set{m_t_start_wave = value;}} // 第几个刷挂点刷怪 
				private int m_t_maxrounds; // 最大回合数 
				public int t_maxrounds{get{return m_t_maxrounds;} set{m_t_maxrounds = value;}} // 最大回合数 
				public string t_wave_monster1 {get; set;}   // 第一波怪物
				public string t_wave_monster2 {get; set;}   // 第二波怪物
				public string t_wave_monster3 {get; set;}   // 第三波怪物
				public string t_wave_dialog1 {get; set;}   // 第一波对白表id
				public string t_wave_dialog2 {get; set;}   // 第二波对白表id
				public string t_wave_dialog3 {get; set;}   // 第三波对白表id
				private int m_t_chapter_id; // 隶属章节（也许不需要？） 
				public int t_chapter_id{get{return m_t_chapter_id;} set{m_t_chapter_id = value;}} // 隶属章节（也许不需要？） 
				private string m_t_name_id;   // 关卡名语言包ID
				public string t_name_id
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_name_id, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_name_id;
                        }
                        else
                            return m_t_name_id;
                    }
                    set { m_t_name_id = value; }
                } 
				private string m_t_intro_id;   // 关卡引言语言包ID
				public string t_intro_id
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_intro_id, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_intro_id;
                        }
                        else
                            return m_t_intro_id;
                    }
                    set { m_t_intro_id = value; }
                } 
				private int m_t_act_type; // 副本类型（1：主线，2：精英，6：活动副本） 
				public int t_act_type{get{return m_t_act_type;} set{m_t_act_type = value;}} // 副本类型（1：主线，2：精英，6：活动副本） 
				private int m_t_difficulty; // 副本难度1=简单 2=普通 3=困难 4=大师 5=深渊 6=苦痛 
				public int t_difficulty{get{return m_t_difficulty;} set{m_t_difficulty = value;}} // 副本难度1=简单 2=普通 3=困难 4=大师 5=深渊 6=苦痛 
				private int m_t_level_limit; // 限制等级 
				public int t_level_limit{get{return m_t_level_limit;} set{m_t_level_limit = value;}} // 限制等级 
				public string t_pet_limit {get; set;}   // 限定宝贝条件（宝贝特性.不填不限制）1=元素系,2=xx
				private int m_t_type; // 关卡类型                  0普通小怪               1普通大怪               2普通终关boss       3精英小怪               4精英大怪                
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 关卡类型                  0普通小怪               1普通大怪               2普通终关boss       3精英小怪               4精英大怪                
				public string t_icon {get; set;}   // 章节地图上展示的关卡头像图片
				public string t_lihui {get; set;}   // 关卡扉页立绘
				public string t_drop_show_id {get; set;}   // 可能掉落道具ID列表（展示用）
				public string t_drop_id {get; set;}   // 关卡掉落ID
				public string t_first_drop {get; set;}   // 首次掉落
				private int m_t_drop_gold1; // 第一波金币掉落 
				public int t_drop_gold1{get{return m_t_drop_gold1;} set{m_t_drop_gold1 = value;}} // 第一波金币掉落 
				private int m_t_drop_gold2; // 第二波金币掉落 
				public int t_drop_gold2{get{return m_t_drop_gold2;} set{m_t_drop_gold2 = value;}} // 第二波金币掉落 
				private int m_t_drop_gold3; // 第三波金币掉落 
				public int t_drop_gold3{get{return m_t_drop_gold3;} set{m_t_drop_gold3 = value;}} // 第三波金币掉落 
				public string t_monster_name {get; set;}   // 怪物头像名称语言ID列表
				public string t_monster_icon {get; set;}   // 怪物头像ID列表
				private int m_t_priority_value1; // 第一波怪物先手值列表 
				public int t_priority_value1{get{return m_t_priority_value1;} set{m_t_priority_value1 = value;}} // 第一波怪物先手值列表 
				private int m_t_priority_value2; // 第二波怪物先手值列表 
				public int t_priority_value2{get{return m_t_priority_value2;} set{m_t_priority_value2 = value;}} // 第二波怪物先手值列表 
				private int m_t_priority_value3; // 第三波怪物先手值列表 
				public int t_priority_value3{get{return m_t_priority_value3;} set{m_t_priority_value3 = value;}} // 第三波怪物先手值列表 
				public string t_box_item_id {get; set;}   // 关卡宝箱道具1ID+数量1+道具1ID+数量1+道具1ID+数量1+道具1ID+数量1
				private int m_t_exp; // 关卡宝贝经验 
				public int t_exp{get{return m_t_exp;} set{m_t_exp = value;}} // 关卡宝贝经验 
				private int m_t_star_dead; // 三星评价之减员要求（不多于N个） 
				public int t_star_dead{get{return m_t_star_dead;} set{m_t_star_dead = value;}} // 三星评价之减员要求（不多于N个） 
				public string t_star_hit {get; set;}   // 三星评价之连击要求（X类型以上比例大于等于）1=一般 2=不错 3=很好 4=完美一击(大于10000时，是要求这个档次以上的数量大于等于N/10000个)
				private int m_t_boss_id; // 需要出场介绍的怪物ID 
				public int t_boss_id{get{return m_t_boss_id;} set{m_t_boss_id = value;}} // 需要出场介绍的怪物ID 
				private int m_t_comsumePower; // 挑战需消耗的体力 
				public int t_comsumePower{get{return m_t_comsumePower;} set{m_t_comsumePower = value;}} // 挑战需消耗的体力 
				private int m_t_auto_exp; // 扫荡获得总经验 
				public int t_auto_exp{get{return m_t_auto_exp;} set{m_t_auto_exp = value;}} // 扫荡获得总经验 
				private string m_t_tip;   // 打完当前关卡会开启的功能提示比如精英关卡
				public string t_tip
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_tip, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_tip;
                        }
                        else
                            return m_t_tip;
                    }
                    set { m_t_tip = value; }
                } 
				private int m_t_act_petID; // 出场记载的宠物ID 
				public int t_act_petID{get{return m_t_act_petID;} set{m_t_act_petID = value;}} // 出场记载的宠物ID 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_next_id = XBuffer.ReadInt(data, ref offset);
					m_t_pre_id = XBuffer.ReadInt(data, ref offset);
					t_scene = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_start_wave = XBuffer.ReadInt(data, ref offset);
					m_t_maxrounds = XBuffer.ReadInt(data, ref offset);
					t_wave_monster1 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_wave_monster2 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_wave_monster3 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_wave_dialog1 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_wave_dialog2 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_wave_dialog3 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_chapter_id = XBuffer.ReadInt(data, ref offset);
					t_name_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_intro_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_act_type = XBuffer.ReadInt(data, ref offset);
					m_t_difficulty = XBuffer.ReadInt(data, ref offset);
					m_t_level_limit = XBuffer.ReadInt(data, ref offset);
					t_pet_limit = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_type = XBuffer.ReadInt(data, ref offset);
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_lihui = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_drop_show_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_drop_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_first_drop = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_drop_gold1 = XBuffer.ReadInt(data, ref offset);
					m_t_drop_gold2 = XBuffer.ReadInt(data, ref offset);
					m_t_drop_gold3 = XBuffer.ReadInt(data, ref offset);
					t_monster_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_monster_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_priority_value1 = XBuffer.ReadInt(data, ref offset);
					m_t_priority_value2 = XBuffer.ReadInt(data, ref offset);
					m_t_priority_value3 = XBuffer.ReadInt(data, ref offset);
					t_box_item_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_exp = XBuffer.ReadInt(data, ref offset);
					m_t_star_dead = XBuffer.ReadInt(data, ref offset);
					t_star_hit = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_boss_id = XBuffer.ReadInt(data, ref offset);
					m_t_comsumePower = XBuffer.ReadInt(data, ref offset);
					m_t_auto_exp = XBuffer.ReadInt(data, ref offset);
					t_tip = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_act_petID = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


