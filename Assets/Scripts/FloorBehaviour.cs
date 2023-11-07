using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBehaviour : MonoBehaviour
{
    private Transform _playerPos;
    private float _height;
    private TileBehaviour[] _tiles;
    private bool[] _availableTiles;
    private Coroutine fallingCoroutine;

    // public FloorBehaviour(float height, Transform playerPos)
    // {
    //     _height = height;
    //     _playerPos = playerPos;
    //     _tiles = new TileBehaviour[25];
    //     _availableTiles = new bool[25];
    // }
    // Start is called before the first frame update
    void Start()
    {
        _height = transform.position.y;
        _playerPos = GameManager.Singleton().playerPos;
        _tiles = new TileBehaviour[25];
        _availableTiles = new bool[25];
        transform.eulerAngles = new Vector3(0, 45, 0);
        int counter = 0;
        for (int i = -2; i < 3; i++)
        {
            for (int j = -2; j < 3; j++)
            {
                GameObject curTile = Instantiate(GameManager.Singleton().tileTemplates[Random.Range(0,GameManager.Singleton().tileTemplates.Length)].gameObject);
                Vector3 curPos = new Vector3(i * 2.1f, _height, j * 2.1f);
                curTile.transform.position = curPos;
                _tiles[counter] = curTile.GetComponent<TileBehaviour>();
                _availableTiles[counter++] = true;
                Vector3 eulerAngles = curTile.transform.eulerAngles;
                eulerAngles.y = Random.Range(0, 4)*90;
                curTile.transform.eulerAngles = eulerAngles;
                curTile.transform.parent = transform;
            }   
        }
        transform.eulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(_playerPos.position.y - _height) < 4f)
        {
            if (fallingCoroutine == null && GameManager.Singleton().ShouldPlatformFall)
            {
                fallingCoroutine = StartCoroutine(TilesFalling());
            }
        }
        else if(fallingCoroutine != null)
        {
            StopCoroutine(fallingCoroutine);
        }
    }

    private IEnumerator TilesFalling()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            List<int> availableTiles = new List<int>();
            for (int i = 0; i < _tiles.Length; i++)
            {
                if (_availableTiles[i])
                {
                    availableTiles.Add(i);
                }
            }
            int tileToShake = availableTiles[Random.Range(0,availableTiles.Count)];
            _tiles[tileToShake].ShakeAndFall();
            _availableTiles[tileToShake] = false;
        }
        
    }
}
