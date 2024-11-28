/**
 * Auto generated, do not edit it
 *
 * t_dialog
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_dialogContainer : BaseContainer
	{
		private List<t_dialogBean> list = new List<t_dialogBean>();
		private Dictionary<int, t_dialogBean> map = new Dictionary<int, t_dialogBean>();

		//public override List<t_dialogBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_dialogBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_dialogBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_dialogBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_dialogBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_dialogBean bean = new t_dialogBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_dialogContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_dialogBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_dialogBean).Name + ".bytes");
			}
		}
	}

}


