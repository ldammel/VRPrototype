using Photon.Realtime;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private string gameVersion = "0.0.0";
    public string GameVersion => gameVersion;

    [SerializeField] private string nickName = "Player";
    public string NickName => nickName;
    
    [SerializeField] private int colorInt = 1;
    public int ColorInt => colorInt;

    public void SetNickName(string nick)
    {
        nickName = nick;
    }

    public void SetColorInt(int newColorInt)
    {
        colorInt = newColorInt;
    }
}
