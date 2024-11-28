/**
 * Auto generated, do not edit it
 *
 * t_library
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_libraryContainer : BaseContainer
	{
		private List<t_libraryBean> list = new List<t_libraryBean>();
		private Dictionary<int, t_libraryBean> map = new Dictionary<int, t_libraryBean>();

		//public override List<t_libraryBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_libraryBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_libraryBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_libraryBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_libraryBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_libraryBean bean = new t_libraryBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_libraryContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_libraryBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_libraryBean).Name + ".bytes");
			}
		}
	}

}


