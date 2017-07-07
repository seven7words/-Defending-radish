using UnityEngine;
using System.Collections;

public class CountDownCompleteCommand : Controller
{
    public override void Execute(object data)
    {
        GameModel gModel = GetModel<GameModel>();
        gModel.IsPlaying = true;
        //开始出怪
        RoundModel rModel = GetModel<RoundModel>();
        rModel.StartRound();
        
       // Debug.Log("出怪");
        
    }
}
