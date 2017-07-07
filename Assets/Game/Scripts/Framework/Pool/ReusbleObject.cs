using UnityEngine;
using System.Collections;

/// <summary>
/// 1.当前类不实例化所以加abstract。而方法必须子类实现所以也加abstract
/// </summary>
public abstract class ReusbleObject : MonoBehaviour,IReusable {

    public abstract void OnSpawn();
    public abstract void OnUnspawn();
}
