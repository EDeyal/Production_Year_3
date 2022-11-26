using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller 
{
    // velocity - the direction and speed the object is going to move in, updates every 
    private float movementSpeed;

    private Vector3 velocity;
    public Vector3 Velocity { get => velocity;}

    public Controller(float givenSpeed)
    {
        movementSpeed = givenSpeed;
    }

    

}
