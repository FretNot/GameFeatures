using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CelestialFire : MonoBehaviour
{
    public Transform cam;
    public Transform attackPoint;
    public GameObject celestialFire;

    public int totalThrows;
    public float throwCooldown;

    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
    }

    public void OnMelee(InputAction.CallbackContext context)
    {
        if (readyToThrow && totalThrows > 0)
        {
            Throw();
            Invoke("Throw", .25f);
            Invoke("Throw", .5f);
        }
    }

    private void Throw()
    {
        readyToThrow = false;

        GameObject projectile = Instantiate(celestialFire, attackPoint.position, cam.rotation);

        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

        Vector3 throwDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 5000f))
        {
            throwDirection = (hit.point - attackPoint.position).normalized;
        }

        Vector3 forceToAdd = throwDirection * throwForce + transform.up * throwUpwardForce;

        projectileRB.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        Invoke(nameof(ResetThrow), throwCooldown);

    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
