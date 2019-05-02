using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    // initialize variables
    Rigidbody2D rigidBodyShip;
    Vector2 thrustDirection = new Vector2(1, 0);
    const float ThrustForce = 10;
    const float rotateDegrees = 45;

    // Screen wrapping support
    float shipColliderRadius;


    /// <summary>
    /// Initialization 
    /// </summary>
    
    // Start is called before the first frame update
    void Start()
    {
        // save rigid body and ship collider for efficency
        rigidBodyShip = GetComponent<Rigidbody2D>();
        shipColliderRadius = GetComponent<CircleCollider2D>().radius;
    }

    /// <summary>
    /// Rotation control.  Update is called once per frame.
    /// </summary>
    void Update()
    {
        // check for rotation input
        float rotationInput = Input.GetAxis("Rotate");
        if (rotationInput != 0)
        {
           
            //calculate and rotate
            float rotationAmount = rotateDegrees * Time.deltaTime;
            if (rotationAmount < 0)
            {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);

            //move in the chosen direction

            float ThrustDirection = transform.eulerAngles.z * Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(ThrustDirection);
            thrustDirection.y = Mathf.Sin(ThrustDirection);
        }
    }


    /// <summary>
    /// Thrust control.  Fied updated is called 50 times per second.
    /// </summary>
    void FixedUpdate()
    {
        //set input thrust variables

        float thrustInput = Input.GetAxis("Thrust");

        if (thrustInput != 0)
        {
            rigidBodyShip.AddForce(ThrustForce * thrustDirection,
                ForceMode2D.Force);
        }



    }

    /// <summary>
    /// Screen wrap.  Called when the game object becomes invisible to the camera.
    /// </summary>
    void OnBecameInvisible()
    {
        Vector2 position = transform.position;

        //check left, right, top, and bottom sides
        if (position.x + shipColliderRadius < ScreenUtils.ScreenLeft ||
            position.x - shipColliderRadius > ScreenUtils.ScreenRight)
        {
            position.x *= -1;
        }
        if (position.y - shipColliderRadius > ScreenUtils.ScreenTop ||
            position.y + shipColliderRadius < ScreenUtils.ScreenBottom)
        {
            position.y *= -1;
        }

        //move the ship
        transform.position = position;   
    }
}
