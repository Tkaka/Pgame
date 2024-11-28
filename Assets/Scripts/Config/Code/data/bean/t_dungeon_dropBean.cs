/**
 * Auto generated, do not edit it
 *
 * t_dungeon_drop
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_dungeon_dropBean : BaseBin
	{
				private int m_t_id; // ID（关卡ID） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID（关卡ID） 
				private int m_t_drop_mode; // 掉落模式 1 = n选m, 2= n选1 
				public int t_drop_mode{get{return m_t_drop_mode;} set{m_t_drop_mode = value;}} // 掉落模式 1 = n选m, 2= n选1 
				public string t_drop {get; set;}   // 掉落道具配置（ID1+掉率1+数量1+权重1+数量2+权重2+…;+ID2+掉率2+…）
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_drop_mode = XBuffer.ReadInt(data, ref offset);
					t_drop = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


