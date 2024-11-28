/**
 * Auto generated, do not edit it
 *
 * t_guild
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_guildBean : BaseBin
	{
				private int m_t_id; // id=公会等级 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id=公会等级 
				private int m_t_exp; // 升级所需经验 
				public int t_exp{get{return m_t_exp;} set{m_t_exp = value;}} // 升级所需经验 
				private int m_t_exp_max; // 每日获取经验上限 
				public int t_exp_max{get{return m_t_exp_max;} set{m_t_exp_max = value;}} // 每日获取经验上限 
				private int m_t_member_num; // 成员数量 
				public int t_member_num{get{return m_t_member_num;} set{m_t_member_num = value;}} // 成员数量 
				private int m_t_vice_chairman_num; // 副会长数量 
				public int t_vice_chairman_num{get{return m_t_vice_chairman_num;} set{m_t_vice_chairman_num = value;}} // 副会长数量 
				private int m_t_elite_num; // 精英数量 
				public int t_elite_num{get{return m_t_elite_num;} set{m_t_elite_num = value;}} // 精英数量 
				public string t_daily_reward {get; set;}   // 每日奖励
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_exp = XBuffer.ReadInt(data, ref offset);
					m_t_exp_max = XBuffer.ReadInt(data, ref offset);
					m_t_member_num = XBuffer.ReadInt(data, ref offset);
					m_t_vice_chairman_num = XBuffer.ReadInt(data, ref offset);
					m_t_elite_num = XBuffer.ReadInt(data, ref offset);
					t_daily_reward = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


