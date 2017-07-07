using UnityEngine;
using System.Collections;

public class StartUpCommand :Controller {
    public override void Execute(object data)
    {
        //1.注册模型Model
        RegisterModel(new GameModel());
        RegisterModel(new RoundModel());

        //2.注册控制器（命令）Controller
      
        RegisterController(Consts.E_EnterScene, typeof(EnterSceneCommand));
        RegisterController(Consts.E_ExitScene, typeof(ExitSceneCommand));
        RegisterController(Consts.E_StartLevel, typeof(StartLevelCommand));
        RegisterController(Consts.E_EndLevel, typeof(EndLevelCommand));
        RegisterController(Consts.E_CountDownComplete, typeof(CountDownCompleteCommand));

        RegisterController(Consts.E_UpgradeTower, typeof(UpgradeTowerCommand));
        RegisterController(Consts.E_SellTower, typeof(SellTowerCommand));

        //初始化
        GameModel gModel = GetModel<GameModel>();
        gModel.Initialize();

        //3.跳转到开始 界面
        Game.Instance.LoadScene(1);

    }
	
}
