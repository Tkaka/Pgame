/**
 * Auto generated, do not edit it
 *
 * t_theme
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_themeContainer : BaseContainer
	{
		private List<t_themeBean> list = new List<t_themeBean>();
		private Dictionary<int, t_themeBean> map = new Dictionary<int, t_themeBean>();

		//public override List<t_themeBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_themeBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_themeBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_themeBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_themeBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_themeBean bean = new t_themeBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_themeContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_themeBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_themeBean).Name + ".bytes");
			}
		}
	}

}


