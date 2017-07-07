using UnityEngine;
using System.Collections;
using System;

public abstract class Tower :ReusbleObject,IReusable
{
    public int ID
    {
        get; private set;
    }
    public int Level
    {
        get { return m_Level; }
        set
        {
            m_Level = Mathf.Clamp(value, 0, MaxLevel);
            //根据级别设置大小
            transform.localScale = Vector3.one * (1 + (m_Level) * 0.25f);
        }
    }
    public int MaxLevel
    {
        get; private set;
    }
    public bool IsTopLevel
    {
        get { return Level >= MaxLevel; }

    }
    public float ShotRate
    {
        get; private set;
    }
    public float GuardRange
    {
        get; private set;
    }
    public int BasePrice
    {
        get; private set;
    }
    public int UseBulletID
    {
        get; private set;
    }
    public int Price
    {
        get { return BasePrice * Level; }
    }
    //所在tile
    public Tile Tile { get; private set; }

    public Rect MapRect { get; private set; }
    //攻击目标
    private Monster m_Target;
    //动画组件
    protected Animator m_Animator;

    //等级
    private int m_Level = 0;

    //上次攻击事件
    private float m_LastAttackTime = 0f;




    protected virtual void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }


    void Update()
    {
        //搜索目标
        if (m_Target == null)
        {
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
            foreach (GameObject monster in monsters)
            {
                Monster m = monster.GetComponent<Monster>();
                float dis = Vector3.Distance(m.transform.position, transform.position);
                if (!m.IsDead && this.GuardRange >= dis)
                {
                    m_Target = m;
                  //  Debug.Log(m_Target.name+"target");
                    break;//找到就退出
                }
            }
        }
        else
        {
            //攻击目标
            float dis = Vector3.Distance(m_Target.transform.position, transform.position);
            //   Debug.Log(dis+m_Target.name);
            //目标已经搜索到
            if (m_Target.IsDead || this.GuardRange < dis)
            {
                m_Target = null;
                Debug.Log("目标跑了");
               
                LookAt(null);
                return;
            }
            //朝向目标
            LookAt(m_Target);
            float attackTime = m_LastAttackTime + 1f / ShotRate;
        
            if (Time.time >= attackTime)
            {
                //创建子弹
                
                Attack(m_Target);
                Debug.Log("攻击");
                //记录攻击事件
                m_LastAttackTime = Time.time;
            }
        }

        //看着目标
        LookAt(m_Target);



    }


  

    public void Load(int towerID,Tile tile,Rect mapRect)
    {
        TowerInfo info = Game.Instance.StaticData.GetTowerInfo(towerID);
        this.ID = info.ID;
        this.MaxLevel = info.MaxLevel;
        this.BasePrice = info.BasePrice;
        this.GuardRange = info.GuardRange;
        this.ShotRate = info.ShotRate;
        this.Level = 1;
        this.UseBulletID = info.UseBulletID;
        Tile = tile;
        MapRect = mapRect;
    }
    
    public virtual void Attack(Monster monster)
    {
        m_Animator.SetTrigger("IsAttack");
    }

   

    void LookAt(Monster target)
    {
        if (m_Target == null)
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.z = 0;
            transform.eulerAngles = eulerAngles;
        }
        else
        {
            Vector3 dir = (m_Target.transform.position - transform.position).normalized;
            float dy = dir.y;
            float dx = dir.x;
            //这个比较好【-180，180】弧度值
            float angles = Mathf.Atan2(dy, dx);
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.z = angles * Mathf.Rad2Deg - 90f;
            transform.eulerAngles = eulerAngles;
        }
        

    }
    public   override void OnSpawn()
    {
      
    }

    public  override void OnUnspawn()
    {
      m_Animator.ResetTrigger("IsAttack");
        m_Animator.Play("Idle");
        
        m_Target = null;
        ID = 0;
        m_LastAttackTime = 0;

        Tile = null;

        Level = 0;
        MaxLevel = 0;
        ShotRate = 0;
        BasePrice = 0;


    }
}
