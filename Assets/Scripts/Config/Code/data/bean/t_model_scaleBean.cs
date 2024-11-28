/**
 * Auto generated, do not edit it
 *
 * t_model_scale
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_model_scaleBean : BaseBin
	{
				public string t_id {get; set;}   // 模型资源名
				private int m_t_boss_biref; // 战斗中boss简介界面 
				public int t_boss_biref{get{return m_t_boss_biref;} set{m_t_boss_biref = value;}} // 战斗中boss简介界面 
		
		public void LoadData(byte[] data, ref int offset)
		{
					t_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_boss_biref = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


