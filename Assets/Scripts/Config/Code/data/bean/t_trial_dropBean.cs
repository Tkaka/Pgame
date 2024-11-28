/**
 * Auto generated, do not edit it
 *
 * t_trial_drop
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_trial_dropBean : BaseBin
	{
				private int m_t_id; // ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID 
				public string t_fix_drop {get; set;}   // 固定掉落（道具ID1+道具数量1;道具ID2+道具数量2）
				public string t_random_drop_m {get; set;}   // 随机n选m掉落（道具ID1+概率1+权重1+数量1+权重2+数量2;道具ID2+概率2+权重3+数量3+权重4+数量4）
				public string t_random_drop_1 {get; set;}   // 随机n选1掉落（道具ID1+权重1+数量1;道具ID2+权重2+数量2）
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_fix_drop = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_random_drop_m = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_random_drop_1 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


