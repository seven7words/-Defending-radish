using UnityEngine;
using System.Collections;

public class EnterSceneCommand : Controller {
    public override void Execute(object data)
    {
        //注册视图（View）
        SceneArgs  e = data as SceneArgs;
        switch (e.SceneIndex)
        {
            case 0://Init
                break;
            case 1://Start
              RegisterView(GameObject.Find("UIStart").GetComponent<UIStart>()); 

                break;
            case 2://Select
                RegisterView(GameObject.Find("UISelect").GetComponent<UISelect>());
                break;
            case 3://Level
                RegisterView(GameObject.Find("Map").GetComponent<Spawner>());
                RegisterView(GameObject.Find("TowerPopup").GetComponent<TowerPopup>());
                //Find只能查setactive为true的方法
                //但是如果根节点一直存在，则子节点不管是否显示都可以找到(transform.Find)
                RegisterView(GameObject.Find("Canvas").transform.Find("UICountDown").GetComponent<UICountDown>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIBoard").GetComponent<UIBoard>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIWin").GetComponent<UIWin>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UILost").GetComponent<UILost>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UISystem").GetComponent<UISystem>());

                break;
            case 4://Complete
                RegisterView(GameObject.Find("UIComplete").GetComponent<UIComplete>());
                break;
            default:
                break;
        }
       
    }
}
