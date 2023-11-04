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

    public FloorBehaviour(float height, Transform playerPos)
    {
        _height = height;
        _playerPos = playerPos;
        _tiles = new TileBehaviour[25];
        _availableTiles = new bool[25];
    }
    // Start is called before the first frame update
    void Start()
    {
        int counter = 0;
        for (int i = -2; i < 3; i++)
        {
            for (int j = -2; j < 3; j++)
            {
                GameObject curTile = Instantiate(GameManager.Singleton().tileTemplates[Random.Range(0,GameManager.Singleton().tileTemplates.Length)].gameObject);
                Vector3 curPos = new Vector3(i * 2.1f, _height, j * 2.1f);
                _tiles[counter++] = curTile.GetComponent<TileBehaviour>();
                _availableTiles[counter++] = true;
                curTile.transform.position = curPos;
                Vector3 eulerAngles = curTile.transform.eulerAngles;
                eulerAngles.y = Random.Range(0, 4)*90;
                curTile.transform.eulerAngles = eulerAngles;
            }   
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(_playerPos.position.y - _height) < 4f && fallingCoroutine == null)
        {
            fallingCoroutine = StartCoroutine(TilesFalling());
        }
        else
        {
            StopCoroutine(fallingCoroutine);
        }
    }

    private IEnumerator TilesFalling()
    {
        for (int i = 0; i < _tiles.Length; i++)
        {
            if (_availableTiles[i])
            {
                _availableTiles[i] = false;
                _tiles[i].ShakeAndFall();
            }
        }

        yield return null;
    }
}
