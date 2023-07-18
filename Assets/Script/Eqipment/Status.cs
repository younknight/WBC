using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Status
{
    [SerializeField] float hp = 10;//
    [SerializeField] float attack = 1;//
    [SerializeField] float defence = 0;//
    [SerializeField] float criDamage = 0;//
    [SerializeField] float criRate = 0;//
    #region getter
    public float Hp { get => hp; set => hp = value; }
    public float Attack { get => attack; set => attack = value; }
    public float Defence { get => defence; set => defence = value; }
    public float CriDamage { get => criDamage; set => criDamage = value; }
    public float CriRate { get => criRate; set => criRate = value; }

    public void PlusStatus(float hp, float attack, float defence, float criDamage, float criRate)
    {
        this.hp += hp;
        this.attack += attack;
        this.defence += defence;
        this.criDamage += criDamage;
        this.criRate += criRate;
    }
    public void MinusStatus(float hp, float attack, float defence, float criDamage, float criRate)
    {
        this.hp -= hp;
        this.attack -= attack;
        this.defence -= defence;
        this.criDamage -= criDamage;
        this.criRate -= criRate;
    }
    #endregion

}
