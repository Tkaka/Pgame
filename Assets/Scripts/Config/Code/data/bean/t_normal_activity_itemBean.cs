/**
 * Auto generated, do not edit it
 *
 * t_normal_activity_item
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_normal_activity_itemBean : BaseBin
	{
				private int m_t_id; // 子项目ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 子项目ID 
				private string m_t_desc;   // 子项目描述
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
				public string t_open_condition {get; set;}   // 子项开启条件(1=训练家等级大于等于N级2=开服时间大于等于N天 + 值)
				public string t_open_time {get; set;}   // 开启时间(xxxx-xx-xx xx:xx:xx)
				public string t_finish_condition {get; set;}   // 完成条件(1=登录次数,2=通关指定关卡ID,3=主线关卡挑战次数大于等于N次,4=精英关卡挑战次数大于等于N次,5=金币活动关挑战大于等于N次,6=经验活动关挑战大于等于N次,7=幻象副本挑战大于等于N次,8=宠物升星大于等于N次,9=宠物升品大于等于N次,10=宠物升级大于等于N次,11=宠物技能升级大于等于N次,12=消费钻石大于等于N,13=消耗金币大于等于N,14=购买体力次数大于等于N,15=充值钻石大于等于N,16=酒吧抽奖大于等于N次(含免费),17=酒吧金币抽奖大于等于N次(含免费),18=酒吧钻石抽奖大于等于N次(含免费),19=奥义抽奖大于等于N次(含免费),20=奥义金币抽奖大于等于N次(含免费),21=奥义钻石抽奖大于等于N次(含免费),22=终极试炼层数大于等于N,23=终极试炼获取积分大于等于N,24=竞技场完成次数大于等于N次,25=竞技场得分大于等于N分,26=竞技场排名大于N,27=社团捐献大于等于N次,28=社团玩法1进行次数大于等于N,29=训练所为别人加速N次,501=商店装备宝箱单抽N折 (万分比),502=商店装备宝箱十连抽N折,601=主线关卡掉落N倍,602=金币关掉落N倍,603=经验关掉落N倍,604=终极试炼金币/掉落N倍) + 值
				public string t_reward {get; set;}   // 道具奖励
				public string t_cost {get; set;}   // 兑换道具所需
				private int m_t_limit; // 次数上限 
				public int t_limit{get{return m_t_limit;} set{m_t_limit = value;}} // 次数上限 
				private int m_t_jump; // 前往跳转类型（） 
				public int t_jump{get{return m_t_jump;} set{m_t_jump = value;}} // 前往跳转类型（） 
				private int m_t_version; // 版本号 
				public int t_version{get{return m_t_version;} set{m_t_version = value;}} // 版本号 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_desc = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_open_condition = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_open_time = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_finish_condition = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_reward = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_cost = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_limit = XBuffer.ReadInt(data, ref offset);
					m_t_jump = XBuffer.ReadInt(data, ref offset);
					m_t_version = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


