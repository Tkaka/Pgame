/**
 * Auto generated, do not edit it
 *
 * t_buff
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_buffBean : BaseBin
	{
				private int m_t_id; // buff ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // buff ID 
				private int m_t_relace_mark; // 叠加标识 
				public int t_relace_mark{get{return m_t_relace_mark;} set{m_t_relace_mark = value;}} // 叠加标识 
				private int m_t_replace_type; // 叠加规则 
				public int t_replace_type{get{return m_t_replace_type;} set{m_t_replace_type = value;}} // 叠加规则 
				private int m_t_show_type; // 特效显示类别 
				public int t_show_type{get{return m_t_show_type;} set{m_t_show_type = value;}} // 特效显示类别 
				private int m_t_show_mark; // UI叠加标识 
				public int t_show_mark{get{return m_t_show_mark;} set{m_t_show_mark = value;}} // UI叠加标识 
				private int m_t_ui_replace_type; // ui叠加显示规则 
				public int t_ui_replace_type{get{return m_t_ui_replace_type;} set{m_t_ui_replace_type = value;}} // ui叠加显示规则 
				private int m_t_type; // 1=增益 -1=减益 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 1=增益 -1=减益 
				private int m_t_priority; // 优先级 
				public int t_priority{get{return m_t_priority;} set{m_t_priority = value;}} // 优先级 
				private int m_t_is_ctrl; // 是否为控制类buff 0=否 1=是 
				public int t_is_ctrl{get{return m_t_is_ctrl;} set{m_t_is_ctrl = value;}} // 是否为控制类buff 0=否 1=是 
				private int m_t_fun_type; // buff功能类别   
				public int t_fun_type{get{return m_t_fun_type;} set{m_t_fun_type = value;}} // buff功能类别   
				private int m_t_property_id; // 属性id 
				public int t_property_id{get{return m_t_property_id;} set{m_t_property_id = value;}} // 属性id 
				private int m_t_base_cur; // 操作基础值还是当前值 0=基础值 1=当前值 
				public int t_base_cur{get{return m_t_base_cur;} set{m_t_base_cur = value;}} // 操作基础值还是当前值 0=基础值 1=当前值 
				private int m_t_per_int; // 百分比或数值 0=百分比 1=数值 
				public int t_per_int{get{return m_t_per_int;} set{m_t_per_int = value;}} // 百分比或数值 0=百分比 1=数值 
				public string t_icon {get; set;}   // buff ui图片
				public string t_prefab {get; set;}   // buff特效
				private int m_t_buff_param; // buff默认参数（105buff转魂，转成魂大类的ID;106buff驱散，为驱散优先级N以下的debuff和控制buff） 
				public int t_buff_param{get{return m_t_buff_param;} set{m_t_buff_param = value;}} // buff默认参数（105buff转魂，转成魂大类的ID;106buff驱散，为驱散优先级N以下的debuff和控制buff） 
				private int m_t_buff_param2; // buff参数2(当类型为驱散时,1=驱散控制;2=驱散负面3=驱散正面) 
				public int t_buff_param2{get{return m_t_buff_param2;} set{m_t_buff_param2 = value;}} // buff参数2(当类型为驱散时,1=驱散控制;2=驱散负面3=驱散正面) 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_relace_mark = XBuffer.ReadInt(data, ref offset);
					m_t_replace_type = XBuffer.ReadInt(data, ref offset);
					m_t_show_type = XBuffer.ReadInt(data, ref offset);
					m_t_show_mark = XBuffer.ReadInt(data, ref offset);
					m_t_ui_replace_type = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					m_t_priority = XBuffer.ReadInt(data, ref offset);
					m_t_is_ctrl = XBuffer.ReadInt(data, ref offset);
					m_t_fun_type = XBuffer.ReadInt(data, ref offset);
					m_t_property_id = XBuffer.ReadInt(data, ref offset);
					m_t_base_cur = XBuffer.ReadInt(data, ref offset);
					m_t_per_int = XBuffer.ReadInt(data, ref offset);
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_prefab = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_buff_param = XBuffer.ReadInt(data, ref offset);
					m_t_buff_param2 = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


