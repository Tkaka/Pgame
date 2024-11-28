/**
 * Auto generated, do not edit it
 *
 * t_global
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_globalContainer : BaseContainer
	{
		private List<t_globalBean> list = new List<t_globalBean>();
		private Dictionary<int, t_globalBean> map = new Dictionary<int, t_globalBean>();

		//public override List<t_globalBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_globalBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_globalBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_globalBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_globalBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_globalBean bean = new t_globalBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_globalContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_globalBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_globalBean).Name + ".bytes");
			}
		}
	}

}


