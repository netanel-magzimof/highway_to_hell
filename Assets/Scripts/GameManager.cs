using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    public GameObject[] tileTemplates; 
    // Start is called before the first frame update
    void Start()
    {
        CreateFloorAtHeight(-1);
        CreateFloorAtHeight(9);
        CreateFloorAtHeight(19);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateFloorAtHeight(float height)
    {
        for (int i = -2; i < 3; i++)
        {
            for (int j = -2; j < 3; j++)
            {
                GameObject curTile = Instantiate(tileTemplates[Random.Range(0,tileTemplates.Length)]);
                Vector3 curPos = new Vector3(i * 2.1f, height, j * 2.1f);
                curTile.transform.position = curPos;
                Vector3 eulerAngles = curTile.transform.eulerAngles;
                eulerAngles.y = Random.Range(0, 4)*90;
                curTile.transform.eulerAngles = eulerAngles;

            } 
        }
    }
}
