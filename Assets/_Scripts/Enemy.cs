using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP = 25;
    public GameObject ExplosionPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Instantiate(ExplosionPrefab, transform.position + new Vector3(0, .2f, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
