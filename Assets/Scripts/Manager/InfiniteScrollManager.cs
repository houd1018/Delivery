using Isekai.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScrollManager:MonoBehaviour
{
    public Transform Parent;
    public GameObject ChunkPrefab;
    [SerializeField]
    private InfiniteScrollData m_infiniteScrollData;
    [SerializeField]
    private float m_startPos;
    [SerializeField]
    private float m_chunckHeight;
    [Header("CurLevel:")]
    private CurLevel Level;

    private int m_totalChunks;
    private Camera m_mainCamera;
    private Queue<Transform> m_chunks;
    
    private void Start()
    {
        m_chunks = new Queue<Transform>();
        m_mainCamera = Camera.main;
    }
    private void Update()
    {
        if (Game.Instance!=null&&GameModel.Instance.GameStarted)
        {
            m_mainCamera.transform.position += Vector3.down * Time.deltaTime*GameModel.Instance.ScrollSpeed;
        }
        checkIfNeedNewChunk();
        checkIfNeedDeleteOldChunk();
    }

    private void checkIfNeedNewChunk()
    {
        float bottomPos = m_mainCamera.ScreenToWorldPoint(Vector3.zero).y;
        
        if (bottomPos<m_startPos - (m_totalChunks-1)*m_chunckHeight)
        {
            Debug.Log(bottomPos + " " + (m_startPos - m_totalChunks * m_chunckHeight));
            Debug.Log("need spawn chunk");
            var go = Instantiate(m_infiniteScrollData.ChunkPrefabs[Random.Range((int)0,m_infiniteScrollData.ChunkPrefabs.Length)], Parent);
            go.GetComponent<BaseChunk>().Level = Level;
            go.transform.localPosition = new Vector3(0, m_startPos - m_totalChunks * m_chunckHeight, 0);
            m_chunks.Enqueue(go.transform);
            m_totalChunks++;
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
}
