using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBobScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0);
    }

    //adjust this to change speed
	float speed = 2f;
	//adjust this to change how high it goes
	float height = 0.1f;
 
	void Update() {
        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.position;
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, newY, pos.z);
	}
}
