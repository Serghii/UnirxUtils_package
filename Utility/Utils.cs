using System;
using System.Collections.Generic;
using TMPro;
using Zenject;

namespace DotweenStuff
{
    public static class Utils
    {
        public static  T GetFromSceneContext<T>() where T : class 
        {
            var source = ProjectContext.Instance.Container.Resolve<SceneContextRegistry>().SceneContexts;
            if (source is IList<SceneContext> sourceList)
            {
                if (sourceList.Count > 0)
                {
                    return sourceList[0]?.Container?.Resolve<T>();
                }
                return null;
            }
            else
            {
                using (IEnumerator<SceneContext> enumerator = source.GetEnumerator())
                {
                    if (!enumerator.MoveNext())
                        return null;
                    SceneContext current = enumerator.Current;
                    if (!enumerator.MoveNext())
                        return current.Container.Resolve<T>();;
                }
            }
            return null;
        }
        
        public static IDisposable SubscribeToText(this System.IObservable<string> source, TextMeshProUGUI text)
        {
            return source.SubscribeWithState(text, (x, t) => t.text = x);
        }
        
        public static IDisposable SubscribeToText<T>(this System.IObservable<T> source, TextMeshProUGUI text)
        {
            return source.SubscribeWithState(text, (x, t) => t.text = x.ToString());
        }

    }
}