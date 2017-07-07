using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubPool
{
    private Transform m_parent;
    //对象模板（预设）
    GameObject m_prefab;
    //集合
    List<GameObject> m_objects = new List<GameObject>();
    //名字标识
    public string Name
    {
        get
        {
            return m_prefab.name;
        }
    }
    //构造
    public SubPool(GameObject prefab,Transform parent)
    {
        this.m_prefab = prefab;
        this.m_parent = parent;
    }
    //取对象
    public GameObject Spawn()
    {
        GameObject go = null;
        foreach(GameObject  obj in m_objects){
            //如果obj是隐藏状态
            if (!obj.activeSelf)
            {
                go = obj;
                break;
            }
        }
        //池子中没有，重新创建，只要是池子创建，都需要加在集合里
        if(go == null)
        {
            go = GameObject.Instantiate<GameObject>(m_prefab);
            go.transform.parent = m_parent;
            m_objects.Add(go);
        }
        go.SetActive(true);
        //如果没有这个方法也不要报错
        go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);
        return go;
    }
    //回收对象
    public void Unspawn(GameObject go)
    {
        if (Contains(go))
        {
            go.SendMessage("OnUnspawn", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
        }
    }
    //回收该池子中所有对象
    public void UnspawnAll()
    {
        foreach(
            GameObject item in m_objects)
        {
            if (item.activeSelf)
            {
                Unspawn(item);
            }
        }
    }
    //是否包含对象
    public bool Contains(GameObject go)
    {
        return m_objects.Contains(go);
    }
}
