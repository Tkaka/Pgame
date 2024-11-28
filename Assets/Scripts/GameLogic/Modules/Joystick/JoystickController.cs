using FairyGUI;
using UnityEngine;
using DG.Tweening;

public class JoystickController : EventDispatcher
{
	float _InitX;
	float _InitY;
	float _startStageX;
	float _startStageY;
	float _lastStageX;
	float _lastStageY;
	GImage center;
	GObject _touchArea;
	GObject bottom;
	int touchId;
    private GComponent joystickView;
	Tweener _tweener;

	public EventListener onMove { get; private set; }
	public EventListener onEnd { get; private set; }

	public int radius { get; set; }

	public JoystickController(GComponent view)
	{
        joystickView = view;
        onMove = new EventListener(this, "onMove");
		onEnd = new EventListener(this, "onEnd");

		center = view.GetChild("center").asImage;
		_touchArea = view.GetChild("touchArea");
		bottom = view.GetChild("bottom");

		_InitX = bottom.x + bottom.width / 2;
		_InitY = bottom.y + bottom.height / 2; 
		touchId = -1;
		radius = 80;

		_touchArea.onTouchBegin.Add(this.OnTouchBegin);
		_touchArea.onTouchMove.Add(this.OnTouchMove);
		_touchArea.onTouchEnd.Add(this.OnTouchEnd);
	}

	public void Trigger(EventContext context)
	{
		OnTouchBegin(context);
	}

	private void OnTouchBegin(EventContext context)
	{
		if (touchId == -1)//First touch
		{
			InputEvent evt = (InputEvent)context.data;
			touchId = evt.touchId;

			if (_tweener != null)
			{
				_tweener.Kill();
				_tweener = null;
			}

            Vector2 pt = joystickView.GlobalToLocal(new Vector2(evt.x, evt.y));
            float bx = pt.x;
			float by = pt.y;

			if (bx < 0)
				bx = 0;
			else if (bx > _touchArea.width)
				bx = _touchArea.width;

			if (by > GRoot.inst.height)
				by = GRoot.inst.height;
			else if (by < _touchArea.y)
				by = _touchArea.y;

			_lastStageX = bx;
			_lastStageY = by;
			_startStageX = bx;
			_startStageY = by;

            bottom.SetXY(bx - bottom.width / 2, by - bottom.height / 2);
			center.SetXY(bx - center.width / 2, by - center.height / 2);

			float deltaX = bx - _InitX;
			float deltaY = by - _InitY;
			float degrees = Mathf.Atan2(deltaY, deltaX) * 180 / Mathf.PI;

			context.CaptureTouch();
		}
	}

	private void OnTouchEnd(EventContext context)
	{
		InputEvent inputEvt = (InputEvent)context.data;
		if (touchId != -1 && inputEvt.touchId == touchId)
		{
			touchId = -1;
			_tweener = center.TweenMove(new Vector2(_InitX - center.width / 2, _InitY - center.height / 2), 0.3f).OnComplete(() =>
			{
				_tweener = null;
				bottom.SetXY(_InitX - bottom.width / 2, _InitY - bottom.height / 2);
			}
			);

			this.onEnd.Call();
            ActorManagerMC.Singleton.MainPlayer.changeState(ActorState.idle);
        }
	}

	private void OnTouchMove(EventContext context)
	{
		InputEvent evt = (InputEvent)context.data;
		if (touchId != -1 && evt.touchId == touchId)
		{
			Vector2 pt = joystickView.GlobalToLocal(new Vector2(evt.x, evt.y));
            float bx = pt.x;
			float by = pt.y;
			float moveX = bx - _lastStageX;
			float moveY = by - _lastStageY;
			_lastStageX = bx;
			_lastStageY = by;
			float buttonX = center.x + moveX;
			float buttonY = center.y + moveY;

			float offsetX = buttonX + center.width / 2 - _startStageX;
			float offsetY = buttonY + center.height / 2 - _startStageY;

			float rad = Mathf.Atan2(offsetY, offsetX);
			float degree = rad * 180 / Mathf.PI;

			float maxX = radius * Mathf.Cos(rad);
			float maxY = radius * Mathf.Sin(rad);
			if (Mathf.Abs(offsetX) > Mathf.Abs(maxX))
				offsetX = maxX;
			if (Mathf.Abs(offsetY) > Mathf.Abs(maxY))
				offsetY = maxY;

			buttonX = _startStageX + offsetX;
			buttonY = _startStageY + offsetY;
			if (buttonX < 0)
				buttonX = 0;
			if (buttonY > GRoot.inst.height)
				buttonY = GRoot.inst.height;

			center.SetXY(buttonX - center.width / 2, buttonY - center.height / 2);

			this.onMove.Call(degree);
            //Debug.Log(degree);
            //设置主角朝向，切换到移动状态
            ActorManagerMC.Singleton.MainPlayer.SetRotByJoystick(degree + 90);
            ActorManagerMC.Singleton.MainPlayer.changeState(ActorState.move);


        }
	}
}