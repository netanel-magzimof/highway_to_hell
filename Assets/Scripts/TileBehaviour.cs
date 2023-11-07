using System;
using System.Collections;
using System.Collections.Generic;
using Magzimof;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Random = UnityEngine.Random;

// using System.Collections;
    
public class TileBehaviour : MonoBehaviour
{
    
    #region Inspector
    
    [Header("Shaking Stats")]
    [SerializeField] private float ShakeTime = 1;
    
    
    #endregion
    
    
    #region Fields
        
    private TileState _tileState;
    private Vector2 startingPos;
    private Rigidbody _physics;
    
    #endregion
    
    
    #region MonoBehaviour
    
    private void Start()
    {
        // StartCoroutine(ShakeAndFallCoroutine());
        _physics = GetComponent<Rigidbody>();
        _physics.useGravity = false;
        startingPos.x = transform.position.x;
        startingPos.y = transform.position.z;
        _tileState = TileState.Default;
    }

    void Update()
    {
        switch (_tileState)
        {
            case TileState.Shaking:
                Shake();
                break;
            case TileState.Falling:
                StartCoroutine(Fall());
                break;
        }
    }
        
    #endregion
    
    
    #region Methods
              
    private void Shake()
    {
        Vector3 pos = transform.position;
        if (_tileState == TileState.Shaking)
        {
            pos.x = startingPos.x + Mathf.Sin(Time.time*100)* (Util.randomBoolean() ? 1 : -1) * 0.05f;
            pos.z = startingPos.y + Mathf.Cos(Time.time*100) * (Util.randomBoolean() ? 1 : -1) * 0.05f;
        }
        transform.position = pos;
    }

    public void ShakeAndFall()
    {
        StartCoroutine(ShakeAndFallCoroutine());
    }

    private IEnumerator ShakeAndFallCoroutine()
    {
        _tileState = TileState.Shaking;
        yield return new WaitForSeconds(ShakeTime);
        _tileState = TileState.Falling;
    }

    private IEnumerator Fall()
    {
        _physics.useGravity = true;
        _physics.isKinematic = false;
        yield return new WaitForSeconds(0.7f);
        gameObject.SetActive(false);
    }
    
    #endregion
    
    enum TileState { Default, Shaking, Falling }
    
    
}
