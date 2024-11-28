/**
 * Auto generated, do not edit it
 *
 * t_aoyi_draw
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_aoyi_drawContainer : BaseContainer
	{
		private List<t_aoyi_drawBean> list = new List<t_aoyi_drawBean>();
		private Dictionary<int, t_aoyi_drawBean> map = new Dictionary<int, t_aoyi_drawBean>();

		//public override List<t_aoyi_drawBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_aoyi_drawBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_aoyi_drawBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_aoyi_drawBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_aoyi_drawBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_aoyi_drawBean bean = new t_aoyi_drawBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_aoyi_drawContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_aoyi_drawBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_aoyi_drawBean).Name + ".bytes");
			}
		}
	}

}


