/**
 * Auto generated, do not edit it
 *
 * t_equip_attr
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_equip_attrBean : BaseBin
	{
				private int m_t_id; // ID（装备类别*100+ 品阶) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID（装备类别*100+ 品阶) 
				public string t_base_atk {get; set;}   // N品N星0级攻击基础值总值
				public string t_base_def {get; set;}   // N品N星0级防御基础值总值
				public string t_base_hp {get; set;}   // N品N星0级生命基础值总值
				public string t_lv_up_atk {get; set;}   // N品N星每升1级增加攻击值
				public string t_lv_up_def {get; set;}   // N品N星每升1级增加防御值
				public string t_lv_up_hp {get; set;}   // N品N星每升1级增加生命值
				private int m_t_lv_base; // 当前品最低等级 
				public int t_lv_base{get{return m_t_lv_base;} set{m_t_lv_base = value;}} // 当前品最低等级 
				private int m_t_lv_max; // 当前品可升最高等级 
				public int t_lv_max{get{return m_t_lv_max;} set{m_t_lv_max = value;}} // 当前品可升最高等级 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_base_atk = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_base_def = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_base_hp = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_lv_up_atk = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_lv_up_def = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_lv_up_hp = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_lv_base = XBuffer.ReadInt(data, ref offset);
					m_t_lv_max = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


