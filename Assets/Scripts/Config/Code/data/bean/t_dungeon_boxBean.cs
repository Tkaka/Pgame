/**
 * Auto generated, do not edit it
 *
 * t_dungeon_box
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_dungeon_boxBean : BaseBin
	{
				private int m_t_id; // 宝箱ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 宝箱ID 
				public string t_drop {get; set;}   // 宝箱内容（道具ID+数量；道具ID+数量）
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_drop = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


