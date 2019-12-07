using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    public class ConsumableItem : ItemController
    {
        protected float deltaTime;
        internal CharController usingChar;
        public int Units = 1;

        public ConsumableStats GetConsumableStats()
        {
            if (Stats is ConsumableStats)
            {
                return Stats as ConsumableStats;
            }
            ConsumableStats defect = new ConsumableStats();
            Stats = defect;
            return defect;
        }
        public override void Use(CharController character)
        {
            usingChar = character;
            int index = usingChar.anim.GetLayerIndex(Stats.animLayer);
            usingChar.anim.Play(Stats.animation, index );
            usingChar.states.Consuming = true;
            usingChar.anim.SetLayerWeight(index, 1);
        }
        private void FixedUpdate()
        {
            if (usingChar != null)
            {
                deltaTime += Time.deltaTime;
                if (deltaTime >= GetConsumableStats().Duration)
                {
                    Finish(false);
                }
                else
                {
                    int index = usingChar.anim.GetLayerIndex(Stats.animLayer);
                    usingChar.anim.SetLayerWeight(index, 1);
                }
            }
        }
        public virtual float GetActualUseState()
        {
            return deltaTime / GetConsumableStats().Duration;
        }
        public virtual void Finish(bool interrupt)
        {
            usingChar.states.Consuming = false;
            int index = usingChar.anim.GetLayerIndex(Stats.animLayer);
            usingChar.anim.SetLayerWeight(index, 0);
            deltaTime = 0;
            if (!interrupt)
            {
                ConsumableStats consumeStats = GetConsumableStats();
                usingChar.Consume(consumeStats.EffectStrength, consumeStats.EffecStat);
                usingChar.inventary.RemoveItem(this);
                Destroy(this.gameObject);
            }
            usingChar = null;
            deltaTime = 0;
        }
    }

}
