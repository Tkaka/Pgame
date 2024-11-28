/**
 * Auto generated, do not edit it
 *
 * t_pet_attr_arg
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_pet_attr_argBean : BaseBin
	{
				private int m_t_id; // Id(1开头=等级加成属性 2开头=升品加成成长3=升品成长) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // Id(1开头=等级加成属性 2开头=升品加成成长3=升品成长) 
				private int m_t_atk; // 加成成长-攻 
				public int t_atk{get{return m_t_atk;} set{m_t_atk = value;}} // 加成成长-攻 
				private int m_t_def; // 加成成长-防 
				public int t_def{get{return m_t_def;} set{m_t_def = value;}} // 加成成长-防 
				private int m_t_hp; // 加成成长-血 
				public int t_hp{get{return m_t_hp;} set{m_t_hp = value;}} // 加成成长-血 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_atk = XBuffer.ReadInt(data, ref offset);
					m_t_def = XBuffer.ReadInt(data, ref offset);
					m_t_hp = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


