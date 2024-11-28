/**
 * Auto generated, do not edit it
 *
 * t_aoyi_level_consume
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_aoyi_level_consumeBean : BaseBin
	{
				private int m_t_id; // id（突破等级*10+品质） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id（突破等级*10+品质） 
				private int m_t_break_level; // 突破等级 
				public int t_break_level{get{return m_t_break_level;} set{m_t_break_level = value;}} // 突破等级 
				private int m_t_break_consume; // 突破消耗金币 
				public int t_break_consume{get{return m_t_break_consume;} set{m_t_break_consume = value;}} // 突破消耗金币 
				private int m_t_level_consume; // 升级消耗精华 
				public int t_level_consume{get{return m_t_level_consume;} set{m_t_level_consume = value;}} // 升级消耗精华 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_break_level = XBuffer.ReadInt(data, ref offset);
					m_t_break_consume = XBuffer.ReadInt(data, ref offset);
					m_t_level_consume = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


