 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] backgrounds;         //Array of all the background elements to be parallax
    private float[] parallaxScales;        //The proportion of the camera mocement to move the Bakcgound by
    public float smoothing = 1f;            //How smooth the parallaw is going to be

    private Transform cam;                  //reference to the main camera transform
    private Vector3 previousCamPosition;    //Store the position of the camera in the previous frame 

    //Is called before Start().

    void Awake()
    {
        //set up camera refernece
        cam = Camera.main.transform;

    }
    void Start()
    {
        //The previous had the current freme's camera position 
        previousCamPosition = cam.position;

        parallaxScales = new float[backgrounds.Length];

        //assining corresping parallax Scale
        for (int i = 0; i < backgrounds.Length; i++){
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
       // for each Background
            for(int i=0; i< backgrounds.Length; i++)
            {
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];

            float backgroundtargetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundtargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
            }
        //set the previous camPos to the camera position at the end of the frame 

        previousCamPosition = cam.position;
    }
}
