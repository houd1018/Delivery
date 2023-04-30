using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurLevel
{
    Heaven,
    Earth,
    Hell
}
public class GameModel : Singleton<GameModel>
{
    private CurLevel m_curLevel;
    public CurLevel CurLevel
    {
        get => m_curLevel;
        set
        {
            m_curLevel = value;
        }
    }
}
