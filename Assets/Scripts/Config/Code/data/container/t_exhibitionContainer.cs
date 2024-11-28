/**
 * Auto generated, do not edit it
 *
 * t_exhibition
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_exhibitionContainer : BaseContainer
	{
		private List<t_exhibitionBean> list = new List<t_exhibitionBean>();
		private Dictionary<int, t_exhibitionBean> map = new Dictionary<int, t_exhibitionBean>();

		//public override List<t_exhibitionBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_exhibitionBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_exhibitionBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_exhibitionBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_exhibitionBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_exhibitionBean bean = new t_exhibitionBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_exhibitionContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_exhibitionBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_exhibitionBean).Name + ".bytes");
			}
		}
	}

}


