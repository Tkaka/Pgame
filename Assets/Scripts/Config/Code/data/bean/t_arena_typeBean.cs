/**
 * Auto generated, do not edit it
 *
 * t_arena_type
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_arena_typeBean : BaseBin
	{
				private int m_t_id; // ID就是类型（1竞技场 2拳皇争霸 3社团战 4世界首领） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID就是类型（1竞技场 2拳皇争霸 3社团战 4世界首领） 
				public string t_reward {get; set;}   // 奖励预览（道具id+道具id)
				public string t_icon {get; set;}   // 背景图
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_reward = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


