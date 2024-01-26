using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    private int currentHearts;
    public int maxHEARTS;
    public Animator animator;

    private void Start()
    {
        currentHearts = maxHEARTS;
    }

    // Update is called once per frame
    public void takeDamage(int damage)
    {
        if (damage >= currentHearts)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            animator.SetTrigger("TakeDamage");
        }

        currentHearts -= damage;
    }

    public void heal(int heal)
    {
        if (heal > maxHEARTS)
        {
            currentHearts = maxHEARTS;
            return;
        }
        else
        {
            animator.SetTrigger("Heal");
        }

        currentHearts += heal;

    }
}
