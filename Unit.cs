using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    public virtual void ReceiveDamage()
    {
        Die();
    }

    // Update is called once per frame
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
