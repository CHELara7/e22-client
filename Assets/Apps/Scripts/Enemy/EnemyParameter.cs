using System;
using UnityEngine;

public class EnemyParameter : MonoBehaviour
{
    [Serializable]
    public class EnemyInfo
    {
        public int ID;
        public string Name;
        public int Hp;
        public int ResourceID;
        public Projectile Projectile;
        public int EventID;
    }

    /// <summary>
    /// 投擲物
    /// </summary>
    [Serializable]
    public class Projectile
    {
        public int ID;
        public float Interval;
        public float Speed;
        public int Amount;
    }
}
