/**
 * Auto generated, do not edit it
 *
 * t_guild_boss_rank
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_guild_boss_rankBean : BaseBin
	{
				private int m_t_id; // id（排名） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id（排名） 
				public string t_single_damage {get; set;}   // 单个boss伤害奖励（公会内）
				public string t_month_damage {get; set;}   // 月总伤害排名奖励（公会内）
				public string t_guild_rank {get; set;}   // 每个boss击败排名奖励
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_single_damage = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_month_damage = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_guild_rank = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


