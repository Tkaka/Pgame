/**
 * Auto generated, do not edit it
 *
 * t_star_attach_precent
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_star_attach_precentContainer : BaseContainer
	{
		private List<t_star_attach_precentBean> list = new List<t_star_attach_precentBean>();
		private Dictionary<int, t_star_attach_precentBean> map = new Dictionary<int, t_star_attach_precentBean>();

		//public override List<t_star_attach_precentBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_star_attach_precentBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_star_attach_precentBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_star_attach_precentBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_star_attach_precentBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_star_attach_precentBean bean = new t_star_attach_precentBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_star_attach_precentContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_star_attach_precentBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_star_attach_precentBean).Name + ".bytes");
			}
		}
	}

}


