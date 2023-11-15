using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHeight : MonoBehaviour
{
    #region Inspector
    [SerializeField, Range(-3,10)] private float offset;
    [SerializeField] private Transform playerPos;
    #endregion

    
    #region MonoBehaviour
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = playerPos.position.y + offset;
        transform.position = pos;
    }   
    #endregion
    
    
}
