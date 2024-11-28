/**
 * Auto generated, do not edit it
 *
 * t_skill_lvup_cost
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_skill_lvup_costBean : BaseBin
	{
				private int m_t_id; // 等级 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 等级 
				private int m_t_standard; // 技能升级下一级需求金币基础值 
				public int t_standard{get{return m_t_standard;} set{m_t_standard = value;}} // 技能升级下一级需求金币基础值 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_standard = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


