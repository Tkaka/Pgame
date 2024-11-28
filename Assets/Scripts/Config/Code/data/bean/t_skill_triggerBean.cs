/**
 * Auto generated, do not edit it
 *
 * t_skill_trigger
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_skill_triggerBean : BaseBin
	{
				private int m_t_id; // 条件ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 条件ID 
				private int m_t_type; // 类型(1=被暴击 2=被格挡 3=被造成伤害 4=出现暴击 5=出现格挡 6=死亡 7=被附加负面状态 9=每个完整的回合开始前) 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 类型(1=被暴击 2=被格挡 3=被造成伤害 4=出现暴击 5=出现格挡 6=死亡 7=被附加负面状态 9=每个完整的回合开始前) 
				private int m_t_camp; // 阵营 100=自己方 200=敌方 300=双方 400= 自己 
				public int t_camp{get{return m_t_camp;} set{m_t_camp = value;}} // 阵营 100=自己方 200=敌方 300=双方 400= 自己 
				private int m_t_pet_id; // 对象ID（宝贝ID+boss） 
				public int t_pet_id{get{return m_t_pet_id;} set{m_t_pet_id = value;}} // 对象ID（宝贝ID+boss） 
				private int m_t_param; // 参数 
				public int t_param{get{return m_t_param;} set{m_t_param = value;}} // 参数 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					m_t_camp = XBuffer.ReadInt(data, ref offset);
					m_t_pet_id = XBuffer.ReadInt(data, ref offset);
					m_t_param = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


