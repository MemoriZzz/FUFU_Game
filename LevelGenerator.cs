using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 30f;

    [SerializeField] private Transform pfTestingPlatform;
    [SerializeField] private Transform levelPart_Start;
    
    [SerializeField] private List<Transform> levelPart_EasyList;
    [SerializeField] private List<Transform> levelPart_MediumList;
    [SerializeField] private List<Transform> levelPart_HardList;
    [SerializeField] private List<Transform> levelPart_ImpossibleList;

    [SerializeField] private Player player;

    private enum Difficulty
    {
        Easy, Medium, Hard, Impossible
    }

    private Vector3 lastEndPosition;
    private int levelPartsSpawned; //count

    private void Awake()
    {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        
       // int startingSpawnLevelParts = 5;
       // for(int i = 0; i< startingSpawnLevelParts; i++)
       // {
         //   SpawnLevelPart();
       // }
    }

    private void Update()
    {
        if(Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            //Spawn another level part
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        List<Transform> difficultyLevelPartList;

        switch (GetDifficulty()) {
            default:
            case Difficulty.Easy:           difficultyLevelPartList = levelPart_EasyList;       break;
            case Difficulty.Medium:         difficultyLevelPartList = levelPart_MediumList;     break;
            case Difficulty.Hard:           difficultyLevelPartList = levelPart_HardList;       break;
            case Difficulty.Impossible:     difficultyLevelPartList = levelPart_ImpossibleList; break;
        }

        Transform chosenLevelPart = difficultyLevelPartList[Random.Range(0, difficultyLevelPartList.Count)];

        if(pfTestingPlatform != null)
        {
            chosenLevelPart = pfTestingPlatform;
        }

        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        levelPartsSpawned++;
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }

    private Difficulty GetDifficulty()
    {
        if (levelPartsSpawned >= 15) return Difficulty.Impossible;
        if (levelPartsSpawned >= 10) return Difficulty.Hard;
        if (levelPartsSpawned >= 5) return Difficulty.Medium;
        return Difficulty.Easy;
    }


}
