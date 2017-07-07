using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{

    public float Speed = 360f;//度/秒
	void Update ()
	{
	    transform.Rotate(Vector3.forward, Time.deltaTime*Speed, Space.Self);
	}
}
