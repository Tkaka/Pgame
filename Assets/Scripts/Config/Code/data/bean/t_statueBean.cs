/**
 * Auto generated, do not edit it
 *
 * t_statue
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_statueBean : BaseBin
	{
				private int m_t_id; // 铜像ID（宠物ID*材质） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 铜像ID（宠物ID*材质） 
				public string t_add_prop {get; set;}   // 四个品阶加成攻防血
				public string t_model {get; set;}   // 铜像模型
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_add_prop = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_model = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


