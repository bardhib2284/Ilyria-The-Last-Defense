using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ghostling : Character
{
    public bool attackOrdered;
    public bool goBackOrdered;
    [Space]
    [Header("HERO SKILLS")]
    public string Skill_1_Description = "ACITVE: Shoots a deadly poision to the target that deals 200% of the current damage and has a 30% chance to stun the enemy for 1 round";
    public string Skill_2_Description = "PASSIVE: Increases self ATTACK(20%), HP(15%),ARMOR(10%)";
    public string Skill_3_Description = "PASSIVE: Everytime Soul Reaper hits an enemy it reaps his enemies soul and feeding his own causing an increase in his current HP and Attack by 3% for 3 rounds(STACKABLE)";
    public string Skill_4_Description = "PASSIVE: Everytime Soul Reaper gets damaged he heals him self 50% of Soul Reapers CURRENT damage";
    public int critChance;
    public GameObject specialAbilityProjectile;

    public Ghostling(CharacterJson characterJson) : base(characterJson)
    {

    }

    public override void Attack()
    {
        attackOrdered = true;
    }

    public IEnumerator AttackEnum()
    {
        Animator.SetTrigger("Attack");
        yield return new WaitWhile(() => goBackOrdered == false);
    }

    private new void Start()
    {
        Dead = false;
        base.Start();
    }
    private void GoBack()
    {
        goBackOrdered = true;
    }

    private void Update()
    {
        if (attackOrdered)
        {
            if(enemyTeam.Count > 0)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, enemyTeam[0].transform.position, 20f * Time.deltaTime);
                if (Vector3.Distance(this.transform.position, enemyTeam[0].transform.position) < 1.3f)
                {
                    attackOrdered = false;
                    StartCoroutine(AttackEnum());
                }
            }
        }
        if (goBackOrdered)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.parent.position, 20f * Time.deltaTime);
            if (Vector3.Distance(this.transform.position, transform.parent.position) < 1.3f)
            {
                IsBusy = false;
                goBackOrdered = false;
            }
        }
    }

    public void DealDamage()
    {
        var damage = DamageManager.CalculateDamage(this, enemyTeam[0]);
        FindObjectOfType<DamageGUI>().ShowDamageUI(enemyTeam[0].transform, damage.Item1, damage.Item2 == true ? Color.red : Color.white, damage.Item2 == true ? 24 : 16);
        Debug.Log("Damage : " + damage);
        UpdateManaUI(50);
        enemyTeam[0].TakeDamage(damage.Item1);
    }

    public override void ActiveSkill()
    {
        StartCoroutine(ActiveSkillEnum());
    }

    public IEnumerator ActiveSkillEnum()
    {
        foreach (var c in enemyTeam)
        {
            c.Select = true;
        }
        FindObjectOfType<PlayerScript>().SelectTargetForCharacter(this);
        yield return new WaitWhile(() => SelectedEnemy == null);
        Animator.SetTrigger("Special");
        foreach (var c in enemyTeam)
        {
            c.Select = false;
        }
        yield return new WaitForSecondsRealtime(1.5f);
        yield return new WaitWhile(() => goBackOrdered == false);
    }

    public void SpawnTheSpecialAbilityProjectile()
    {
        Debug.LogError("The Special Ability Activated");
        GameObject projectile = Instantiate(specialAbilityProjectile.gameObject, this.transform);
        projectile.GetComponent<GhostlingProjectile>().ChaseTarget(SelectedEnemy.transform);
    }

    public void DealDamageFromSpecial()
    {
        //TODO:FIX THIS
        int stunRandom = Random.Range(0, 101);
        if (stunRandom <= Stun_Percentage)
        {
            Debug.Log(this.name + " has successfully stunned " + SelectedEnemy.name + " for 2 rounds ");
            StunnedEffect stunnedEffect = new StunnedEffect();
            FindObjectOfType<GameManager>().StartCoroutine(stunnedEffect.UseEffect(2, SelectedEnemy, FindObjectOfType<GameManager>()));
        }
        var damage = DamageManager.CalculateDamage(this, SelectedEnemy);
        FindObjectOfType<DamageGUI>().ShowDamageUI(SelectedEnemy?.transform, damage.Item1, damage.Item2 == true ? Color.red : Color.white, damage.Item2 == true ? 24 : 16);
        Debug.Log("Damage : " + damage);
        UpdateManaUI(-100);
        SelectedEnemy.TakeDamage(damage.Item1 * 2);
        SelectedEnemy = null;

    }
}