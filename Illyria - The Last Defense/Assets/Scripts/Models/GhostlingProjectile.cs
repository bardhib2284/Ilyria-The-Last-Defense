using UnityEngine;
using System.Collections;

public class GhostlingProjectile : Projectile
{
    public Transform t;

    public override void ChaseTarget(Transform t)
    {
        this.t = t;
    }

    private void Start()
    {
        this.transform.localScale = transform.localScale * 2f;
    }
    private void Update()
    {
        if(t != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, t.transform.position, speed * Time.deltaTime);
            if(Vector3.Distance(this.transform.position,t.transform.position) < 0.5f)
            {
                SendMessageUpwards("DealDamageFromSpecial");
                SendMessageUpwards("GoBack");
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
