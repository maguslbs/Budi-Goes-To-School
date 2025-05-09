//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Parallax : MonoBehaviour
//{
//    public Camera cam;
//    public Transform subject;

//    Vector2 startPosition;

//    float startZ;

//    Vector2 Travel => (Vector2)cam.transform.position - startPosition;

//    float distanceFromSubject => transform.position.z - subject.position.z;
//    float clippingPlane => (cam.transform.position.z + distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane);
//    float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;
//    Start is called before the first frame update
//    public void Start()
//    {
//        startPosition = transform.position;
//        startZ = transform.position.z;
//    }

//    Update is called once per frame
//    public void Update()
//    {
//        Vector2 newPos = startPosition + Travel * parallaxFactor;
//        transform.position = new Vector3(newPos.x, newPos.y, startZ);

//    }
//}

using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;

    private float oldPosition;

    void Start()
    {
        oldPosition = transform.position.x;
    }

    void Update()
    {
        if (transform.position.x != oldPosition)
        {
            if (onCameraTranslate != null)
            {
                float delta = oldPosition - transform.position.x;
                onCameraTranslate(delta);
            }

            oldPosition = transform.position.x;
        }
    }
}
