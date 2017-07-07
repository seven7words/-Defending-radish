using UnityEngine;
using System.Collections;

public class FanBullet : Bullet {

    public float RotateSpeed = 180f;//度/每秒

    public Vector2 Direction { get; private set; }

    public void Load(int bulletID,int level,Rect mapRect,Vector3 direction)
    {
        Load(bulletID, level, mapRect);
        Direction = direction;
    }
    protected override void Update()
    {
        //已经爆炸跳过
        if (m_IsExploded)
            return;
        //移动
        transform.Translate(Direction * Speed * Time.deltaTime, Space.World);
        //旋转
        transform.Rotate(Vector3.forward, RotateSpeed * Time.deltaTime, Space.World);
        //检车 存活/死亡
        GameObject[] monsterObjects = GameObject.FindGameObjectsWithTag("Monster");
        foreach(GameObject monsterObject in monsterObjects)
        {
            Monster monster = monsterObject.GetComponent<Monster>();
            //忽略已死亡的怪物
            if (monster.IsDead)
                continue;
            if (Vector3.Distance(transform.position, monster.transform.position) <= Consts.RangeCloseDistance)
            {
                //敌人受伤
                monster.Damage(this.Attack);
                //爆炸
                Explode();
                //退出（重点）
                break;


            }
        }
        //边间检测
        if (!m_IsExploded && !MapRect.Contains(transform.position))
        {
            Explode();
        }
    }
}
