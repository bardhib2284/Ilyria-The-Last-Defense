using System.Collections;

public interface ICharacter
{
    bool isBusy { get; set; }
    void StartTurn(System.Collections.Generic.List<Character> team, System.Collections.Generic.List<Character> enemyTeam);
    void EndTurn();
    void UpdateHealthUI();
    void UpdateManaUI();
    IEnumerator Attack();
    void TakeDamage(int damage);
    void PlayAttackAnimation();
    void PlayStunnedAnimation();
    void PlayDeathAnimation();
    void PlaySpecialAbilityAnimation(System.Collections.Generic.List<Character> team, System.Collections.Generic.List<Character> enemyteam);
}