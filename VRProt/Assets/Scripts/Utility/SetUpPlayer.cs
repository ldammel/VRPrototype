using System;
using UnityEngine;


public class SetUpPlayer : MonoBehaviour
{
    public static SetUpPlayer Instance;
    public string storedName;
    public int storedColorInt;

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

    private void Start()
    {
        if (PlayerPrefs.HasKey("MyColorInt"))
        {
            storedColorInt = PlayerPrefs.GetInt("MyColorInt");
        }
        else
        {
            PlayerPrefs.SetInt("MyColorInt",1);
        }
        
        if (PlayerPrefs.HasKey("MyName"))
        {
            storedName = PlayerPrefs.GetString("MyName");
        }
        else
        {
            PlayerPrefs.SetString("MyName","Player");
        }
    }

    public void SetColor(int colorInt)
    {
        storedColorInt = colorInt;
        PlayerPrefs.SetInt("MyColorInt",storedColorInt);
    }

    public void SetName(string newName)
    {
        storedName = newName;
        PlayerPrefs.SetString("MyName",storedName);
    }


}
