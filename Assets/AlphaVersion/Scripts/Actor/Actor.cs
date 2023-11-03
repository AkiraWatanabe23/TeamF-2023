using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    public enum ActorType
    {
        Male,
        Female,
        Muscle,
        Robber,
    }

    public enum BehaviorType
    {
        Customer,
        Robber,
    }

    public class Actor : MonoBehaviour
    {
        [SerializeField] ActorType _actorType;
        [SerializeField] BehaviorType _behaviorType;

        Waypoint _lead;

        public ActorType ActorType => _actorType;
        public BehaviorType BehaviorType => _behaviorType;
    }
}
