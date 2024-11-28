#pragma strict

var scrollSpeed = 5;
var countX : int = 4;
var countY : int = 4;

private var offsetX = 0.0;
private var offsetY = 0.0;
private var singleTexSize;

function Start() {
    singleTexSize = Vector2(1.0/countX, 1.0/countY);
    GetComponent.<Renderer>().material.mainTextureScale = singleTexSize;
}
function Update ()
{
    
    var frame = Mathf.Floor(Time.time*scrollSpeed);
    offsetX = frame/countX;
    offsetY = -(frame - frame%countX) /countY / countX;
    if(offsetX > 1.0)
    {
     	offsetX = offsetX - Mathf.Floor(offsetX);
    }
    else if(offsetX < 0.0)
    {
    	offsetX = offsetX - Mathf.Floor(offsetX) + 1.0;
    }
    
    if(offsetY > 1.0)
    {
     	offsetY = offsetY - Mathf.Floor(offsetY);
    }
    else if(offsetY < 0.0)
    {
    	offsetY = offsetY - Mathf.Floor(offsetY) + 1.0;
    }
    //Debug.LogError(Vector2(offsetX, offsetY));
    GetComponent.<Renderer>().material.SetTextureOffset ("_MainTex", Vector2(offsetX, offsetY));
}
