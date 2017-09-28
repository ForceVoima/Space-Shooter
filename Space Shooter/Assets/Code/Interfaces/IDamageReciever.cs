using UnityEngine;

namespace SpaceShooter
{
    public interface IDamageReciever
    {
        void TakeDamage(int amount);
    }
}