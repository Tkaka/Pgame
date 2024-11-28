using Data.Beans;
using FairyGUI;
using UI_Common;

public class GuideDialog : UI_GuideDialog, IGuideTip
{
    private bool typeEnd = false;
    private TypingEffect typeEft;
    private string tip;
    public void Init(t_guide_stepBean guideBean, GObject clickObj)
    {
        this.SetSize(GRoot.inst.width, GRoot.inst.height);
        this.AddRelation(GRoot.inst, RelationType.Size);

        if (guideBean.t_left_right == 1)
        {
            //左
            this.m_leftBg.visible = true;
            this.m_txtLeftName.visible = true;
            this.m_rightBg.visible = false;
            this.m_txtRightName.visible = false;

            this.m_txtLeftName.text = guideBean.t_title;
            
        }
        else
        {
            //右
            this.m_leftBg.visible = false;
            this.m_txtLeftName.visible = false;
            this.m_rightBg.visible = true;
            this.m_txtRightName.visible = true;

            this.m_txtRightName.text = guideBean.t_title;
        }

        tip = guideBean.t_tip;
        this.m_txtContent.text = guideBean.t_tip;

        typeEnd = false;
        typeEft = new TypingEffect(m_txtContent);

        typeEft.Start();
        Timers.inst.Add(0.050f, 0, typeWords);
        GED.GuideED.removeListener((int)GuideEventID.ClickScreen, typeAll);
        GED.GuideED.addListener((int)GuideEventID.ClickScreen, typeAll);
    }

    private void typeAll(GameEvent evt)
    {
        typeEnd = true;
        typeEft.Cancel();
        this.m_txtContent.text = tip;
        Timers.inst.Remove(typeWords);
        GED.GuideED.removeListener((int)GuideEventID.ClickScreen, typeAll);
        tip = null;
    }

    private void typeWords(object param)
    {
        typeEnd = !typeEft.Print();
        if(typeEnd)
        {
            Timers.inst.Remove(typeWords);
            GED.GuideED.removeListener((int)GuideEventID.ClickScreen, typeAll);
        }
    }

    public bool EffectEnd()
    {
        return typeEnd;
    }
}