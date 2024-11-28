/**
 * Auto generated, do not edit it
 *
 * t_guild_battle_rank
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_guild_battle_rankBean : BaseBin
	{
				private int m_t_id; // 排名(51名之后取51名数值） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 排名(51名之后取51名数值） 
				public string t_preliminary_award {get; set;}   // 初赛排名奖励
				public string t_intermediary_award1 {get; set;}   // 复赛排名奖励（攻）
				public string t_intermediary_award2 {get; set;}   // 复赛排名奖励（防）
				public string t_intermediary_award3 {get; set;}   // 复赛排名奖励（技）
				public string t_final_award {get; set;}   // 决赛排名奖励
				private int m_t_perliminary_score; // 初赛排名积分 
				public int t_perliminary_score{get{return m_t_perliminary_score;} set{m_t_perliminary_score = value;}} // 初赛排名积分 
				private int m_t_intermediary_score; // 复赛排名积分 
				public int t_intermediary_score{get{return m_t_intermediary_score;} set{m_t_intermediary_score = value;}} // 复赛排名积分 
				private int m_t_final_score; // 决赛排名积分 
				public int t_final_score{get{return m_t_final_score;} set{m_t_final_score = value;}} // 决赛排名积分 
				public string t_guild_overall {get; set;}   // 公会总排名奖励
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_preliminary_award = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_intermediary_award1 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_intermediary_award2 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_intermediary_award3 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_final_award = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_perliminary_score = XBuffer.ReadInt(data, ref offset);
					m_t_intermediary_score = XBuffer.ReadInt(data, ref offset);
					m_t_final_score = XBuffer.ReadInt(data, ref offset);
					t_guild_overall = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


