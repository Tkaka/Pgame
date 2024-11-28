/**
 * Auto generated, do not edit it
 *
 * t_pet_starup_cost
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_pet_starup_costBean : BaseBin
	{
				private int m_t_id; // Id---(星级 * 100 + 资质)当前星级*100 + 升星类别 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // Id---(星级 * 100 + 资质)当前星级*100 + 升星类别 
				private int m_t_gold; // 下个星级消耗的金币 
				public int t_gold{get{return m_t_gold;} set{m_t_gold = value;}} // 下个星级消耗的金币 
				private int m_t_num; // 下个星级消耗碎片数量 
				public int t_num{get{return m_t_num;} set{m_t_num = value;}} // 下个星级消耗碎片数量 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_gold = XBuffer.ReadInt(data, ref offset);
					m_t_num = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


