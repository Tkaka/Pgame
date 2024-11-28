/**
 * Auto generated, do not edit it
 *
 * t_activity_act
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_activity_actContainer : BaseContainer
	{
		private List<t_activity_actBean> list = new List<t_activity_actBean>();
		private Dictionary<int, t_activity_actBean> map = new Dictionary<int, t_activity_actBean>();

		//public override List<t_activity_actBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_activity_actBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_activity_actBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_activity_actBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_activity_actBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_activity_actBean bean = new t_activity_actBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_activity_actContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_activity_actBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_activity_actBean).Name + ".bytes");
			}
		}
	}

}


