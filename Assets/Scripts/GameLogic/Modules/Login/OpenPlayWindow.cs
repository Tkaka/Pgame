using FairyGUI;
using UI_Login;
using UnityEngine;

public class OpenPlayWindow : BaseWindow
{
    RTVideoPlayer player;
    UI_OpenPlayWindow window;
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_OpenPlayWindow>();
        window.m_TiaoGuoBtn.onClick.Add(OnTiaoGuo);
        InitView();
    }
    public override void InitView()
    {
        base.InitView();
        //播放视频

        //var rt = RenderTexture.GetTemporary(1280, 720);
        //new RTVideoPlayer().Play(Application.streamingAssetsPath + "/cg2.mp4", rt, OnTiaoGuo);
        //(window.m_shipin as UIGloader).SetTexture(rt);
        //window.m_shipin.texture = new NTexture(rt);

        player = new RTVideoPlayer();
        player.Play(Application.streamingAssetsPath + "/cg.mp4", OnTiaoGuo, 20);
    }
    private void OnTiaoGuo()
    {
        player.Stop();
        Close();
        WinMgr.Singleton.Open<SelectRoleWindow>();
    }
}