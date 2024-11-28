/**
 * Auto generated, do not edit it
 *
 * t_pet
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_petBean : BaseBin
	{
				private int m_t_id; // Id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // Id 
				private string m_t_name;   // 名字语言ID
				public string t_name
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_name, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_name;
                        }
                        else
                            return m_t_name;
                    }
                    set { m_t_name = value; }
                } 
				private int m_t_fragment_id; // 本宠物碎片Id 
				public int t_fragment_id{get{return m_t_fragment_id;} set{m_t_fragment_id = value;}} // 本宠物碎片Id 
				private string m_t_desc1;   // 定位描述语言文字ID
				public string t_desc1
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_desc1, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_desc1;
                        }
                        else
                            return m_t_desc1;
                    }
                    set { m_t_desc1 = value; }
                } 
				private string m_t_desc2;   // 简介语言文字ID
				public string t_desc2
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_desc2, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_desc2;
                        }
                        else
                            return m_t_desc2;
                    }
                    set { m_t_desc2 = value; }
                } 
				private int m_t_ifadd; // 是否开放 
				public int t_ifadd{get{return m_t_ifadd;} set{m_t_ifadd = value;}} // 是否开放 
				public string t_icon {get; set;}   // 头像
				public string t_city_prefab {get; set;}   // 主城模型
				public string t_star_xingtai {get; set;}   // 形态对应星级
				public string t_battle_prefab {get; set;}   // 战斗模型
				private int m_t_type; // 类型 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 类型 
				public string t_race {get; set;}   // 种族
				private int m_t_raceiselement; // 种族是否元素系（1=元素系对应女性，0=非元素系对应男性） 
				public int t_raceiselement{get{return m_t_raceiselement;} set{m_t_raceiselement = value;}} // 种族是否元素系（1=元素系对应女性，0=非元素系对应男性） 
				private int m_t_zizhi; // 资质 
				public int t_zizhi{get{return m_t_zizhi;} set{m_t_zizhi = value;}} // 资质 
				private int m_t_atk_fix; // 攻击修正 
				public int t_atk_fix{get{return m_t_atk_fix;} set{m_t_atk_fix = value;}} // 攻击修正 
				private int m_t_def_fix; // 防御修正 
				public int t_def_fix{get{return m_t_def_fix;} set{m_t_def_fix = value;}} // 防御修正 
				private int m_t_hp_fix; // 生命修正 
				public int t_hp_fix{get{return m_t_hp_fix;} set{m_t_hp_fix = value;}} // 生命修正 
				private int m_t_hecheng_star; // 合成星级 
				public int t_hecheng_star{get{return m_t_hecheng_star;} set{m_t_hecheng_star = value;}} // 合成星级 
				private int m_t_atk; // 初始攻击 
				public int t_atk{get{return m_t_atk;} set{m_t_atk = value;}} // 初始攻击 
				private int m_t_def; // 初始防御 
				public int t_def{get{return m_t_def;} set{m_t_def = value;}} // 初始防御 
				private int m_t_hp; // 初始生命 
				public int t_hp{get{return m_t_hp;} set{m_t_hp = value;}} // 初始生命 
				private int m_t_attack_rale; // 攻击率 
				public int t_attack_rale{get{return m_t_attack_rale;} set{m_t_attack_rale = value;}} // 攻击率 
				private int m_t_defence_rale; // 防御率 
				public int t_defence_rale{get{return m_t_defence_rale;} set{m_t_defence_rale = value;}} // 防御率 
				private int m_t_baoji; // 初始暴击率 
				public int t_baoji{get{return m_t_baoji;} set{m_t_baoji = value;}} // 初始暴击率 
				private int m_t_anti_baoji; // 初始抗暴击率 
				public int t_anti_baoji{get{return m_t_anti_baoji;} set{m_t_anti_baoji = value;}} // 初始抗暴击率 
				private int m_t_baoji_strength; // 初始暴击强度 
				public int t_baoji_strength{get{return m_t_baoji_strength;} set{m_t_baoji_strength = value;}} // 初始暴击强度 
				private int m_t_gedang; // 初始格挡率 
				public int t_gedang{get{return m_t_gedang;} set{m_t_gedang = value;}} // 初始格挡率 
				private int m_t_poji; // 初始破击率 
				public int t_poji{get{return m_t_poji;} set{m_t_poji = value;}} // 初始破击率 
				private int m_t_gedang_strength; // 初始格挡强度 
				public int t_gedang_strength{get{return m_t_gedang_strength;} set{m_t_gedang_strength = value;}} // 初始格挡强度 
				private int m_t_shanghai; // 初始伤害率 
				public int t_shanghai{get{return m_t_shanghai;} set{m_t_shanghai = value;}} // 初始伤害率 
				private int m_t_mianshang; // 初始免伤率 
				public int t_mianshang{get{return m_t_mianshang;} set{m_t_mianshang = value;}} // 初始免伤率 
				public string t_soul_detail_type {get; set;}   // 战魂ID
				private int m_t_if_boss; // 是否boss 
				public int t_if_boss{get{return m_t_if_boss;} set{m_t_if_boss = value;}} // 是否boss 
				public string t_skill_shengji {get; set;}   // 技能升级消耗缩放比例（使用时除以10000）
				private int m_t_starUp_type; // 宠物升星类别 
				public int t_starUp_type{get{return m_t_starUp_type;} set{m_t_starUp_type = value;}} // 宠物升星类别 
				private string m_t_normal_names;   // 装备未觉醒时的名字id
				public string t_normal_names
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_normal_names, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_normal_names;
                        }
                        else
                            return m_t_normal_names;
                    }
                    set { m_t_normal_names = value; }
                } 
				private string m_t_awaken_names;   // 装备觉醒后的名字id
				public string t_awaken_names
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_awaken_names, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_awaken_names;
                        }
                        else
                            return m_t_awaken_names;
                    }
                    set { m_t_awaken_names = value; }
                } 
				public string t_commet_equip_id {get; set;}   // 普通装备ID
				public string t_normal_icons {get; set;}   // 装备觉醒前图标id
				public string t_awaken_icons {get; set;}   // 装备觉醒后图标列表
				public string t_weap_star_id_nums {get; set;}   // 武器升星所需道具id与数量与觉醒石数量
				public string t_badge_star_id {get; set;}   // 徽章升星所需特殊道具id与特殊道具数量与觉醒石数量
				public string t_book_star_nums {get; set;}   // 秘籍升星所需特殊道具id与数量与觉醒石数量
				private int m_t_equip_grow_type; // 装备成长类型1-10 
				public int t_equip_grow_type{get{return m_t_equip_grow_type;} set{m_t_equip_grow_type = value;}} // 装备成长类型1-10 
				public string t_fetter {get; set;}   // 羁绊id
				private int m_t_bd_grow_skill_id; // 该宠物的强化被动技能ID 
				public int t_bd_grow_skill_id{get{return m_t_bd_grow_skill_id;} set{m_t_bd_grow_skill_id = value;}} // 该宠物的强化被动技能ID 
				private int m_t_team; // 宠物所属战队id 
				public int t_team{get{return m_t_team;} set{m_t_team = value;}} // 宠物所属战队id 
				public string t_qipao {get; set;}   // 名人堂宠物的气泡语言包id
				public string t_aoyi {get; set;}   // 奥义列表
				public string t_init_skillID {get; set;}   // 宠物的初始技能（1-6栏位）
				private int m_t_chouka_cardID; // 抽卡获得整卡ID 
				public int t_chouka_cardID{get{return m_t_chouka_cardID;} set{m_t_chouka_cardID = value;}} // 抽卡获得整卡ID 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_fragment_id = XBuffer.ReadInt(data, ref offset);
					t_desc1 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_desc2 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_ifadd = XBuffer.ReadInt(data, ref offset);
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_city_prefab = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_star_xingtai = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_battle_prefab = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_type = XBuffer.ReadInt(data, ref offset);
					t_race = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_raceiselement = XBuffer.ReadInt(data, ref offset);
					m_t_zizhi = XBuffer.ReadInt(data, ref offset);
					m_t_atk_fix = XBuffer.ReadInt(data, ref offset);
					m_t_def_fix = XBuffer.ReadInt(data, ref offset);
					m_t_hp_fix = XBuffer.ReadInt(data, ref offset);
					m_t_hecheng_star = XBuffer.ReadInt(data, ref offset);
					m_t_atk = XBuffer.ReadInt(data, ref offset);
					m_t_def = XBuffer.ReadInt(data, ref offset);
					m_t_hp = XBuffer.ReadInt(data, ref offset);
					m_t_attack_rale = XBuffer.ReadInt(data, ref offset);
					m_t_defence_rale = XBuffer.ReadInt(data, ref offset);
					m_t_baoji = XBuffer.ReadInt(data, ref offset);
					m_t_anti_baoji = XBuffer.ReadInt(data, ref offset);
					m_t_baoji_strength = XBuffer.ReadInt(data, ref offset);
					m_t_gedang = XBuffer.ReadInt(data, ref offset);
					m_t_poji = XBuffer.ReadInt(data, ref offset);
					m_t_gedang_strength = XBuffer.ReadInt(data, ref offset);
					m_t_shanghai = XBuffer.ReadInt(data, ref offset);
					m_t_mianshang = XBuffer.ReadInt(data, ref offset);
					t_soul_detail_type = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_if_boss = XBuffer.ReadInt(data, ref offset);
					t_skill_shengji = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_starUp_type = XBuffer.ReadInt(data, ref offset);
					t_normal_names = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_awaken_names = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_commet_equip_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_normal_icons = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_awaken_icons = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_weap_star_id_nums = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_badge_star_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_book_star_nums = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_equip_grow_type = XBuffer.ReadInt(data, ref offset);
					t_fetter = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_bd_grow_skill_id = XBuffer.ReadInt(data, ref offset);
					m_t_team = XBuffer.ReadInt(data, ref offset);
					t_qipao = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_aoyi = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_init_skillID = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_chouka_cardID = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


