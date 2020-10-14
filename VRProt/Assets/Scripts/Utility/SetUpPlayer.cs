using Managers;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Utility
{
    public class SetUpPlayer : MonoBehaviour
    {
        public static SetUpPlayer Instance;
        public string storedName;
        public Color storedColor;
        public int maxCharCount = 10;

        [SerializeField] private GameObject nameTooLongSign;
        [SerializeField] private GameObject roomTooLongSign;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private GameObject nameButton;
        [SerializeField] private GameObject roomButton;
        public UnityEvent onChangeName;
        private bool nameTooLong;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if (Instance != this)
                {
                    Destroy(Instance.gameObject);
                    Instance = this;
                }
            }
            DontDestroyOnLoad(gameObject);
        }

        public void SetColor(string colorString)
        {
            var color = storedColor;
            switch (colorString)
            {
                case "Blue":
                    color = Color.blue;
                    break;
                case "Red":
                    color = Color.red;
                    break;
                case "Green":
                    color = Color.green;
                    break;
                case "Yellow":
                    color = Color.yellow;
                    break;
                case "Aqua":
                    color = Color.cyan;
                    break;
                case "Orange":
                    color = new Color(255,145,0,255);
                    break;
                case "Purple":
                    color = Color.magenta;
                    break;
                case "DarkPurple":
                    color = new Color(120,0,255,255);
                    break;
                default:
                    break;
            }
            storedColor = color;
            MasterManager.Instance.GameSettings.SetColor(storedColor);
        }

        public void SetName(string newName)
        {
            if (nameTooLong) return;
            storedName = newName;
            MasterManager.Instance.GameSettings.SetNickName(storedName);
            PhotonNetwork.LocalPlayer.NickName = newName;
            nameText.text = storedName;
        }

        public void CheckNameLength(string checkName)
        {
            if (checkName.Length > maxCharCount)
            {
                nameTooLongSign.SetActive(true);
                roomTooLongSign.SetActive(true);
                nameTooLong = true;
                roomButton.SetActive(false);
                nameButton.SetActive(false);
            }
            else
            {
                nameTooLongSign.SetActive(false);
                roomTooLongSign.SetActive(false);
                nameTooLong = false;
                roomButton.SetActive(true);
                nameButton.SetActive(true);
            }
        }

        public void InvokeEvent()
        {
            if (nameTooLong) return;
            onChangeName.Invoke();
        }


    }
}
