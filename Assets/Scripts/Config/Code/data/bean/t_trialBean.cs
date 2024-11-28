/**
 * Auto generated, do not edit it
 *
 * t_trial
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_trialBean : BaseBin
	{
				private int m_t_id; // ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID 
				private int m_t_type; // 层类型（1：怪物，2：宝箱，3：属性） 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 层类型（1：怪物，2：宝箱，3：属性） 
				private int m_t_free_box; // 免费宝箱掉落ID 
				public int t_free_box{get{return m_t_free_box;} set{m_t_free_box = value;}} // 免费宝箱掉落ID 
				private int m_t_diamond_box; // 钻石宝箱掉落ID 
				public int t_diamond_box{get{return m_t_diamond_box;} set{m_t_diamond_box = value;}} // 钻石宝箱掉落ID 
				public string t_attr_cost_star {get; set;}   // 属性购买需求的星星
				public string t_base_score {get; set;}   // 基础积分
				public string t_arg {get; set;}   // 难度参数
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					m_t_free_box = XBuffer.ReadInt(data, ref offset);
					m_t_diamond_box = XBuffer.ReadInt(data, ref offset);
					t_attr_cost_star = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_base_score = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_arg = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


