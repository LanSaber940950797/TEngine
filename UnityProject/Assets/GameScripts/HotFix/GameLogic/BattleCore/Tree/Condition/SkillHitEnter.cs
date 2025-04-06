// using System;
// using Kurisu.AkiBT;
// using UnityEngine;
//
// namespace GameLogic.Battle
// {
//     [AkiInfo("Conditional:技能打击进入")]
//     [AkiLabel("Skill:技能打击")]
//     [AkiGroup("Skill")]
//     public class SkillHitEnter : SkillConditional
//     {
//         protected Skill skill;
//
//         [AkiLabel("要求打击次数")]
//         [SerializeField]
//         public int hitTimes;
//
//         protected override void OnAwake()
//         {
//             base.OnAwake();
//             skill = skillTree.Ability as Skill;
//         }
//         
//         protected override bool IsUpdatable()
//         {
//             return hitTimes == skill.CurrentHitTimes;
//         }
//     }
// }