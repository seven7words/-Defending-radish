using UnityEngine;
using System.Collections;

public class UpgradeIcon : MonoBehaviour
{
   
    private SpriteRenderer m_Render;
    private Tower m_Tower;

    void Awake()
    {
        m_Render = GetComponent<SpriteRenderer>();
    }
	public void Load (GameModel gm, Tower tower)
	{
        //保存数据
	    m_Tower = tower;
        //显示图片
	    TowerInfo info = Game.Instance.StaticData.GetTowerInfo(tower.ID);
	    string path = "Res/Roles/" + (tower.IsTopLevel ? info.DisabledIcon : info.NormalIcon);
	    m_Render.sprite = Resources.Load<Sprite>(path);
	}

    void OnMouseDown()
    {

        if(m_Tower.IsTopLevel)
            return;
        
        UpgradeTowerArg e = new UpgradeTowerArg()
        {
            Tower =  m_Tower
        };

        SendMessageUpwards("OnUpgradeTower",e,SendMessageOptions.RequireReceiver);
    }

}
