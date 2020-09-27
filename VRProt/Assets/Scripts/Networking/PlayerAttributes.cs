using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameField;
    [SerializeField] private MeshRenderer mRenderer;
    [SerializeField] private PhotonView photonView;
    private int colorInt;
    public bool isImposter;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine) photonView.RPC("LoadAttributes", RpcTarget.AllBuffered);
    }

    public void StartGames()
    {
        var manager = FindObjectOfType<NetworkManager>();
        manager.StartGame();
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Imposter", out object value))
        {
            isImposter = (bool)value;
        }
    }

    [PunRPC]
    public void LoadAttributes()
    {
        colorInt = SetUpPlayer.Instance.storedColorInt;
        switch (colorInt)
        {
            case 1:
                mRenderer.material.color = Color.blue;   
                break;
            case 2:
                mRenderer.material.color = Color.red;   
                break;
            case 3:
                mRenderer.material.color = Color.green;   
                break;
            case 4:
                mRenderer.material.color = Color.yellow;   
                break;
            default:
                break;
        }

        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Imposter", out object value))
        {
            isImposter = (bool)value;
        }
        nameField.text = SetUpPlayer.Instance.storedName;
        Debug.Log("Setting name and color for: " + photonView.ViewID);
    }
}
