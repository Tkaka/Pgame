/**
 * Auto generated, do not edit it
 *
 * t_guild_battle_time
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_guild_battle_timeBean : BaseBin
	{
				private int m_t_id; // 星期 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 星期 
				private int m_t_pet_type; // 可上阵类型限制 
				public int t_pet_type{get{return m_t_pet_type;} set{m_t_pet_type = value;}} // 可上阵类型限制 
				private int m_t_num; // 单个阵容可上阵数量 
				public int t_num{get{return m_t_num;} set{m_t_num = value;}} // 单个阵容可上阵数量 
				private int m_t_formation; // 阵容数 
				public int t_formation{get{return m_t_formation;} set{m_t_formation = value;}} // 阵容数 
				public string t_level {get; set;}   // 除第一组外的限制等级
				public string t_sign {get; set;}   // 报名奖励
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_pet_type = XBuffer.ReadInt(data, ref offset);
					m_t_num = XBuffer.ReadInt(data, ref offset);
					m_t_formation = XBuffer.ReadInt(data, ref offset);
					t_level = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_sign = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


