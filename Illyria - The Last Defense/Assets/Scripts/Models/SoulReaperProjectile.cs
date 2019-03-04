using UnityEngine;
using System.Collections;

public class SoulReaperProjectile : Projectile
{
    public Transform t;

    public override void ChaseTarget(Transform t)
    {
        this.t = t;
    }

    private void Update()
    {
        if (t != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, t.transform.position + (Vector3.right * 1.5f), speed * Time.deltaTime);
            if (Vector3.Distance(this.transform.position, (t.transform.position + Vector3.up * 2.5f)) < 2f)
            {
                SendMessageUpwards("DealDamageFromSpecial", t.GetComponent<Character>());
                Destroy(gameObject);
            }
        }
    }
}