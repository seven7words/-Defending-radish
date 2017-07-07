using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class RoundModel : Model {

    #region 常量

    public const float ROUND_INTERVAL = 3f;//回合间隔，秒

    public const float SPAWN_INTERVAL = 1f;//出怪间隔
    #endregion
    #region 事件
    #endregion

    #region 字段
    //该关卡的所欲的出怪信息
    List<Round> m_Rounds = new List<Round>();
    //当前所在怪物回合的索引
    private int m_RoundIndex = -1;
    //是否所有怪物都释放出来了
    private bool m_AllRoundComplete = false;
    Coroutine m_Coroutine;
    #endregion
    #region 属性
    public override string Name
    {
        get { return Consts.M_RoundModel; }
    }

    public int RoundIndex
    {
        get { return m_RoundIndex; }
    }
    public int RoundTotal
    {
        get { return m_Rounds.Count; }
    }
    public bool AllRoundComplete
    {
        get
        {
            return m_AllRoundComplete;
        }

    }

  

    #endregion
    #region 方法

    public void LoadLevel(Level level)
    {
       
        m_Rounds = level.Rounds;
       // m_RoundIndex = -1;
       // m_AllRoundComplete = false;
    }
    public void StartRound()
    {
        //Game.Instance.StopCoroutine(RunRound());
     m_Coroutine =   Game.Instance.StartCoroutine(RunRound());
    }

    public void StopRound()
    {
        Game.Instance.StopCoroutine(m_Coroutine);
    }

    IEnumerator RunRound()
    {

        m_RoundIndex = -1;
        m_AllRoundComplete = false;
       // m_AllRoundComplete = false;
        for (int i = 0; i < m_Rounds.Count; i++)
        {
            //设置回合
            m_RoundIndex = i;
            //回合开始事件
            StartRoundArgs e = new StartRoundArgs();
            e.RoundIndex = m_RoundIndex;
            e.RoundTotal = RoundTotal;
            SendEvent(Consts.E_StartRound, e);

            Round round = m_Rounds[i];
            for (int j = 0; j < round.Count; j++)
            {
                //出怪间隔
                yield return new WaitForSeconds(SPAWN_INTERVAL);
                //出怪事件
                SpawnMonsterArgs ee = new SpawnMonsterArgs()
                {
                    MonsterID = round.Monster
            };
    
                SendEvent(Consts.E_SpawnMonster,ee);
                //最后一波出怪完成
                if ((i == m_Rounds.Count - 1) && (j == round.Count - 1))
                {
                    //出怪完成
                    m_AllRoundComplete = true;
                }
            }


            //回合数自加
            //m_RoundIndex = m_RoundIndex + 1;
            //等待回合事件间隔
            // if(i<m_Rounds.Count-1)

            if (!m_AllRoundComplete)
            {
                //回合间隙
                yield return new WaitForSeconds(ROUND_INTERVAL);
            }
                
        }
        //回合结束事件


 
    }
    #endregion
    #region Unity回调
    #endregion
    #region 事件回调
    #endregion
    #region 帮助方法
    #endregion
}
