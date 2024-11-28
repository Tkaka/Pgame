/**
 * Auto generated, do not edit it
 *
 * t_profession
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_professionContainer : BaseContainer
	{
		private List<t_professionBean> list = new List<t_professionBean>();
		private Dictionary<int, t_professionBean> map = new Dictionary<int, t_professionBean>();

		//public override List<t_professionBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_professionBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_professionBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_professionBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_professionBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_professionBean bean = new t_professionBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_professionContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_professionBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_professionBean).Name + ".bytes");
			}
		}
	}

}


