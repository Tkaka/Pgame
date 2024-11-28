/**
 * Auto generated, do not edit it
 *
 * t_guild_battle_win
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_guild_battle_winBean : BaseBin
	{
				private int m_t_id; // 连胜次数 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 连胜次数 
				public string t_award_pre {get; set;}   // 初赛奖励
				public string t_award_rem1 {get; set;}   // 复赛奖励（攻）
				public string t_award_rem2 {get; set;}   // 复赛奖励（防）
				public string t_award_rem3 {get; set;}   // 复赛奖励（技）
				public string t_award_finals {get; set;}   // 决赛奖励
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_award_pre = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_award_rem1 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_award_rem2 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_award_rem3 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_award_finals = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


