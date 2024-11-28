/**
 * Auto generated, do not edit it
 *
 * t_trumpet_color
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_trumpet_colorContainer : BaseContainer
	{
		private List<t_trumpet_colorBean> list = new List<t_trumpet_colorBean>();
		private Dictionary<int, t_trumpet_colorBean> map = new Dictionary<int, t_trumpet_colorBean>();

		//public override List<t_trumpet_colorBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_trumpet_colorBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_trumpet_colorBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_trumpet_colorBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_trumpet_colorBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_trumpet_colorBean bean = new t_trumpet_colorBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_trumpet_colorContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_trumpet_colorBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_trumpet_colorBean).Name + ".bytes");
			}
		}
	}

}


