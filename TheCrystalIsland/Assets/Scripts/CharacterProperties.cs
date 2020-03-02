using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class CharacterProperties
    {
        public static Color Colour(this Character character)
        {
            switch (character)
            {
                case Character.Scientist:
                    return Color.green;
                case Character.Ninja:
                    return Color.red;
                case Character.Computer:
                    return Color.gray;
                default:
                    return Color.black;
            }
        }
    }
}
