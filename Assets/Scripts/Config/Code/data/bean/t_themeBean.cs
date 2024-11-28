/**
 * Auto generated, do not edit it
 *
 * t_theme
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_themeBean : BaseBin
	{
				private int m_t_id; // 主题表 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 主题表 
				private int m_t_type; // 类型(无效果0，反弹1，技能2，相克3) 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 类型(无效果0，反弹1，技能2，相克3) 
				private string m_t_topic;   // 标题语言id
				public string t_topic
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_topic, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_topic;
                        }
                        else
                            return m_t_topic;
                    }
                    set { m_t_topic = value; }
                } 
				private string m_t_desc;   // 描述语言ID
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
				private int m_t_desc_number; // 语言标题数字部分（也是触发比，百分比） 
				public int t_desc_number{get{return m_t_desc_number;} set{m_t_desc_number = value;}} // 语言标题数字部分（也是触发比，百分比） 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					t_topic = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_desc = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_desc_number = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


