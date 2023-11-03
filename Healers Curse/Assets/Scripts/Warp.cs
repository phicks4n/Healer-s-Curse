using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public Transform warpTarget;
    public GameObject targetExit;
    public GameObject camera;
    public GameObject prevCamera;

    private bool justWarped = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.position = warpTarget.position;
        camera.SetActive(true);
        prevCamera.SetActive(false);
        justWarped = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (justWarped)
        {
            targetExit.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(EnableCollision(2));
            justWarped=false;
        }
        
    }
    private IEnumerator EnableCollision(float delay)
    {
        yield return new WaitForSeconds(delay);
        targetExit.GetComponent<Collider2D>().enabled = true;
    }

}
