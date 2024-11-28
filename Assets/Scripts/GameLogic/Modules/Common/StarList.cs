using UI_Common;
using FairyGUI;

public class StarList 
{

    private UI_StarList starList;
    public StarList(UI_StarList starList)
    {
        this.starList = starList;
        starList.m_starList.foldInvisibleItems = true;
    }

    public void SetStar(int star)
    {
        int oldStar = starList.m_starList.numItems;
        GImage img = null;
        for (int i = 0; i < oldStar; i++)
        {
            img = starList.m_starList.GetChildAt(i) as GImage;
            if (img.displayObject != null && !img.displayObject.isDisposed)
                img.visible = i < star;
            else
                Debuger.Err("------------??????星星已经销毁");
        }

        for (int i = oldStar; i < star; i++)
        {
            img = UIPackage.CreateObject(WinEnum.UI_Common, "UI_TY_tubiao_xing_huangse_xiao").asImage;
            starList.m_starList.AddChild(img);
        }
    }

}
