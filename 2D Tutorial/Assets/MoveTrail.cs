using UnityEngine;
using System.Collections;

public class MoveTrail : MonoBehaviour {

    public int MoveSpeed = 230;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
        Destroy(gameObject, 1);
	}
}
