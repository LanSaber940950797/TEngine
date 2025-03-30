using System;
using System.Collections.Generic;
using GameLogic.Battle;
using ET;
using UnityEditor.Networking.PlayerConnection;

namespace GameLogic.Battle
{
    public struct NumericWatcherInfo
    {
        public SceneType SceneType { get; }
        public INumericWatcher INumericWatcher { get; }

        public NumericWatcherInfo(SceneType sceneType, INumericWatcher numericWatcher)
        {
            this.SceneType = sceneType;
            this.INumericWatcher = numericWatcher;
        }
    }

    /// <summary>
    /// 监视数值变化组件,分发监听
    /// </summary>
    [Code]
    public class ActorNumericWatcherComponent : Singleton<ActorNumericWatcherComponent>, ISingletonAwake
    {
        private readonly Dictionary<int, List<NumericWatcherInfo>> allWatchers = new();
        
        public void Awake()
        {
            HashSet<Type> types = CodeTypes.Instance.GetTypes(typeof(ActorNumericWatcherAttribute));
            foreach (Type type in types)
            {
                //如果类型不是类
                if (!type.IsClass)
                {
                    continue;
                }
                object[] attrs = type.GetCustomAttributes(typeof(ActorNumericWatcherAttribute), false);

                foreach (object attr in attrs)
                {
                    ActorNumericWatcherAttribute actorNumericWatcherAttribute = (ActorNumericWatcherAttribute)attr;
                    INumericWatcher obj = (INumericWatcher)Activator.CreateInstance(type);
                    NumericWatcherInfo numericWatcherInfo = new(actorNumericWatcherAttribute.SceneType, obj);
                    if (!this.allWatchers.ContainsKey(actorNumericWatcherAttribute.NumericType))
                    {
                        this.allWatchers.Add(actorNumericWatcherAttribute.NumericType, new List<NumericWatcherInfo>());
                    }
                    this.allWatchers[actorNumericWatcherAttribute.NumericType].Add(numericWatcherInfo);
                }
            }
        }
        
        public void Run(Actor actor, ActorNumbericChange args)
        {
            List<NumericWatcherInfo> list;
            if (!this.allWatchers.TryGetValue(args.NumericType, out list))
            {
                return;
            }

            SceneType unitDomainSceneType = actor.IScene.SceneType;
            foreach (NumericWatcherInfo numericWatcher in list)
            {
                if (!numericWatcher.SceneType.HasSameFlag(unitDomainSceneType))
                {
                    continue;
                }
                numericWatcher.INumericWatcher.Run(actor, args);
            }
        }
    }
}