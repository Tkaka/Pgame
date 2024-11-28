/**
 * Auto generated, do not edit it
 *
 * t_rule
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_ruleBean : BaseBin
	{
				private int m_t_id; // 规则表 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 规则表 
				private int m_t_type; // 类型（1今日玩法，2基本规则，3拳皇争霸，4巅峰对决，5比赛时间安排，6奖励） 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 类型（1今日玩法，2基本规则，3拳皇争霸，4巅峰对决，5比赛时间安排，6奖励） 
				private int m_t_order; // 顺序,如果属于没有下标的语句话下标请填-1 
				public int t_order{get{return m_t_order;} set{m_t_order = value;}} // 顺序,如果属于没有下标的语句话下标请填-1 
				public string t_desc {get; set;}   // 描述语言包ID
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					m_t_order = XBuffer.ReadInt(data, ref offset);
					t_desc = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


