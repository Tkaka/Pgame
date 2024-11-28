/**
 * Auto generated, do not edit it
 *
 * t_skill
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_skillBean : BaseBin
	{
				private int m_t_id; // 技能ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 技能ID 
				private string m_t_name;   // 技能名字
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
				private int m_t_type; // 技能类别 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 技能类别 
				private string m_t_type_name;   // 技能类型名字
				public string t_type_name
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_type_name, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_type_name;
                        }
                        else
                            return m_t_type_name;
                    }
                    set { m_t_type_name = value; }
                } 
				public string t_icon {get; set;}   // 技能图片
				private string m_t_describe;   // 技能介绍语言包id
				public string t_describe
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_describe, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_describe;
                        }
                        else
                            return m_t_describe;
                    }
                    set { m_t_describe = value; }
                } 
				private string m_t_desc;   // 大招简介语言文字ID
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
				private int m_t_cost_mp; // 技能条件 
				public int t_cost_mp{get{return m_t_cost_mp;} set{m_t_cost_mp = value;}} // 技能条件 
				private int m_t_condition_skillId; // 条件技能ID 
				public int t_condition_skillId{get{return m_t_condition_skillId;} set{m_t_condition_skillId = value;}} // 条件技能ID 
				private int m_t_main_effect_id; // 技能主效果id 
				public int t_main_effect_id{get{return m_t_main_effect_id;} set{m_t_main_effect_id = value;}} // 技能主效果id 
				public string t_extra_effect_id {get; set;}   //  效果id (额外伤害效果+小招和绝技的额外效果)
				public string t_bd_effect_id {get; set;}   //  效果id (被动效果)
				private string m_t_effect_str;   // 效果描述语言包ID
				public string t_effect_str
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_effect_str, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_effect_str;
                        }
                        else
                            return m_t_effect_str;
                    }
                    set { m_t_effect_str = value; }
                } 
				public string t_effect_str_type {get; set;}   // 效果描述语言包ID类型ID(1 = 百分比,2=值)
				private int m_t_if_hiderole; // 是否隐藏不受击角色 
				public int t_if_hiderole{get{return m_t_if_hiderole;} set{m_t_if_hiderole = value;}} // 是否隐藏不受击角色 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_type = XBuffer.ReadInt(data, ref offset);
					t_type_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_describe = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_desc = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_cost_mp = XBuffer.ReadInt(data, ref offset);
					m_t_condition_skillId = XBuffer.ReadInt(data, ref offset);
					m_t_main_effect_id = XBuffer.ReadInt(data, ref offset);
					t_extra_effect_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_bd_effect_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_effect_str = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_effect_str_type = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_if_hiderole = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


