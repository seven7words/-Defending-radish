using UnityEngine;
using System.Collections;

public class TowerIcon : MonoBehaviour
{
    private SpriteRenderer m_Render;
    private Vector3 m_Position;
    private TowerInfo m_Info;
    private bool m_IsEnough = false;

    void Awake()
    {
        m_Render = GetComponent<SpriteRenderer>();
    }
    public void Load(GameModel gm,TowerInfo info,Vector3 position,bool upSide)
    {
        //保存必要的信息
        m_Info = info;
        m_Position = position;
        //判断金币是否足够
       m_IsEnough = gm.Gold >= info.BasePrice;
      //m_IsEnough = true;
        //加载图片
        string path = "Res/Roles/" + (m_IsEnough ? info.NormalIcon : info.DisabledIcon);
        m_Render.sprite = Resources.Load<Sprite>(path);
        //摆放位置
        Vector3 locPos = transform.localPosition;
        locPos.y = upSide ? Mathf.Abs(locPos.y) : -Mathf.Abs(locPos.y);
        transform.localPosition = locPos;

    }

    void OnMouseDown()
    {
        //金币是否足够
       // if(!m_IsEnough)
       //     return;
        
        ////创建塔的类型TowerID
        //int towerID = m_Info.ID;

        ////创建位置
        //Vector3 position = m_Position;
        ////参数
        //object[] args = {towerID, position};
        ////消息冒泡
        /// s
        SpawnTowerArgs e = new SpawnTowerArgs()
        {
            Position = m_Position,
            TowerID = m_Info.ID,
        };
        SendMessageUpwards("OnSpawnTower",e,SendMessageOptions.RequireReceiver);//找不到报错

        
    }
}
