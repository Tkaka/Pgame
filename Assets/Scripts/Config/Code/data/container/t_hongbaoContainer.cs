/**
 * Auto generated, do not edit it
 *
 * t_hongbao
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_hongbaoContainer : BaseContainer
	{
		private List<t_hongbaoBean> list = new List<t_hongbaoBean>();
		private Dictionary<int, t_hongbaoBean> map = new Dictionary<int, t_hongbaoBean>();

		//public override List<t_hongbaoBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_hongbaoBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_hongbaoBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_hongbaoBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_hongbaoBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_hongbaoBean bean = new t_hongbaoBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_hongbaoContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_hongbaoBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_hongbaoBean).Name + ".bytes");
			}
		}
	}

}


