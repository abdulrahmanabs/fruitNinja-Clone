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
    private float minSliceVelocity = 1;
    public float force { get; private set; } = 5;

    void Awake()
    {
        cam = Camera.main;
        collider = GetComponent<Collider>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void OnEnable()
    {
        StopSlicing();
    }

    void OnDisable()
    {
        StopSlicing();
    }

    void Update()
    {

        if (GameManager.instance.gameState != GameManager.GameState.Play)
            return;

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
        transform.position = newPosition;
        slicing = true;
        collider.enabled = true;
        trailRenderer.enabled = true;
        trailRenderer.Clear();
    }

    private void StopSlicing()
    {
        slicing = false;
        collider.enabled = false;
        trailRenderer.Clear();
        trailRenderer.enabled = false;
        direction = Vector3.zero;
    }

    private void ContinueSlicing()
    {
        Vector3 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        // Check if the new position is significantly different from the current position
        if (newPosition != transform.position)
        {
            direction = newPosition - transform.position;
            float velocity = direction.magnitude / Time.deltaTime;
            collider.enabled = velocity > minSliceVelocity;
            transform.position = newPosition;
        }
    }
}
