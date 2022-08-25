using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SerialLabs.FunTools
{
 

    public class FunTools
    {

        /// <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        /// How to declare a nested enum ?
        /// FROM https://stackoverflow.com/questions/980766/how-do-i-declare-a-nested-enum
        /// 
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>



        /// <summary>
        /// This makes use of:
        /// 
        /// + a 32-bit uint as the underlying type
        /// + multiple small numbers stuffed into an integer with bit shifts (you get maximum four levels of nested ID's with 256 entries at each level -- you could convert to ulong for more levels or more bits per level)
        /// + ID 0 as the special root of all ID's (possibly ID.none should be called ID.root, and any id.isa(ID.root) should be true)
        /// + implicit type conversion to convert an int into an ID
        /// + an indexer to chain ID's together
        /// + overloaded equality operators to support comparisons
        /// </summary>
        public struct ID
        {
            public static ID none;

            public ID this[int childID]
            {
                get { return new ID((mID << 8) | (uint)childID); }
            }

            public ID super
            {
                get { return new ID(mID >> 8); }
            }

            public bool isa(ID super)
            {
                return (this != none) && ((this.super == super) || this.super.isa(super));
            }

            public static implicit operator ID(int id)
            {
                if (id == 0)
                {
                    throw new System.InvalidCastException("top level id cannot be 0");
                }
                return new ID((uint)id);
            }

            public static bool operator ==(ID a, ID b)
            {
                return a.mID == b.mID;
            }

            public static bool operator !=(ID a, ID b)
            {
                return a.mID != b.mID;
            }

            public override bool Equals(object obj)
            {
                if (obj is ID)
                    return ((ID)obj).mID == mID;
                else
                    return false;
            }

            public override int GetHashCode()
            {
                return (int)mID;
            }

            private ID(uint id)
            {
                mID = id;
            }

            private readonly uint mID;
        }
        
        /// <summary>
        /// Exemple d'une classe qui remplace un type énuméré.
        /// Ne peut pas être static si utilisé comme type paramètre, comme c'est le cas dans la classe d'extension pour implémenter le tostring
        /// </summary>
        public /*static*/ class Animal
        {
            public static readonly ID dog = 1;
            public static class dogs
            {
                public static readonly ID bulldog = dog[0];
                public static readonly ID greyhound = dog[1];
                public static readonly ID husky = dog[3];
            }

            public static readonly ID cat = 2;
            public static class cats
            {
                public static readonly ID persian = cat[0];
                public static readonly ID siamese = cat[1];
                public static readonly ID burmese = cat[2];
            }

            public static readonly ID reptile = 3;
            public static class reptiles
            {
                public static readonly ID snake = reptile[0];
                public static class snakes
                {
                    public static readonly ID adder = snake[0];
                    public static readonly ID boa = snake[1];
                    public static readonly ID cobra = snake[2];
                }

                public static readonly ID lizard = reptile[1];
                public static class lizards
                {
                    public static readonly ID gecko = lizard[0];
                    public static readonly ID komodo = lizard[1];
                    public static readonly ID iguana = lizard[2];
                    public static readonly ID chameleon = lizard[3];
                }
            }
        }
        /// <summary>
        /// to test concept of nested enums
        /// </summary>
        void Animalize()
        {
            ID rover = Animal.dogs.bulldog;
            ID rhoda = Animal.dogs.greyhound;
            ID rafter = Animal.dogs.greyhound;

            ID felix = Animal.cats.persian;
            ID zorro = Animal.cats.burmese;

            ID rango = Animal.reptiles.lizards.chameleon;

            if (rover.isa(Animal.dog))
                Console.WriteLine("rover is a dog");
            else
                Console.WriteLine("rover is not a dog?!");

            if (rover == rhoda)
                Console.WriteLine("rover and rhoda are the same");

            if (rover.super == rhoda.super)
                Console.WriteLine("rover and rhoda are related");

            if (rhoda == rafter)
                Console.WriteLine("rhoda and rafter are the same");

            if (felix.isa(zorro))
                Console.WriteLine("er, wut?");

            if (rango.isa(Animal.reptile))
                Console.WriteLine("rango is a reptile");

            Console.WriteLine("rango is an {0}", rango.ToString<Animal>());
        }
    }

    /// <summary>
    /// For use with Nested Enum 
    /// </summary>
    /// <see cref="https://stackoverflow.com/questions/980766/how-do-i-declare-a-nested-enum"/>
    public static class IDExtensions
    {
        public static string ToString<T>(this FunTools.ID id)
        {
            return ToString(id, typeof(T));
        }

        public static string ToString(this FunTools.ID id, Type type)
        {
            foreach (var field in type.GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Static))
            {
                if ((field.FieldType == typeof(FunTools.ID)) && id.Equals(field.GetValue(null)))
                {
                    return string.Format("{0}.{1}", type.ToString().Replace('+', '.'), field.Name);
                }
            }

            foreach (var nestedType in type.GetNestedTypes())
            {
                string asNestedType = ToString(id, nestedType);
                if (asNestedType != null)
                {
                    return asNestedType;
                }
            }

            return null;
        }
    }
}
