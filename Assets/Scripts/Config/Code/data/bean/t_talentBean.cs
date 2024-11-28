/**
 * Auto generated, do not edit it
 *
 * t_talent
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_talentBean : BaseBin
	{
				private int m_t_id; // 天赋ID，天赋页*100+天赋序号 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 天赋ID，天赋页*100+天赋序号 
				private string m_t_name;   // 天赋名称
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
				private int m_t_open_level; // 天赋开启要求战队等级 
				public int t_open_level{get{return m_t_open_level;} set{m_t_open_level = value;}} // 天赋开启要求战队等级 
				private int m_t_befor; // 前置天赋ID 
				public int t_befor{get{return m_t_befor;} set{m_t_befor = value;}} // 前置天赋ID 
				private int m_t_befor_point; // 前置要求上一个天赋等级 
				public int t_befor_point{get{return m_t_befor_point;} set{m_t_befor_point = value;}} // 前置要求上一个天赋等级 
				private int m_t_level_max; // 天赋等级上限 
				public int t_level_max{get{return m_t_level_max;} set{m_t_level_max = value;}} // 天赋等级上限 
				public string t_condition {get; set;}   // 属性加成范围（2位：第一位：1=前排、2=后排、3=全体；第二位：1=攻、2=防、3=技4=全体;第三位：0=全部1=竞技场2=拳皇争霸3=社团战(第3位不为0，则天赋效果走技能)）
				public string t_property {get; set;}   // 天赋升级加成属性列表(属性ID+属性类型(2=值3=万分比)+（属性值）;
				public string t_money {get; set;}   // 天赋升级需求金币（1+2+3+...）
				public string t_point {get; set;}   // 天赋升级需求天赋点（1+2+3+..）
				public string t_icon {get; set;}   // 图标
				public string t_skillId {get; set;}   // 天赋对应的效果的ID，多个效果的ID，用+链接
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_open_level = XBuffer.ReadInt(data, ref offset);
					m_t_befor = XBuffer.ReadInt(data, ref offset);
					m_t_befor_point = XBuffer.ReadInt(data, ref offset);
					m_t_level_max = XBuffer.ReadInt(data, ref offset);
					t_condition = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_property = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_money = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_point = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_skillId = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


