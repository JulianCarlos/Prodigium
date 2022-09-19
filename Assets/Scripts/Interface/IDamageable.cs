public interface IDamageable
{
    public void TakeDamage(DamageTypes[] types, float damage);
    public void TakeDamage(DamageTypes types, float damage);
    public void ApplyDamage();
    public void Die();
}
