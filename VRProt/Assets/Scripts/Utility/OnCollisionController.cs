using UI.Console;
using UnityEngine;
using UnityEngine.Events;

namespace Utility
{
    [System.Serializable]
    public class UnityEventCollision : UnityEvent<Collision>{}

    [System.Serializable]
    public class CollisionAction : InteractionAction<UnityEventCollision>{}

    public class OnCollisionController : OnInteractionController<CollisionAction, UnityEventCollision>
    {
        private void OnCollisionEnter(Collision other)
        {
            var col = other.collider;
            if (printDebug)DeveloperConsole.Instance.AddLine("Collided with " + col.name);
            if (Actions == null) return;
            if (!Actions.ContainsKey(col.tag)) return;
            Actions[col.tag].OnInteraction?.Invoke(other);
        }
    }
}