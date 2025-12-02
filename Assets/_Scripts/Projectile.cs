using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;
    public float Damage = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Hit object {collision.gameObject.transform.name}");

        //check if this is an ememy
        Enemy hitEnemy = collision.gameObject.GetComponent<Enemy>();
        if (hitEnemy != null)
        {
            hitEnemy.TakeDamage(Damage);
        }
        // if it is, notify it of damage
        Destroy(gameObject);
    }
}
