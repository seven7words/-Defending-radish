using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

public  class Bottle:Tower
{
    private Transform m_AttackPoint;

    protected override void Awake()
    {
        base.Awake();
        m_AttackPoint = transform.Find("ShotPostion");
    }
  
    public override void Attack(Monster monster)
    {
        base.Attack(monster);

        GameObject go = Game.Instance.ObjectPool.Spawn("BallBullet");
        BallBullet bullet = go.GetComponent<BallBullet>();
        bullet.transform.position =transform.position;
        bullet.Load(this.UseBulletID, this.Level,this.MapRect , monster);
    }
    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnspawn()
    {
        base.OnUnspawn();
    }
    }

