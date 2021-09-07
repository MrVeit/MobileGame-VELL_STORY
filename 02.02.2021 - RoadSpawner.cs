using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] RoadPrefabs;
    public GameObject StartBlock;

    float blockZpos = 0; //хранение позиции по Х позиции
    int blocksCount = 3; //количество генерируемых блоков
    float blockLength = 0; //значение длины 1 блока
    int safeZone = 50;

    public Transform PlayerTransform;
    List<GameObject> CurrentBlocks = new List<GameObject>();

    void Start()
    {
        blockZpos = StartBlock.transform.position.z;
        blockLength = StartBlock.GetComponent<BoxCollider>().bounds.size.z;

        CurrentBlocks.Add(StartBlock); ///delete firstSpawnBlock

        for (int i = 0; i < blocksCount; i++)
            SpawnBlock();
    }

    void Update()
    {
        CheckForSpawn();
    }

    void CheckForSpawn()
    {
        if (PlayerTransform.position.z - safeZone > (blockZpos - blocksCount * blockLength))
        {
            SpawnBlock();
            DestroyBlock();
        }
    }

    void SpawnBlock()
    {
        GameObject block = Instantiate(RoadPrefabs[Random.Range(0, RoadPrefabs.Length)], transform);
        blockZpos += blockLength;
        block.transform.position = new Vector3(0, 0, blockZpos);

        CurrentBlocks.Add(block);
    }

    void DestroyBlock()
    {
        Destroy(CurrentBlocks[0]);
        CurrentBlocks.RemoveAt(0);
    }    
}
