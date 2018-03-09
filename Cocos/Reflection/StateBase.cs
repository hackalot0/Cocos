namespace Cocos.Reflection
{
    public abstract class StateBase : Initializable
    {
        internal int BindingCount { get; set; }

        protected override void Dispose(bool managed) => BindingCount = 0;
    }
}