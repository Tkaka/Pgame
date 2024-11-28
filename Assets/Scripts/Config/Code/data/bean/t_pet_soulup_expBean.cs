/**
 * Auto generated, do not edit it
 *
 * t_pet_soulup_exp
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_pet_soulup_expBean : BaseBin
	{
				private int m_t_id; // Id---(升级类型*100+当前战魂等级) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // Id---(升级类型*100+当前战魂等级) 
				private int m_t_exp; // 升下一级所需经验(1~4=11、12资质；5~8=13、13.5资质；9~12=14资质；13~16=15资质） 
				public int t_exp{get{return m_t_exp;} set{m_t_exp = value;}} // 升下一级所需经验(1~4=11、12资质；5~8=13、13.5资质；9~12=14资质；13~16=15资质） 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_exp = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


