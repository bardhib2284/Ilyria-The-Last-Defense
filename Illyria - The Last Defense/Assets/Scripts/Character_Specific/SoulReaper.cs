using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

public class SoulReaper : Character
{
    [Space]
    [Space]
    [Space]
    [Header("HERO SKILLS")]
    public string Skill_1_Description = "ACITVE: Storms down a thunder of death lighting on the enemies dealing 170% damage to all the enemies";
    public string Skill_2_Description = "PASSIVE: Increases self ATTACK(20%), HP(15%),ARMOR(10%)";
    public string Skill_3_Description = "PASSIVE: Everytime Soul Reaper hits an enemy it reaps his enemies soul and feeding his own causing an increase in his current HP and Attack by 3% for 3 rounds(STACKABLE)";
    public string Skill_4_Description = "PASSIVE: Everytime Soul Reaper gets damaged he heals him self 50% of Soul Reapers CURRENT damage";
    public bool attackOrdered;
    public bool goBackOrdered;
    public GameObject passiveEffectBuff;
    private int _skill_3_buffed;
    public int SKILL_3_BUFFED_UI
    {
        get
        {
            return _skill_3_buffed;
        }
        set
        {
            _skill_3_buffed = value;
            if (_skill_3_buffed > 1)
            {
                if (passiveEffectBuff != null)
                    passiveEffectBuff.GetComponentInChildren<TextMeshProUGUI>().text = _skill_3_buffed.ToString();
            }
            else if (_skill_3_buffed == 1)
            {
                passiveEffectBuff = Instantiate(passiveEffectBuff, this.transform.GetChild(0).GetChild(4).transform);
                passiveEffectBuff.GetComponent<Image>().sprite = Skill_3_Icon;
            }
        }
    }
    public GameObject projectile;
    public SoulReaper(CharacterJson characterJson) : base(characterJson)
    {
    }

    private void GoBack()
    {
        goBackOrdered = true;
    }

    public IEnumerator AttackEnum()
    {
        Animator.SetTrigger("Attack");
        yield return new WaitWhile(() => goBackOrdered == false);
    }

    public void DealDamage()
    {
        var damage = DamageManager.CalculateDamage(this, enemyTeam[0]);
        FindObjectOfType<DamageGUI>().ShowDamageUI(enemyTeam[0].transform, damage.Item1, damage.Item2 == true ? Color.red : Color.white, damage.Item2 == true ? 24 : 16);
        Debug.Log("Damage : " + damage);
        enemyTeam[0].TakeDamage(damage.Item1);
        if (Skills_3_Unclocked)
        {
            SKILL_3_BUFFED_UI++;
            Health_Current = (Health_Current * 115) / 100;
            Attack_Current = (Attack_Current * 115) / 100;
            Armor_Current = (Armor_Current * 115) / 100;
        }
        UpdateManaUI(50);
    }

    private void Start()
    {
        if (Skills_2_Unlocked)
        {
            Health_Max = (Health_Max * 120) / 100;
            Attack_Max = (Attack_Max * 120) / 100;
            Armor_Max = (Armor_Max * 120) / 100;
        }
        passiveEffectBuff = Resources.Load<GameObject>("UI/Effect");
        base.Start();
    }

    private void Update()
    {
        if (attackOrdered)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, (enemyTeam[0].transform.position + (Vector3.left * 2f)), 20f * Time.deltaTime);
            if (Vector3.Distance(this.transform.position, enemyTeam[0].transform.position) < 4f)
            {
                attackOrdered = false;
                StartCoroutine(AttackEnum());
            }
        }
        if (goBackOrdered)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.parent.position, 20f * Time.deltaTime);
            if (Vector3.Distance(this.transform.position, transform.parent.position) < 1f)
            {
                IsBusy = false;
                goBackOrdered = false;
            }
        }
    }

    #region OVERRIDES
    public override void Attack()
    {
        attackOrdered = true;
    }

    public override void StartTurn()
    {
        base.StartTurn();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (Skills_4_Unlocked && !Dead)
        {
            Debug.LogWarning("Healed from take damage");
            int healAmount = (Attack_Current / 2);
            Health_Current += healAmount;
            Health_Current = LimitToRange(Health_Current, 0, Health_Max);
            FindObjectOfType<GameManager>().StartCoroutine(UpdateHealthUI());
            GameObject heal = Instantiate(Resources.Load<GameObject>("UI/HealthIndicator"), this.transform);
            heal.transform.position = heal.transform.position + Vector3.up * 3f;
            TextMeshPro tmp = heal.GetComponent<TextMeshPro>();
            tmp.fontSize = 12;
            tmp.color = Color.green;
            tmp.text = "+" + healAmount.ToString();
            Destroy(heal, 1.3f);
        }
        UpdateHealthUI();
    }

    public override IEnumerator UpdateHealthUI()
    {
        return base.UpdateHealthUI();
    }

    public override void UpdateManaUI(int manaToRefill)
    {
        base.UpdateManaUI(manaToRefill);
    }

    public override void ActiveSkill()
    {
        Animator.SetTrigger("Super");
        foreach (var enemy in enemyTeam)
        {
            if (!enemy.Dead)
            {
                GameObject projectileGO = Instantiate(projectile, this.transform);
                projectileGO.transform.position = enemy.transform.position + (Vector3.up * 20f) + (Vector3.right * 2f);
                SoulReaperProjectile projectilescript = projectileGO.GetComponent<SoulReaperProjectile>();
                projectilescript.t = enemy.transform;
            }
        }
    }

    public void DealDamageFromSpecial(Character enemy)
    {
        var damage = DamageManager.CalculateDamage(this, enemy);
        FindObjectOfType<DamageGUI>().ShowDamageUI(enemy.transform, damage.Item1, damage.Item2 == true ? Color.red : Color.white, damage.Item2 == true ? 24 : 16);
        UpdateManaUI(-100);
        enemy.TakeDamage((int)(damage.Item1 * 1.7f));
    }

    #endregion
}
