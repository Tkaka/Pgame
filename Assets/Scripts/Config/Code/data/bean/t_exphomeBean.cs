/**
 * Auto generated, do not edit it
 *
 * t_exphome
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_exphomeBean : BaseBin
	{
				private int m_t_id; // 栏位 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 栏位 
				private int m_t_level; // 所需等级 
				public int t_level{get{return m_t_level;} set{m_t_level = value;}} // 所需等级 
				private int m_t_vipLevel; // 所需vip等级 
				public int t_vipLevel{get{return m_t_vipLevel;} set{m_t_vipLevel = value;}} // 所需vip等级 
				private int m_t_money; // 开启所需钻石 
				public int t_money{get{return m_t_money;} set{m_t_money = value;}} // 开启所需钻石 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_level = XBuffer.ReadInt(data, ref offset);
					m_t_vipLevel = XBuffer.ReadInt(data, ref offset);
					m_t_money = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


