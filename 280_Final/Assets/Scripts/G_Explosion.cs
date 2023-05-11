using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Explosion : MonoBehaviour
{
    private void Update()
    {
        Invoke("Despawn", .5f);
    }



    private void Despawn()
    {
        Destroy(gameObject);
    }
}
