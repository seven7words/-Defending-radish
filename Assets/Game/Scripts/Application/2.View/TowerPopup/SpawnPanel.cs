  using UnityEngine;
using System.Collections;

public class SpawnPanel : MonoBehaviour
{






    #region 常量

    #endregion
    #region 事件
    #endregion

    #region 字段
    private TowerIcon[] m_TowerIcons;

    #endregion
    #region 属性
    #endregion
    #region 方法
    public void Show(GameModel gm, Vector3 position, bool upSide)
    {
        //设置位置
        transform.position = position;
        //动态加载图标
        for (int i = 0; i < m_TowerIcons.Length; i++)
        {
            TowerInfo info = Game.Instance.StaticData.GetTowerInfo(i);
            m_TowerIcons[i].Load(gm, info, position, upSide);
        }

        //显示
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        //隐藏
        gameObject.SetActive(false);
    }
    #endregion
    #region Unity回调
    void Awake()
    {
        m_TowerIcons = GetComponentsInChildren<TowerIcon>();
    }

    #endregion
    #region 事件回调
    #endregion
    #region 帮助方法
    #endregion










   

  
    
	
}
