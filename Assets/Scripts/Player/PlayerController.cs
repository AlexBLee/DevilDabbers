using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed = 7.5f;
    private float temps;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Camera mainCamera;
    public GunController gun;
    public GameOverScreen gmSc;
    public Canvas canvas;

	// Use this for initialization
	void Start ()
    {
        mainCamera = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 1.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveVelocity = moveInput * moveSpeed;
        rb.velocity = moveVelocity;

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        if(Input.GetMouseButtonDown(0))
        {
            gun.isFiring = true;
            EventManager em = ServiceLocator.GetService<EventManager>(); 
            em.TriggerEvent(ConstManager.EVENT_PLAYER_FIRE);
        }

        if(Input.GetMouseButtonDown(1))
        {
            gun.shotGunFiring = true;

        }

        if(Input.GetMouseButtonUp(0))
        {
            gun.isFiring = false;
        }

        if (Input.GetMouseButtonUp(1))
        {
            gun.shotGunFiring = false;
        }
    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == ConstManager.TAG_ENEMY)
        {
            Destroy(gameObject);
            Time.timeScale = 0.0f;
            gmSc.canvas.gameObject.SetActive(true);
            gmSc.Time();
            EventManager em = ServiceLocator.GetService<EventManager>();
            em.TriggerEvent(ConstManager.EVENT_PLAYER_DEATH);
        }
    }
}
