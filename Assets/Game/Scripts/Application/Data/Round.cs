using UnityEngine;
using System.Collections;

public class Round
{

    public int Monster;//怪物类型ID
    public int Count;//怪物数量s

    public Round(int monster, int count)
    {
        this.Monster = monster;
        this.Count = count;
    }
}
