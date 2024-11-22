using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    Vector2 inputVec = new Vector2(0, 0);
    Rigidbody rb;
    float rotationAmountZ;
    float rotationAmountX;

    [Header("Properties")]
    public float maxZRotation;
    public float maxXRotation;
    public float slerpFactor;

    public void UpdateInputs()
    {
        if (Input.GetKey(KeyCode.D)) 
        {
            inputVec.Set(inputVec.x, 1);
        }
        else if(Input.GetKey(KeyCode.A)) 
        {
            inputVec.Set(inputVec.x, -1);
        }
        else
        {
            inputVec.Set(inputVec.x, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            inputVec.Set(1, inputVec.y);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            inputVec.Set(-1, inputVec.y);
        }
        else
        {
            inputVec.Set(0, inputVec.y);
        }
    }

    public void UpdatePositions()
    {
        rotationAmountZ = maxZRotation * inputVec.y;
        rotationAmountX = maxXRotation * inputVec.x;

        Quaternion targetRotation = rb.rotation;

        targetRotation = Quaternion.AngleAxis(rotationAmountZ, new Vector3(0, 0, 1)) * Quaternion.AngleAxis(-rotationAmountX, new Vector3(1, 0, 0));

        Quaternion slerpedQuat = Quaternion.Slerp(rb.rotation, targetRotation, 1 - Mathf.Pow(slerpFactor, Time.deltaTime));

        rb.MoveRotation(slerpedQuat);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        UpdatePositions();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInputs();
    }
}
