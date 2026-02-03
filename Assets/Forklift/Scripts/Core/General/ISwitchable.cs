namespace Forklift.Core.General
{
    public interface ISwitchable
    {
        public bool IsEnabled { get; }
        public void Enable();
        public void Disable();
    }
}
