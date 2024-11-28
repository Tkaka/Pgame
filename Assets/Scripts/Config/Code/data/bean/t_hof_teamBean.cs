/**
 * Auto generated, do not edit it
 *
 * t_hof_team
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_hof_teamBean : BaseBin
	{
				private int m_t_id; // 战队ID（宠物id*100+1） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 战队ID（宠物id*100+1） 
				public string t_pets {get; set;}   // 包含的宠物ID
				public string t_food {get; set;}   // 食物ID列表
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_pets = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_food = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


