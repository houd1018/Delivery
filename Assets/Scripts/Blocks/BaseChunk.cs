using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseChunk : MonoBehaviour
{
    public CurLevel Level;
    public bool SpawnGoldenApple;
    public bool SpawnHeart;

    [SerializeField]
    private InfiniteScrollData m_scrollData;
    private List<GameObject> Blocks;
    private GameObject goldenApple;
    private GameObject heart;
    // Start is called before the first frame update
    void Start()
    {
        goldenApple = Resources.Load<GameObject>("Prefabs/GameObject/Apple");
        heart = Resources.Load<GameObject>("Prefabs/Gameobject/Heart");
        m_scrollData = Resources.Load<InfiniteScrollData>("Data/InfiniteScrollData/InfiniteScrollData");
        Blocks = new List<GameObject>();
        
        foreach (Transform item in transform.Find("Floors"))
        {
            Blocks.Add(item.gameObject);
            
            item.GetComponent<SpriteRenderer>().sprite = GetNormalSprite();
            if (m_scrollData.Difficulty > Random.Range(0f,1f))
            {
                BlockData data = GetData();

                item.GetComponent<SpriteRenderer>().sprite = data.BlockSprite;
                assignBlockFunction(item.gameObject, data.Type);
            }
        }
        trySpawnGoldenAppleAndHeart();
    }
    Sprite GetNormalSprite()
    {
        switch (Level)
        {
            case CurLevel.Heaven:
                return m_scrollData.HeavenBlocks[0].BlockSprite;
            case CurLevel.Earth:
                return m_scrollData.EarthBlocks[0].BlockSprite;
            case CurLevel.Hell:
                return m_scrollData.EarthBlocks[0].BlockSprite;
            default:
                return default;
        }
    }
    BlockData GetData()
    {
        switch (Level)
        {
            case CurLevel.Heaven:
                return m_scrollData.HeavenBlocks[Random.Range((int)1, (int)m_scrollData.HeavenBlocks.Length)];
            case CurLevel.Earth:
                return m_scrollData.EarthBlocks[Random.Range((int)1, (int)m_scrollData.EarthBlocks.Length)];
            case CurLevel.Hell:
                return m_scrollData.HellBlocks[Random.Range((int)1, (int)m_scrollData.HellBlocks.Length)]; 
            default:
                return default;                
        }
    }
    public void trySpawnGoldenAppleAndHeart()
    {
        if (SpawnGoldenApple && Level == CurLevel.Heaven)
        {
            var parent = Blocks[Random.Range((int)0, (int)Blocks.Count)].transform;
            var go = Instantiate(goldenApple, parent.position+Vector3.up,Quaternion.identity);
            go.transform.SetParent(parent);
        }
        if (SpawnHeart)
        {
            var parent = Blocks[Random.Range((int)0, (int)Blocks.Count)].transform;
            var go = Instantiate(heart, parent.position + Vector3.up, Quaternion.identity);
            go.transform.SetParent(parent);
        }
    }
    private void assignBlockFunction(GameObject block,BlockType type)
    {
        switch (type)
        {
            case BlockType.BounceBlock:
                block.AddComponent<TestBounceBlock>();
                break;
            case BlockType.ConveyBlock:
                block.AddComponent<TestConveyBlock>();
                break;
            case BlockType.DisappearBlock:
                block.AddComponent<TestDisappearBlock>();
                break;
            case BlockType.MovingBlock:
                block.AddComponent<TestMovingBlock>();
                break;
            case BlockType.TrapBlock:
                block.AddComponent<TestTrapBlock>();
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
