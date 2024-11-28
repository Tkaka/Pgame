/**
 * Auto generated, do not edit it
 *
 * t_skill_property
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_skill_propertyBean : BaseBin
	{
				private int m_t_id; // 奥义技能属性(宠物ID+500）*10000+（技能流水号*10000）+品质*1000 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 奥义技能属性(宠物ID+500）*10000+（技能流水号*10000）+品质*1000 
				public string t_property {get; set;}   // 属性Id+值;...
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_property = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


