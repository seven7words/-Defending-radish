using UnityEngine;
using System.Collections;
using System;

public class StartLevelCommand : Controller {
    public override void Execute(object data)
    {
        StartLevelArgs e = data as StartLevelArgs;
        //第一步
        GameModel gModel = GetModel<GameModel>();
        
        gModel.StartLevel(e.LevelIndex);
        Debug.Log("游戏开始时"+gModel.IsPlaying);
       
        //第二步
        RoundModel rModel = GetModel<RoundModel>();
        rModel.LoadLevel(gModel.PlayLevel);

        //



        //进入游戏关卡
        Game.Instance.LoadScene(3);
    }

  
}
