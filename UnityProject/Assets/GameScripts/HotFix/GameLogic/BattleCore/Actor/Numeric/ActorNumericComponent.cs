using System.Collections.Generic;
using ET;
using GameConfig;
using MemoryPack;
using MongoDB.Bson.Serialization.Attributes;
using TEngine;

namespace GameLogic.Battle
{
    public struct ActorNumbericChange
    {
        public Actor Actor;
        public int NumericType;
        public long Old;
        public long New;
    }
    public class ActorNumericComponent : Entity, IAwake
    {
        
        public Dictionary<int, long> NumericDic = new Dictionary<int, long>();
        [MemoryPackIgnore]
        [BsonIgnore]
        public ActorEventDispatcher Event;
        public long this[int numericType]
        {
            get
            {
                return this.GetByKey(numericType);
            }
            set
            {
                this.Insert(numericType, value);
            }
        }
    }
    
    [EntitySystemOf(typeof(ActorNumericComponent))]
    public  static partial class LSNumericComponentSystem
    {
        [EntitySystem]
        public static void Awake(this ActorNumericComponent self)
        {
            self.NumericDic.Clear();
        }


        //浮点数精度
        public static readonly int Precision = 1000;
        public static float GetAsFloat(this ActorNumericComponent self, int numericType)
        {
            return (float)self.GetByKey(numericType) / Precision;
        }
        
        public static void Modify(this ActorNumericComponent self, int nt, long value, bool isPublicEvent = true)
        {
            var curValue = self[nt];
            curValue += value;
            self.Insert(nt, curValue, isPublicEvent);
        }

        public static int GetAsInt(this ActorNumericComponent self, int numericType)
        {
            return (int)self.GetByKey(numericType);
        }
        public static float GetHpPercent(this ActorNumericComponent self)
        {
            var p = self.GetAsLong(NumericType.Hp)  * Precision / self.GetAsLong(NumericType.MaxHp);
            return (float)p / Precision;
        }

        public static long GetAsLong(this ActorNumericComponent self, int numericType)
        {
            return self.GetByKey(numericType);
        }

        public static void Set(this ActorNumericComponent self, int nt, float value)
        {
            self[nt] = (long)(value * Precision);
        }
  

        public static void Set(this ActorNumericComponent self, int nt, int value)
        {
            self[nt] = value;
        }

        public static void Set(this ActorNumericComponent self, int nt, long value)
        {
            self[nt] = value;
        }

        public static void SetNoEvent(this ActorNumericComponent self, int numericType, long value)
        {
            self.Insert(numericType, value, false);
        }

        public static void Insert(this ActorNumericComponent self, int numericType, long value, bool isPublicEvent = true)
        {
            long oldValue = self.GetByKey(numericType);
            if (oldValue == value)
            {
                return;
            }

            self.NumericDic[numericType] = value;

            if (numericType >= NumericType.Max)
            {
                self.Update(numericType, isPublicEvent);
                return;
            }

            if (isPublicEvent)
            {
                EventSystem.Instance.Publish(self.Scene(),
                    new  ActorNumbericChange()
                    {
                        Actor = self.GetParent<Actor>(), 
                        New = value, 
                        Old = oldValue, 
                        NumericType = numericType
                    });
                //self.Event.SendEvent(numericType, numericType, oldValue, value);
            }
        }

        public static long GetByKey(this ActorNumericComponent self, int key)
        {
            long value = 0;
            self.NumericDic.TryGetValue(key, out value);
            return value;
        }

        public static void Update(this ActorNumericComponent self, int numericType, bool isPublicEvent)
        {
            int final = (int)numericType / 10;
            int bas = final * 10 + 1;
            int add = final * 10 + 2;
            int pct = final * 10 + 3;
            int finalAdd = final * 10 + 4;
            int finalPct = final * 10 + 5;

            // 一个数值可能会多种情况影响，比如速度,加个buff可能增加速度绝对值100，也有些buff增加10%速度，所以一个值可以由5个值进行控制其最终结果
            // final = (((base + add) * (100 + pct) / 100) + finalAdd) * (100 + finalPct) / 100;
            long result = (long)(((self.GetByKey(bas) + self.GetByKey(add)) * (Precision + self.GetByKey(pct)) / Precision + self.GetByKey(finalAdd)) *
                (Precision + self.GetByKey(finalPct)) / Precision);
            self.Insert(final, result, isPublicEvent);
        }
   
    }
    

}