/**
 * Auto generated, do not edit it
 *
 * t_bexp
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_bexpBean : BaseBin
	{
				private int m_t_id; // 训练师等级=id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 训练师等级=id 
				private int m_t_basexp; // 训练所宝贝加速基础经验 
				public int t_basexp{get{return m_t_basexp;} set{m_t_basexp = value;}} // 训练所宝贝加速基础经验 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_basexp = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


