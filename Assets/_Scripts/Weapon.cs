using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Ammo = 100;
    public GameObject ProjectilePrefab;
    public Transform MuzzlePoint;
    public float ProjScale = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Ammo > 0)
        {
            Ammo--;
            GameObject proj = Instantiate(ProjectilePrefab, MuzzlePoint.position, MuzzlePoint.rotation);
            proj.transform.localScale = new Vector3(ProjScale, ProjScale, ProjScale);
        }
    }
}
