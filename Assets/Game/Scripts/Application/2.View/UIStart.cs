using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIStart : View {
    public override string Name
    {
        get { return Consts.V_Start; }
    }

    public void GotoSelect()
    {
        Game.Instance.LoadScene(2);
    }
    public override void HandleEvent(string eventName, object data)
    {
        
    }
}
