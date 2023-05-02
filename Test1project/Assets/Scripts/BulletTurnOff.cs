using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurnOff : MonoBehaviour
{
    [SerializeField] private float _shotRangeTime;

    private void Start()
    {
        SetShotRangeCarutine();
    }

    private IEnumerator ShotRangeCarutine()
    {
        yield return new WaitForSeconds(_shotRangeTime);
        BulletSwitchOff();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            BulletSwitchOff();
        }
    }

    private void BulletSwitchOff()
    {
        gameObject.SetActive(false);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void SetShotRangeCarutine()
    {
        StartCoroutine(ShotRangeCarutine());
    }
}
