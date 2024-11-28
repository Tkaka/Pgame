//using Data.Beans;
//using Message.Task;
//using UI_TaskSystem;

//public class DaoJuHuoDeWindow : BaseWindow
//{
//    UI_DaoJuHuoDeWindow window;

//    public override void OnOpen()
//    {
//        base.OnOpen();
//        InitView();
//    }
//    public override void InitView()
//    {
//        window = getUiWindow<UI_DaoJuHuoDeWindow>();
//        window.m_CloseBtn.onClick.Add(CloseBtn);
//        window.m_QueDing.onClick.Add(CloseBtn);
//        FillData();
//    }
//    private void FillData()
//    {
//        int taskId = (int)Info.param;
//        t_taskBean taskInfo = ConfigBean.GetBean<t_taskBean,int>(taskId);
//        window.m_TaskName.text = taskInfo.t_name;
//        int[,] jiangli = UIUtils.splitStringTotwodimensionArry(taskInfo.t_reward);
//        DaoJuHuoDeListItem listItem;
//        for (int i = 0; i < jiangli.Rank; ++i)
//        {
//            listItem = DaoJuHuoDeListItem.CreateInstance();
//            listItem.Init(jiangli[i,0],jiangli[i,1]);
//            window.m_JiangLiList.AddChild(listItem);
//        }
//    }
//    public override void RefreshView()
//    {
//        base.RefreshView();
//    }
//    private void CloseBtn()
//    {
//        window = null;
//        Close();
//    }

//}
