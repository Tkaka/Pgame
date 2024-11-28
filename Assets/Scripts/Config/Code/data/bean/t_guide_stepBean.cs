/**
 * Auto generated, do not edit it
 *
 * t_guide_step
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_guide_stepBean : BaseBin
	{
				private int m_t_id; // 新手ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 新手ID 
				public string t_view {get; set;}   // 预制件（如果有，半身像和对话框,FGUI里面的url）
				private int m_t_arrow_angle; // 箭头显示方向 
				public int t_arrow_angle{get{return m_t_arrow_angle;} set{m_t_arrow_angle = value;}} // 箭头显示方向 
				private int m_t_left_right; // 对话时人在左右还是右（1左2右） 
				public int t_left_right{get{return m_t_left_right;} set{m_t_left_right = value;}} // 对话时人在左右还是右（1左2右） 
				private string m_t_title;   // 说话标题
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
				private string m_t_tip;   // 提示文字
				public string t_tip
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_tip, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_tip;
                        }
                        else
                            return m_t_tip;
                    }
                    set { m_t_tip = value; }
                } 
				public string t_guide_model {get; set;}   // 引导模型
				public string t_click_name {get; set;}   // 要点击的按钮名字（不填则为全屏废话）
				private int m_t_reset_main; // 是否重置主界面icon(1是，不填不是） 
				public int t_reset_main{get{return m_t_reset_main;} set{m_t_reset_main = value;}} // 是否重置主界面icon(1是，不填不是） 
				private int m_t_clickable; // 按钮是否可以点击（1可以0不可以） 
				public int t_clickable{get{return m_t_clickable;} set{m_t_clickable = value;}} // 按钮是否可以点击（1可以0不可以） 
				public string t_click_param {get; set;}   // 点击的按钮的参数
				private int m_t_auto_time; // 自动执行时间（毫秒，0必须手点） 
				public int t_auto_time{get{return m_t_auto_time;} set{m_t_auto_time = value;}} // 自动执行时间（毫秒，0必须手点） 
				private int m_t_guide_type; // 引导类型（1强制引导，2非强制引导） 
				public int t_guide_type{get{return m_t_guide_type;} set{m_t_guide_type = value;}} // 引导类型（1强制引导，2非强制引导） 
				public string t_trigger {get; set;}   // 触发条件(条件可以叠加eg. 1+101;2+30)
				public string t_finish {get; set;}   // 完成条件
				private int m_t_point; // 是否为记录节点（0不是，1（步骤开始时）,2（步骤结束时）） 
				public int t_point{get{return m_t_point;} set{m_t_point = value;}} // 是否为记录节点（0不是，1（步骤开始时）,2（步骤结束时）） 
				private int m_t_delay; // 出现延时时间(ms) 
				public int t_delay{get{return m_t_delay;} set{m_t_delay = value;}} // 出现延时时间(ms) 
				public string t_snd {get; set;}   // 声音资源名字
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_view = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_arrow_angle = XBuffer.ReadInt(data, ref offset);
					m_t_left_right = XBuffer.ReadInt(data, ref offset);
					t_title = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_tip = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_guide_model = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_click_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_reset_main = XBuffer.ReadInt(data, ref offset);
					m_t_clickable = XBuffer.ReadInt(data, ref offset);
					t_click_param = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_auto_time = XBuffer.ReadInt(data, ref offset);
					m_t_guide_type = XBuffer.ReadInt(data, ref offset);
					t_trigger = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_finish = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_point = XBuffer.ReadInt(data, ref offset);
					m_t_delay = XBuffer.ReadInt(data, ref offset);
					t_snd = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


