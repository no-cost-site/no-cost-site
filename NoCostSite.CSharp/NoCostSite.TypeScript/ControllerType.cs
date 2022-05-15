using System;

namespace NoCostSite.TypeScript
{
    internal class ControllerType
    {
        internal string Name { get; set; } = null!;
        
        internal Type Type { get; set; } = null!;

        internal ControllerProperty[] Properties { get; set; } = null!;
        
        public bool Equals(ControllerType obj)
        {
            return Name == obj.Name;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != GetType()) return false;

            return Equals((ControllerType) obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return Name.GetHashCode();
        }
    }
}