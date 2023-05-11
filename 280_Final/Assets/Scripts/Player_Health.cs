using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public float Health = 100;
    public float Shield = 0;
    private float maxHP = 100;
    private float minHP = 0;
    private float leftOver;
    private float ableftOver;
    private float maxOverShield = 50;
    public bool Regain = false;
    public bool Regen = false;
    public bool noDamageTaken = true;
    public bool riftActive = false;
    public float totalHealth;
    public Text HealthText;

    IEnumerator ActiveRegainHealth;

    public void start()
    {
        SetCountText();
    }
    public void SetCountText()
    {
        HealthText.text = "Health: " + totalHealth.ToString();
    }

    void Update()
    {
        //StartCoroutine(RiftRegen());
        //StartCoroutine(RegainHealth());
        totalHealth = Health + Shield;
        SetCountText();
        if (riftActive == false)
        {
            Shield = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Debug.Log("collided with enemy");
            TakeDamage(20);
            if (ActiveRegainHealth != null)
            {
                StopCoroutine(ActiveRegainHealth);
            }
            Regain = true;
            noDamageTaken = false;
            ActiveRegainHealth = RegainHealth();
            StartCoroutine(ActiveRegainHealth);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Rift")
        {
            riftActive = true;
            Invoke("RiftDeactivate", 15f);
            Debug.Log("standing in rift");
            Regen = true;
            StartCoroutine(RiftRegen());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Rift")
        {
            Debug.Log("leaving rift");
            Regen = false;
            Shield = 0;
        }
    }

    public void TakeDamage(float DamageAmount)
    {
        if (Shield > 0)
        {

            Shield -= DamageAmount;
            leftOver = Shield - DamageAmount;
            ableftOver = Mathf.Abs(leftOver);
            if (Health > minHP)
            {
                Health -= ableftOver;
            }
        }
        if (Shield <= 0)
        {
            Shield = 0;
            if (Health > minHP)
            {
                Health -= DamageAmount;
            }
        }
        if (Health < 0)
        {
            Health = minHP;
        }
        SetCountText();
    }
    IEnumerator RegainHealth()
    {
        //need a requirement for not already regening

        yield return new WaitForSeconds(5f);
        noDamageTaken = true;
        Regain = true;
        while (noDamageTaken && Regain != false && Health < maxHP)
        {
                Health += .1f;
                yield return new WaitForSeconds(.01f);
        }
        if (Health > maxHP)
        {
            Health = maxHP;
        }
        Regain = false;
    }
    IEnumerator RiftRegen()
    {
        while (Regen != false && Shield < maxOverShield)
        {
            if (Health < maxHP)
            {
                Health += .1f;
                yield return new WaitForSeconds(.01f);
            }
            if (Health >= maxHP)
            {
                Shield += 1f;
                yield return new WaitForSeconds(.01f);
            }
        }
        if (Shield > maxOverShield)
        {
            Shield = maxOverShield;
        }
        if (Health > maxHP)
        {
            Health = maxHP;
        }
    }
    public void RiftDeactivate()
    {
        riftActive = false;
    }
}
