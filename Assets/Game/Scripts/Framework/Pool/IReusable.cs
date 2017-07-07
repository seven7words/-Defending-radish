using UnityEngine;
using System.Collections;

public interface IReusable {

    //1.当取出时调用
    void OnSpawn();
    //2.当回收时调用
    void OnUnspawn();
  
}
