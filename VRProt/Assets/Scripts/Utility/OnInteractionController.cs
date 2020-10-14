using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    [System.Serializable]
    public abstract class InteractionAction<T_UNITY_EVENT>
    {
        [SerializeField] 
        private List<string> tags;
        public List<string> Tags => tags;

        [SerializeField] 
        private T_UNITY_EVENT onInteraction;
        public T_UNITY_EVENT OnInteraction => onInteraction;
    }

    public abstract class OnInteractionController<T_INTERACTION, T_UNITY_EVENT> : MonoBehaviour
        where T_INTERACTION : InteractionAction<T_UNITY_EVENT>
    {
        [SerializeField]
        protected bool printDebug = false;
        [SerializeField] private List<T_INTERACTION> interactions;

        protected Dictionary<string, T_INTERACTION> Actions;

        private void Start()
        {
            InitActions();
        }

        private void InitActions()
        {
            Actions = new Dictionary<string, T_INTERACTION>();
            foreach (var action in interactions)
            {
                foreach (var tags in action.Tags)
                    Actions.Add(tags, action);
            }
        }
    }
}