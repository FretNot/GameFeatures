using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealingRift : MonoBehaviour
{
    public Transform cam;
    public Transform HealingPoint;
    public GameObject Rift;
    public bool placingRift;

    public float riftCooldown;

    bool readyToPlace;

    private void Start()
    {
        readyToPlace = true;
    }

    public void OnPlaceRift(InputAction.CallbackContext context)
    {
        if (readyToPlace && placingRift != true)
        {
            Invoke("Place", 1.5f);
            placingRift = true;
            Debug.Log(placingRift);
            Invoke("finishplace", 2.5f);
        }
    }

    private void Place()
    {
        readyToPlace = false;
        GameObject rift = Instantiate(Rift, HealingPoint.position, cam.rotation);
        Rigidbody riftRB = rift.GetComponent<Rigidbody>();
        Invoke(nameof(ResetRift), riftCooldown);
    }
    public void finishplace()
    {
        placingRift = false;
    }
    private void ResetRift()
    {
        readyToPlace = true;
    }
}
