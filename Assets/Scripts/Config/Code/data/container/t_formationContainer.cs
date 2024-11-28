/**
 * Auto generated, do not edit it
 *
 * t_formation
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_formationContainer : BaseContainer
	{
		private List<t_formationBean> list = new List<t_formationBean>();
		private Dictionary<int, t_formationBean> map = new Dictionary<int, t_formationBean>();

		//public override List<t_formationBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_formationBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_formationBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_formationBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_formationBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_formationBean bean = new t_formationBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_formationContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_formationBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_formationBean).Name + ".bytes");
			}
		}
	}

}


