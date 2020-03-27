public interface IHurtful
{
    int DamageDone { get; set; }
    void TakeDamage(IDamageable damagableObj);
}
