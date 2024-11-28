/**
 * Auto generated, do not edit it
 *
 * t_shop
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_shopBean : BaseBin
	{
				private int m_t_id; // index(类型 1 杂货商店,2 荣誉商店,3 试练商店,4 工会商店,5 道具商店,6 兑换商店 7神秘商店* 100 + 顺序) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // index(类型 1 杂货商店,2 荣誉商店,3 试练商店,4 工会商店,5 道具商店,6 兑换商店 7神秘商店* 100 + 顺序) 
				public string t_item {get; set;}   // 商品库id+权重;...
				private int m_t_super; // 是否超级 1为超级  
				public int t_super{get{return m_t_super;} set{m_t_super = value;}} // 是否超级 1为超级  
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_item = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_super = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


