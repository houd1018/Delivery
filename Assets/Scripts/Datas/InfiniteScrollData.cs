using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BlockType
{
    TestBlock1,
}
[CreateAssetMenu(fileName ="InfiniteScrollData",menuName ="Data/InfiniteScrollData",order = 1)]
public class InfiniteScrollData : ScriptableObject
{
    public float Difficulty;
    public GameObject[] ChunkPrefabs;
    public BlockType[] HeavenBlocks;

    public void Reset()
    {
        Difficulty = 0;
    }
}
