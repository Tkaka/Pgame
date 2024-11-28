/**
 * Auto generated, do not edit it
 *
 * t_equip_colorup
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_equip_colorupBean : BaseBin
	{
				private int m_t_id; // ID（当前品阶） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID（当前品阶） 
				private int m_t_lv_limit; // 当前品装备升品需求训练家等级（武器+0，衣服+1，裤子+2，鞋子+3） 
				public int t_lv_limit{get{return m_t_lv_limit;} set{m_t_lv_limit = value;}} // 当前品装备升品需求训练家等级（武器+0，衣服+1，裤子+2，鞋子+3） 
				public string t_item {get; set;}   // 当前品装备升品需求道具列表
				private int m_t_lv_base; // 当前品装备初始等级 
				public int t_lv_base{get{return m_t_lv_base;} set{m_t_lv_base = value;}} // 当前品装备初始等级 
				private int m_t_lv_max; // 当前品装备可升最大等级 
				public int t_lv_max{get{return m_t_lv_max;} set{m_t_lv_max = value;}} // 当前品装备可升最大等级 
				private int m_t_gold; // 当前品升品需求金币 
				public int t_gold{get{return m_t_gold;} set{m_t_gold = value;}} // 当前品升品需求金币 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_lv_limit = XBuffer.ReadInt(data, ref offset);
					t_item = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_lv_base = XBuffer.ReadInt(data, ref offset);
					m_t_lv_max = XBuffer.ReadInt(data, ref offset);
					m_t_gold = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


