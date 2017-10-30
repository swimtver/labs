using System;

namespace Worker
{
    public struct Unit : IEquatable<Unit>
    {
        public bool Equals(Unit other) => true;
        public override bool Equals(object obj) => obj is Unit;
        public override int GetHashCode() => 0;
        public static readonly Unit Value = new Unit();
    }
}