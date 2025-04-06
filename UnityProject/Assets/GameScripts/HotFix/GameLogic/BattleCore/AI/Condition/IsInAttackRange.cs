// using System;
// using Kurisu.AkiBT;
// using UnityEngine;
//
// namespace GameLogic.Battle
// {
//     [AkiInfo("Conditional:是否在攻击范围内")]
//     [AkiLabel("BattleAI:是否在攻击范围内")]
//     [AkiGroup("BattleAI")]
//     public class IsInAttackRange :BattleAIConditional
//     {
//         [AkiLabel("目标")]
//         [SerializeField]
//         public SharedSTObject<Actor> target;
//         [AkiLabel("是否在")]
//         [SerializeField]
//         public SharedBool isInRange;
//
//         protected override void OnAwake()
//         {
//             base.OnAwake();
//             InitVariable(target);
//             InitVariable(isInRange);
//         }
//
//         protected override bool IsUpdatable()
//         {
//             return ChecknAttackRange() == isInRange.Value;
//         }
//         
//         private bool ChecknAttackRange()
//         {
//             if (target.Value == null)
//             {
//                 return false;
//             }
//
//             var targetTransform = target.Value.GetComponent<TransformComponent>();
//             var selfTransform = actor.GetComponent<TransformComponent>();
//             var targetPos = targetTransform.Position;
//             var actorPos = selfTransform.Position;
//             var distanceSqr =  (targetPos - actorPos).sqrMagnitude;
//             var attackRange = actor.GetAttackRange() + targetTransform.Radius;
//             var attackRangeSqr = attackRange * attackRange;
//             return distanceSqr <= attackRangeSqr;
//         }
//     }
// }