using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject topPlayer;
    public GameObject bottomPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var dy = topPlayer.transform.position.y - bottomPlayer.transform.position.y;
        
	    var y = bottomPlayer.transform.position.y + dy / 2;

	    transform.position = new Vector3(topPlayer.transform.position.x, y, transform.position.z);
	}
}
