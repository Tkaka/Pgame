/**
 * Auto generated, do not edit it
 *
 * t_sensitive_word
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_sensitive_wordContainer : BaseContainer
	{
		private List<t_sensitive_wordBean> list = new List<t_sensitive_wordBean>();
		private Dictionary<int, t_sensitive_wordBean> map = new Dictionary<int, t_sensitive_wordBean>();

		//public override List<t_sensitive_wordBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_sensitive_wordBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_sensitive_wordBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_sensitive_wordBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_sensitive_wordBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_sensitive_wordBean bean = new t_sensitive_wordBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_sensitive_wordContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_sensitive_wordBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_sensitive_wordBean).Name + ".bytes");
			}
		}
	}

}


