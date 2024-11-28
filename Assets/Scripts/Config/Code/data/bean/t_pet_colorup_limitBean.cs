/**
 * Auto generated, do not edit it
 *
 * t_pet_colorup_limit
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_pet_colorup_limitBean : BaseBin
	{
				private int m_t_id; // 品阶*1000+资质*10+类别（1=攻击2=防3=技） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 品阶*1000+资质*10+类别（1=攻击2=防3=技） 
				private int m_t_level; // 等级限制 
				public int t_level{get{return m_t_level;} set{m_t_level = value;}} // 等级限制 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_level = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


