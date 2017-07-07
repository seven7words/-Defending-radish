using UnityEngine;
using System.Collections;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T m_instance = null;
    public static T Instance
    {
        get
        {
            return m_instance;
        }
    }
    //virtual子类可以重写
    //当组件挂载之后很快就会执行
    protected virtual void Awake()
    {
        m_instance = this as T;
    }
}
void FindChild(Transform go, List<string> findName , ref List<Transform> tr)
{
    if (tr.Count == findName.Count){
        return;
    }
	if (go.name.Equals(findName))
	{
        tr.Add(go);
		return;
	}
	if (go.childCount != 0)
	{
		for (int i = 0; i < go.childCount; i++)
		{
			FindChild(go.GetChild(i), findName, ref tr);
		}
	}
}