using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

namespace Networking
{
    public class Helper : MonoBehaviour
    {
        public static T GetCustomProperty<T>(PhotonView view, string property, T defaultValue)
        {
            if (view != null && view.Owner != null && view.Owner.CustomProperties.ContainsKey(property))
            {
                return (T) view.Owner.CustomProperties[property];
            }
            return defaultValue;
        }

        public static void SetCustomProperty<T>(PhotonView view, string property, T defaultValue )
        {
            if (view == null || view.Owner == null) return;
            Hashtable hash = new Hashtable {{property, defaultValue}};
            view.Owner.SetCustomProperties(hash);
        }

    }
}
