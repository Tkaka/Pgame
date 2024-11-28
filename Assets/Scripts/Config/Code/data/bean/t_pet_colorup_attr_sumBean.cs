/**
 * Auto generated, do not edit it
 *
 * t_pet_colorup_attr_sum
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_pet_colorup_attr_sumBean : BaseBin
	{
				private int m_t_id; // Id--(当前品id*10+角色类型（攻1防2技3）) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // Id--(当前品id*10+角色类型（攻1防2技3）) 
				private int m_t_atk; // 攻 
				public int t_atk{get{return m_t_atk;} set{m_t_atk = value;}} // 攻 
				private int m_t_def; // 防 
				public int t_def{get{return m_t_def;} set{m_t_def = value;}} // 防 
				private int m_t_hp; // 血 
				public int t_hp{get{return m_t_hp;} set{m_t_hp = value;}} // 血 
				private int m_t_priority; // 先手值 
				public int t_priority{get{return m_t_priority;} set{m_t_priority = value;}} // 先手值 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_atk = XBuffer.ReadInt(data, ref offset);
					m_t_def = XBuffer.ReadInt(data, ref offset);
					m_t_hp = XBuffer.ReadInt(data, ref offset);
					m_t_priority = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


