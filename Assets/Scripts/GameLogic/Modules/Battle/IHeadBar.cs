
public interface IHeadBar
{

    void ToggleVisible(bool flag);

    void TouchToggle(bool flag);

    void ToogleSwipe(bool flag);

    void ShowHeadBar();

    void Init(Actor owner);

    void Destroy(float delay=0);

    void Update();

    void OnDead();

    void ResetStatus();

}
