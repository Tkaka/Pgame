/**
 * Auto generated, do not edit it
 *
 * t_open_activity_item
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_open_activity_itemBean : BaseBin
	{
				private int m_t_id; // 子项目ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 子项目ID 
				public string t_open_condition {get; set;}   // 子项开启条件(1=训练家等级大于等于N级2=开服时间大于等于N天)
				public string t_finish_condition {get; set;}   // 完成条件(1=战力到达N；2=战力排名N；3=等级N；4=主线关卡id；5=精英关卡id；6=格斗升品N张+N品质ID；7=格斗升星N张+N星级；8=装备升品N张+N品质ID；9=装备升星N张+N星级；10=竞技积分N；11=竞技排名N；12=终极试炼积分N；13=终极试炼层数N
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
					t_open_condition = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_finish_condition = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_reward = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_cost = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_limit = XBuffer.ReadInt(data, ref offset);
					m_t_jump = XBuffer.ReadInt(data, ref offset);
					m_t_version = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


