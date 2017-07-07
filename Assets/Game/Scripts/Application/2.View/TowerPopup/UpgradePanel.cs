using UnityEngine;
using System.Collections;

public class UpgradePanel : MonoBehaviour
{

    #region 常量

    #endregion
    #region 事件
    #endregion

    #region 字段
    private UpgradeIcon m_UpgradeIcon;
    private SellIcon m_SellIcon;
    #endregion
    #region 属性
    #endregion
    #region 方法
    public void Show(GameModel gm, Tower tower)
    {
        //位置
        transform.position = tower.transform.position;
        //显示
        m_UpgradeIcon.Load(gm, tower);
        m_SellIcon.Load(tower);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    #endregion
    #region Unity回调
    void Awake()
    {
        m_UpgradeIcon = GetComponentInChildren<UpgradeIcon>();
        m_SellIcon = GetComponentInChildren<SellIcon>();
    }
    #endregion
    #region 事件回调
    #endregion
    #region 帮助方法
    #endregion

    // Use this for initialization



}
