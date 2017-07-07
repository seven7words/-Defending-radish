using UnityEngine;
using System.Collections;
using System;

public class Spawner : View {
    #region 常量

    #endregion
    #region 事件
    #endregion

    #region 字段

    private Map m_Map = null;
    private Luobo m_Luobo = null;
    #endregion
    #region 属性
    public override string Name
    {
        get { return Consts.V_Spawner; }
    }
    #endregion
    #region 方法
    //创建萝卜
    public void SpawnLuobo(Vector3 position)
    {
        GameObject go = Game.Instance.ObjectPool.Spawn("Luobo");
    Luobo    luobo = go.GetComponent<Luobo>();
        luobo.Position = position;
        luobo.Dead += luobo_Dead;

        m_Luobo = luobo;

    }
    public void SpawnMonster(int MonsterID)
    {
        //创建怪物
        string prefabName = "Monster" + MonsterID;
        GameObject go = Game.Instance.ObjectPool.Spawn(prefabName);
        // Debug.Log("地图缠身了一个怪物，类型是"+MonsterID);
        Monster monster = go.GetComponent<Monster>();
        //if (monster != null)
        //{
        monster.Reached += monster_Reached;
        monster.HpChanged += monster_HpChanged;
        monster.Dead += monster_Dead;
        monster.Load(m_Map.Path);
        //}
    }
   
   

    void SpawnTower(int towerID, Vector3 position)
    {
        ////创建Tower
        //TowerInfo info = Game.Instance.StaticData.GetTowerInfo(towerID);
        //GameObject go = Game.Instance.ObjectPool.Spawn(info.PrefabName);
        //Tower tower = go.GetComponent<Tower>();
        //tower.transform.position = position;

        ////Tile里放入Tower信息
        //Tile tile = m_Map.GetTile(position);
        //tile.Data = tower;

        ////初始化Tower
        //tower.Load(towerID, tile);
        //找到Tile
      Tile tile =   m_Map.GetTile(position);
        //创建Tower
      TowerInfo info =   Game.Instance.StaticData.GetTowerInfo(towerID);
       GameObject go = Game.Instance.ObjectPool.Spawn(info.PrefabName);
        Tower tower = go.GetComponent<Tower>();
        tower.transform.position = position;
        tower.Load(towerID,tile,m_Map.MapRect);
    }

    void monster_HpChanged(int arg1, int arg2)
    {

    }
    void monster_Dead(Role monster)
    {
        //回收自己

        Game.Instance.ObjectPool.Unspawn(monster.gameObject);
        //失败判断
        GameModel gm = GetModel<GameModel>();
        RoundModel rm = GetModel<RoundModel>();
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        if (monsters.Length==0&&!m_Luobo.IsDead&&rm.AllRoundComplete)//萝卜没死，。场景已没有怪物 所有怪物已出完
        {
            //游戏胜利
            SendEvent(Consts.E_EndLevel, new EndLevelArgs() { LevelID = gm.PlayLevelID, IsWin = true });
        }
    }

    void luobo_Dead(Role luobo)
    {
        //回收萝卜
        Game.Instance.ObjectPool.Unspawn(luobo.gameObject);
        //游戏胜利
        GameModel gm = GetModel<GameModel>();
        SendEvent(Consts.E_EndLevel, new EndLevelArgs() { LevelID = gm.PlayLevelID, IsWin = false });
    }
    void monster_Reached(Monster monster)
    {
        ////移除自己
        //Game.Instance.ObjectPool.Unspawn(monster.gameObject);
        //萝卜掉血
        m_Luobo.Damage(1);
        //怪物死亡
        monster.Hp = 0;



    }

    void Map_OnTileClick(object sender, TileClickEventArgs e)
    {
        GameModel gm = GetModel<GameModel>();
       
       
        //游戏还未开始，那么不操作菜单

        if(!gm.IsPlaying)
            return;
        //如果有菜单显示，那么隐藏菜单
        if (TowerPopup.Instance.IsPopShow)
        {
            SendEvent(Consts.E_HidePopups);
            return;
        }
        if (!e.Tile.CanHold)
        {
            SendEvent(Consts.E_HidePopups);
        }
        if (e.Tile.Data == null)
        {
            ShowSpawnPanelArgs e1 = new ShowSpawnPanelArgs()
            {
                // Position = null,
                Position = m_Map.GetPosition(e.Tile),
                UpSide = e.Tile.Y < (m_Map.RowCount/2),


            };

            //
            SendEvent(Consts.E_ShowSpawnPanel, e1);
        }
        else
        {
            ShowUpgradePanelArgs e2 = new ShowUpgradePanelArgs()
            {
                Tower = e.Tile.Data as Tower,
            };
            SendEvent(Consts.E_ShowUpgradePanel, e2);
        }

        
    }

    #endregion
    #region Unity回调
    #endregion
    #region 事件回调

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_EnterScene);
        AttentionEvents.Add(Consts.E_SpawnMonster);
        AttentionEvents.Add(Consts.E_SpawnTower);
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_EnterScene:
                SceneArgs e0 = (SceneArgs)data;
                if (e0.SceneIndex == 3)
                {
                    //获取地图组件
                   
                    
                    m_Map = GetComponent<Map>();
                 
                    m_Map.OnTileClick += Map_OnTileClick;
                    //加载地图
                    GameModel gModel = GetModel<GameModel>();
                    m_Map.LoadLevel(gModel.PlayLevel);
                   

                    //加载萝卜
                    Vector3[] path = m_Map.Path;
                    Vector3 pos = path[path.Length - 1];
                    SpawnLuobo(pos);
                }
                break;

            case Consts.E_SpawnMonster:
                SpawnMonsterArgs e1 = (SpawnMonsterArgs)data ;
                SpawnMonster(e1.MonsterID);
                break;
            case Consts.E_SpawnTower:
                SpawnTowerArgs e2 = data as SpawnTowerArgs;
                SpawnTower(e2.TowerID,e2.Position);
                break;
                
        }
    }

  
    #endregion
    #region 帮助方法
    #endregion




}
