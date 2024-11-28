/**
 * Auto generated, do not edit it
 *
 * t_promise
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_promiseBean : BaseBin
	{
				private int m_t_id; // 资质id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 资质id 
				public string t_item_id {get; set;}   // 包含道具id
				private int m_t_quantity; // 请求数量上限 
				public int t_quantity{get{return m_t_quantity;} set{m_t_quantity = value;}} // 请求数量上限 
				private int m_t_Grant; // 赠予数量上限 
				public int t_Grant{get{return m_t_Grant;} set{m_t_Grant = value;}} // 赠予数量上限 
				public string t_Return {get; set;}   // 回报
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_item_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_quantity = XBuffer.ReadInt(data, ref offset);
					m_t_Grant = XBuffer.ReadInt(data, ref offset);
					t_Return = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


