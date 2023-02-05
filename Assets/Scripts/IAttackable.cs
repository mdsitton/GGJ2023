using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IAttackable
{
    float Hp { get; }
    void Attack(float damage);
}