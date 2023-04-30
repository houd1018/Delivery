using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseChunk : MonoBehaviour
{
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
                assignBlockFunction(item.gameObject, m_scrollData.HeavenBlocks[Random.Range((int)0, (int)m_scrollData.HeavenBlocks.Length)]);
            }
        }
        
    }
    private void assignBlockFunction(GameObject block,BlockType type)
    {
        switch (type)
        {
            case BlockType.TestBlock1:
                block.AddComponent<TestBlock1>();
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
