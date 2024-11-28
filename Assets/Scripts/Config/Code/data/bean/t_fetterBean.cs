/**
 * Auto generated, do not edit it
 *
 * t_fetter
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_fetterBean : BaseBin
	{
				private int m_t_id; // Id宠物id* 100 + 羁绊类型 * 10 + id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // Id宠物id* 100 + 羁绊类型 * 10 + id 
				private int m_t_type; // 羁绊类型：1宝贝羁绊，2=武器羁绊，3=徽章羁绊，4秘籍羁绊 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 羁绊类型：1宝贝羁绊，2=武器羁绊，3=徽章羁绊，4秘籍羁绊 
				public string t_condition {get; set;}   // 羁绊条件参数，激活羁绊所需的条件id，宠物就填宠物id，装备觉醒填觉醒后装备名字语言包id，多个以加号连接
				public string t_propety_type {get; set;}   // 羁绊加成属性值类型 1 = 攻击，2 = 防御，3 = 生命， 4 = 替换技能id  +   者替换的技能id
				private string m_t_name;   // 羁绊名称语言包id
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
				private string m_t_content;   // 羁绊内容语言包id
				public string t_content
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_content, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_content;
                        }
                        else
                            return m_t_content;
                    }
                    set { m_t_content = value; }
                } 
				private string m_t_content_result;   // 羁绊属性语言包id
				public string t_content_result
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_content_result, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_content_result;
                        }
                        else
                            return m_t_content_result;
                    }
                    set { m_t_content_result = value; }
                } 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					t_condition = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_propety_type = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_content = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_content_result = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
		} 

	}
}


