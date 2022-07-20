using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject player; // Self-explanatory, the player's GameObject
    public LineRenderer lr; // Uses LineRenderer to show bullet tracer
    [SerializeField] private float damage = 10f; // How much damage the bullet does (Will configure)
    [SerializeField] private float shootCooldown = 0.2f; // Will re-implement later if I decide to not be lazy
    [SerializeField] private float reloadTime = 1f; // Time it takes to reload (not time between shots)
    [SerializeField] private int maxAmmo = 8; // Maximum ammo the gun has
    private int currentAmmo = 0; // The amount of ammo the gun currently has
    private float nextAction = 0f; // Timestamp of the next available time do an action


    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        float headingAngle = (transform.eulerAngles.y - 90) * Mathf.Deg2Rad * -1;
        float x = Mathf.Cos(headingAngle);
        float z = Mathf.Sin(headingAngle);

        RaycastHit hit;
        Vector3 aimPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z); // This is where the player shoots from
        if (Physics.Raycast(aimPosition, this.transform.forward, out hit, 20f)) // Checks if it hits a collider within 10 units
        {
            lr.SetPosition(0, aimPosition);
            lr.SetPosition(1, hit.point);
        }
        else
        {
            lr.SetPosition(0, aimPosition);
            lr.SetPosition(1, new Vector3(aimPosition.x + x * 20f, aimPosition.y, aimPosition.z + z * 20f));
        }
        

        if(Input.GetButtonDown("Fire1") && currentAmmo > 0 && Time.time >= nextAction) // Checks if left mouse button is clicked, shot is available, and has ammo
        {
            Debug.Log("Bang");

            nextAction = Time.time + shootCooldown;
            currentAmmo -= 1;            
        }

        if(Input.GetButtonDown("Reload") && Time.time >= nextAction && currentAmmo < maxAmmo)
        {
            Debug.Log("Reloading");
            nextAction = Time.time + reloadTime;
            currentAmmo = maxAmmo;
        }
    }
}
