/**
 * Auto generated, do not edit it
 *
 * t_exhibition
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_exhibitionBean : BaseBin
	{
				private int m_t_id; // 展厅ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 展厅ID 
				private string m_t_name;   // 展厅名字语言ID
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
				private int m_t_type; // 加成类型（攻击1，防御2，技能3） 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 加成类型（攻击1，防御2，技能3） 
				private int m_t_level; // 开启等级 
				public int t_level{get{return m_t_level;} set{m_t_level = value;}} // 开启等级 
				public string t_pets {get; set;}   // 包含铜像(宠物ID)
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_type = XBuffer.ReadInt(data, ref offset);
					m_t_level = XBuffer.ReadInt(data, ref offset);
					t_pets = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


