using UnityEngine;
using System.Collections;
using System;

public class Bullet : ReusbleObject,IReusable {
    //子弹类型
    public int ID
    {
        get; private set; }
    //子弹等级
    public int Level { get; set; }
    //基本速度
    public float BaseSpeed { get; private set; }
    //基本攻击力
    public int BaseAttack { get; private set; }
    //移动速度
    public float Speed { get { return BaseSpeed*Level; } }
    //攻击力
    public int Attack { get { return BaseAttack*Level; } }
    //矩形的一个范围，风扇需要地图范围
    public Rect MapRect { get; private set; }
    //子弹如果碰到了敌人延迟回收时间（秒）
    public float DelayToDestroy = 1f;
    //是否爆炸了
    protected bool m_IsExploded = false;
    //动画组件
    private Animator m_Animator;

    protected virtual void Awake()
    {
        m_Animator = GetComponent<Animator>();
     
    }

    protected virtual void Update()
    {
        
    }

    public void Load(int bulletID, int level, Rect mapRect)
    {
        MapRect = mapRect;

        this.ID = bulletID;
        this.Level = level;

        BulletInfo info = Game.Instance.StaticData.GetBulletInfo(bulletID);
        this.BaseSpeed = info.BaseSpeed;
        this.BaseAttack = info.BaseAttack;
    }

    public void Explode()
    {
      //  Debug.Log("子弹爆炸");
        //标记已爆炸
        
        m_IsExploded = true;
        //播放爆炸动画
        m_Animator.SetTrigger("IsExplode");
        //延迟回收
        StartCoroutine("DestroyCoroutine");
    }

    IEnumerator DestroyCoroutine()
    {
        //延迟
        yield return new WaitForSeconds(DelayToDestroy);
        //回收
        Game.Instance.ObjectPool.Unspawn(this.gameObject);
    }
    public override void OnSpawn()
    {
        
    }

    public override void OnUnspawn()
    {
        m_IsExploded = false;
        m_Animator.Play("Play");
        m_Animator.ResetTrigger("IsExplode");
    }

    
}
