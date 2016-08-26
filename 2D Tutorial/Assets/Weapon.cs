using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public float FireRate = 0f;
    public float Damage = 10f;
    public LayerMask WhatToHit;

    public Transform BulletTrailPrefab;
    float TimeToSpawnEffect = 0f;
    public float EffectSpawnRate = 10f;

    private float TimeToFire = 0;
    private Transform FirePoint;



	// Use this for initialization
	void Awake () {
        FirePoint = transform.FindChild("FirePoint");
        if (FirePoint == null)
        {
            Debug.LogError("No firepoint?  What and why?!");
        }
	}

    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        // If weapon used is a single fire
        if (FireRate == 0)
        {
            // If a player's input is Fire1, shoot once
            if (Input.GetButtonDown("Fire1"))
                Shoot();
        }
        else
        {
            // If Fire1 is being held down
            if (Input.GetButton("Fire1") && Time.time > TimeToFire)
            {
                TimeToFire = Time.time + 1 / FireRate;
                Shoot();
            }
        }
	}

    void Shoot () {
        // This is converting screen coordinates to world coordinates to allow for a ray cast
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(FirePoint.position.x, FirePoint.position.y);

        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, WhatToHit);
        if (Time.time >= TimeToSpawnEffect)  // Prevent too many effects from spawning at once.  Good for high fire rates.
        {
            Effect();
            TimeToSpawnEffect = Time.time + 1 / EffectSpawnRate;
        }
        //Effect();

        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);

        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit" + hit.collider.name + " and did " + Damage + " damage.");
        }
    }

    void Effect()
    {
        Instantiate(BulletTrailPrefab, FirePoint.position, FirePoint.rotation);
    }
}
