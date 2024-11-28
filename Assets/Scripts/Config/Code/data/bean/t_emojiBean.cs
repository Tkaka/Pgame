/**
 * Auto generated, do not edit it
 *
 * t_emoji
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_emojiBean : BaseBin
	{
				private int m_t_id; // ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID 
				public string t_emoji_icon {get; set;}   // 表情的图片路径
				public string t_emoji_name {get; set;}   // 表情名
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_emoji_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_emoji_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


