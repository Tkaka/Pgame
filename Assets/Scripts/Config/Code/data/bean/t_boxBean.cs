/**
 * Auto generated, do not edit it
 *
 * t_box
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_boxBean : BaseBin
	{
				private int m_t_id; // Id(填写道具表中的宝箱ID) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // Id(填写道具表中的宝箱ID) 
				public string t_fix_drop {get; set;}   // 必然掉落（道具+数量）
				public string t_n_choose_m {get; set;}   // 掉落道具id+数量+概率
				public string t_n_choose_1 {get; set;}   // 掉落道具id+数量+权重
				private int m_t_limit; // 等级限制 
				public int t_limit{get{return m_t_limit;} set{m_t_limit = value;}} // 等级限制 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_fix_drop = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_n_choose_m = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_n_choose_1 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_limit = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


