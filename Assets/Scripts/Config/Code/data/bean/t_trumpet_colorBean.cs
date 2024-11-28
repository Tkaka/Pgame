/**
 * Auto generated, do not edit it
 *
 * t_trumpet_color
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_trumpet_colorBean : BaseBin
	{
				private int m_t_id; // ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID 
				public string t_color_icon {get; set;}   // 喇叭颜色（应该配图片路径，现暂时配为文本）
				public string t_color_value {get; set;}   // 颜色值r+g+b+a(都是0-1)
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_color_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_color_value = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


