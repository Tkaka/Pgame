/**
 * Auto generated, do not edit it
 *
 * t_module
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_moduleBean : BaseBin
	{
				private int m_t_id; // 功能id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 功能id 
				public string t_condition {get; set;}   // 限制类型（条件+参数;条件+参数）（1等级，2关卡，3开服天数）
				private string m_t_name;   // 功能名字
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
				private int m_t_lock; // 未解锁时表现（0不处理/特殊处理，1提前5级开放和锁，2业签加锁，3隐藏) 
				public int t_lock{get{return m_t_lock;} set{m_t_lock = value;}} // 未解锁时表现（0不处理/特殊处理，1提前5级开放和锁，2业签加锁，3隐藏) 
				private int m_t_next_tip; // 显示在主界面的下一个功能 
				public int t_next_tip{get{return m_t_next_tip;} set{m_t_next_tip = value;}} // 显示在主界面的下一个功能 
				public string t_icon {get; set;}   // 主界面预告图标
				private string m_t_desc1;   // 描述1
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
				private string m_t_desc2;   // 描述2
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
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_condition = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_lock = XBuffer.ReadInt(data, ref offset);
					m_t_next_tip = XBuffer.ReadInt(data, ref offset);
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_desc1 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_desc2 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
		} 

	}
}


