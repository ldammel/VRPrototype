using System;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SetUpPlayer : MonoBehaviour
{
    public static SetUpPlayer Instance;
    public string storedName;
    public int storedColorInt;
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
        if (SetUpPlayer.Instance == null)
        {
            SetUpPlayer.Instance = this;
        }
        else
        {
            if (SetUpPlayer.Instance != this)
            {
                Destroy(SetUpPlayer.Instance.gameObject);
                SetUpPlayer.Instance = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetColor(int colorInt)
    {
        storedColorInt = colorInt;
        MasterManager.Instance.GameSettings.SetColorInt(storedColorInt);
    }

    public void SetName(string newName)
    {
        if (nameTooLong) return;
        storedName = newName;
        MasterManager.Instance.GameSettings.SetNickName(storedName);
        PhotonNetwork.NickName = newName;
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
