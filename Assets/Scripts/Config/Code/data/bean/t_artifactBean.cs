/**
 * Auto generated, do not edit it
 *
 * t_artifact
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_artifactBean : BaseBin
	{
				private int m_t_id; // ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID 
				public string t_base_attr {get; set;}   // 初始属性
				public string t_add_attr {get; set;}   // 升级增加属性
				private int m_t_base_level; // 开放等级 
				public int t_base_level{get{return m_t_base_level;} set{m_t_base_level = value;}} // 开放等级 
				private int m_t_max_level; // 最高等级 
				public int t_max_level{get{return m_t_max_level;} set{m_t_max_level = value;}} // 最高等级 
				private string m_t_name;   // 名字语言ID
				public string t_name
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_name, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_name;
                        }
                        else
                            return m_t_name;
                    }
                    set { m_t_name = value; }
                } 
				public string t_open_condition {get; set;}   // 开放条件
				public string t_condition_id {get; set;}   // 条件语言ID
				public string t_introduce_id {get; set;}   // 简介id
				public string t_attr_id {get; set;}   // 加成的属性ID
				public string t_train_free {get; set;}   // 免费培养
				public string t_train_gold {get; set;}   // 金币培养
				public string t_train_diamond {get; set;}   // 钻石培养
				public string t_model {get; set;}   // 神器模型
				public string t_icon {get; set;}   // 神器图标
				public string t_comsume {get; set;}   // 消耗
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_base_attr = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_add_attr = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_base_level = XBuffer.ReadInt(data, ref offset);
					m_t_max_level = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_open_condition = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_condition_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_introduce_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_attr_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_train_free = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_train_gold = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_train_diamond = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_model = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_comsume = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


