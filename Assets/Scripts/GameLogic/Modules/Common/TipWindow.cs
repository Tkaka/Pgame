using UI_Common;
using DG.Tweening;

public class TipWindow : SingletonWindow<TipWindow>
{

    private UI_TipWindow window;

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_TipWindow>();
        window.m_cont.visible = false;
        window.m_tipTxt.text = "";
    }

    private Tween tween;
    public void ShowTip(string str)
    {
        window.m_tipTxt.text = str;
        window.m_cont.visible = true;
        if (tween != null && tween.IsActive())
            tween.Kill();
        window.m_cont.alpha = 1;
        tween = window.m_cont.TweenFade(0, 2.5f);
    }
    /// <summary>
    /// 参数 语言包ID
    /// </summary>
    /// <param name="languageID"></param>
    public void ShowTip(int languageID)
    {
        string str = UIUtils.GetStrByLanguageID(languageID);

        window.m_tipTxt.text = str;
        window.m_cont.visible = true;
        if (tween != null && tween.IsActive())
            tween.Kill();
        window.m_cont.alpha = 1;
        tween = window.m_cont.TweenFade(0, 2.5f);
    }

}


