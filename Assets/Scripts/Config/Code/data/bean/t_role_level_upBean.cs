/**
 * Auto generated, do not edit it
 *
 * t_role_level_up
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_role_level_upBean : BaseBin
	{
				private int m_t_id; // 等级 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 等级 
				private int m_t_exp; // 升下一级需求经验 
				public int t_exp{get{return m_t_exp;} set{m_t_exp = value;}} // 升下一级需求经验 
				private int m_t_energy_recover; // 恢复体力 
				public int t_energy_recover{get{return m_t_energy_recover;} set{m_t_energy_recover = value;}} // 恢复体力 
				private int m_t_energy_max; // 当前等级体力上限 
				public int t_energy_max{get{return m_t_energy_max;} set{m_t_energy_max = value;}} // 当前等级体力上限 
				private int m_t_skillpoint; // 技能点存储上限 
				public int t_skillpoint{get{return m_t_skillpoint;} set{m_t_skillpoint = value;}} // 技能点存储上限 
				private int m_t_exp_home; // 训练馆每分钟基础经验 
				public int t_exp_home{get{return m_t_exp_home;} set{m_t_exp_home = value;}} // 训练馆每分钟基础经验 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_exp = XBuffer.ReadInt(data, ref offset);
					m_t_energy_recover = XBuffer.ReadInt(data, ref offset);
					m_t_energy_max = XBuffer.ReadInt(data, ref offset);
					m_t_skillpoint = XBuffer.ReadInt(data, ref offset);
					m_t_exp_home = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


