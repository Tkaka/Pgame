/**
 * Auto generated, do not edit it
 *
 * t_language
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_languageContainer : BaseContainer
	{
		private List<t_languageBean> list = new List<t_languageBean>();
		private Dictionary<int, t_languageBean> map = new Dictionary<int, t_languageBean>();

		//public override List<t_languageBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_languageBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_languageBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_languageBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_languageBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_languageBean bean = new t_languageBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_languageContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_languageBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_languageBean).Name + ".bytes");
			}
		}
	}

}


