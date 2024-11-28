//引导事件
public enum GuideEventID
{
    Invalid = 0,            //无效
    ClickScreen = 101,      //点击屏幕
    GuideStepFinish,        //完成小步骤
    ClickGuideBtn,          //点击引导按钮
    OpenWnd,                //打开界面
    CloseWnd,               //关闭界面
    SpawnMonster,           //刷怪


    GuideTriggerCheck,      //检查触发
    GuideFinish,            //大步骤完成（不用于条件）
}

//引导触发/完成条件
public class GuideTriggerType
{
    //状态
    public const string MainLevel   = "1";        //关卡
    public const string ReachLevel  = "2";        //达到等级
    public const string FinishGuide = "3";        //完成引导(大步骤)
    public const string WndIsOpen   = "4";        //窗口打开
    public const string WndNotOpen  = "5";        //窗口未开启

    //事件
    public const string Auto            = "100";      //自动触发
    public const string ClickScreen     = "101";      //点击屏幕
    public const string ClickGuideBtn   = "102";      //点击引导按钮
    public const string OpenWnd         = "103";      //打开界面
    public const string CloseWnd        = "104";      //关闭界面
    public const string GuideStepFinish = "105";      //小步骤完成
    public const string SpawnMonster    = "106";      //刷怪
}

public class GuideConditions
{
    public static bool CheckTrigger(bool isStep, int checkId, string trigger, GuideEventID evt = GuideEventID.Invalid, string param = null)
    {
        //未配置不让触发
        if (string.IsNullOrEmpty(trigger))
            return false;

        var terms = trigger.Split(';');
        for (int i = 0, len = terms.Length; i < len; ++i)
        {
            var arr = terms[i].Split('+');
            switch(arr[0])
            {
                //状态
                case GuideTriggerType.MainLevel:
                    int levelId = int.Parse(arr[2]);
                    var levelInfo = LevelService.Singleton.GetActInfoByID(levelId);
                    if (levelInfo == null) //未解锁关卡
                        return false;
                    if(arr[1] == "2" && levelInfo.star <= 0)//未完成关卡
                        return false;
                    break;
                case GuideTriggerType.ReachLevel:
                    //到达等级
                    var roleInfo = RoleService.Singleton.GetRoleInfo();
                    if (roleInfo == null || roleInfo.level < int.Parse(arr[1]))
                        return false;
                    break;
                case GuideTriggerType.FinishGuide:
                    //完成引导

                    break;
                case GuideTriggerType.WndIsOpen:
                    //窗口打开
                    if (false == WinMgr.Singleton.IsWindOpen(arr[1]))
                        return false;
                    break;
                case GuideTriggerType.WndNotOpen:
                    if (WinMgr.Singleton.IsWindOpen(arr[1]))
                        return false;
                    //窗口没打开
                    break;


                //事件
                case GuideTriggerType.Auto:
                    //自动触发
                    return true;
                case GuideTriggerType.ClickScreen:
                    //点击屏幕
                    if (evt != GuideEventID.ClickScreen)
                        return false;
                    break;
                case GuideTriggerType.GuideStepFinish:
                    if (evt != GuideEventID.GuideStepFinish || arr[1] != param)
                        return false;
                    //小步骤完成
                    break;
                case GuideTriggerType.ClickGuideBtn:
                    //点击完成按钮
                    if (evt != GuideEventID.ClickGuideBtn || checkId.ToString() != param)
                        return false;
                    break;
                case GuideTriggerType.OpenWnd:
                    //打开窗口
                    if (evt != GuideEventID.OpenWnd || arr[1] != param)
                        return false;
                    break;
                case GuideTriggerType.CloseWnd:
                    //关闭窗口
                    if (evt != GuideEventID.CloseWnd || arr[1] != param)
                        return false;
                    break;
                case GuideTriggerType.SpawnMonster:
                    //刷怪(关卡+波数)
                    if (evt != GuideEventID.SpawnMonster)
                        return false;
                    var pArr = param.Split('+');
                    if (arr[1] != pArr[0] || arr[2] != pArr[1])
                        return false;
                    break;
            }
        }
        return true;
    }
}