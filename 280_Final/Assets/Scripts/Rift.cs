using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rift : MonoBehaviour
{
    private void Update()
    {
        Invoke("Despawn", 15f);
    }



    private void Despawn()
    {
        Destroy(gameObject);
    }
}
