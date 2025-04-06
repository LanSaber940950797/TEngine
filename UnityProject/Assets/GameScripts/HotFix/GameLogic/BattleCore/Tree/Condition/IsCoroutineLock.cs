using ET;
using GameLogic.Battle;
using Kurisu.AkiBT;
using UnityEngine;

namespace GameLogic
{
    [AkiInfo("Conditional:是否协程锁")]
    [AkiLabel("BattleCommon:是否协程锁")]
    [AkiGroup("BattleCommon")]
    public class IsCoroutineLock : SkillConditional
    {
        [SerializeField, Tooltip("协程锁类型")] 
        public int Type;
        [SerializeField, Tooltip("协程锁id")] 
        public int Id;
        
        protected CoroutineLock cLock;
        protected bool isFirst = true;
        protected override void OnAwake()
        {
            base.OnAwake();
            isFirst = true;
            cLock = null;
        }

        protected override void OnStop()
        {
            cLock?.Dispose();
            cLock = null;
            base.OnStop();
        }

        protected override Status OnUpdate()
        {
            var ret = base.OnUpdate();
            if (ret != Status.Running)
            {
                if (cLock != null)
                {
                    cLock.Dispose();
                    cLock = null;
                    isFirst = true;
                }
            }
            return ret;
        }

      
        
        protected override bool IsUpdatable()
        {
            if (isFirst && cLock == null)
            {
                CheckCoroutineLock().NoContext();
                isFirst = false;
            }

            return cLock != null;
        }

        protected async ETTask CheckCoroutineLock()
        {
            var fiber = this.skillTree.Owner.Fiber();
            cLock = await fiber.Root.GetComponent<CoroutineLockComponent>().Wait(Type, Id);
        }
    }
}