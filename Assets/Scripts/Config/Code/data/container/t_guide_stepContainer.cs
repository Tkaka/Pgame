/**
 * Auto generated, do not edit it
 *
 * t_guide_step
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_guide_stepContainer : BaseContainer
	{
		private List<t_guide_stepBean> list = new List<t_guide_stepBean>();
		private Dictionary<int, t_guide_stepBean> map = new Dictionary<int, t_guide_stepBean>();

		//public override List<t_guide_stepBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_guide_stepBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_guide_stepBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_guide_stepBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_guide_stepBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_guide_stepBean bean = new t_guide_stepBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_guide_stepContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_guide_stepBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_guide_stepBean).Name + ".bytes");
			}
		}
	}

}


