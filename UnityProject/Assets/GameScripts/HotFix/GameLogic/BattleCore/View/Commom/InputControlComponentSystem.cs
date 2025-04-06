using System;
using System.Collections.Generic;
using ET;
using UnityEngine;

namespace GameLogic.Battle
{
    [EntitySystemOf(typeof(InputControlComponent))]
    public static partial class InputControlComponentSystem
    {
        [EntitySystem]
        public  static void Awake(this InputControlComponent self)
        {
           
            self.InputDic = new Dictionary<InputType, KeyCode>
            {
                { InputType.Jump, KeyCode.Space },
                { InputType.Attack, KeyCode.Q },
                { InputType.Skill1, KeyCode.E },
                //{ InputType.Skill2, KeyCode.E },
            };
        }
        
        [EntitySystem]
        public static void Update(this InputControlComponent self)
        {
          
            self.ReflshMoveDir();
            self.ReflshKeyCode();
            self.ReflshKeyCode();
            self.RefreshAttackDir();
            //CheckCastSkill();
        }
        
       
        
        private static void RefreshAttackDir(this InputControlComponent self)
        {
            self.AttackPos = Input.mousePosition;
            self.AttackPos.z = 10;
            self.AttackPos = Camera.main.ScreenToWorldPoint(self.AttackPos);
            //self.attackDir = self.attackPos - actor.transformComponet.GetCenterPosition();
            self.AttackDir.z = 0;
            self.AttackDir.Normalize();
        }
        
        private static void ReflshMoveDir(this InputControlComponent self)
        {
          
            //获取输入
            var keyH = Input.GetAxis("Horizontal");
            var keyV = Input.GetAxis("Vertical");

            var keyControls = Math.Abs(keyH) > 0.1f || Math.Abs(keyV) > 0.1f;
            if (keyControls)
            {
                //self.MoveDir = new Vector3(keyH, keyV, 0);
                if (BattleConstValue.WorldType == BattleWorldType.TwoDimensional)
                {
                    self.MoveDir = new Vector3(keyH, keyV, 0);
                   
                }
                else
                {
                    self.MoveDir = new Vector3(keyH, 0, keyV);
                    //如果相机不是固定视角的，移动方向是相对相机的，获取实际方向
                    //self.MoveDir = Camera.main.transform.TransformDirection(self.MoveDir);
                }
                
                
            }
            else
            {
                self.MoveDir = Vector3.zero;
            }
        }

      
        
        private static void ReflshKeyCode(this InputControlComponent self)
        {
            foreach (var it in self.InputDic)
            {
                if (Input.GetKey(it.Value))
                {
                    self.LastInputType = it.Key;
                    break;
                }
            }
        }
        

        
    }
}