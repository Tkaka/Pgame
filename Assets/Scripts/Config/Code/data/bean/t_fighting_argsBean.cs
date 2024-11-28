/**
 * Auto generated, do not edit it
 *
 * t_fighting_args
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_fighting_argsBean : BaseBin
	{
				private int m_t_id; // Id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // Id 
				private int m_t_value; // 攻 
				public int t_value{get{return m_t_value;} set{m_t_value = value;}} // 攻 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_value = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


