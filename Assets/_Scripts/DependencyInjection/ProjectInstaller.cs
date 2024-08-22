using PhotonTest.Signals;
using Zenject;

namespace PhotonTest.DependencyInjection 
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallSignalBus();
        }

        private void InstallSignalBus() 
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<PlayClickedSignal>().OptionalSubscriber();
            Container.DeclareSignal<QuitClickedSignal>().OptionalSubscriber();
            Container.DeclareSignal<SceneLoadedSignal>().OptionalSubscriber();
            Container.DeclareSignal<JoinClickedSignal>().OptionalSubscriber();
            Container.DeclareSignal<QuickMatchSignal>().OptionalSubscriber();
            Container.DeclareSignal<CreateRoomSignal>().OptionalSubscriber();
            Container.DeclareSignal<ToMainMenuSignal>().OptionalSubscriber();
            Container.DeclareSignal<RoomUpdateSignal>().OptionalSubscriber();
            Container.DeclareSignal<ToLobbySignal>().OptionalSubscriber();
        }
    }
}