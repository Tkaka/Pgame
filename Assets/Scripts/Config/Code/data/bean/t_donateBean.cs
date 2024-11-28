/**
 * Auto generated, do not edit it
 *
 * t_donate
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_donateBean : BaseBin
	{
				private int m_t_id; // (类型 * 1000 + level)(2公会成员数量 3工会精英数量 4金币加成 5钻石加成 6神器之源) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // (类型 * 1000 + level)(2公会成员数量 3工会精英数量 4金币加成 5钻石加成 6神器之源) 
				private int m_t_exp; // 该等级所需经验 
				public int t_exp{get{return m_t_exp;} set{m_t_exp = value;}} // 该等级所需经验 
				public string t_param {get; set;}   // 效果
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_exp = XBuffer.ReadInt(data, ref offset);
					t_param = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


