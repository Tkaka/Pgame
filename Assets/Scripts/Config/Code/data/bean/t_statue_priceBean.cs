/**
 * Auto generated, do not edit it
 *
 * t_statue_price
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_statue_priceBean : BaseBin
	{
				private int m_t_id; // 铜像ID（材质*10+品级） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 铜像ID（材质*10+品级） 
				private int m_t_currency_type; // 货币类型 
				public int t_currency_type{get{return m_t_currency_type;} set{m_t_currency_type = value;}} // 货币类型 
				private int m_t_price; // 价格 
				public int t_price{get{return m_t_price;} set{m_t_price = value;}} // 价格 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_currency_type = XBuffer.ReadInt(data, ref offset);
					m_t_price = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


