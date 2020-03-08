using System;

namespace Assets.Scripts
{
    public static class IterationHelper
    {
        public static T NextEnum<T>(int currentValue) where T : Enum
        {
            if (Enum.IsDefined(typeof(T), currentValue + 1))
            {
                return (T)Enum.ToObject(typeof(T), currentValue + 1);
            }
            else
            {
                return (T)Enum.ToObject(typeof(T), 0);
            }
        }

        public static T PreviousEnum<T>(int currentValue) where T : Enum
        {
            if (Enum.IsDefined(typeof(T), currentValue - 1))
            {
                return (T)Enum.ToObject(typeof(T), currentValue - 1);
            }
            else
            {
                return (T)Enum.ToObject(typeof(T), Enum.GetValues(typeof(T)).Length - 1);
            }
        }

        public static int NextInArray(int currentValue, int length)
        {
            currentValue++;
            return currentValue > length - 1 ? 0 : currentValue;
        }

        public static int PreviousInArray(int currentValue, int length)
        {
            currentValue--;
            return currentValue < 0 ? length - 1 : currentValue;
        }
    }
}
