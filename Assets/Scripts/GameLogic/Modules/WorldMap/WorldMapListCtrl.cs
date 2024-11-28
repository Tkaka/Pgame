
using FairyGUI;
using UI_WorldMap;

public class WorldMapListCtrl
{
    private GList _scrollList;

    private BaseWindow owner;
    private UI_WorldMapWindow view;

    private UI_MapItem01 mapItem01;
    private UI_MapItem02 mapItem02;
    private UI_MapItem03 mapItem03;

    public WorldMapListCtrl(BaseWindow owner)
    {
        this.owner = owner;
        view = owner.getUiWindow<UI_WorldMapWindow>();
        _scrollList = view.m_scrollList;
        FixScale();
        InitLoader();
        Stage.inst.onStageResized.Add(FixScale);
    }

    private void FixScale()
    {
        float h = GRoot.inst.root.height;
        float scale = h / _scrollList.height;
        _scrollList.SetScale(scale, scale);
    }

    private void InitLoader()
    {
        mapItem01 = (UI_MapItem01)_scrollList.GetChildAt(0);
        //mapItem01.m_mapLoader.url = WinEnum.BasePath + "world_map1";
        UIGloader.SetUrl(mapItem01.m_mapLoader, WinEnum.BasePath + "world_map1");
        mapItem02 = (UI_MapItem02)_scrollList.GetChildAt(1);
        //mapItem02.m_mapLoader.url = WinEnum.BasePath + "world_map2";
        UIGloader.SetUrl(mapItem02.m_mapLoader, WinEnum.BasePath + "world_map2");
        mapItem03 = (UI_MapItem03)_scrollList.GetChildAt(2);
        //mapItem03.m_mapLoader.url = WinEnum.BasePath + "world_map3";
        UIGloader.SetUrl(mapItem03.m_mapLoader, WinEnum.BasePath + "world_map3");

        mapItem01.m_outline01.alpha = 0;
        mapItem01.m_outline01.onClick.Add(OnOutlineClick);
    }

    private void OnOutlineClick()
    {
        mapItem01.m_outline01.alpha = 1;
        WinMgr.Singleton.Open<SelectMissionWindow>();
        //owner.Close();
    }

    public void OnClose()
    {
        Stage.inst.onStageResized.Remove(FixScale);
        _scrollList = null;
        view = null;
        mapItem01 = null;
        mapItem02 = null;
        mapItem03 = null;
    }

}
