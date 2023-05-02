using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderMovement : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private float _speed;
    [SerializeField] private float _waitTime;

    private Transform _currentTargetPosition;
    private bool isMove;

    public bool IsMove { set { isMove = value; } }

    private void Start()
    {
        _currentTargetPosition = _pointA;
        isMove = true;
    }
    private void Update()
    {
        if (isMove)
        {
            transform.position =
            Vector3.MoveTowards(transform.position,
            new Vector3(_currentTargetPosition.position.x, transform.position.y, _currentTargetPosition.position.z), _speed * Time.deltaTime);
        }
        

        if (transform.position == _currentTargetPosition.position)
        {
            StartCoroutine(Wait(_waitTime));

            if (_currentTargetPosition == _pointA)
                _currentTargetPosition = _pointB;
            else
                _currentTargetPosition = _pointA;
        }
    }

    private IEnumerator Wait(float waitTime)
    {
        isMove = false;
        yield return new WaitForSeconds(waitTime);
        isMove = true;
    }
}
