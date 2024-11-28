/**
 * Auto generated, do not edit it
 *
 * t_shop
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_shopContainer : BaseContainer
	{
		private List<t_shopBean> list = new List<t_shopBean>();
		private Dictionary<int, t_shopBean> map = new Dictionary<int, t_shopBean>();

		//public override List<t_shopBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_shopBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_shopBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_shopBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_shopBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_shopBean bean = new t_shopBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_shopContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_shopBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_shopBean).Name + ".bytes");
			}
		}
	}

}


