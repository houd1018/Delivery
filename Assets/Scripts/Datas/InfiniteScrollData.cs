using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BlockType
{
    BounceBlock,
    ConveyBlock,
    DisappearBlock,
    MovingBlock,
    TrapBlock,
    NormalBlock
}
[Serializable]
public class BlockData
{
    public BlockType Type;
    public Sprite BlockSprite;
}
[CreateAssetMenu(fileName ="InfiniteScrollData",menuName ="Data/InfiniteScrollData",order = 1)]
public class InfiniteScrollData : ScriptableObject
{
    public float Difficulty;
    public GameObject[] ChunkPrefabs;
    public BlockData[] HeavenBlocks;
    public BlockData[] EarthBlocks;
    public BlockData[] HellBlocks;
    public GameObject HeavenLastChunk;
    public GameObject EarthLastChunk;
    public GameObject HellLastChunk;

    public void Reset()
    {
        Difficulty = 0;
    }
}
