using System;
using System.Collections.Generic;
using System.Text;

namespace oop_team_project
{
    internal delegate void DeathNotification(Creature deathCharacter);
    internal abstract class Creature : IComparable<Creature>
    {
        private string name;
        private int currentHp;
        private int maxHp;
        private int attackPower;
        private double defenseRate;

        public DeathNotification OnDead;

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

        public virtual void Heal(int amount)
        {
            CurrentHp += amount;
            Console.WriteLine(Name + "가 " + amount + " 회복했습니다.");
        }

        // 오버로드된 Heal 메서드로 회복 메시지를 출력할 수 있도록 함
        public virtual void Heal(int amount, string message)
        {
            Random rand = new Random();
            if (rand.Next(0, 100) < 20)
            {
                Console.WriteLine("럭키!" + message);
                Heal(1000);
            }
            else
            {
                Heal(amount);
            }
        }


        public abstract void ShowStatus();
        public abstract void UseSkill(int skillNumber, Creature target);
        public abstract void ShowSkills();

        public int CompareTo(Creature other) => other.AttackPower.CompareTo(this.AttackPower);


        //out 키워드 메서드 (out 2개째 달성)
        public void GetQuickStats(out int hp, out int atk)
        {
            hp = CurrentHp;
            atk = AttackPower;
        }

        internal struct BattleHistory
        {
            public int DamageDealt;
            public string SkillName;
        }
    }
}