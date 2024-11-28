/**
 * Auto generated, do not edit it
 *
 * t_trial_buff
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_trial_buffBean : BaseBin
	{
				private int m_t_id; // ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID 
				private int m_t_attr_type; // 加成属性ID（1=攻、2=防、3=格挡、4=暴击、5=吸血、6=反伤、7=单体回怒、8=群体回怒、9=单体回血、10=群体回血、11=复活） 
				public int t_attr_type{get{return m_t_attr_type;} set{m_t_attr_type = value;}} // 加成属性ID（1=攻、2=防、3=格挡、4=暴击、5=吸血、6=反伤、7=单体回怒、8=群体回怒、9=单体回血、10=群体回血、11=复活） 
				private int m_t_attr_value; // 加成属性值 
				public int t_attr_value{get{return m_t_attr_value;} set{m_t_attr_value = value;}} // 加成属性值 
				private int m_t_price; // 购买价格 
				public int t_price{get{return m_t_price;} set{m_t_price = value;}} // 购买价格 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_attr_type = XBuffer.ReadInt(data, ref offset);
					m_t_attr_value = XBuffer.ReadInt(data, ref offset);
					m_t_price = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


