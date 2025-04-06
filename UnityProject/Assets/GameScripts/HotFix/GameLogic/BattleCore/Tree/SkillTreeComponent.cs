using Cysharp.Threading.Tasks;
using ET;
using MemoryPack;

using UnityEngine;

namespace GameLogic.Battle
{
   
    public partial class SkillTreeComponent : Entity,IAwake<string>,ET.IUpdate,IDestroy
    {
        public string TreeName;
        
        public SkillTreeSO Tree;


        public bool Enable { get; set; }
    }
    
    [EntitySystemOf(typeof(SkillTreeComponent))]
 
    public static partial class SkillTreeComponentSystem
    {
        [EntitySystem]
        public static void Awake(this SkillTreeComponent self, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                self.TreeName = name;
                self.LoadSkillTree().NoContext();
            }
        }
        
        public static async ETTask LoadSkillTree(this SkillTreeComponent self)
        {
            if (self.Tree == null)
            {
                var treeSo = await GameModule.Resource.LoadAssetAsync<SkillTreeSO>(self.TreeName);
                self.Tree = GameObject.Instantiate(treeSo);
                self.Tree.IsInitialized = false;
                self.Tree.IsRuning = false;
            }

#if ENABLE_VIEW && UNITY_EDITOR
            var component = self.ViewGO.AddComponent<SkillTreeViewComponent>();
            component.skillTreeSO = self.Tree;
#endif
        }
        
       
        [EntitySystem]
        public static void Update(this SkillTreeComponent self)
        {
            if (!self.Enable)
            {
                return;
            }
            
            if (self.Tree != null)
            {
                self.Tree.Update();
            }
        }

        public static void ForceExit(this SkillTreeComponent self)
        {
            if (self.IsRunning())
            {
                self.Tree.OnSkillExit();
            }
        }
    
        [EntitySystem]
        public static void Destroy(this SkillTreeComponent self)
        {
            self.TreeName = null;
            if (self.Tree != null)
            {
                self.Tree.Root.Stop();
                self.Tree = null;
            }
        }

        public static bool IsRunning(this SkillTreeComponent self)
        {
            return self.Tree != null && self.Tree.IsRuning;
        }

        public static void Init(this SkillTreeComponent self, IAbility ability, IAbilityExecute execute)
        {
            self.Tree.Init(ability, execute);
        }

        public static bool IsReady(this SkillTreeComponent self)
        {
            return self.Tree != null;
        }
        

    }
}