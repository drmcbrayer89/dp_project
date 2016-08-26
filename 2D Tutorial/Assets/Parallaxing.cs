using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

    public Transform[] backgrounds;     //Array of all of back- and foregrounds to be parallaxed
    private float[] parallax_scales;    //The proportion of the camera's movement to move the backgrounds by
    public float smoothing = 1f;        //How smooth the parallax is going to be.  Set above 0

    private Transform cam;              //Reference to the main camera's transform    
    private Vector3 previous_cam_pos;     //Position of camera in previous frame

    //Called before Start().  Great for references.
    void Awake()
    {
        //Set up camera reference
        cam = Camera.main.transform;
    }

	// Use this for initialization
	void Start () {
        //Previous frame had current frame's camera position
        previous_cam_pos = cam.position;

        parallax_scales = new float[backgrounds.Length];

        //Assigning corresponding parallax_scales
        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallax_scales[i] = -(backgrounds[i].position.z);
        }
	}
	
	// Update is called once per frame
	void Update () {

        //For each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            //The prallax is the opposite of the camera movement because the previous framne multiplied by the scale
            float parallax = (previous_cam_pos.x - cam.position.x) * parallax_scales[i];

            //Set a target x position which is the current position + the parallax
            float background_target_pos_X = backgrounds[i].position.x + parallax;

            //Create a target position which is the background's current with its target x position
            Vector3 background_target_pos = new Vector3(background_target_pos_X, backgrounds[i].position.y, backgrounds[i].position.z);

            //Fade between current position and the target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, background_target_pos, smoothing * Time.deltaTime);
        }

        //Set the previous_cam_pos to the camera's position and the end of the frame
        previous_cam_pos = cam.position;
	}
}
