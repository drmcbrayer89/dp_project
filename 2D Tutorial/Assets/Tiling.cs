using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Tiling : MonoBehaviour {

    public int offsetX = 2;                 //The offset so we don't get any weird errors

    //These are used for checking if we need to instantiate
    public bool hasARightBuddy = false;    
    public bool hasALeftBuddy = false;

    //Use if the object is not tileable
    public bool reverse_scale = false;

    //Width of our element
    private float sprite_width = 0f;
    private Camera cam;
    private Transform perf_transform;

    void Awake()
    {
        cam = Camera.main;
        perf_transform = transform;
    }

	// Use this for initialization
	void Start () {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        sprite_width = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
        //Does it still need buddies?  If not, do nothing
	    if (hasALeftBuddy == false || hasARightBuddy == false)
        {
            //Calculate the cameras extend (half the width) of what the camera can see in world coordinates
            float cam_horizontal_extend = cam.orthographicSize * Screen.width / Screen.height;

            //Calculate the x position where the camera can see the edge of the sprite
            float edge_visible_pos_right = (perf_transform.position.x + sprite_width / 2) - cam_horizontal_extend;
            float edge_visible_pos_left = (perf_transform.position.x - sprite_width / 2) + cam_horizontal_extend;

            //Checking if we can see the edge of the element and then calling MakeNewBuddy() if we can
            if (cam.transform.position.x >= edge_visible_pos_right - offsetX && hasARightBuddy == false)
            {
                MakeNewBuddy(1);
                hasARightBuddy = true;
            }
            else if (cam.transform.position.x <= edge_visible_pos_left + offsetX && hasALeftBuddy == false)
            {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
	}

    //Creates a buddy on the side required (direction will be 1 or -1)
    void MakeNewBuddy(int direction)
    {
        //Calculating the new position for our new buddy
        Vector3 new_position = new Vector3(perf_transform.position.x + sprite_width * direction, perf_transform.position.y, perf_transform.position.z);
        //Instantiating our new buddy and storing him in a variable
        Transform new_buddy = Instantiate(perf_transform, new_position, perf_transform.rotation) as Transform;

        //If not tileable, reverse the x size of object to make to get rid of ugly seams
        if (reverse_scale == true)
        {
            new_buddy.localScale = new Vector3(-(new_buddy.localScale.x), new_buddy.localScale.y, new_buddy.localScale.z);
        }

        new_buddy.parent = perf_transform.parent;

        if (direction > 0)
        {
            new_buddy.GetComponent<Tiling>().hasALeftBuddy = true;
        }
        else if (direction < 0)
        {
            new_buddy.GetComponent<Tiling>().hasARightBuddy = true;
        }
    }
}
