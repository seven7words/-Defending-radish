using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class UISelect : View {

    #region 常量

    #endregion
    #region 事件
    #endregion

    #region 字段

    public Button btnStart;
    List<Card > m_Cards=new List<Card>();
    private int m_SelectIndex = -1;
    private GameModel m_GameModel = null;
    #endregion
    #region 属性
    public override string Name
    {
        get
        {
            return Consts.V_Select;
        }
    }
    #endregion
    #region 方法
    /// <summary>
    /// 返回开始界面
    /// </summary>
    public void GoBack()
    {
        Game.Instance.LoadScene(1);
    }
    /// <summary>
    /// 选中关卡游戏
    /// </summary>
    public void ChooseLevel()
    {
        StartLevelArgs e = new StartLevelArgs()
        {
            LevelIndex = m_SelectIndex,
        };
      
        SendEvent(Consts.E_StartLevel, e);
    }

     void LoadCards()
    {
        print("-----------------LockCards");

        
        //构建Levels集合
        List<Level> levels = m_GameModel.AllLevels;
     

        //构建Card集合
        List<Card> cards = new List<Card>();
        for (int i = 0; i < levels.Count; i++)
        {
            Card card = new Card()
            {
                LevelID = i,
                CardImage = levels[i].CardImage,
         
               // IsLocked = i>m_GameModel.GameProgress,
               IsLocked = !(i<=m_GameModel.GameProgress+1)
            };
            cards.Add(card);
        }
        //保存
        this.m_Cards = cards;
        //监听卡片点击事件
         UICard[] uiCards = this.transform.Find("UICards").GetComponentsInChildren<UICard>();
         foreach (UICard uiCard in uiCards)
         {
             uiCard.OnClick += (card) =>
             {
                 SelectCard(card.LevelID);
             };
         }

        //默认选择第一个卡片
        SelectCard(0);

    }

    void SelectCard(int index)
    {
        if (m_SelectIndex == index)
        {
            return;
        }
        m_SelectIndex = index;

        //计算索引号
        int left = m_SelectIndex - 1;
        int current = m_SelectIndex;
        int right =m_SelectIndex + 1;
        //绑定数据
        Transform container = this.transform.Find("UICards");
        //左边
        if (left < 0)
        {
            container.GetChild(0).gameObject.SetActive(false);

        }
        else
        {
            container.GetChild(0).gameObject.SetActive(true);
            container.GetChild(0).GetComponent<UICard>().IsTransparent = true;

          container.GetChild(0).GetComponent<UICard>().DataBind(m_Cards[left]);
        }
        //当前
     

        container.GetChild(1).GetComponent<UICard>().DataBind(m_Cards[current]);
        container.GetChild(1).GetComponent<UICard>().IsTransparent = false;
        //控制游戏开始按钮状态
        btnStart.gameObject.SetActive(!m_Cards[current].IsLocked);
        //右边
        if (right >= m_Cards.Count)
        {
            container.GetChild(2).gameObject.SetActive(false);

        }
        else
        {
           

            container.GetChild(2).gameObject.SetActive(true);
            container.GetChild(2).GetComponent<UICard>().IsTransparent = true;
            container.GetChild(2).GetComponent<UICard>().DataBind(m_Cards[right]);
        }
    

    }
    #endregion
    #region Unity回调
    #endregion
    #region 事件回调

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_EnterScene);
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_EnterScene:

                SceneArgs e = data as SceneArgs;
                if (e.SceneIndex == 2)
                {
                    //获取模型数据
                    m_GameModel = GetModel<GameModel>();
                    //初始化Card列表
                    LoadCards();
                }
                break;
                
        }
    }
    #endregion
    #region 帮助方法
    #endregion

}
