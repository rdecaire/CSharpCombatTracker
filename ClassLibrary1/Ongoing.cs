using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CombatTracker
{
    public class Ongoing
    {
        public enum Keyword
        {
            ACID, COLD, FIRE, FORCE, LIGHTNING, NECROTIC, POISON, PSYCHIC,
            RADIANT, THUNDER, DAMAGE, NULL
        };
        public enum EffectType
        {
            ONGOING, DEFPEN, ATTPEN, ATTBON, DEFBON, BLIND, DAZE, DEAF,
            DOMINATE, IMMOB, MARK, PETRIFY, PRONE, RESTRAIN, SLOW, STUN, WEAK, NULL
        };
        Keyword key;
        EffectType type;
        int mod;
        int dur;

        public Ongoing(Keyword keyword, EffectType effect_type, int modifier, int duration)
        {
            key = keyword;
            type = effect_type;
            mod = modifier;
            dur = duration;

        }

        public EffectType Type
        {
            get
            {
                return this.type;
            }
        }

        public int Modifier
        {
            get
            {
                return mod;
            }
            set
            {
                mod = value;
            }
        }

        public int Duration
        {
            get
            {
                return dur;
            }
            set
            {
                dur = value;
            }
        }

        public bool equals(Ongoing x)
        {
            if (x.key == this.key)
            {
                return true;
            }
            else return false;
        }

           
    }
}
