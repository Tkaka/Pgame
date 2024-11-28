/**
 * Auto generated, do not edit it
 *
 * t_arena_robot
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_arena_robotBean : BaseBin
	{
				private int m_t_id; // 机器人id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 机器人id 
				private int m_t_rank_min; // 排名下限 
				public int t_rank_min{get{return m_t_rank_min;} set{m_t_rank_min = value;}} // 排名下限 
				private int m_t_rank_max; // 排名上限 
				public int t_rank_max{get{return m_t_rank_max;} set{m_t_rank_max = value;}} // 排名上限 
				private int m_t_id_min; // 宠物库id范围下限 
				public int t_id_min{get{return m_t_id_min;} set{m_t_id_min = value;}} // 宠物库id范围下限 
				private int m_t_id_max; // 宠物库id范围上限 
				public int t_id_max{get{return m_t_id_max;} set{m_t_id_max = value;}} // 宠物库id范围上限 
				private int m_t_precede_value_min; // 先手值下限 
				public int t_precede_value_min{get{return m_t_precede_value_min;} set{m_t_precede_value_min = value;}} // 先手值下限 
				private int m_t_precede_value_max; // 先手值上限 
				public int t_precede_value_max{get{return m_t_precede_value_max;} set{m_t_precede_value_max = value;}} // 先手值上限 
				private int m_t_win_min; // 胜场下限 
				public int t_win_min{get{return m_t_win_min;} set{m_t_win_min = value;}} // 胜场下限 
				private int m_t_win_max; // 胜场上限 
				public int t_win_max{get{return m_t_win_max;} set{m_t_win_max = value;}} // 胜场上限 
				public string t_pets {get; set;}   // 阵容(petId+petId+..+;petId+petId+...;...)
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_rank_min = XBuffer.ReadInt(data, ref offset);
					m_t_rank_max = XBuffer.ReadInt(data, ref offset);
					m_t_id_min = XBuffer.ReadInt(data, ref offset);
					m_t_id_max = XBuffer.ReadInt(data, ref offset);
					m_t_precede_value_min = XBuffer.ReadInt(data, ref offset);
					m_t_precede_value_max = XBuffer.ReadInt(data, ref offset);
					m_t_win_min = XBuffer.ReadInt(data, ref offset);
					m_t_win_max = XBuffer.ReadInt(data, ref offset);
					t_pets = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


