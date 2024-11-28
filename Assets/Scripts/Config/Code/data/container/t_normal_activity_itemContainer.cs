/**
 * Auto generated, do not edit it
 *
 * t_normal_activity_item
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_normal_activity_itemContainer : BaseContainer
	{
		private List<t_normal_activity_itemBean> list = new List<t_normal_activity_itemBean>();
		private Dictionary<int, t_normal_activity_itemBean> map = new Dictionary<int, t_normal_activity_itemBean>();

		//public override List<t_normal_activity_itemBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_normal_activity_itemBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_normal_activity_itemBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_normal_activity_itemBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_normal_activity_itemBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_normal_activity_itemBean bean = new t_normal_activity_itemBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_normal_activity_itemContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_normal_activity_itemBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_normal_activity_itemBean).Name + ".bytes");
			}
		}
	}

}


