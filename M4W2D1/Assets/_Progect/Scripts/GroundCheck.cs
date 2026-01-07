using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float _distanceToGround = 1f;

    public bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down , _distanceToGround))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

} 
