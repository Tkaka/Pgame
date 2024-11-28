/**
 * Auto generated, do not edit it
 *
 * t_money
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_moneyBean : BaseBin
	{
				private int m_t_id; // Id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // Id 
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
				public string t_icon {get; set;}   // 代币图标
				public string t_icon_rule {get; set;}   // 大于等于{数量}+大于等于{数量}
				public string t_quality {get; set;}   // 品质    0=白色 1=绿色 2=蓝色 3=紫色 4=橙色 5=红色
				private string m_t_describe;   // 代币描述
				public string t_describe
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_describe, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_describe;
                        }
                        else
                            return m_t_describe;
                    }
                    set { m_t_describe = value; }
                } 
				private int m_t_source; // 来源关卡id 
				public int t_source{get{return m_t_source;} set{m_t_source = value;}} // 来源关卡id 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_icon_rule = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_quality = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_describe = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_source = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


