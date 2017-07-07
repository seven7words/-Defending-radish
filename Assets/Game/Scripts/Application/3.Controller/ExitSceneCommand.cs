using UnityEngine;
using System.Collections;

public class ExitSceneCommand : Controller {
    public override void Execute(object data)
    {
        //离开场景前回收所有可回收的对象
        Game.Instance.ObjectPool.UnspawnAll();
    }
}
