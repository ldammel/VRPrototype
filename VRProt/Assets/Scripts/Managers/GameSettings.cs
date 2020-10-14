using UnityEngine;

namespace Managers
{
    [CreateAssetMenu(menuName = "Manager/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private string gameVersion = "0.0.0";
        public string GameVersion => gameVersion;

        [SerializeField] private string nickName = "Player";
        public string NickName => nickName;
    
        [SerializeField] private Color myColor = Color.white;
        public Color MyColor => myColor;

        public void SetNickName(string nick)
        {
            nickName = nick;
        }

        public void SetColor(Color newColor)
        {
            myColor = newColor;
        }
    }
}
