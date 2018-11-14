using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class Character : MonoBehaviour {

    [SerializeField] private float speed = 10;
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private float tilt = 5;
    [SerializeField] private Boundary boundary;

    [SerializeField] private GameObject shot;
    [SerializeField] private Transform shotSpawn;
    [SerializeField] private float fireRate;

    private float nextFire;
    private Vector3 lastMovement;
    private Transform mainCamera;
    Rigidbody myBody;
    private Vector2 touchOrigin = -Vector2.one;

    private void Start()
    {
        myBody = GetComponent<Rigidbody>();
        GameObject mainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCameraObject != null)
            mainCamera = mainCameraObject.GetComponent<Transform>();
        if (mainCamera == null)
            Debug.LogError("Cannot find 'GameController' script");
    }

    void MoveSpaceShip(Vector3 movement)
    {
        GetComponent<Rigidbody>().velocity = movement * speed;

        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(GetComponent<Rigidbody>().position.y, boundary.yMin, boundary.yMax),
            0.0f
        );
        mainCamera.position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(GetComponent<Rigidbody>().position.y, boundary.yMin, boundary.yMax) + 2,
            -4
        );
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(GetComponent<Rigidbody>().velocity.y * -tilt / speed * rotationSpeed, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt / speed * rotationSpeed);
    }

    public void Move(Vector3 movement, bool hasShoted)
    {
        InitiateMoveSmoothly(ref movement);
        InitiateGoBackSmoothly(ref movement);
        MoveSpaceShip(movement);
        if (hasShoted == true)
            CreateShot();
    }

    void CreateShot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn);
        }
    }

    void InitiateMoveSmoothly(ref Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            float x = lastMovement.x, y = lastMovement.y;
            if (movement.x - x < -0.2f)
                movement.x = x - 0.2f;
            else if (movement.x - x > 0.2f)
                movement.x = x + 0.2f;
            if (movement.y - y < -0.2f)
                movement.y = y - 0.2f;
            else if (movement.y - y > 0.2f)
                movement.y = y + 0.2f;
        }
    }

    void InitiateGoBackSmoothly(ref Vector3 movement)
    {
        if (movement == Vector3.zero && lastMovement != Vector3.zero)
        {
            float x = 0, y = 0;
            if (lastMovement.x < -0.1f)
                x = 0.1f;
            else if (lastMovement.x > 0.1f)
                x = -0.1f;
            if (lastMovement.y < -0.1f)
                y = 0.1f;
            else if (lastMovement.y > 0.1f)
                y = -0.1f;
            movement = lastMovement + new Vector3(x, y, 0f);
        }
        lastMovement = movement;
    }
}
