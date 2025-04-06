using ET;

namespace GameLogic.Battle
{

    [ChildOf]
    public class DestroyWaiter : Entity,IAwake,IDestroy
    {
        public ETTask Tsc;
    }
    
    [EntitySystemOf(typeof(DestroyWaiter))]
    public static partial class DestroyWaiterSystem
    {
        [EntitySystem]
        public static void Destroy(this DestroyWaiter self)
        {
            if (self.Tsc != null)
            {
                var tsc = self.Tsc;
                self.Tsc = null;
                tsc.SetResult();
            }
        }

        public static async ETTask Waiting(this DestroyWaiter self)
        {
            self.Tsc = ETTask.Create();
            await self.Tsc;
        }

        public static async ETTask WaitEntityDestroy(this Entity self)
        {
            if (self == null || self.IsDisposed)
            {
                return;
            }
            await self.AddChild<DestroyWaiter>(true).Waiting();
        }
        
    }
}