using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    public float Speed;
    public Movement(float speed)
    {
        Speed = speed;
    }

   public Vector2 Calculate2D(float x, float y, float deltaTime)
    {
        float horizontal = x * Speed * deltaTime;
        float vertical = y * Speed * deltaTime;


        return new Vector2(horizontal, vertical);
    }

    public Vector3 Calculate(Vector2 direction, float deltaTime)
    {
        return Calculate(direction.x, direction.y, deltaTime);
    }
    public Vector3 Calculate(float x, float z, float deltaTime)
    {
        float horizontal = x * Speed * deltaTime;
        //float vertical = y * Speed * deltaTime;
        float depth = z * Speed * deltaTime;


        return new Vector3(horizontal,0, depth).normalized;
    }
}
