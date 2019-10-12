using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Level_Parameter
{
    public int backNum;
}

public class Level_ParameterList : ScriptableObject
{
    public List<Level_Parameter> levelList = new List<Level_Parameter>();

    public int GetLevel(int level)
    {
        return levelList[level].backNum;
    }
}
