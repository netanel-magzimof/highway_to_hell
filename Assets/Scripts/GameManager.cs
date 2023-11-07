using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    #region Inspector
    
    [Header("Floor And Tiles")]
    public TileBehaviour[] tileTemplates;
    [SerializeField] private int numFloors = 3;
    [SerializeField] private int FloorSizeInTiles = 5;
    [SerializeField] private int HeightDiffBetweenFloors = 10;
    [SerializeField] private FloorBehaviour _floorBehaviour;
    
    [Header("Player")]
    public Transform playerPos;

    
    [Header("Debug")]
    public bool ShouldPlatformFall = true;
    
    #endregion
    
    
    #region Fields
        
    private static GameManager _singleton;
    
    #endregion
    
    
    #region MonoBehaviour
    private void Awake()
    {
        if (!_singleton) _singleton = this;
        else Destroy(gameObject); 
    }
    
    void Start()
    {
        for (int i = 0; i < numFloors; i++)
        {
            Instantiate(_floorBehaviour, Vector3.up*i*HeightDiffBetweenFloors,Quaternion.identity);
        }
    }
    
    #endregion
    
    
    #region Methods
    
    public static GameManager Singleton()
    {
        if (!_singleton)
        {
            _singleton = new GameManager();
        }
        return _singleton;
    }
    
    #endregion

    
    
    

    
    
    
}
