using System;
using System.Collections.Generic;
using System.Text;

namespace oop_team_project
{
    internal delegate void DeathNotification(Creature deathCharacter);
    internal abstract class Creature : IComparable<Creature>
    {
        public DeathNotification OnDead;
        protected List<PoisonStatus> poisonChimeraAttack = new List<PoisonStatus>();
        private string name;
        private int currentHp;
        private int maxHp;
        private int attackPower;
        private double defenseRate;
        internal struct PoisonStatus
        {
            public int DamageTurn;
            public int RemainTurn;
        }

        /*
         * Property와 protedted setter를 사용하여 외부에서 직접 필드에 접근하지 못하도록 캡슐화
         */
        public string Name
        {
            get { 
                return name; 
            }
            protected set { 
                name = value; 
            }
        }

        public int CurrentHp
        {
            get { return currentHp; }
            protected set
            {
                currentHp = value;

                if (currentHp < 0)
                {
                    currentHp = 0;
                }

                if (currentHp > MaxHp)
                {
                    currentHp = MaxHp;
                }
            }
        }

        public int MaxHp
        {
            get { 
                return maxHp; 
            }
            protected set { 
                maxHp = value; 
            }
        }

        public int AttackPower
        {
            get { 
                return attackPower; 
            }
            protected set { 
                attackPower = value; 
            }
        }

        public double DefenseRate
        {
            get { 
                return defenseRate; 
            }
            protected set { 
                defenseRate = value; 
            }
        }

        public bool IsDead
        {
            get { 
                return CurrentHp <= 0; 
            }
        }

        protected Creature(string name, int maxHp, int attackPower, double defenseRate)
        {
            Name = name;
            MaxHp = maxHp;
            CurrentHp = maxHp;
            AttackPower = attackPower;
            DefenseRate = defenseRate;
        }

        public int CompareTo(Creature other)
        {
            return other.CurrentHp.CompareTo(this.CurrentHp);
        }

        public virtual void TakeDamage(int damage)
        {
            Console.WriteLine("들어온 공격력 : " + damage);
            Console.WriteLine(Name + " 방어율 : " + (DefenseRate * 100) + "%");

            int finalDamage = (int)(damage * (1 - DefenseRate));
            Console.WriteLine("최종 피해량 : " + finalDamage);
            CurrentHp -= finalDamage;

            Console.WriteLine(Name + "이 " + finalDamage + " 피해를 입었습니다.");
            Console.WriteLine("남은 HP : " + CurrentHp + "/" + MaxHp);

            if (IsDead)
            {
                Console.WriteLine(Name + " 사망했습니다.");
                OnDead?.Invoke(this);
            }
        }

        public virtual void TakeDamage(int damage, string effectName)
        {
            Console.WriteLine(effectName + "발동! 추가 데미지 100 발생!");
            TakeDamage(damage + 100);
        }

        public virtual void TakeDamage(int damage, int duration)
        {
            Random rand = new Random();
            if (rand.Next(0, 100) < 50)
            {
                Console.WriteLine( Name + "중독 상태");
                poisonChimeraAttack.Add(new PoisonStatus { DamageTurn = damage, RemainTurn = duration });
            }
        }

        public virtual void Heal(int amount)
        {
            CurrentHp += amount;
            Console.WriteLine(Name + "가 " + amount + " 회복했습니다.");
        }

        public virtual void Heal(int amount, string message)
        {
                Console.WriteLine(message);
                Heal(amount);
        }


        public abstract void ShowStatus();
        public abstract void UseSkill(int skillNumber, Creature target);
        public abstract void ShowSkills();

        public void UpdatePoisonStatus()
        {
            if (poisonChimeraAttack.Count == 0) {
                return;
            }


            for (int i = poisonChimeraAttack.Count - 1; i >= 0; i--)
            {
                PoisonStatus effect = poisonChimeraAttack[i];

                Console.WriteLine("\n키메라의 독에 중독 됨");
                CurrentHp -= effect.DamageTurn;

                effect.RemainTurn--;
                if (effect.RemainTurn <= 0)
                {
                    poisonChimeraAttack.RemoveAt(i);
                    Console.WriteLine("\n키메라의 중독 상태가 해제되었습니다.");
                }
                else
                {
                    poisonChimeraAttack[i] = effect;
                    Console.WriteLine("중독 남은 턴: " + effect.RemainTurn);
                }
            }

            if (IsDead)
            {
                OnDead?.Invoke(this);
            }
        }


        //out 키워드 메서드 (out 2개째 달성)
        public void GetQuickStats(out int hp, out int atk)
        {
            hp = CurrentHp;
            atk = AttackPower;
        }
    }
}