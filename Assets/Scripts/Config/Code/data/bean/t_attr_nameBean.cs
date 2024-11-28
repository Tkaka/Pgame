/**
 * Auto generated, do not edit it
 *
 * t_attr_name
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_attr_nameBean : BaseBin
	{
				private int m_t_id; // ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // ID 
				private string m_t_name_id;   // 语言包id
				public string t_name_id
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_name_id, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_name_id;
                        }
                        else
                            return m_t_name_id;
                    }
                    set { m_t_name_id = value; }
                } 
				private int m_t_value_type; // 值类型（1算法类型为值相加  0算法类型为百分比类型） 
				public int t_value_type{get{return m_t_value_type;} set{m_t_value_type = value;}} // 值类型（1算法类型为值相加  0算法类型为百分比类型） 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_value_type = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


