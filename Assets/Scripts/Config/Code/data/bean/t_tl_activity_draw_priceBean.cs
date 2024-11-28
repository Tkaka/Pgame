/**
 * Auto generated, do not edit it
 *
 * t_tl_activity_draw_price
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_tl_activity_draw_priceBean : BaseBin
	{
				private int m_t_id; // 活动ID*10000+t_num 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 活动ID*10000+t_num 
				private int m_t_type; // 类型：代币id 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 类型：代币id 
				private int m_t_free_num; // 免费次数 
				public int t_free_num{get{return m_t_free_num;} set{m_t_free_num = value;}} // 免费次数 
				private int m_t_across_day; // 免费次数是否跨天刷新（刷新填1） 
				public int t_across_day{get{return m_t_across_day;} set{m_t_across_day = value;}} // 免费次数是否跨天刷新（刷新填1） 
				private int m_t_price; // 价格 
				public int t_price{get{return m_t_price;} set{m_t_price = value;}} // 价格 
				private int m_t_score; // 获得积分 
				public int t_score{get{return m_t_score;} set{m_t_score = value;}} // 获得积分 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					m_t_free_num = XBuffer.ReadInt(data, ref offset);
					m_t_across_day = XBuffer.ReadInt(data, ref offset);
					m_t_price = XBuffer.ReadInt(data, ref offset);
					m_t_score = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


