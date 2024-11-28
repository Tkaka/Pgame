/**
 * Auto generated, do not edit it
 *
 * t_guide
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_guideBean : BaseBin
	{
				private int m_t_id; // 新手ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 新手ID 
				public string t_trigger {get; set;}   // 触发条件(条件可以叠加eg. 1+1+101;2+30)
				public string t_step {get; set;}   // 使用步骤
				private int m_t_block; // 所有填写为1的引导只能同时触发一个 
				public int t_block{get{return m_t_block;} set{m_t_block = value;}} // 所有填写为1的引导只能同时触发一个 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_trigger = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_step = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_block = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


