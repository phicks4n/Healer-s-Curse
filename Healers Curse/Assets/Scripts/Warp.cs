using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public Transform warpTarget;
    public GameObject camera;
    public GameObject prevCamera;

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.position = warpTarget.position;
        camera.SetActive(true);
        prevCamera.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
