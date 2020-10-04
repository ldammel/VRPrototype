using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

public class Helper : MonoBehaviour
{

    public static Hashtable offlineValues;

    public static T GetCustomProperty<T>(PhotonView view, string property, T offlineValue, T defaultValue)
    {
        if (PhotonNetwork.OfflineMode)
        {
            return offlineValue;
        }
        else
        {
            if (view != null && view.Owner != null && view.Owner.CustomProperties.ContainsKey(property))
            {
                return (T) view.Owner.CustomProperties[property];
            }

            return defaultValue;
        }
    }

    public static void SetCustomProperty<T>(PhotonView view, string property, T offlineValue, T defaultValue )
    {
        if (PhotonNetwork.OfflineMode)
        {
            if (view != null && view.Owner != null)
            {
                offlineValues.Add(property, offlineValue);
            }
        }
        else
        {
            if (view != null && view.Owner != null)
            {
                Hashtable hash = new Hashtable {{property, defaultValue}};
                view.Owner.SetCustomProperties(hash);
            }
        }
    }

}
