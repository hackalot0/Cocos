namespace GK.Reflection
{
    public enum TypeQueryFlags : byte
    {
        None = 0,
        Equal = 1,
        Inherit = 2,
        Implement = 4,

        EqualOrInherit = Equal | Inherit,
        ImplementOrInherit = Implement | Inherit,
        Any = byte.MaxValue,
    }
}