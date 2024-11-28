/**
 * Auto generated, do not edit it
 *
 * t_aoyi_list
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_aoyi_listContainer : BaseContainer
	{
		private List<t_aoyi_listBean> list = new List<t_aoyi_listBean>();
		private Dictionary<int, t_aoyi_listBean> map = new Dictionary<int, t_aoyi_listBean>();

		//public override List<t_aoyi_listBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_aoyi_listBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_aoyi_listBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_aoyi_listBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_aoyi_listBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_aoyi_listBean bean = new t_aoyi_listBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_aoyi_listContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_aoyi_listBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_aoyi_listBean).Name + ".bytes");
			}
		}
	}

}

