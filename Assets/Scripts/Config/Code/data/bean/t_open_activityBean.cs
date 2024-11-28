/**
 * Auto generated, do not edit it
 *
 * t_open_activity
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_open_activityBean : BaseBin
	{
				private int m_t_id; // 活动ID(1战斗力冲榜，2嘉年华) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 活动ID(1战斗力冲榜，2嘉年华) 
				private int m_t_open_date; // 第几天开启 
				public int t_open_date{get{return m_t_open_date;} set{m_t_open_date = value;}} // 第几天开启 
				public string t_open_time {get; set;}   // 开启时间（xx:xx:xx）
				private int m_t_over_date; // 第几天结束 
				public int t_over_date{get{return m_t_over_date;} set{m_t_over_date = value;}} // 第几天结束 
				public string t_over_time {get; set;}   // 结束时间（xx:xx:xx）
				private int m_t_close_date; // 第几天关闭 
				public int t_close_date{get{return m_t_close_date;} set{m_t_close_date = value;}} // 第几天关闭 
				public string t_close_time {get; set;}   // 关闭时间（xx:xx:xx）
				private int m_t_see_level; // 限制等级(可见) 
				public int t_see_level{get{return m_t_see_level;} set{m_t_see_level = value;}} // 限制等级(可见) 
				private int m_t_open_level; // 限制等级(可参与) 
				public int t_open_level{get{return m_t_open_level;} set{m_t_open_level = value;}} // 限制等级(可参与) 
				private int m_t_pet; // 活动宠物id（宠物表） 
				public int t_pet{get{return m_t_pet;} set{m_t_pet = value;}} // 活动宠物id（宠物表） 
				private string m_t_desc;   // 活动描述
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
				public string t_child {get; set;}   // 子活动
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_open_date = XBuffer.ReadInt(data, ref offset);
					t_open_time = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_over_date = XBuffer.ReadInt(data, ref offset);
					t_over_time = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_close_date = XBuffer.ReadInt(data, ref offset);
					t_close_time = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_see_level = XBuffer.ReadInt(data, ref offset);
					m_t_open_level = XBuffer.ReadInt(data, ref offset);
					m_t_pet = XBuffer.ReadInt(data, ref offset);
					t_desc = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_child = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


