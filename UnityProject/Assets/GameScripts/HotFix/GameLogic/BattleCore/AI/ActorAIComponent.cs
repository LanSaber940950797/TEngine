using Cysharp.Threading.Tasks;
using ET;
using TEngine;
using UnityEngine;

namespace GameLogic.Battle
{
    [ComponentOf(typeof(Actor))]
    public class ActorAIComponent : Entity, IAwake,ET.IUpdate, IAwake<string>
    {
        public BattleAITreeSO AITreeSo;
        public string AITreeName;
        public Actor Actor => GetParent<Actor>();
        public int LastUpdateTime;
        //public readonly float DefaultTargetTime = 1f;

        private EntityRef<Actor> target;

        public Actor Target
        {
            get => target;
            set => target = value;
        }
       
    
        // public Vector3 MoveDir;
        // public Vector3 MovePos;
        // public Vector3 AttackDir;
        // public Vector3 AttackPos;
        
        
    }
    
    [EntitySystemOf(typeof(ActorAIComponent))]
    [LSEntitySystemOf(typeof(ActorAIComponent))]
    public static  partial class ActorAIComponentSystem
    {
        [EntitySystem]
        public static void Awake(this ActorAIComponent self, string treeName)
        {
            if (string.IsNullOrEmpty(treeName))
            {
               return; 
            }
            
            self.AITreeName = treeName;
            self.LoadAITree().Forget();
        }
        
        public static async UniTask LoadAITree(this ActorAIComponent self, string treeName = null)
        {
            if (treeName != null)
            {
                self.AITreeName = treeName;
            }
            if (self.AITreeSo == null)
            {
                var treeSo = await GameModule.Resource.LoadAssetAsync<BattleAITreeSO>(self.AITreeName);
                self.AITreeSo = GameObject.Instantiate(treeSo);
                self.AITreeSo.Actor = self.Actor;
                self.AITreeSo.IsInitialized = false;
                self.AITreeSo.Init(null);
            }

#if ENABLE_VIEW && UNITY_EDITOR
            var component = self.ViewGO.AddComponent<BattleAITreeViewComponent>();
            component.treeSo = self.AITreeSo;
#endif
        }

        [EntitySystem]
        public static void Update(this ActorAIComponent self)
        {
            self.AITreeSo?.Update();
            if (self.Target != null)
            {
                var dir = self.Target.GetComponent<TransformComponent>().Position - self.Actor.GetComponent<TransformComponent>().Position;
                self.Actor.GetComponent<TransformComponent>().Forward = dir.normalized;
            }
        }
        
        public static bool IsCanSpell(this ActorAIComponent self)
        {
            return false;
        }
        
        public static bool TrySpell(this ActorAIComponent self)
        {
            // var skills = self.Actor.GetComponent<SkillComponent>().GetReadySkillsByPriority();
            // if (skills.Count == 0)
            // {
            //     return false;
            // }
            //
            // foreach (var skill in skills)
            // {
            //     if(!skill.TryAICalcSpell(self, out var castParam))
            //     {
            //         continue;
            //     }
            //
            //     if (self.Actor.GetComponent<SkillComponent>().TrySpell(skill, castParam))
            //     {
            //         return true;
            //     }
            // }

            return false;
        }
        
    }
}