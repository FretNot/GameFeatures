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
    private float maxOverShield = 100;
    bool Regain = false;
    bool Regen = false;
    private float currentHealth;
    public float totalHealth;
    public Text HealthText;

    void start()
    {
        SetCountText();
    }
    public void SetCountText()
    {
        HealthText.text = "Health: " + totalHealth.ToString();
    }

    void Update()
    {
        StartCoroutine(RegainHealth());
        StartCoroutine(RiftRegen());
        totalHealth = Health + Shield;
        SetCountText();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Debug.Log("collided with enemy");
            TakeDamage(20);
            Regain = true;
            StartCoroutine(RegainHealth());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Rift")
        {
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
        if (Regain == true)
        {
            yield return new WaitForSeconds(8f);
            if (Health < maxHP)
            {
                Health += .1f;
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return null;
            }
            if (Health > maxHP)
            {
                Health = maxHP;
            }
        }
        Regain = false;
    }
    IEnumerator RiftRegen()
    {
        if (Regen == true)
        {
            if (Shield < maxOverShield)
            {
                Shield += .3f;
                yield return new WaitForSeconds(15f);
            }
            else
            {
                yield return null;
            }
            if (Shield > maxOverShield)
            {
                Shield = maxOverShield;
            }
            Regen = false;
        }
    }
}
