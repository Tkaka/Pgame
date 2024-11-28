/**
 * Auto generated, do not edit it
 *
 * t_hof_level_up
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_hof_level_upBean : BaseBin
	{
				private int m_t_id; // ID（品阶） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID（品阶） 
				private string m_t_name;   // 本好感度的名字语言包id
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
				public string t_icon {get; set;}   // 本好感度的图片数据
				public string t_attr_atk {get; set;}   // 攻类型属性(1=攻击,2防御，3=生命；4=暴击率；5=抗暴率；7=格挡率；8=破击率；10=伤害率；11=免伤率）
				public string t_attr_def {get; set;}   // 防类型属性(1=攻击,2防御，3=生命；4=暴击率；5=抗暴率；7=格挡率；8=破击率；10=伤害率；11=免伤率）
				public string t_attr_skill {get; set;}   // 技类型属性(1=攻击,2防御，3=生命；4=暴击率；5=抗暴率；7=格挡率；8=破击率；10=伤害率；11=免伤率）
				private int m_t_priority; // 升品额外先手值 
				public int t_priority{get{return m_t_priority;} set{m_t_priority = value;}} // 升品额外先手值 
				private int m_t_color_base_exp; // 等级初始值 
				public int t_color_base_exp{get{return m_t_color_base_exp;} set{m_t_color_base_exp = value;}} // 等级初始值 
				private int m_t_exp_add; // 每5级等级经验增加值 
				public int t_exp_add{get{return m_t_exp_add;} set{m_t_exp_add = value;}} // 每5级等级经验增加值 
				private int m_t_level_count; // 品阶包含等级 
				public int t_level_count{get{return m_t_level_count;} set{m_t_level_count = value;}} // 品阶包含等级 
				private int m_t_class_priority; // 本好感度单个等级加成的先手值 
				public int t_class_priority{get{return m_t_class_priority;} set{m_t_class_priority = value;}} // 本好感度单个等级加成的先手值 
				public string t_present_item {get; set;}   // 升级到本好感度获得的道具
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_attr_atk = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_attr_def = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_attr_skill = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_priority = XBuffer.ReadInt(data, ref offset);
					m_t_color_base_exp = XBuffer.ReadInt(data, ref offset);
					m_t_exp_add = XBuffer.ReadInt(data, ref offset);
					m_t_level_count = XBuffer.ReadInt(data, ref offset);
					m_t_class_priority = XBuffer.ReadInt(data, ref offset);
					t_present_item = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


