/**
 * Auto generated, do not edit it
 *
 * t_authority
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_authorityBean : BaseBin
	{
				private int m_t_id; // 职位id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 职位id 
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
				public string t_authority_type {get; set;}   // 权限列表(0=什么都不做 1=解散公会 2=修改名字 3=修改公会徽章 4=修改公会类型 5=修改公告 6=招募限制设置 7=招募会员 8=管理申请者列表 9=发放公会邮件 10=修改会长 11=修改副会长 12=修改精英 13=踢人 14=领福利
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_authority_type = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


