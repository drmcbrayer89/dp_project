using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour {

    public int rotation_offset = 90;

	// Update is called once per frame
	void Update () {
        // Subtracting the position of the player from the mouse position.
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;      
        difference.Normalize();     // Normalizing the vector, meaning the sum of the vector components is 1.

        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;     // Find the angle in degrees using the Tangent.
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + rotation_offset);


	}
}
