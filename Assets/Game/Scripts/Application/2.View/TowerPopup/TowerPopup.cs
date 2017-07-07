using UnityEngine;
using System.Collections;
using System;

public class TowerPopup : View 
{
    #region 常量

    #endregion
    #region 事件

    void Awake()
    {
        m_Instance = this;
    }

    void Start()
    {
        HideAllPopups();
    }
    #endregion

    #region 字段
    public SpawnPanel SpawnPanel;
    public UpgradePanel UpgradePanel;
    private static TowerPopup m_Instance = null;

    public static TowerPopup Instance
    {
        get { return m_Instance; }
    }
    #endregion
    #region 属性
    public override string Name
    {
        get { return Consts.V_TowerPopup; }
    }

    public bool IsPopShow
    {
        get
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf)
                {
                    return true;
                }
            }
            return false;
        }
    }
    #endregion
    #region 方法
    void ShowSpawnPanel(Vector3 position, bool upSide)
    {

        GameModel gm = GetModel<GameModel>();

        HideAllPopups();
        SpawnPanel.Show(gm, position, upSide);
    }

    void ShowUpgradePanel(Tower tower)
    {
        GameModel gm = GetModel<GameModel>();
        HideAllPopups();

        UpgradePanel.Show(gm, tower);
    }

    void HideAllPopups()
    {
        SpawnPanel.Hide();
        UpgradePanel.Hide();

    }
    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_ShowSpawnPanel);
        AttentionEvents.Add(Consts.E_ShowUpgradePanel);
        AttentionEvents.Add(Consts.E_HidePopups);

    }
    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_ShowSpawnPanel:
                ShowSpawnPanelArgs e1 = data as ShowSpawnPanelArgs;
                ShowSpawnPanel(e1.Position, e1.UpSide);
                break;
            case Consts.E_ShowUpgradePanel:
                ShowUpgradePanelArgs e2 = data as ShowUpgradePanelArgs;
                ShowUpgradePanel(e2.Tower);
                break;
            case Consts.E_HidePopups:
                HideAllPopups();
                break;
            default:
                break;

        }
    }
    void OnSpawnTower(SpawnTowerArgs e)
    {
       // HideAllPopups();
        SendEvent(Consts.E_SpawnTower,e);

    }

    void OnUpgradeTower(UpgradeTowerArg e)
    {
        //HideAllPopups();
        SendEvent(Consts.E_UpgradeTower, e);
    }

    void OnSellTower(SellTowerArgs e)
    {
        SendEvent(Consts.E_SellTower,e);
    }
    #endregion

    #region Unity回调
    #endregion
    #region 事件回调
    #endregion
    #region 帮助方法
    #endregion










}
