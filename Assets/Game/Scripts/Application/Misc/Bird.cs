using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{

    public float Time = 1;//一次循环所需时间，秒
    public float OffsetY = 8;//y方向浮动偏移
	// Use this for initialization
	void Start () {
	iTween.MoveBy(gameObject,iTween.Hash(
        "y",OffsetY,
        "easeType",iTween.EaseType.easeInOutSine,
        "loopType",iTween.LoopType.pingPong,
        "time",Time));
	}
	
	
}
