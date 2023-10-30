using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    Camera cam;
    TrailRenderer trailRenderer;
    private Collider collider;
    public Vector3 direction { get; private set; }
    bool slicing;
    private float minSliceVeclociy = 0.01f;
    public float force { get; private set; }=5;

    void Awake()
    {
        cam = Camera.main;
        collider = GetComponent<Collider>();
        trailRenderer = GetComponent<TrailRenderer>();
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        StopSlicing();
    }
    void OnDisable()
    {
        StopSlicing();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (slicing)
        {
            ContinueSlicing();
        }

    }

    private void StartSlicing()
    {

        Vector3 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;
        direction = newPosition - transform.position;
        slicing = true;
        collider.enabled = true;
        trailRenderer.enabled = true;
    }
    private void StopSlicing()
    {
        slicing = false;
        collider.enabled = false;
        trailRenderer.enabled = false;
    }
    private void ContinueSlicing()
    {
        Vector3 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;
        direction = newPosition - transform.position;
        float velocity = newPosition.magnitude / Time.deltaTime;
        collider.enabled = velocity > minSliceVeclociy;
        transform.position = newPosition;
        force=5*(velocity/60);
        force=Mathf.Clamp(force,5,10);
        

    }
}
