using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowGrenade : MonoBehaviour
{
    public Transform cam;
    public Transform attackPoint;
    public GameObject grenade;

    public int totalThrows;
    public float throwCooldown;

    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (readyToThrow && totalThrows > 0)
        {
            Throw();
        }
    }

    private void Throw()
    {
        readyToThrow = false;
        GameObject projectile = Instantiate(grenade, attackPoint.position, cam.rotation);
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;
        projectileRB.AddForce(forceToAdd, ForceMode.Impulse);
        totalThrows--;
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}

