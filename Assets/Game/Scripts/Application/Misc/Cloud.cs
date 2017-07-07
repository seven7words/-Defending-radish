using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour
{

    public float OffsetX = 1000;//x方向偏移量
    public float Duration = 1f;//一个周期的持续时间

	// Use this for initialization
	void Start () {
	iTween.MoveBy(gameObject,iTween.Hash("x",OffsetX,
        "easeType",iTween.EaseType.linear,
        "loopType",iTween.LoopType.loop,
        "time",Duration));
	}
	
	
}
