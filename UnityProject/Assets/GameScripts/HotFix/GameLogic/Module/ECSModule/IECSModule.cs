using ET;

namespace GameLogic
{
    public interface IECSModule
    {
        public Scene Root { get; set; }

        public ETTask StartAsync();
    }
}