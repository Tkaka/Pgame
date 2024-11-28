/**
 * Auto generated, do not edit it
 *
 * t_pet_starup_cost_xiuzheng
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_pet_starup_cost_xiuzhengBean : BaseBin
	{
				private int m_t_id; // Id---(宠物id*100+当前星级) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // Id---(宠物id*100+当前星级) 
				private int m_t_num; // 下一个星级消耗的碎片增加 
				public int t_num{get{return m_t_num;} set{m_t_num = value;}} // 下一个星级消耗的碎片增加 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_num = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


