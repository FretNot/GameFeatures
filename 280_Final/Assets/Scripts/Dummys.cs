using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummys : MonoBehaviour
{
    public float Health = 250;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
        {
        if (other.transform.tag == "GExplosion")
        {
            Debug.Log("enemy collided with fusion");
            Damage(150);
        }
        if (other.transform.tag == "CF")
        {
            Debug.Log("enemy collided with celestial fire");
            Damage(100);
        }
    }
    public void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Damage(float DamageAmout)
    {
        Health -= DamageAmout;
    }
}
