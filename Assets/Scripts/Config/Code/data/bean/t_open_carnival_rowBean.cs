/**
 * Auto generated, do not edit it
 *
 * t_open_carnival_row
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_open_carnival_rowBean : BaseBin
	{
				private int m_t_id; // id（行） 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id（行） 
				private int m_t_open_day; // 开服天数 
				public int t_open_day{get{return m_t_open_day;} set{m_t_open_day = value;}} // 开服天数 
				public string t_column {get; set;}   // 列id
				private string m_t_name;   // 名字id
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
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_open_day = XBuffer.ReadInt(data, ref offset);
					t_column = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
		} 

	}
}


