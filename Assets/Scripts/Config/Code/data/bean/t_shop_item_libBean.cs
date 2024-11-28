/**
 * Auto generated, do not edit it
 *
 * t_shop_item_lib
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_shop_item_libBean : BaseBin
	{
				private int m_t_id; // id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id 
				private int m_t_itemId; // 道具id 
				public int t_itemId{get{return m_t_itemId;} set{m_t_itemId = value;}} // 道具id 
				public string t_num {get; set;}   // 数量(数量+权重;...)
				public string t_price {get; set;}   // 单价 价格（商店类型 1 杂货商店,2 荣誉商店,3 试练商店,4 工会商店,5 道具商店,6 兑换商店 7 神秘商店 +代币类型+价钱;...）
				public string t_levelLimit {get; set;}   // 等级区间(起始+结束)
				public string t_discount {get; set;}   // 折扣(折扣万分比+权重;...)
				private int m_t_limit; // 购买次数限制 不填没次数限制 
				public int t_limit{get{return m_t_limit;} set{m_t_limit = value;}} // 购买次数限制 不填没次数限制 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_itemId = XBuffer.ReadInt(data, ref offset);
					t_num = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_price = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_levelLimit = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_discount = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_limit = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


