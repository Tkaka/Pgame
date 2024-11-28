/**
 * Auto generated, do not edit it
 *
 * t_mail
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_mailBean : BaseBin
	{
				private int m_t_id; // 邮件模板ID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 邮件模板ID 
				private int m_t_topic; // 邮件主题（语言包） 
				public int t_topic{get{return m_t_topic;} set{m_t_topic = value;}} // 邮件主题（语言包） 
				private int m_t_content; // 内容描述（语言包） 
				public int t_content{get{return m_t_content;} set{m_t_content = value;}} // 内容描述（语言包） 
				private int m_t_sender; // 发件人（语言包） 
				public int t_sender{get{return m_t_sender;} set{m_t_sender = value;}} // 发件人（语言包） 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_topic = XBuffer.ReadInt(data, ref offset);
					m_t_content = XBuffer.ReadInt(data, ref offset);
					m_t_sender = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


