/**
 * Auto generated, do not edit it
 *
 * t_exphome
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_exphomeContainer : BaseContainer
	{
		private List<t_exphomeBean> list = new List<t_exphomeBean>();
		private Dictionary<int, t_exphomeBean> map = new Dictionary<int, t_exphomeBean>();

		//public override List<t_exphomeBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_exphomeBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_exphomeBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_exphomeBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_exphomeBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_exphomeBean bean = new t_exphomeBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_exphomeContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_exphomeBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_exphomeBean).Name + ".bytes");
			}
		}
	}

}


