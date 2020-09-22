using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    void UponSpawn();

    void WhileAlive();

    void UponDamge();

}

public interface IDamageable
{
    void TakenDamge(float damage, float pointAdded, bool addPointToMultiplier);
}

public interface IAbility
{
    void StartAbility();
    void StopAbility();
}