using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_ROYALE
{

    public enum type { IRREGULAR, CONTROLLED, PREFORMED }

    class Explosive : Item
    {
        private int explosionTime { get; set; }
        private int blastRadius { get; set; }
        private Player play { get; }

        private type bomb;

        public Explosive(String itemName, Texture2D sprite, Vector2 pos) : base(itemName, sprite, pos)
        {
            Use();
        }



        public new void Use()
        {
            ArmExplosive();
        }

        public void Throw()
        {
            // throw function from trhow class here.
        }

        public void ArmExplosive()
        {
            for (int i = 0; i == 500; i++)
            {
                Throw();
                for (int x = 0; x == GetExplosionTime(); x++)
                {
                    int dmg = 25;
                    // blast
                    // something to do an explosion somehow. don't know yet how.
                    // use blastradius for the dealing dmg, dmg is selected by the type of explosive
                    // dmg is here an idea
                    if (bomb == type.CONTROLLED)
                    {
                        // moet uitgewerkt worden
                        GetBlastRadius();
                        dmg = 50;
                        // mogelijke uitwerking voor explosive 
                        play.ThrowExplosive();
                    }
                    else if(bomb == type.IRREGULAR)
                    {
                        // moet uitgewerkt worden
                        GetBlastRadius();
                        dmg = 60;
                        // mogelijke uitwerking voor explosive 
                        play.ThrowExplosive();
                    }
                    else if(bomb == type.PREFORMED)
                    {
                        // moet uitgewerkt worden
                        GetBlastRadius();
                        dmg = 70;
                        // mogelijke uitwerking voor explosive 
                        play.ThrowExplosive();
                    }
                    x = 0;
                }
                i = 0;
            }
        }

        public int GetBlastRadius()
        {
            return blastRadius;
        }

        public int GetExplosionTime()
        {
            return explosionTime;
        }
    }
}
