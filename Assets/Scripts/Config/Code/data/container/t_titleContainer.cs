/**
 * Auto generated, do not edit it
 *
 * t_title
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_titleContainer : BaseContainer
	{
		private List<t_titleBean> list = new List<t_titleBean>();
		private Dictionary<int, t_titleBean> map = new Dictionary<int, t_titleBean>();

		//public override List<t_titleBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_titleBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_titleBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_titleBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_titleBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_titleBean bean = new t_titleBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_titleContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_titleBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_titleBean).Name + ".bytes");
			}
		}
	}

}


