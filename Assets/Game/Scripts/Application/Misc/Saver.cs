using UnityEngine;
using System.Collections;

static class Saver  {
    public static int GetProgerss()
    {
        return PlayerPrefs.GetInt(Consts.GameProgress,-1);
    }

    public static void SetProgress(int levelIndex)
    {
        PlayerPrefs.SetInt(Consts.GameProgress,levelIndex);
    }
}
