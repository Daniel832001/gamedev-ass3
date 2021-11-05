using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tweener : MonoBehaviour
{
    private List<Tween> activeTweens = new List<Tween>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Tween activeTween in activeTweens.ToList())
        {
            if (activeTween.Target != null)
            {
                if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) > 0.1f)
                {
                    float elapsedTime = Time.time - activeTween.StartTime;
                    float timeFraction = elapsedTime / activeTween.Duration;
                    Vector3 lerpPos = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, timeFraction);
                    activeTween.Target.position = lerpPos;
                }
                else
                {
                    activeTween.Target.position = activeTween.EndPos;
                    activeTweens.Remove(activeTween);
                }

            }
        }
            
    }

    public bool AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        if (TweenExists(targetObject))
        {
            return false;
        }
        else
        {
            activeTweens.Add(new Tween(targetObject, startPos, endPos, Time.time, duration));
            return true;
        }        
    }

    public bool TweenExists(Transform target)
    {
        foreach (Tween activeTween in activeTweens)
        {
            if(activeTween.Target == target)
            {
                return true;
            }
        }
        return false;
    }
}
