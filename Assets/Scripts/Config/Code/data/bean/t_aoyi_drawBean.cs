/**
 * Auto generated, do not edit it
 *
 * t_aoyi_draw
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_aoyi_drawBean : BaseBin
	{
				private int m_t_id; // id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id 
				private int m_t_cost_id; // 消耗道具类型 
				public int t_cost_id{get{return m_t_cost_id;} set{m_t_cost_id = value;}} // 消耗道具类型 
				private int m_t_cost_num; // 消耗道具数量 
				public int t_cost_num{get{return m_t_cost_num;} set{m_t_cost_num = value;}} // 消耗道具数量 
				private int m_t_replace_id; // 替代道具id 
				public int t_replace_id{get{return m_t_replace_id;} set{m_t_replace_id = value;}} // 替代道具id 
				private int m_t_replace_num; // 替代道具数量 
				public int t_replace_num{get{return m_t_replace_num;} set{m_t_replace_num = value;}} // 替代道具数量 
				private int m_t_free_num; // 每日免费次数 
				public int t_free_num{get{return m_t_free_num;} set{m_t_free_num = value;}} // 每日免费次数 
				private int m_t_free_interval; // 免费次数间隔（分） 
				public int t_free_interval{get{return m_t_free_interval;} set{m_t_free_interval = value;}} // 免费次数间隔（分） 
				private int m_t_open_level; // 开放等级 
				public int t_open_level{get{return m_t_open_level;} set{m_t_open_level = value;}} // 开放等级 
				private int m_t_buy_num; // 可购买次数，-1为不限次数 
				public int t_buy_num{get{return m_t_buy_num;} set{m_t_buy_num = value;}} // 可购买次数，-1为不限次数 
				public string t_fix_drop {get; set;}   // 极品碎片箱子id+数量
				public string t_weight_drop {get; set;}   // 权重掉落（类型+数量+权重）
				public string t_n_choose_1 {get; set;}   // n选1（类型+数量+权重）
				private int m_t_drop_num; // 总共掉落数量 
				public int t_drop_num{get{return m_t_drop_num;} set{m_t_drop_num = value;}} // 总共掉落数量 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_cost_id = XBuffer.ReadInt(data, ref offset);
					m_t_cost_num = XBuffer.ReadInt(data, ref offset);
					m_t_replace_id = XBuffer.ReadInt(data, ref offset);
					m_t_replace_num = XBuffer.ReadInt(data, ref offset);
					m_t_free_num = XBuffer.ReadInt(data, ref offset);
					m_t_free_interval = XBuffer.ReadInt(data, ref offset);
					m_t_open_level = XBuffer.ReadInt(data, ref offset);
					m_t_buy_num = XBuffer.ReadInt(data, ref offset);
					t_fix_drop = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_weight_drop = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_n_choose_1 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_drop_num = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


