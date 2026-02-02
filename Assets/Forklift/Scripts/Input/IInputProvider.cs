namespace Forklift.Input
{
    public interface IInputProvider
    {
        public bool IsEnabled { get; }
        public void Enable();
        public void Disable();
    }
}