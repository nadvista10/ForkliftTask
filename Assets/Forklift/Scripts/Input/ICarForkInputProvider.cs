using System;

namespace Forklift.Input
{
    public interface ICarForkInputProvider : IInputProvider
    {
        public float GetLift();
    }
}