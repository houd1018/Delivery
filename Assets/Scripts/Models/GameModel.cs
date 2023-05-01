using Isekai.Managers;
using MyPackage;
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
    private bool m_gameStarted;
    public bool GameStarted
    {
        get => m_gameStarted;
        set
        {
            m_gameStarted = value;
        }
    }
    private CurLevel m_curLevel;
    public CurLevel CurLevel
    {
        get => m_curLevel;
        set
        {
            m_curLevel = value;
        }
    }

    private float m_prevScrollSpeed;
    private float m_scrollSpeed;
    public float ScrollSpeed
    {
        get => m_scrollSpeed;
        set
        {
            m_scrollSpeed = value;
        }
    }
    public float OriginDepth;
    private float m_depth;
    public float Depth
    {
        get => m_depth;
        set
        {
            m_depth = value;
            EventSystem.Instance.SendEvent<DepthEvent>(typeof(DepthEvent), new DepthEvent() { Depth = m_depth });
        }
    }
    private bool m_canInteract;
    public bool CanInteract
    {
        get => m_canInteract;
        set
        {
            m_canInteract = value;
        }
    }

    private bool m_inTimeline;
    public bool InTimeLine
    {
        get => m_inTimeline;
        set
        {
            m_inTimeline = value;
        }
    }
    public GameModel()
    {
        Reset();
        EventSystem.Instance.Subscribe<GameStartEvent>(typeof(GameStartEvent), onGameStart);
        EventSystem.Instance.Subscribe<GameOverEvent>(typeof(GameOverEvent), onGameOver);

        EventSystem.Instance.Subscribe<PauseGameEvent>(typeof(PauseGameEvent), onPauseGame);
        EventSystem.Instance.Subscribe<ResumeGameEvent>(typeof(ResumeGameEvent), onResumeGame);

        EventSystem.Instance.Subscribe<PauseScrollEvent>(typeof(PauseScrollEvent), onPauseScroll);
        EventSystem.Instance.Subscribe<ResumeScrollEvent>(typeof(ResumeScrollEvent), onResumeScroll);
    }
    ~GameModel()
    {
        EventSystem.Instance.Unsubscribe<GameStartEvent>(typeof(GameStartEvent), onGameStart);
        EventSystem.Instance.Unsubscribe<GameOverEvent>(typeof(GameOverEvent), onGameOver);

        EventSystem.Instance.Unsubscribe<PauseGameEvent>(typeof(PauseGameEvent), onPauseGame);
        EventSystem.Instance.Unsubscribe<ResumeGameEvent>(typeof(ResumeGameEvent), onResumeGame);
    }
    void onGameStart(GameStartEvent e)
    {
        CurLevel = CurLevel.Heaven;
        ScrollSpeed = 0;
        GameStarted = true;
        ScrollSpeed = 1;
    }
    void onGameOver(GameOverEvent e)
    {
        Reset();
    }
    void onPauseGame(PauseGameEvent e)
    {
        GameStarted = false;
    }
    void onResumeGame(ResumeGameEvent e)
    {
        GameStarted = true;
    }
    void onPauseScroll(PauseScrollEvent e)
    {
        m_prevScrollSpeed = ScrollSpeed;
        ScrollSpeed = 0;
    }
    void onResumeScroll(ResumeScrollEvent e)
    {
        ScrollSpeed = m_prevScrollSpeed;
    }
    public void Reset()
    {
        GameStarted = false;
        CurLevel = CurLevel.Heaven;
        ScrollSpeed = 0;
        OriginDepth = 0;
        Depth = 0;
    }
}
