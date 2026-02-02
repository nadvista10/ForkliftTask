namespace Forklift.Core.Car.Systems
{
    public interface ICarEngineSystem
    {
        public enum EngineStatus
        {
            Stopped,
            Starting,
            Running
        }

        public EngineStatus Status { get; set; }

        public float EvaluateTorque();
    }
}