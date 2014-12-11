﻿namespace ChickenProtector.Components
{
    using System;
    using Artemis.Interface;

    public class HealthComponent : IComponent
    {
         public HealthComponent()
             : this(0.0f)
         {
         }

         public HealthComponent(float points)
         {
             this.Points = this.MaximumHealth = points;
         }

         public float Points { get; private set; }

         public double HealthPercentage
         {
             get
             {
                 return Math.Round(this.Points / this.MaximumHealth * 100f);
             }
         }

         public bool IsAlive
         {
             get
             {
                 return this.Points > 0;
             }
         }
         public float MaximumHealth { get; private set; }

         public void AddDamage(int damage)
         {
             this.Points -= damage;
             if (this.Points < 0)
             {
                 this.Points = 0;
             }
         }
    }
}
