/**
 * Auto generated, do not edit it
 *
 * t_monster
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_monsterBean : BaseBin
	{
				private int m_t_id; // 怪物ID(（关卡ID5位） + （1-3波1位） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 怪物ID(（关卡ID5位） + （1-3波1位） 
				private string m_t_name;   // 名字
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
				public string t_battle_prefab {get; set;}   // 战斗预制件
				private int m_t_level; // 等级 
				public int t_level{get{return m_t_level;} set{m_t_level = value;}} // 等级 
				private int m_t_type; // 类别 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 类别 
				private int m_t_race; // 种族 
				public int t_race{get{return m_t_race;} set{m_t_race = value;}} // 种族 
				private int m_t_raceiselement; // 种族是否元素系（1=元素系对应女性，0=非元素系对应男性2=既不受男性加减成影响,也不受女性加减成影响） 
				public int t_raceiselement{get{return m_t_raceiselement;} set{m_t_raceiselement = value;}} // 种族是否元素系（1=元素系对应女性，0=非元素系对应男性2=既不受男性加减成影响,也不受女性加减成影响） 
				private int m_t_atk; // 攻击 
				public int t_atk{get{return m_t_atk;} set{m_t_atk = value;}} // 攻击 
				private int m_t_def; // 防御 
				public int t_def{get{return m_t_def;} set{m_t_def = value;}} // 防御 
				private int m_t_hp; // 生命 
				public int t_hp{get{return m_t_hp;} set{m_t_hp = value;}} // 生命 
				private int m_t_shanghai_lv; // 伤害率(使用时用万分比) 
				public int t_shanghai_lv{get{return m_t_shanghai_lv;} set{m_t_shanghai_lv = value;}} // 伤害率(使用时用万分比) 
				private int m_t_mianshang_lv; // 免伤率(使用时用万分比) 
				public int t_mianshang_lv{get{return m_t_mianshang_lv;} set{m_t_mianshang_lv = value;}} // 免伤率(使用时用万分比) 
				private int m_t_if_boss; // 绝技防御率(使用时用万分比) 
				public int t_if_boss{get{return m_t_if_boss;} set{m_t_if_boss = value;}} // 绝技防御率(使用时用万分比) 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_battle_prefab = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_level = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					m_t_race = XBuffer.ReadInt(data, ref offset);
					m_t_raceiselement = XBuffer.ReadInt(data, ref offset);
					m_t_atk = XBuffer.ReadInt(data, ref offset);
					m_t_def = XBuffer.ReadInt(data, ref offset);
					m_t_hp = XBuffer.ReadInt(data, ref offset);
					m_t_shanghai_lv = XBuffer.ReadInt(data, ref offset);
					m_t_mianshang_lv = XBuffer.ReadInt(data, ref offset);
					m_t_if_boss = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


