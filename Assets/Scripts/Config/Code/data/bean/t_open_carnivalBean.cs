/**
 * Auto generated, do not edit it
 *
 * t_open_carnival
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_open_carnivalBean : BaseBin
	{
				private int m_t_id; // id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id 
				private int m_t_cond_type; // 条件类型（1：等级，2：获得格斗家） 
				public int t_cond_type{get{return m_t_cond_type;} set{m_t_cond_type = value;}} // 条件类型（1：等级，2：获得格斗家） 
				public string t_cond_arg {get; set;}   // 条件参数
				private int m_t_row; // 行(1:等级关卡，2：格斗家，3：装备，4：竞技场，5：终极试炼） 
				public int t_row{get{return m_t_row;} set{m_t_row = value;}} // 行(1:等级关卡，2：格斗家，3：装备，4：竞技场，5：终极试炼） 
				private int m_t_column; // 列 
				public int t_column{get{return m_t_column;} set{m_t_column = value;}} // 列 
				public string t_reward {get; set;}   // 奖励
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_cond_type = XBuffer.ReadInt(data, ref offset);
					t_cond_arg = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_row = XBuffer.ReadInt(data, ref offset);
					m_t_column = XBuffer.ReadInt(data, ref offset);
					t_reward = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


