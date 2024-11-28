/**
 * Auto generated, do not edit it
 *
 * t_title
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_titleBean : BaseBin
	{
				private int m_t_id; // id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id 
				private string m_t_name;   // 名字ID
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
				private int m_t_condition; // 达成成就点数 
				public int t_condition{get{return m_t_condition;} set{m_t_condition = value;}} // 达成成就点数 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_condition = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


