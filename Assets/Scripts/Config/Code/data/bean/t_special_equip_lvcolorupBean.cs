/**
 * Auto generated, do not edit it
 *
 * t_special_equip_lvcolorup
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_special_equip_lvcolorupBean : BaseBin
	{
				private int m_t_id; // ID（当前品阶*100+特殊装备类型1=徽章；2=秘籍）

 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID（当前品阶*100+特殊装备类型1=徽章；2=秘籍）

 
				private int m_t_exp_base; // 当前品每级需求经验初始值 
				public int t_exp_base{get{return m_t_exp_base;} set{m_t_exp_base = value;}} // 当前品每级需求经验初始值 
				private int m_t_exp_add; // 当前品每级需求经验增加值 
				public int t_exp_add{get{return m_t_exp_add;} set{m_t_exp_add = value;}} // 当前品每级需求经验增加值 
				public string t_token_ids {get; set;}   // 当前品装备升下品需求代币ID（道具币+金币）
				public string t_token_nums {get; set;}   // 当前品装备升下品需求代币数量
				private int m_t_lv_base; // 当前品装备初始等级 
				public int t_lv_base{get{return m_t_lv_base;} set{m_t_lv_base = value;}} // 当前品装备初始等级 
				private int m_t_lv_max; // 当前品装备可升最大等级 
				public int t_lv_max{get{return m_t_lv_max;} set{m_t_lv_max = value;}} // 当前品装备可升最大等级 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_exp_base = XBuffer.ReadInt(data, ref offset);
					m_t_exp_add = XBuffer.ReadInt(data, ref offset);
					t_token_ids = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_token_nums = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_lv_base = XBuffer.ReadInt(data, ref offset);
					m_t_lv_max = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


