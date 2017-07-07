using UnityEngine;
using System.Collections;
using System;

public class EndLevelCommand : Controller
{
    public override void Execute(object data)
    {
        EndLevelArgs e = data as EndLevelArgs;
        //保存游戏状态
        GameModel gm = GetModel<GameModel>();
        RoundModel rModel = GetModel<RoundModel>();
        //停止出怪
        rModel.StopRound();
        //停止游戏

        gm.StopLevel(e.IsWin);
        Debug.Log("游戏结束"+gm.IsPlaying);
        //弹出UI
        //胜利
        if (e.IsWin)
        {
            //显示胜利面板
            GetView<UIWin>().Show();
        }
        else
        {
            //显示失败面板
            GetView<UILost>().Show();
        }

    }
}
