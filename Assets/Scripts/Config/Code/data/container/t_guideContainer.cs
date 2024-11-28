/**
 * Auto generated, do not edit it
 *
 * t_guide
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_guideContainer : BaseContainer
	{
		private List<t_guideBean> list = new List<t_guideBean>();
		private Dictionary<int, t_guideBean> map = new Dictionary<int, t_guideBean>();

		//public override List<t_guideBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_guideBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_guideBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_guideBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_guideBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_guideBean bean = new t_guideBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_guideContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_guideBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_guideBean).Name + ".bytes");
			}
		}
	}

}


