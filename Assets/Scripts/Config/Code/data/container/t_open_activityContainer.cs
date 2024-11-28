/**
 * Auto generated, do not edit it
 *
 * t_open_activity
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_open_activityContainer : BaseContainer
	{
		private List<t_open_activityBean> list = new List<t_open_activityBean>();
		private Dictionary<int, t_open_activityBean> map = new Dictionary<int, t_open_activityBean>();

		//public override List<t_open_activityBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_open_activityBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_open_activityBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_open_activityBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_open_activityBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_open_activityBean bean = new t_open_activityBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_open_activityContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_open_activityBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_open_activityBean).Name + ".bytes");
			}
		}
	}

}


