/**
 * Auto generated, do not edit it
 *
 * t_trial_robot
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_trial_robotBean : BaseBin
	{
				private int m_t_id; // ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID 
				public string t_pets {get; set;}   // 宠物id
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_pets = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


