/**
 * Auto generated, do not edit it
 *
 * t_color_name_bean
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_color_name_beanBean : BaseBin
	{
				private int m_t_id; // Id---(品阶) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // Id---(品阶) 
				private int m_t_color; // 名字颜色(1白,2绿,3蓝,4紫,5橙,6红) 
				public int t_color{get{return m_t_color;} set{m_t_color = value;}} // 名字颜色(1白,2绿,3蓝,4紫,5橙,6红) 
				private int m_t_num; // 名字的后缀 
				public int t_num{get{return m_t_num;} set{m_t_num = value;}} // 名字的后缀 
				public string t_border {get; set;}   // 边框
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_color = XBuffer.ReadInt(data, ref offset);
					m_t_num = XBuffer.ReadInt(data, ref offset);
					t_border = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


