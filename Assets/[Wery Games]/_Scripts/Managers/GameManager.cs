using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector]
    public Block[][] blockContainer; // Works like a grid

    public BlockPool blockPool;
    public BlockOperationHandler blockHandler;

    [Header("Properties")]
    public BlockProperties blockProperties;
    public ContainerProperties containerProperties;

    private void Start()
    {
        LevelGenerator.GenerateContainer(containerProperties);
        LevelGenerator.FillContainer(blockProperties, containerProperties);
    }
}