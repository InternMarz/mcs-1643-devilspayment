using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public int Ammo = 100;
    public GameObject ProjectilePrefab;
    public Transform AttackPoint;
    public float ProjScale = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject) ;// Placeholder to avoid empty Update method warning
        {
            Ammo--;
            GameObject proj = Instantiate(ProjectilePrefab, AttackPoint.position, AttackPoint.rotation);
            proj.transform.localScale = new Vector3(ProjScale, ProjScale, ProjScale);
        }
    }
}
