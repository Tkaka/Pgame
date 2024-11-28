/**
 * Auto generated, do not edit it
 *
 * t_pet_colorup_attr
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_pet_colorup_attrBean : BaseBin
	{
				private int m_t_id; // 品阶 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 品阶 
				private int m_t_atk; // 攻 
				public int t_atk{get{return m_t_atk;} set{m_t_atk = value;}} // 攻 
				private int m_t_def; // 防 
				public int t_def{get{return m_t_def;} set{m_t_def = value;}} // 防 
				private int m_t_hp; // 血 
				public int t_hp{get{return m_t_hp;} set{m_t_hp = value;}} // 血 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_atk = XBuffer.ReadInt(data, ref offset);
					m_t_def = XBuffer.ReadInt(data, ref offset);
					m_t_hp = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


