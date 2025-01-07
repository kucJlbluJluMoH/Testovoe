using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
	[SerializeField] private float _moveSpeed = 5f;
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");  

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        if (moveDirection.sqrMagnitude > 1f)
        {
	        moveDirection = moveDirection.normalized;
        }

        transform.Translate(moveDirection * _moveSpeed * Time.deltaTime);
    }
}
