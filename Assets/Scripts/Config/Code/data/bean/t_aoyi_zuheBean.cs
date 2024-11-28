/**
 * Auto generated, do not edit it
 *
 * t_aoyi_zuhe
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_aoyi_zuheBean : BaseBin
	{
				private int m_t_id; // 奥义技能id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 奥义技能id 
				public string t_group {get; set;}   // 组合(方向)
				public string t_property {get; set;}   // ;
				private string m_t_name;   // 名字
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
					t_group = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_property = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
		} 

	}
}


