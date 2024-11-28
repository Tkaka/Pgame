/**
 * Auto generated, do not edit it
 *
 * t_equipawakenbox
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_equipawakenboxBean : BaseBin
	{
				private int m_t_id; // 次数 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 次数 
				private int m_t_price; // 价钱（钻石） 
				public int t_price{get{return m_t_price;} set{m_t_price = value;}} // 价钱（钻石） 
				private int m_t_ticket; // 代金券道具id 
				public int t_ticket{get{return m_t_ticket;} set{m_t_ticket = value;}} // 代金券道具id 
				public string t_items {get; set;}   // 随机道具库(掉落id+数量+权重;...)
				public string t_tenth {get; set;}   // 第10次道具(掉落id+数量+权重;...)
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_price = XBuffer.ReadInt(data, ref offset);
					m_t_ticket = XBuffer.ReadInt(data, ref offset);
					t_items = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_tenth = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


