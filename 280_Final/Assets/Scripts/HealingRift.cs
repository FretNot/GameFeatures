using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealingRift : MonoBehaviour
{
    public Transform cam;
    public Transform HealingPoint;
    public GameObject Rift;

    public float riftCooldown;

    bool readyToPlace;

    private void Start()
    {
        readyToPlace = true;
    }

    public void OnPlaceRift(InputAction.CallbackContext context)
    {
        if (readyToPlace)
        {
            Place();
        }
    }

    private void Place()
    {
        readyToPlace = false;
        GameObject rift = Instantiate(Rift, HealingPoint.position, cam.rotation);
        Rigidbody riftRB = rift.GetComponent<Rigidbody>();
        Invoke(nameof(ResetRift), riftCooldown);
    }

    private void ResetRift()
    {
        readyToPlace = true;
    }
}
