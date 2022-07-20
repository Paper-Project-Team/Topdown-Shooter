using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 20f;
    public float rotSpeed = 10f;
    
    
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 30f;
        }
        else
        {
            speed = 20f;
        }

        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDirection = direction;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
        
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);
        float angle = AngleBetweenPoints(transform.position, worldPosition);
        transform.rotation = Quaternion.Euler(new Vector3(0f, -angle - 90, 0f));

    }

    float AngleBetweenPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.z - b.z, a.x - b.x) * Mathf.Rad2Deg;
    }
}
