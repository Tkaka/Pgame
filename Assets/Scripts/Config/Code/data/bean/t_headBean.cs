/**
 * Auto generated, do not edit it
 *
 * t_head
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_headBean : BaseBin
	{
				private int m_t_id; // id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id 
				public string t_icon {get; set;}   // 头像ui
				private int m_t_cond_type; // 限制类型（1：宠物形态，2：拥有皮肤3:mage进化） 
				public int t_cond_type{get{return m_t_cond_type;} set{m_t_cond_type = value;}} // 限制类型（1：宠物形态，2：拥有皮肤3:mage进化） 
				public string t_cond_arg {get; set;}   // 限制参数(宝贝id+星级/道具id
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_cond_type = XBuffer.ReadInt(data, ref offset);
					t_cond_arg = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


