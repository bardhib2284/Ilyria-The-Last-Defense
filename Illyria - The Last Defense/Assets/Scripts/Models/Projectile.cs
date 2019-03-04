using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public int speed;
    public Sprite Icon;
    public virtual void ChaseTarget(Transform t)
    {
        throw new System.Exception("METHOD NEEDS TO BE OVERRIDEN");
    }
}