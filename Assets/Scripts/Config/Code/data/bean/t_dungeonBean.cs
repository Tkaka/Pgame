/**
 * Auto generated, do not edit it
 *
 * t_dungeon
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_dungeonBean : BaseBin
	{
				private int m_t_id; // 副本ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 副本ID 
				private string m_t_name_id;   // 副本名称语言包ID
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
				public string t_chapter {get; set;}   // 章节列表
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_chapter = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


