using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Random = UnityEngine.Random;

// using System.Collections;
    
public class TileBehaviour : MonoBehaviour
{
    enum TileState { Default, Shaking, Falling }
    [SerializeField] private float ShakeTime = 1;

    private TileState _tileState;
    private Vector2 startingPos;
    private Rigidbody _physics;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    private void Start()
    {
        // StartCoroutine(ShakeAndFallCoroutine());
        _physics = GetComponent<Rigidbody>();
        _physics.useGravity = false;
        startingPos.x = transform.position.x;
        startingPos.y = transform.position.z;
        _tileState = TileState.Default;
    }

    // Update is called once per frame
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

    private void Shake()
    {
        print("Shaking");
        Vector3 pos = transform.position;
        if (_tileState == TileState.Shaking)
        {
            pos.x = startingPos.x + Mathf.Sin(Time.time*100)* (randomBoolean() ? 1 : -1) * 0.05f;
            pos.z = startingPos.y + Mathf.Cos(Time.time*100) * (randomBoolean() ? 1 : -1) * 0.05f;
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

    bool randomBoolean ()
    {
        if (Random.value >= 0.5)
        {
            return true;
        }
        return false;
    }
    
}
