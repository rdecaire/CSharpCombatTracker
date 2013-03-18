using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CombatTracker
{
    public class Character
    {
        private String name;
        private int init;
        private int initbonus;
        private int damage;
        private int maxhp;
        private int temphp;
        private List<Ongoing> effects;
        private static int numchar = 0;
        private int charindex;
        private bool is_unconscious;
        private bool is_dead;
            
        public Character (String nom, int maxhitpoints){
            name = nom;
            init = 0;
            initbonus = 0;
            damage = 0;
            maxhp = maxhitpoints;
            temphp = 0;
            effects = new List<Ongoing>();
            charindex = numchar++;
            is_unconscious = false;
            is_dead = false;
        }
    
        /* List of properties of Character objects:
         * Static properties: NumCharacters
         * Instance properties: ID, Init, Initbonus, Name, NumEffects, MaxHP, TempHP,
         * Damage, Unconscious, Dead
         */

        // return the current number of characters
        public static int NumCharacters {
            get
            {
                return numchar;
            }
        }

        public static void SaveCharacters()
        {
        }    
                
  
        // return this character's serial number
        public int ID{
            get
            {
                return charindex;
            }
        }
    
        // get/set initiative
        public int Init {
            get
            {
                return init;
            }
            set
            {
                init = value;
            }
        }

        // get/set initiative bonus
        public int InitBonus
        {
            get
            {
                return initbonus;
            }
            set
            {
                initbonus = value;
            }
        }
            
        // get/set name
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        
        public int NumEffects{
            get
            {
                return effects.Count;
            }
        }
        
        public int MaxHP {
            get
            {
                return maxhp;
            }
            set
            {
                maxhp=value;
            }
        }
        
        public int TempHP
        {
            get
            {
                return temphp;
            }
            set
            {
                if (value > temphp)
                {
                    temphp = value;
                }
            }
        }
    
        // returns current damage
        public int Damage
        {
            get
            {
                return damage;
            }
        }

        public bool Unconscious
        {
            get
            {
                return is_unconscious;
            }
        }
        public bool Dead
        {
            get
            {
                return is_dead;
            }
        }
        
        // Methods: roll_init(), apply_effect(), remove_effect(), get_effect(), find_effect(),
        // apply_damage()

        

        // randomly determine initiative
        public void RollInit()
        {
            Random random = new Random();
            int roll = (int)random.Next(1, 21);
            init = initbonus + roll;
        }

        // add an effect to the character's effects list
        public void ApplyEffect(Ongoing.Keyword key, Ongoing.EffectType type, int mod, int dur)
        {
            Ongoing new_effect;
            new_effect = new Ongoing(key, type, mod, dur);
            effects.Add(new_effect);
        }
        
        // remove an effect from the character's effects list
        // depends on a modified equals() method in Ongoing
        // maybe use get_effect instead if that works
        public void RemoveEffect(Ongoing.Keyword key)
        {
            Ongoing x = new Ongoing(key, Ongoing.EffectType.NULL, 0, 0);
            effects.Remove(x);
        }

        // find an effect based on its index
        public Ongoing GetEffect(int index)
        {
            Ongoing effect = effects[index];
            return effect;
        }

        /*find effect based on its type in a character's effects array
        /***not sure if works**
        /*
         * Quick version of how this works:
         * Find goes through List<Ongoing> effects, and looks at each object in the list.
         * It runs the object through a predicate to get a boolean.  If the boolean is true, it returns
         * the object that satisfied the predicate.  The predicate in this case is
         * a method (a delegate) which is passed the Ongoing object in effects being
         * looked at.  The method then compares the type variable of the object (provided by getType())
         * to the type specified in the original method call.  If that object's type matches the type
         * it was looking for, the predicate returns true, and Find returns that object.
         */

        public Ongoing FindEffect(Ongoing.EffectType type)
        {

            Ongoing result = effects.Find(delegate(Ongoing item)
            {
                return item.Type == type;
            }
            );

            if (result != null)
            {
                return result;
            }

            else
            {
                return null;
            }
        }

        // applies damage to character
        public void ApplyDamage(int hurty)
        {

            if (temphp > 0)
            {
                if (temphp < hurty)
                {
                    hurty -= temphp;
                    temphp = 0;
                }
                else
                {
                    temphp -= hurty;
                    hurty = 0;
                }
            }
            damage += hurty;
            if (damage >= maxhp)
            {
                is_unconscious = true;
                if (damage >= (maxhp + (int)(maxhp * .5)))
                {
                    is_unconscious = false;
                    is_dead = true;
                }
            }
            if (damage < 0)
            {
                damage = 0;
            }
        }
    }
}
