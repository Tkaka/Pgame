/**
 * Auto generated, do not edit it
 *
 * t_donate_type
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_donate_typeBean : BaseBin
	{
				private int m_t_id; // 类型(1社团实验室 2玩法 3红包) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 类型(1社团实验室 2玩法 3红包) 
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
				private int m_t_level; // 开放等级（公会等级） 
				public int t_level{get{return m_t_level;} set{m_t_level = value;}} // 开放等级（公会等级） 
				public string t_bg_icon {get; set;}   // 背景图
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
				public string t_title_icon {get; set;}   // 标题图标
				public string t_content {get; set;}   // 捐献列表(2公会成员数量 3工会精英数量 4金币加成 5钻石加成 6神器之源)
				public string t_detail_name {get; set;}   // 内容名字(语言包ID+语言包ID)
				public string t_detail_desc {get; set;}   // 内容描述(语言包ID+语言包ID)
				public string t_detail_icon {get; set;}   // 内容标题图标(图标+图标)
				public string t_detai_level {get; set;}   // 开启公会等级
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_level = XBuffer.ReadInt(data, ref offset);
					t_bg_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_desc = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_title_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_content = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_detail_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_detail_desc = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_detail_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_detai_level = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


