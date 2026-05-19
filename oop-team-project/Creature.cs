using System;
using System.Collections.Generic;
using System.Text;

namespace oop_team_project
{
    internal delegate void DeathNotification(Creature deathCharacter);

    internal abstract class Creature : IComparable<Creature>
    {
        internal struct PoisonStatus
        {
            public int DamageTurn;
            public int RemainTurn;
        }

        protected List<PoisonStatus> poisonChimeraAttack = new List<PoisonStatus>();

        private string name;
        private int currentHp;
        private int maxHp;
        private int attackPower;
        private double defenseRate;

        /*
         * Property와 protected setter를 사용하여 외부에서 직접 필드에 접근하지 못하도록 캡슐화
         */
        public string Name {
            get {
                return name;
            }
            protected set {
                name = value;
            }
        }

        public int CurrentHp {
            get {
                return currentHp;
            }
            protected set {
                currentHp = value;

                if (currentHp < 0) {
                    currentHp = 0;
                }

                if (currentHp > MaxHp) {
                    currentHp = MaxHp;
                }
            }
        }

        public int MaxHp {
            get  {
                return maxHp;
            }
            protected set {
                maxHp = value;
            }
        }

        public int AttackPower {
            get {
                return attackPower;
            }
            protected set {
                attackPower = value;
            }
        }

        public double DefenseRate {
            get {
                return defenseRate;
            }
            protected set {
                defenseRate = value;
            }
        }

        public bool IsDead {
            get {
                return CurrentHp <= 0;
            }
        }

        public DeathNotification OnDead;

        public Creature(string name, int maxHp, int attackPower, double defenseRate) {
            Name = name;
            MaxHp = maxHp;
            CurrentHp = maxHp;
            AttackPower = attackPower;
            DefenseRate = defenseRate;
        }

        public abstract void ShowStatus();
        public abstract void UseSkill(int skillNumber, Creature target);
        public abstract void ShowSkills();

        public bool IsHero
        {
            get { 
                return this is Hero; 
            }
        }

        public int CompareTo(Creature other) {
            return other.CurrentHp.CompareTo(this.CurrentHp);
        }

        public virtual int TakeDamage(int damage) {
            Console.WriteLine("들어온 공격력 : " + damage);
            Console.WriteLine(Name + " 방어율 : " + (DefenseRate * 100) + "%");

            int finalDamage = (int)(damage * (1 - DefenseRate));
            Console.WriteLine("최종 피해량 : " + finalDamage);

            CurrentHp -= finalDamage;

            Console.WriteLine(Name + "이 " + finalDamage + " 피해를 입었습니다.");
            Console.WriteLine("남은 HP : " + CurrentHp + "/" + MaxHp);

            if (IsDead) {
                Console.WriteLine(Name + " 사망했습니다.");
                OnDead?.Invoke(this);
            }
            return finalDamage;
        }

        public virtual int TakeDamage(int damage, string effectName) {
            Console.WriteLine(effectName + "발동! 추가 데미지 100 발생!");
            return TakeDamage(damage + 100);
        }

        public virtual void TakeDamage(int damage, int duration) {
            Random rand = new Random();

            if (rand.Next(0, 100) < 50){
                Console.WriteLine("\n상태이상 " + Name + " 키메라의 독에 중독되었습니다! (" + duration + "턴 지속)");

                poisonChimeraAttack.Add(new PoisonStatus{DamageTurn = damage,RemainTurn = duration });
            }else{
                Console.WriteLine("\n" + Name + " 독 공격을 저지하여 중독되지 않았습니다.");
            }
        }

        public virtual void Heal(int amount) {
            CurrentHp += amount;
            Console.WriteLine(Name + "가 " + amount + " 회복했습니다.");
        }

        public virtual void Heal(int amount, string message) {
            Console.WriteLine("\n" + message + " 가 발동합니다!");
            Heal(amount);
        }

        public void ShowStatus(bool showDetail) {
            ShowStatus();

            if (showDetail) {
                Console.WriteLine("공격력 : " + AttackPower);
                Console.WriteLine("방어율 : " + (DefenseRate * 100) + "%");
            }
        }

        public void UpdatePoisonStatus() {
            if (poisonChimeraAttack.Count == 0) return;

            for (int i = poisonChimeraAttack.Count - 1; i >= 0; i--)
            {
                PoisonStatus effect = poisonChimeraAttack[i];

                CurrentHp -= effect.DamageTurn;
                Console.WriteLine("[독 피해] " + Name + "이(가) 독으로 인해 " + effect.DamageTurn + "의 지속 피해를 입었습니다. (남은 HP: " + CurrentHp + "/" + MaxHp + ")");
                BattleGame.battlePowerResult.AddDamage(!IsHero, effect.DamageTurn);

                effect.RemainTurn--;

                if (effect.RemainTurn <= 0)
                {
                    poisonChimeraAttack.RemoveAt(i);
                    Console.WriteLine("[해제] " + Name + "의 중독 상태가 해제되었습니다.");
                }
                else
                {
                    poisonChimeraAttack[i] = effect;
                    Console.WriteLine("-> 중독 남은 턴: " + effect.RemainTurn);
                }
            }

            if (IsDead)
            {
                OnDead?.Invoke(this);
            }
        }

        public void ApplyRoundEffect(int PowerChange, int hpChange, out int finalHp, out int finalPower) {
            AttackPower += PowerChange;
            CurrentHp += hpChange;

            if (AttackPower < 0) {
                AttackPower = 0;
            }

            if (CurrentHp < 0) {
                CurrentHp = 0;
            }

            finalHp = CurrentHp;
            finalPower = AttackPower;
        }
    }
}