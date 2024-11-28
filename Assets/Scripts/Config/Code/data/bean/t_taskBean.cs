/**
 * Auto generated, do not edit it
 *
 * t_task
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_taskBean : BaseBin
	{
				private int m_t_id; // 任务id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 任务id 
				private int m_t_type; // 任务分类：0代表日常任务，1代表主线，2代表随机任务 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 任务分类：0代表日常任务，1代表主线，2代表随机任务 
				private int m_t_diban; // 0日常，1主线，2随机，3Vip 
				public int t_diban{get{return m_t_diban;} set{m_t_diban = value;}} // 0日常，1主线，2随机，3Vip 
				private int m_t_index; // 显示优先级 
				public int t_index{get{return m_t_index;} set{m_t_index = value;}} // 显示优先级 
				private string m_t_name;   // 任务名字语言包ID（1）
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
				private string m_t_desc;   // 任务目标描述语言包ID（2）
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
				public string t_desc_number {get; set;}   // 任务描述数字部分
				private int m_t_icon; // 背景框图片名+任务图片名 
				public int t_icon{get{return m_t_icon;} set{m_t_icon = value;}} // 背景框图片名+任务图片名 
				public string t_reward_icon {get; set;}   // 奖励1的图片与奖励2的图片加载
				public string t_open_condition {get; set;}   // 开启条件{等级达成开启+level（1），关卡完成+id（2），时间开启（3），充值开启（4），任务完成开启+id（5）,功能开放（6）vip等级（7）}
				public string t_finish_condition {get; set;}   // 完成条件（登录次数1，体力赠送次数2，完成某关卡3，竞技场挑战次数4，终极试炼挑战次数5，金币副本挑战次数6，格斗家升级次数7，格斗家升品次数8，格斗家升星次数9，酒吧抽卡次数10，每日点金次数11，体力购买次数12，社团捐献次数13，克隆组队战讨伐次数14，每日消费钻石15，普通关卡通关次数16，精英关卡通关次数17，训练家等级达成18，格斗家收集多少19，累冲20,num 数量宠物达到 xx 品质21）
				public string t_time {get; set;}   // 开启时间段（10:00+15:00+20:00）
				private int m_t_color_condition; // 文字变色条件 
				public int t_color_condition{get{return m_t_color_condition;} set{m_t_color_condition = value;}} // 文字变色条件 
				public string t_reward {get; set;}   // 奖励(id+num;id+num;...)
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					m_t_diban = XBuffer.ReadInt(data, ref offset);
					m_t_index = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_desc = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_desc_number = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_icon = XBuffer.ReadInt(data, ref offset);
					t_reward_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_open_condition = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_finish_condition = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_time = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_color_condition = XBuffer.ReadInt(data, ref offset);
					t_reward = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


