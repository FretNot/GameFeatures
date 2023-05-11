using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFPop : MonoBehaviour
{
    private Rigidbody rb;

    private bool targetHit;

    public GameObject Explosion;

    public Renderer rend;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (targetHit)
            return;
        else
            targetHit = true;

        rb.isKinematic = true;

        transform.SetParent(collision.transform);

        BlowUp();
        Despawn();

    }
    private void Update()
    {
        airBall();
    }
    private void BlowUp()
    {
        Instantiate(Explosion, transform.position, transform.rotation);
    }
    private void Despawn()
    {
        Destroy(gameObject);
    }
    IEnumerator airBall()
    {
        //need a requirement for not already regening
        if (targetHit != true)
        {
            yield return new WaitForSeconds(2f);
            rb.isKinematic = true;
            BlowUp();
            Despawn();
        }
    }
}
