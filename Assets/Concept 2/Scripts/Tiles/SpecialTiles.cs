using Unity.VisualScripting;
using UnityEngine;

namespace Concept2
{
    public class SpecialTiles : MonoBehaviour
    {
        public float multiplier;
        private bool towerBuff;
        private OffenseTowerBase tower;

        public enum BuffType
        {
            AttackSpeed, Damage, Range
        }
        public BuffType type;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!towerBuff && tower != null)
            {
                switch (type)
                {
                    case BuffType.AttackSpeed:
                        AttackSpeedBuff();
                        break;

                    case BuffType.Damage: 
                        DamageBuff(); 
                        break;
                }
                towerBuff = true;
            }

            else if (towerBuff && tower == null)
            {
                towerBuff = false;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.GetComponent<OffenseTowerBase>() != null)
            {
                tower = other.gameObject.GetComponent<OffenseTowerBase>();
            }
        }

        void AttackSpeedBuff()
        {
            tower.timeBetweenAttacks = tower.timeBetweenAttacks - (tower.timeBetweenAttacks * multiplier);
        }

        void DamageBuff()
        {
            tower.damageValue = tower.damageValue + (tower.timeBetweenAttacks * multiplier);
        }
    }
}
