/**
 * Auto generated, do not edit it
 *
 * t_skill_scope
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_skill_scopeBean : BaseBin
	{
				private int m_t_id; // 范围ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 范围ID 
				private int m_t_camp; // 阵营 100=自己方 200=敌方 300=双方
 
				public int t_camp{get{return m_t_camp;} set{m_t_camp = value;}} // 阵营 100=自己方 200=敌方 300=双方
 
				private int m_t_range_type; // 范围类型 1=前排 2=后排 3=全体 4=自己所在行 5=自己所在列 6=目标所在行 7=目标所在列 8=自己 9=自己前排 10=自己后排11=自己以及身后12=目标周围98=主效果目标 
				public int t_range_type{get{return m_t_range_type;} set{m_t_range_type = value;}} // 范围类型 1=前排 2=后排 3=全体 4=自己所在行 5=自己所在列 6=目标所在行 7=目标所在列 8=自己 9=自己前排 10=自己后排11=自己以及身后12=目标周围98=主效果目标 
				private int m_t_sex; // 性别 0=不分 1=男性 2=女性 
				public int t_sex{get{return m_t_sex;} set{m_t_sex = value;}} // 性别 0=不分 1=男性 2=女性 
				private int m_t_condition_type; // 条件类型 2=种族  3=类型类 4=首领类 5=基础属性类 6=当前属性类7=指定宝贝8=中了控制buff的9=种族是否元素类10=魂类 
				public int t_condition_type{get{return m_t_condition_type;} set{m_t_condition_type = value;}} // 条件类型 2=种族  3=类型类 4=首领类 5=基础属性类 6=当前属性类7=指定宝贝8=中了控制buff的9=种族是否元素类10=魂类 
				private int m_t_int_param1; // 条件类型参数1【（如果是血量最多最少，那么2=最低，1=最高；7：宝贝ID;9：种族是否元素系（1=元素对女，0=非元对男）） 
				public int t_int_param1{get{return m_t_int_param1;} set{m_t_int_param1 = value;}} // 条件类型参数1【（如果是血量最多最少，那么2=最低，1=最高；7：宝贝ID;9：种族是否元素系（1=元素对女，0=非元对男）） 
				public string t_str_param1 {get; set;}   // 条件参数字符串参数1（类型7和10用来指定多个宝贝）
				private int m_t_int_param2; // 【2：；6：】 
				public int t_int_param2{get{return m_t_int_param2;} set{m_t_int_param2 = value;}} // 【2：；6：】 
				private int m_t_count; // 选择数量（期望的最大值） 
				public int t_count{get{return m_t_count;} set{m_t_count = value;}} // 选择数量（期望的最大值） 
				private int m_t_include_self; // 是否包含自己（目标） 0=包含 1=不包含 
				public int t_include_self{get{return m_t_include_self;} set{m_t_include_self = value;}} // 是否包含自己（目标） 0=包含 1=不包含 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_camp = XBuffer.ReadInt(data, ref offset);
					m_t_range_type = XBuffer.ReadInt(data, ref offset);
					m_t_sex = XBuffer.ReadInt(data, ref offset);
					m_t_condition_type = XBuffer.ReadInt(data, ref offset);
					m_t_int_param1 = XBuffer.ReadInt(data, ref offset);
					t_str_param1 = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_int_param2 = XBuffer.ReadInt(data, ref offset);
					m_t_count = XBuffer.ReadInt(data, ref offset);
					m_t_include_self = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


