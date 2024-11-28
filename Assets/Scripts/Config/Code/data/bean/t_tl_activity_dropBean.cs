/**
 * Auto generated, do not edit it
 *
 * t_tl_activity_drop
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_tl_activity_dropBean : BaseBin
	{
				private int m_t_id; // ID-小类型（1=限时装备宝箱）

 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID-小类型（1=限时装备宝箱）

 
				public string t_item {get; set;}   // 单次抽卡随机道具库：掉落id1+数量1+权重1; （N 选 1）
				public string t_ten_nn {get; set;}   // 十连抽随机道具库（N 选 N）（掉落ID+掉落ID）
				public string t_ten_item {get; set;}   // 每十次抽卡随机道具库：掉落id1+数量1+权重1; 
				public string t_ten_nm {get; set;}   // 十连抽随机道具库（10-N 选 10-N）
				public string t_item_list {get; set;}   // 道具奖励列表
				private int m_t_num; // N连抽次数 
				public int t_num{get{return m_t_num;} set{m_t_num = value;}} // N连抽次数 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_item = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_ten_nn = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_ten_item = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_ten_nm = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_item_list = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_num = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


