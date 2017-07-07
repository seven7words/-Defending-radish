using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Resources;

public class GameModel :Model {

    #region 常量

    #endregion
    #region 事件
    #endregion

    #region 字段
    //所有关卡
    List<Level> m_Levels = new List<Level>();
    //当前游戏关卡索引
    private int m_PlayLevelId = -1;

    //最大通关关卡索引
    private int m_GameProgress = -1;
    //游戏当前分数
    private int _mGold = 0;
    //是否游戏中
    private bool m_IsPlaying = false;
    //Saver m_saver = new Saver();
    #endregion
    #region 属性
    public override string Name
    {
        get { return Consts.M_GameModel; }
    }
  
    public int Gold
    {
        get
        {
            return _mGold;
        }

        set
        {
            _mGold = value;
        }
    }
      public int LevelCount
    {
        get { return m_Levels.Count; }
    }
    public int GameProgress
    {
        get
        {
            return m_GameProgress;
        }


    }
    public int PlayLevelID
    {
        get
        {
            return m_PlayLevelId;
        }


    }
    public bool IsPlaying
    {
        get
        {
            return m_IsPlaying;
        }
        set { m_IsPlaying = value; }


    }
    public bool IsGamePassed
    {
        get { return GameProgress >= LevelCount - 1; }
    }
    public List<Level> AllLevels
    {
        get { return m_Levels; }
    }
    public Level PlayLevel
    {
        get
        {
            if (PlayLevelID < 0 || PlayLevelID > m_Levels.Count - 1)
                throw new IndexOutOfRangeException("关卡不存在");

            return m_Levels[PlayLevelID];
        }
    }

  

   
  

   

  

  
    #endregion
    #region 方法
    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialize()
    {
        //构建Level集合
        List<FileInfo> files = Tools.GetLevelFiles();

        List<Level> levels = new List<Level>();
        for (int i = 0; i < files.Count; i++)
        {
            
            Level level = new Level();
            Tools.FillLevel(files[i].FullName, ref level);
            levels.Add(level);
        }
        m_Levels = levels;
        //读取游戏进度
        //测试
        //m_GameProgress = 0;
        m_GameProgress = Saver.GetProgerss();

    }
    /// <summary>
    /// 游戏开始
    /// </summary>
    /// <param name="levelIndex"></param>
    public void StartLevel(int levelIndex)
    {
        
        m_PlayLevelId = levelIndex;
       
    }
    /// <summary>
    /// 游戏结束
    /// </summary>
    /// <param name="isWin"></param>
    public void StopLevel(bool isWin)
    {
        if ( isWin&& PlayLevelID > GameProgress)
        {
            //更新内存里的数据
            m_GameProgress = PlayLevelID;
            //保存进度
            Saver.SetProgress(PlayLevelID);
            
           
        }
        m_IsPlaying = false;
    }
    /// <summary>
    /// 清档
    /// </summary>
    public void ClearProgress()
    {
        m_IsPlaying = false;
        m_PlayLevelId = -1;
       
        m_GameProgress = -1;
        Saver.SetProgress(-1);
    }
    #endregion
    #region Unity回调
    #endregion
    #region 事件回调
    #endregion
    #region 帮助方法
    #endregion




}
