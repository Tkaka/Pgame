using UI_Battle;

public class CatchPetWindow : BaseWindow
{

    private UI_CatchPetWindow petWindow;

    private long targetActorId;

    public override void OnOpen()
    {
        base.OnOpen();
        petWindow = getUiWindow<UI_CatchPetWindow>();
    }

}
