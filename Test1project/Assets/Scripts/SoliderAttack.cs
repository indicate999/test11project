using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoliderMovement))]
public class SoliderAttack : MonoBehaviour
{
    [SerializeField] private PoolBullets _poolBullets;
    [SerializeField] private float _bulletCountDown;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _attackRadius;   

    private CapsuleCollider _capsuleCollider;
    private SoliderMovement _soliderMovement;

    private bool isCollision = false;

    private void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _soliderMovement = GetComponent<SoliderMovement>();
        SetAttackRadiusInCollider();
    }

    private void SetAttackRadiusInCollider()
    {
        _capsuleCollider.radius = _attackRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isCollision)
        {
            _soliderMovement.IsMove = false;
            isCollision = true;
            StartCoroutine(ShotCarutine(other.gameObject.transform));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && isCollision)
        {
            _soliderMovement.IsMove = true;
            isCollision = false;
        }
    }

    private IEnumerator ShotCarutine(Transform target)
    {
        while (isCollision)
        {
            MakeShot(target);
            yield return new WaitForSeconds(_bulletCountDown);
        }
    }

    private void MakeShot(Transform target)
    {
        GameObject bullet = _poolBullets.GetFreeBullet();
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
        bullet.gameObject.GetComponent<BulletTurnOff>().SetShotRangeCarutine();
        
        var heading = target.position - bullet.transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        bullet.GetComponent<Rigidbody>().AddForce(direction * _bulletSpeed);
    }
}
