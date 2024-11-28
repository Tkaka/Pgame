using FairyGUI;
using UnityEngine;

public interface IGuidable
{
    GObject GetGuideObj(string param);
}

public interface IGuideTip
{
    void Init(Data.Beans.t_guide_stepBean guideBean, GObject clickObj);

    bool EffectEnd();
}

public class GuideStep
{
    public int id { get; private set; }
    private Guide guide;
    private Data.Beans.t_guide_stepBean stepBean;
    private GComponent guideContainer;
    private GObject clickObj;
    private long sndId;
    private bool disposed;
    private long delayTimer;
    private IGuideTip tip;

    public bool isDisposed { get; private set; }
    public GuideStep(int stepId, Guide ownerGuide)
    {
        disposed = false;
        id = stepId;
        guide = ownerGuide;
        stepBean = ConfigBean.GetBean<Data.Beans.t_guide_stepBean, int>(stepId);
        //配置错误直接完成
        if (stepBean == null)
            this.finish();
    }

    private void onGuideTargetClick()
    {
        var obj = clickObj;
        GED.GuideED.dispatchEvent(GuideEventID.ClickGuideBtn, id);
        if (obj != null)
            obj.onClick.Call();
    }

    public void Begin()
    {
        guideContainer = new GComponent();
        var parent = WinMgr.Singleton.GuideLayer;
        parent.AddChild(guideContainer);
        guideContainer.SetSize(parent.width, parent.height);
        guideContainer.gameObjectName = string.Format("GuideStep-{0}.{1}", guide.id, id);

        var mask = new GGraph();
        mask.name = "GuideMask";
        mask.color = new Color(0, 0, 0, 0);
        mask.width = GRoot.inst.width;
        mask.height = GRoot.inst.height;
        guideContainer.AddChild(mask);

        //强制引导需要锁屏
        mask.touchable = stepBean.t_guide_type == 1;
        if (stepBean.t_delay > 0)
            delayTimer = CoroutineManager.Singleton.delayedCall(stepBean.t_delay / 1000f, showGuide);
        else
            showGuide();
    }

    private void showGuide()
    {
        delayTimer = 0;
        clickObj = null;
        guideContainer.RemoveChildren(0, -1, true);
        if (!string.IsNullOrEmpty(stepBean.t_click_param))
        {
            var arr = stepBean.t_click_param.Split('+');
            var wind = WinMgr.Singleton.GetWindow<BaseWindow>(arr[0]) as IGuidable;
            if (wind != null)
            {
                string param = arr.Length > 1 ? arr[1] : null;
                clickObj = wind.GetGuideObj(param);
            }
        }
        else if (!string.IsNullOrEmpty(stepBean.t_click_name))
        {
            var arr = stepBean.t_click_name.Split('/');
            GObject obj = GRoot.inst.GetChild(arr[0]);
            int idx = 1;
            while (idx < arr.Length)
            {
                if (obj == null)
                    break;

                if (obj.asCom != null)
                    obj = obj.asCom.GetChild(arr[idx]);
                else
                    obj = null;
                idx++;
            }
            if (obj == null)
                Debuger.Err("新手引导找不到要引导的路径对象>" + stepBean.t_click_name);

            clickObj = obj;
        }

        GGraph clickMask = null;
        if (clickObj != null)
        {
            if(clickObj.displayObject == null || clickObj.displayObject.isDisposed)
            {
                Debuger.Err(string.Format("新手引导获取获取到的clickObj异常{0}.{1}", guide.id, id));
                return;
            }

            var rect = new Rect();
            rect.size = clickObj.size;
            var pos = clickObj.TransformPoint(Vector2.zero, GRoot.inst);
            clickObj.SetPivot(clickObj.pivotX, clickObj.pivotY, false);
            var newPos = clickObj.TransformPoint(Vector2.zero, GRoot.inst);
            if(Vector2.Distance(pos, newPos) > 0.01f)
            {
                //位置改变了说明_pivotAsAnchor为true,位置需要修正
                clickObj.SetPivot(clickObj.pivotX, clickObj.pivotY, true);
                pos.x -= clickObj.width * clickObj.pivotX;
                pos.y -= clickObj.height * clickObj.pivotY;
            }
            rect.position = pos;

            if (stepBean.t_guide_type == 1)
            {
                //强制引导,加黑框
                var mask1 = new GGraph();
                var mask2 = new GGraph();
                var mask3 = new GGraph();
                var mask4 = new GGraph();
                guideContainer.AddChild(mask1);
                guideContainer.AddChild(mask2);
                guideContainer.AddChild(mask3);
                guideContainer.AddChild(mask4);

                Color clr = new Color(0, 0, 0, 0.5f);

                float edge = 5f;
                //上
                mask1.SetXY(-edge, -edge);
                mask1.DrawRect(guideContainer.width + edge + edge, rect.y + edge, 0, clr, clr);
                //下
                mask2.SetXY(-edge, rect.height + rect.y);
                mask2.DrawRect(guideContainer.width + edge + edge, guideContainer.height - rect.height - rect.y + edge, 0, clr, clr);
                //左
                mask3.SetXY(-edge, rect.y);
                mask3.DrawRect(rect.x + edge, rect.height, 0, clr, clr);
                //右
                mask4.SetXY(rect.x + rect.width, rect.y);
                mask4.DrawRect(guideContainer.width - rect.x - rect.width, rect.height, 0, clr, clr);
            }

            //点击遮挡
            clickMask = new GGraph();
            guideContainer.AddChild(clickMask);
            clickMask.SetXY(rect.x, rect.y);
#if UNITY_EDITOR
            clickMask.DrawRect(rect.width, rect.height, 0, Color.clear, new Color(1, 0.3f, 0, 0.3f));
#else
            clickMask.DrawRect(rect.width, rect.height, 0, Color.clear,  Color.clear);
#endif
            clickMask.onClick.Add(onGuideTargetClick);

            if (stepBean.t_guide_type == 2)
            {
                //非强制引导, 应当考虑放在list内的情况TODO
            }

            //if (stepBean.t_arrow_type > 0 && !string.IsNullOrEmpty(stepBean.t_arrow))
            {
                //箭头提示，分四个方向TODO
            }
        }
        
        if(stepBean.t_reset_main == 1)
        {
            //重置主界面TODO
        }

        //记录节点
        if (stepBean.t_point == 1)
            GuideService.Singleton.ReqFinishGuide(guide.id);

        //声音
        if (!string.IsNullOrEmpty(stepBean.t_snd))
            sndId = AudioManager.Singleton.PlayEffect(stepBean.t_snd, null, 0.3f);

        //自动执行
        if (stepBean.t_auto_time > 0)
        {
            CoroutineManager.Singleton.delayedCall(stepBean.t_auto_time / 1000f, () => {
                if (clickMask != null && !disposed)
                    clickMask.onClick.Call();
                this.finish();
            });
        }
        
        //立绘
        if(!string.IsNullOrEmpty(stepBean.t_view))
        {
            var arr = stepBean.t_view.Split('+');
            var view = UIPackage.CreateObject(arr[0], arr[1]);
            guideContainer.AddChild(view);
            tip = view as IGuideTip;
            if (tip != null)
                tip.Init(stepBean, clickObj);
        }
    }

    public void CheckFinishTrigger(GuideEventID evt, string param)
    {
        //开始到延时过程中不允许完成
        if (delayTimer > 0)
            return;

        //打字动画完成前不允许完成
        if (tip != null && !tip.EffectEnd())
            return;

        if (GuideConditions.CheckTrigger(true, id, stepBean.t_finish, evt, param))
            this.finish();
    }

    private void finish()
    {
        this.dispose();
        if(stepBean != null && stepBean.t_point == 2)
            GuideService.Singleton.ReqFinishGuide(guide.id);
        guide.FinishStep(id);
        GED.GuideED.dispatchEvent(GuideEventID.GuideStepFinish, id);
    }

    private void dispose()
    {
        if (disposed)
            return;

        delayTimer = 0;
        disposed = true;
        clickObj = null;
        AudioManager.Singleton.Stop(sndId);
        guideContainer.parent.RemoveChild(guideContainer, true);
        guideContainer = null;
    }
}