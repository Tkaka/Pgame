/**
 * Auto generated, do not edit it
 *
 * t_model_scale
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_model_scaleContainer : BaseContainer
	{
		private List<t_model_scaleBean> list = new List<t_model_scaleBean>();
		private Dictionary<String, t_model_scaleBean> map = new Dictionary<String, t_model_scaleBean>();

		//public override List<t_model_scaleBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<String, t_model_scaleBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_model_scaleBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_model_scaleBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_model_scaleBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_model_scaleBean bean = new t_model_scaleBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_model_scaleContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_model_scaleBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_model_scaleBean).Name + ".bytes");
			}
		}
	}

}


