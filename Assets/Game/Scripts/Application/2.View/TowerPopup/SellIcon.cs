using UnityEngine;
using System.Collections;

public class SellIcon : MonoBehaviour {

    private Tower m_Tower;

    
    public void Load(Tower tower)
    {
        //保存数据
        m_Tower = tower;
       
    }

    void OnMouseDown()
    {
        SellTowerArgs e = new SellTowerArgs()
        {
            Tower = m_Tower,
        };
        SendMessageUpwards("OnSellTower",m_Tower,SendMessageOptions.RequireReceiver);
    }
}
