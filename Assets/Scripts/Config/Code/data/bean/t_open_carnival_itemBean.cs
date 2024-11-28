/**
 * Auto generated, do not edit it
 *
 * t_open_carnival_item
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_open_carnival_itemBean : BaseBin
	{
				private int m_t_id; // id(左侧主项目id*1000+子项目id*100+id) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id(左侧主项目id*1000+子项目id*100+id) 
				private int m_t_cond_type; // 条件类型（1=等级；2=主线关卡；3=精英关卡；4=格斗升品；5=格斗升星；6=装备升品；7=装备升星；8=竞技积分；9=竞技排名；10=终极试炼积分；11=终极试炼层数） 
				public int t_cond_type{get{return m_t_cond_type;} set{m_t_cond_type = value;}} // 条件类型（1=等级；2=主线关卡；3=精英关卡；4=格斗升品；5=格斗升星；6=装备升品；7=装备升星；8=竞技积分；9=竞技排名；10=终极试炼积分；11=终极试炼层数） 
				public string t_cond_arg {get; set;}   // 条件参数（条件类型为2时关卡id；4时N张+N品质ID；5时N张+N星级）
				public string t_reward {get; set;}   // 奖励
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_cond_type = XBuffer.ReadInt(data, ref offset);
					t_cond_arg = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_reward = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


