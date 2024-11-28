/**
 * Auto generated, do not edit it
 *
 * t_skill_condition
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_skill_conditionBean : BaseBin
	{
				private int m_t_id; // 条件ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 条件ID 
				private int m_t_type; // 条件类型 0=无条件 1=在场类 2=魂数量类 3=目标生命 4=上场回合 5=x资质以上y数量 6=是否携带某buffID 7=存活人数8=战斗场景
 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 条件类型 0=无条件 1=在场类 2=魂数量类 3=目标生命 4=上场回合 5=x资质以上y数量 6=是否携带某buffID 7=存活人数8=战斗场景
 
				private int m_t_range_id; // 范围选择器id 
				public int t_range_id{get{return m_t_range_id;} set{m_t_range_id = value;}} // 范围选择器id 
				private int m_t_compare1; // 1=大于 2=大于等于 3=等于 4=小于 5=小于等于 
				public int t_compare1{get{return m_t_compare1;} set{m_t_compare1 = value;}} // 1=大于 2=大于等于 3=等于 4=小于 5=小于等于 
				private int m_t_compare2; // 1=大于 2=大于等于 3=等于 4=小于 5=小于等于 
				public int t_compare2{get{return m_t_compare2;} set{m_t_compare2 = value;}} // 1=大于 2=大于等于 3=等于 4=小于 5=小于等于 
				private int m_t_int_param1; // 条件参数1(攻1，防2，技3) 
				public int t_int_param1{get{return m_t_int_param1;} set{m_t_int_param1 = value;}} // 条件参数1(攻1，防2，技3) 
				private int m_t_int_param2; // 条件参数2 
				public int t_int_param2{get{return m_t_int_param2;} set{m_t_int_param2 = value;}} // 条件参数2 
				public string t_str_param {get; set;}   // 条件参数2
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					m_t_range_id = XBuffer.ReadInt(data, ref offset);
					m_t_compare1 = XBuffer.ReadInt(data, ref offset);
					m_t_compare2 = XBuffer.ReadInt(data, ref offset);
					m_t_int_param1 = XBuffer.ReadInt(data, ref offset);
					m_t_int_param2 = XBuffer.ReadInt(data, ref offset);
					t_str_param = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


