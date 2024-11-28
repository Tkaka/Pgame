/**
 * Auto generated, do not edit it
 *
 * t_drawitem
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_drawitemBean : BaseBin
	{
				private int m_t_id; // ID；类型*1000+小类型

 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID；类型*1000+小类型

 
				private int m_t_type; // 类型：1金币，2钻石

 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 类型：1金币，2钻石

 
				public string t_limit {get; set;}   // 等级区间  -1表示无上限
				public string t_item {get; set;}   // 单次抽卡随机道具库：掉落id1+数量1+权重1; （N 选 1）
				public string t_ten_nn {get; set;}   // 十连抽随机道具库（N 选 N）（掉落ID+掉落ID）
				public string t_ten_nm {get; set;}   // 十连抽随机道具库（10-N 选 10-N）
				public string t_ten_item {get; set;}   // 每十次抽卡随机道具库：掉落id1+数量1+权重1; 
				private int m_t_tenth; // 第10次抽卡替换每十次随机掉落库 
				public int t_tenth{get{return m_t_tenth;} set{m_t_tenth = value;}} // 第10次抽卡替换每十次随机掉落库 
				private int m_t_forty; // 第40次抽卡替换每十次随机掉落库 
				public int t_forty{get{return m_t_forty;} set{m_t_forty = value;}} // 第40次抽卡替换每十次随机掉落库 
				public string t_item_list {get; set;}   // 道具奖励列表
				public string t_pet_list {get; set;}   // 宝贝奖励ID
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					t_limit = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_item = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_ten_nn = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_ten_nm = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_ten_item = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_tenth = XBuffer.ReadInt(data, ref offset);
					m_t_forty = XBuffer.ReadInt(data, ref offset);
					t_item_list = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_pet_list = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


