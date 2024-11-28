/**
 * Auto generated, do not edit it
 *
 * t_talent_page
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_talent_pageContainer : BaseContainer
	{
		private List<t_talent_pageBean> list = new List<t_talent_pageBean>();
		private Dictionary<int, t_talent_pageBean> map = new Dictionary<int, t_talent_pageBean>();

		//public override List<t_talent_pageBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_talent_pageBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_talent_pageBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_talent_pageBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_talent_pageBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_talent_pageBean bean = new t_talent_pageBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_talent_pageContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_talent_pageBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_talent_pageBean).Name + ".bytes");
			}
		}
	}

}


