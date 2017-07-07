using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UISystem : View
{
    #region 常量

    #endregion
    #region 事件
    #endregion

    #region 字段

    public Button btnResume;
    public Button btnRestart;
    public Button btnSelect;
    #endregion
    #region 属性
    public override string Name
    {
        get { return Consts.V_System; }
    }

    #endregion
    #region 方法
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    #endregion
    #region Unity回调
    #endregion
    #region 事件回调
    public override void HandleEvent(string eventName, object data)
    {

    }
    public void OnResumeClick()
    {
        
    }

    public void OnRestartClick()
    {
        
    }

    public void OnSelectClick()
    {
        
    }
 
    #endregion
    #region 帮助方法
    #endregion


}
