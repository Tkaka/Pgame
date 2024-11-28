/**
 * Auto generated, do not edit it
 *
 * t_fetter
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_fetterContainer : BaseContainer
	{
		private List<t_fetterBean> list = new List<t_fetterBean>();
		private Dictionary<int, t_fetterBean> map = new Dictionary<int, t_fetterBean>();

		//public override List<t_fetterBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_fetterBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_fetterBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_fetterBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_fetterBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_fetterBean bean = new t_fetterBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_fetterContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_fetterBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_fetterBean).Name + ".bytes");
			}
		}
	}

}


