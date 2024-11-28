/**
 * Auto generated, do not edit it
 *
 * t_achievement
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_achievementBean : BaseBin
	{
				private int m_t_id; // id（类型*100+1） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id（类型*100+1） 
				private int m_t_type; // 类型(1冒险成就，2养成成就，3活动成就) 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 类型(1冒险成就，2养成成就，3活动成就) 
				private string m_t_name;   // 名字ID
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
				private string m_t_desc;   // 描述
				public string t_desc
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_desc, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_desc;
                        }
                        else
                            return m_t_desc;
                    }
                    set { m_t_desc = value; }
                } 
				private int m_t_condition; // 条件（开放完成1，关卡星累计2，主线关卡攻打次数3，问鼎终极试炼4，累计消灭敌人数量5，发现连击奖励次数6，终极宝箱累计发现格斗家数量7，完成日常任务次数8，竞技场一回合触发4次完美次数10，非自动用灼烧击败敌方次数13，战队战斗力14，为格斗家喂美食15，付费重置商店次数16，使用万能碎片个数17，神器洗练3负次数18，铜像收集个数19，同时拥有满级战魂个数20，拥有7星格斗家个数21，累计获得的金币31） 
				public int t_condition{get{return m_t_condition;} set{m_t_condition = value;}} // 条件（开放完成1，关卡星累计2，主线关卡攻打次数3，问鼎终极试炼4，累计消灭敌人数量5，发现连击奖励次数6，终极宝箱累计发现格斗家数量7，完成日常任务次数8，竞技场一回合触发4次完美次数10，非自动用灼烧击败敌方次数13，战队战斗力14，为格斗家喂美食15，付费重置商店次数16，使用万能碎片个数17，神器洗练3负次数18，铜像收集个数19，同时拥有满级战魂个数20，拥有7星格斗家个数21，累计获得的金币31） 
				public string t_target {get; set;}   // 目标（1+2+3）
				public string t_reward {get; set;}   // 奖励成就点（1+2+3）
				public string t_icon {get; set;}   // 底图+边框
				private int m_t_level; // 等级 
				public int t_level{get{return m_t_level;} set{m_t_level = value;}} // 等级 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_desc = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_condition = XBuffer.ReadInt(data, ref offset);
					t_target = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_reward = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_level = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


