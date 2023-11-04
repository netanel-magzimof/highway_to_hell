using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    public TileBehaviour[] tileTemplates;

    [SerializeField] private int numFloors = 3;
    [SerializeField] private int FloorSizeInTiles = 5;
    [SerializeField] private int HeightDiffBetweenFloors = 10;
    [SerializeField] private Transform playerPos;
    private static GameManager _singleton;
    // private 
    
    // Start is called before the first frame update
    void Start()
    {
        // CreateFloorAtHeight(-1);
        new FloorBehaviour(-1, playerPos);
        new FloorBehaviour(9, playerPos);
        new FloorBehaviour(19, playerPos);
        // CreateFloorAtHeight(9);
        CreateFloorAtHeight(19);
    }

    private void Awake()
    {
        if (!_singleton) _singleton = this;
        else Destroy(gameObject); 
    }
    

    private void CreateFloorAtHeight(float height)
    {
        for (int i = -2; i < 3; i++)
        {
            for (int j = -2; j < 3; j++)
            {
                GameObject curTile = Instantiate(tileTemplates[Random.Range(0,tileTemplates.Length)].gameObject);
                Vector3 curPos = new Vector3(i * 2.1f, height, j * 2.1f);
                curTile.transform.position = curPos;
                Vector3 eulerAngles = curTile.transform.eulerAngles;
                eulerAngles.y = Random.Range(0, 4)*90;
                curTile.transform.eulerAngles = eulerAngles;

            } 
        }
    }

    public static GameManager Singleton()
    {
        if (!_singleton)
        {
            _singleton = new GameManager();
        }
        return _singleton;
    }
    
    
}
