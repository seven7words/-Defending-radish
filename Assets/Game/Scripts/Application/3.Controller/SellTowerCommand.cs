using UnityEngine;
using System.Collections;

public class SellTowerCommand : Controller {
    public override void Execute(object data)
    {
        SellTowerArgs e = data as SellTowerArgs;
        Tower tower = e.Tower;
        //清除Tile存储的信息
        tower.Tile.Data = null;
        //半价出售
        GameModel gm = GetModel<GameModel>();
        gm.Gold += e.Tower.Price/2;
        //回收
        Game.Instance.ObjectPool.Unspawn(e.Tower.gameObject);
    }
}
