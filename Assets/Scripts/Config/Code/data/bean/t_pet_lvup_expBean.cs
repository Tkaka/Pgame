/**
 * Auto generated, do not edit it
 *
 * t_pet_lvup_exp
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_pet_lvup_expBean : BaseBin
	{
				private int m_t_id; // 等级 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 等级 
				private int m_t_exp; // (13资质宝贝升级需求经验） 
				public int t_exp{get{return m_t_exp;} set{m_t_exp = value;}} // (13资质宝贝升级需求经验） 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_exp = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


