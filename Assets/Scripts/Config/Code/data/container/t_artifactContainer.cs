/**
 * Auto generated, do not edit it
 *
 * t_artifact
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_artifactContainer : BaseContainer
	{
		private List<t_artifactBean> list = new List<t_artifactBean>();
		private Dictionary<int, t_artifactBean> map = new Dictionary<int, t_artifactBean>();

		//public override List<t_artifactBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_artifactBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_artifactBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_artifactBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_artifactBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_artifactBean bean = new t_artifactBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_artifactContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_artifactBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_artifactBean).Name + ".bytes");
			}
		}
	}

}


