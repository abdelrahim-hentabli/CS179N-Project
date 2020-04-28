using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
	private PlatformEffector2D effector;
	private float time;
	public float waitTime;

    // Start is called before the first frame update
    void Start(){
    	time = waitTime;
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update(){
        
    	if(Input.GetKeyUp("s")){
    		time = waitTime;
    	}

        if(Input.GetKey("s")){
        	if(time <= 0){
        		effector.rotationalOffset = 180f;
        		time = waitTime;
        	} else {
        		time -= Time.deltaTime;
        	}
        }

        if(Input.GetButton("Jump")){
        	effector.rotationalOffset = 0;
        }
    }
}
