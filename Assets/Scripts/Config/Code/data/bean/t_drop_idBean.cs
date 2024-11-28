/**
 * Auto generated, do not edit it
 *
 * t_drop_id
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_drop_idBean : BaseBin
	{
				private int m_t_id; // 掉落ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 掉落ID 
				public string t_item {get; set;}   // 道具ID+数量+权重;道具1ID+数量1+权重1;道具2ID+数量2+权重2;
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_item = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


