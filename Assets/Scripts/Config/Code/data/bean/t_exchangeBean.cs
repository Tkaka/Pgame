/**
 * Auto generated, do not edit it
 *
 * t_exchange
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_exchangeBean : BaseBin
	{
				private int m_t_id; // 拳皇争霸商店商品ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 拳皇争霸商店商品ID 
				public string t_need {get; set;}   // 使用消耗品ID+数量
				public string t_item {get; set;}   // 道具ID+数量
				private int m_t_crit; // 2倍的暴击概率（万分比） 
				public int t_crit{get{return m_t_crit;} set{m_t_crit = value;}} // 2倍的暴击概率（万分比） 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_need = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_item = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_crit = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


