using Isekai.Managers;
using MyPackage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScrollManager:MonoBehaviour
{
    public Transform Parent;
    [SerializeField]
    private InfiniteScrollData m_infiniteScrollData;
    [SerializeField]
    private float m_startPos;
    [SerializeField]
    private float m_chunckHeight;
    [SerializeField]
    private float m_heartRatio = 0.2f;
    [SerializeField]
    private float m_difficultyGrow = 0.01f;
    [Header("CurLevel:")]
    [SerializeField]
    private CurLevel Level;
    

    private int m_totalChunks;
    private Camera m_mainCamera;
    private Queue<Transform> m_chunks;
    private bool m_curLevelClear;
    private bool m_lastChunkInstantiated;
    private float m_lastPosition;
    private void Start()
    {
        m_infiniteScrollData.Difficulty = 0;

        m_chunks = new Queue<Transform>();
        m_mainCamera = Camera.main;

        EventSystem.Instance.Subscribe<GameStartEvent>(typeof(GameStartEvent), onGameStart);
        EventSystem.Instance.Subscribe<GameOverEvent>(typeof(GameOverEvent), onGameOver);
    }
    private void Update()
    {
        if (Game.Instance!=null&&GameModel.Instance.GameStarted)
        {
            m_mainCamera.transform.position += Vector3.down * Time.deltaTime*GameModel.Instance.ScrollSpeed;
            if(!GameModel.Instance.ScrollPaused)
                GameModel.Instance.ScrollSpeed = 1 + m_infiniteScrollData.Difficulty*((int)Level+1);
        }
        checkIfNeedNewChunk();
        checkIfNeedDeleteOldChunk();
        checkLevelClear();
        checkScrollStop();
        setCurDepth();
        
    }
    void checkLevelClear()
    {
        m_infiniteScrollData.Difficulty += Time.deltaTime * m_difficultyGrow;
        if (m_infiniteScrollData.Difficulty >= 1)
        {
            m_curLevelClear = true;
        }
    }
    void checkScrollStop()
    {
        if (m_curLevelClear && m_mainCamera.ScreenToWorldPoint(Vector3.zero).y <= m_lastPosition + 0.1f&& m_lastChunkInstantiated)
        {
            EventSystem.Instance.SendEvent<PauseScrollEvent>(typeof(PauseScrollEvent), new PauseScrollEvent());
            m_infiniteScrollData.Difficulty = 0;
        }
    }
    private void checkIfNeedNewChunk()
    {
        float bottomPos = m_mainCamera.ScreenToWorldPoint(Vector3.zero).y;
        
        if (bottomPos<m_startPos - (m_totalChunks-1)*m_chunckHeight)
        {
            if (!m_curLevelClear)
            {
                var go = Instantiate(m_infiniteScrollData.ChunkPrefabs[Random.Range((int)0, m_infiniteScrollData.ChunkPrefabs.Length)], Parent);

                spawnAppleAndHeart(go);

                go.GetComponent<BaseChunk>().Level = Level;
                
                go.transform.localPosition = new Vector3(0, m_startPos - m_totalChunks * m_chunckHeight, 0);
                m_chunks.Enqueue(go.transform);
                m_totalChunks++;
            }
            else if(!m_lastChunkInstantiated)
            {
                m_lastChunkInstantiated = true;
                var go = Instantiate(getLastChunk(), Parent);
                go.GetComponent<BaseChunk>().Level = Level;
                go.transform.localPosition = new Vector3(0, m_startPos - m_totalChunks * m_chunckHeight, 0);
                m_chunks.Enqueue(go.transform);
                m_totalChunks++;
                m_lastPosition = go.transform.position.y - m_chunckHeight / 2;
            }
        }
    }
    private void spawnAppleAndHeart(GameObject go)
    {
        //If chunk % 5=0, then spawn golden apple
        Debug.Log(m_totalChunks);
        if (m_totalChunks % 5 == 0&&m_totalChunks!=0)
        {
            go.GetComponent<BaseChunk>().SpawnGoldenApple = true;
        }
        if(Random.Range(0f,1f) < m_heartRatio)
        {
            go.GetComponent<BaseChunk>().SpawnHeart = true;
        }
        
    }
    private void checkIfNeedDeleteOldChunk()
    {
        float topPos = m_mainCamera.ScreenToWorldPoint(new Vector3(0,Screen.height,0)).y;
        Transform tf = m_chunks.Peek();
        if (tf.position.y - m_chunckHeight > topPos)
        {
            m_chunks.Dequeue();
            DestroyImmediate(tf.gameObject);
        }
    }

    private void setCurDepth()
    {

        GameModel.Instance.Depth = GameModel.Instance.OriginDepth + m_mainCamera.ScreenToWorldPoint(Vector3.zero).y * 100;
        
    }
    GameObject getLastChunk()
    {
        switch (Level)
        {
            case CurLevel.Heaven:
                return m_infiniteScrollData.HeavenLastChunk;
            case CurLevel.Earth:
                return m_infiniteScrollData.EarthLastChunk;
            case CurLevel.Hell:
                return m_infiniteScrollData.HellLastChunk;
            default:
                return default;
        }
    }
    void onGameStart(GameStartEvent e)
    {
        m_infiniteScrollData.Difficulty = 0;
    }
    void onGameOver(GameOverEvent e)
    {
        m_infiniteScrollData.Difficulty = 0;
    }
    private void OnDestroy()
    {
        GameModel.Instance.OriginDepth += m_lastPosition*100;
        EventSystem.Instance.Unsubscribe<GameStartEvent>(typeof(GameStartEvent), onGameStart);
        EventSystem.Instance.Unsubscribe<GameOverEvent>(typeof(GameOverEvent), onGameOver);
    }
}
