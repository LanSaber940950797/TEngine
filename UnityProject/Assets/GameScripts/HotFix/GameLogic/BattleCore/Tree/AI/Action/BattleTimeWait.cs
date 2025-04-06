// using GameLogic.Battle;
// using UnityEngine;
// namespace Kurisu.AkiBT.Extend
// {
//     [AkiInfo("Action : 等待时间")]
//     [AkiLabel("BattleCommon :  Wait")]
//     public class BattleTimeWait : Action
//     {
//         [SerializeField]
//         [AkiLabel("等待毫秒")]
//         private SharedInt waitTime;
//         private int timer;
//         private int lastUpdateTime;
//         public override void Awake()
//         {
//             InitVariable(waitTime);
//             ClearTimer();
//         }
//         protected override Status OnUpdate()
//         {
//             AddTimer();
//             if (IsAlready())
//             {
//                 ClearTimer();
//                 return Status.Success;
//             }
//             else
//                 return Status.Running;
//         }
//         private void AddTimer()
//         {
//             var nowTime = GetNowTime();
//             if (lastUpdateTime == 0)
//             {
//                 lastUpdateTime = nowTime;
//                 return;
//             }
//             if (lastUpdateTime >= nowTime)
//             {
//                 return;
//             }
//             
//             timer += nowTime - lastUpdateTime;
//             lastUpdateTime = nowTime;
//         }
//
//         private int GetNowTime()
//         {
//             if (this.Tree is BattleAITreeSO aiTreeSo)
//             {
//                 return aiTreeSo.Actor.FixFrameTime();
//             }
//             else if(this.Tree is SkillTreeSO skillTreeSo)
//             {
//                 return skillTreeSo.Owner.FixFrameTime();
//             }
//
//             return 0;
//         }
//         private void ClearTimer()
//         {
//             lastUpdateTime = 0;
//             timer = 0;
//         }
//         private bool IsAlready() => timer > waitTime.Value;
//         public override void Abort()
//         {
//             ClearTimer();
//         }
//     }
// }