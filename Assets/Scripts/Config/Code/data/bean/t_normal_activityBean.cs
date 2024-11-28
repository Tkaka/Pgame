/**
 * Auto generated, do not edit it
 *
 * t_normal_activity
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_normal_activityBean : BaseBin
	{
				private int m_t_id; // 活动ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 活动ID 
				private int m_t_type; // 活动类型1=公告类2=兑换类3=任务类4=月卡充值5=打折购买6=翻倍 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 活动类型1=公告类2=兑换类3=任务类4=月卡充值5=打折购买6=翻倍 
				private int m_t_loop; // 活动轮循天数 
				public int t_loop{get{return m_t_loop;} set{m_t_loop = value;}} // 活动轮循天数 
				public string t_icon {get; set;}   // 活动图标
				public string t_mark {get; set;}   // 活动角标
				private string m_t_title;   // 活动标题
				public string t_title
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_title, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_title;
                        }
                        else
                            return m_t_title;
                    }
                    set { m_t_title = value; }
                } 
				public string t_open_time {get; set;}   // 开启时间(xxxx-xx-xx xx:xx:xx)
				public string t_over_time {get; set;}   // 结束时间(xxxx-xx-xx xx:xx:xx)
				public string t_close_time {get; set;}   // 活动关闭时间(xxxx-xx-xx xx:xx:xx)
				private int m_t_server_day; // 活动要求开服大于等于N天 
				public int t_server_day{get{return m_t_server_day;} set{m_t_server_day = value;}} // 活动要求开服大于等于N天 
				private string m_t_left_content;   // 活动左侧标题文字
				public string t_left_content
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_left_content, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_left_content;
                        }
                        else
                            return m_t_left_content;
                    }
                    set { m_t_left_content = value; }
                } 
				private string m_t_content;   // 活动内容标题文字
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
				private string m_t_desc;   // 活动说明
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
				public string t_child {get; set;}   // 子项目配置(子项id+...)
				private int m_t_sort; // 显示优先级（越大越靠前） 
				public int t_sort{get{return m_t_sort;} set{m_t_sort = value;}} // 显示优先级（越大越靠前） 
				private int m_t_version; // 版本号 
				public int t_version{get{return m_t_version;} set{m_t_version = value;}} // 版本号 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					m_t_loop = XBuffer.ReadInt(data, ref offset);
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_mark = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_title = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_open_time = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_over_time = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_close_time = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_server_day = XBuffer.ReadInt(data, ref offset);
					t_left_content = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_content = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_desc = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_child = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_sort = XBuffer.ReadInt(data, ref offset);
					m_t_version = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


