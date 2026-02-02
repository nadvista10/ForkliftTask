using Forklift.Input.UnityInputSystem;
using Zenject;

namespace Forklift.Compilation.Inputs.Di
{
    public class InputsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameInput>().FromNew().AsTransient();
            
            Container.BindInterfacesAndSelfTo<CarEngineInputsProvider>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<CarMovementInputsProvider>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<CarLiftInputsProvider>().FromNew().AsSingle();
        }
    }
}