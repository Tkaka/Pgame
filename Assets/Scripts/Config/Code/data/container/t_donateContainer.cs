/**
 * Auto generated, do not edit it
 *
 * t_donate
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_donateContainer : BaseContainer
	{
		private List<t_donateBean> list = new List<t_donateBean>();
		private Dictionary<int, t_donateBean> map = new Dictionary<int, t_donateBean>();

		//public override List<t_donateBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_donateBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_donateBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_donateBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_donateBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_donateBean bean = new t_donateBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_donateContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_donateBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_donateBean).Name + ".bytes");
			}
		}
	}

}


