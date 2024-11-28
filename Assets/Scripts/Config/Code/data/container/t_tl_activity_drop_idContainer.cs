/**
 * Auto generated, do not edit it
 *
 * t_tl_activity_drop_id
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_tl_activity_drop_idContainer : BaseContainer
	{
		private List<t_tl_activity_drop_idBean> list = new List<t_tl_activity_drop_idBean>();
		private Dictionary<int, t_tl_activity_drop_idBean> map = new Dictionary<int, t_tl_activity_drop_idBean>();

		//public override List<t_tl_activity_drop_idBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_tl_activity_drop_idBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_tl_activity_drop_idBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_tl_activity_drop_idBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_tl_activity_drop_idBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_tl_activity_drop_idBean bean = new t_tl_activity_drop_idBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_tl_activity_drop_idContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_tl_activity_drop_idBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_tl_activity_drop_idBean).Name + ".bytes");
			}
		}
	}

}


