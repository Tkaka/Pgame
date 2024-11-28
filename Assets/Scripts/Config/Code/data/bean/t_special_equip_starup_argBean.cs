/**
 * Auto generated, do not edit it
 *
 * t_special_equip_starup_arg
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_special_equip_starup_argBean : BaseBin
	{
				private int m_t_id; // ID（星级*100+类别（两种）） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID（星级*100+类别（两种）） 
				private int m_t_add_shuXing; // 增加的属性总值
 
				public int t_add_shuXing{get{return m_t_add_shuXing;} set{m_t_add_shuXing = value;}} // 增加的属性总值
 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_add_shuXing = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


