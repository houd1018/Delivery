using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseChunk : MonoBehaviour
{
    public CurLevel Level;

    [SerializeField]
    private InfiniteScrollData m_scrollData;
    private List<GameObject> Blocks;

    // Start is called before the first frame update
    void Start()
    {
        m_scrollData = Resources.Load<InfiniteScrollData>("Data/InfiniteScrollData/InfiniteScrollData");
        Blocks = new List<GameObject>();
        foreach (Transform item in transform.Find("Floors"))
        {
            Blocks.Add(item.gameObject);
            if(m_scrollData.Difficulty > Random.Range(0f,1f))
            {
                BlockData data = GetData();

                item.GetComponent<SpriteRenderer>().sprite = data.BlockSprite;
                assignBlockFunction(item.gameObject, data.Type);
            }
        }
        
    }

    BlockData GetData()
    {
        switch (Level)
        {
            case CurLevel.Heaven:
                return m_scrollData.HeavenBlocks[Random.Range((int)0, (int)m_scrollData.HeavenBlocks.Length)];
            case CurLevel.Earth:
                return m_scrollData.EarthBlocks[Random.Range((int)0, (int)m_scrollData.EarthBlocks.Length)];
            case CurLevel.Hell:
                return m_scrollData.HellBlocks[Random.Range((int)0, (int)m_scrollData.HellBlocks.Length)]; 
            default:
                return default;                
        }
    }
    private void assignBlockFunction(GameObject block,BlockType type)
    {
        switch (type)
        {
            case BlockType.TestBlock1:
                block.AddComponent<TestDisappearBlock>();
                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
