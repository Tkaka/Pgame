/**
 * Auto generated, do not edit it
 *
 * t_equip_levelup
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_equip_levelupBean : BaseBin
	{
				private int m_t_id; // ID（品阶*1000+资质*10+部位） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID（品阶*1000+资质*10+部位） 
				private int m_t_gold_base; // 当前品装备升级需求金币初始值 
				public int t_gold_base{get{return m_t_gold_base;} set{m_t_gold_base = value;}} // 当前品装备升级需求金币初始值 
				private int m_t_gold_add; // 当前品装备升级需求金币增加值 
				public int t_gold_add{get{return m_t_gold_add;} set{m_t_gold_add = value;}} // 当前品装备升级需求金币增加值 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_gold_base = XBuffer.ReadInt(data, ref offset);
					m_t_gold_add = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


