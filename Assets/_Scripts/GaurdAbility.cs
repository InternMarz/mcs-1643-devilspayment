using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaurdAbility : MonoBehaviour
{
    public int Ammo = 9;
    public GameObject ProjectilePrefab;
    public Transform PlayerCenter;
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
            GameObject proj = Instantiate(ProjectilePrefab, PlayerCenter.position, PlayerCenter.rotation);
            proj.transform.localScale = new Vector3(ProjScale, ProjScale, ProjScale);
        }
    }
}
