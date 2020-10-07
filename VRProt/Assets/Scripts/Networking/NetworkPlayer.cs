using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    public GameObject body;

    private PhotonView photonView;

    private Transform headRig;
    private Transform leftHandRig;
    private Transform rightHandRig;

    // Start is called before the first frame update
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine) body.GetComponent<Renderer>().enabled = false;
        
        XRRig rig = FindObjectOfType<XRRig>();
        headRig = rig.transform.Find("Camera Offset/VR Camera");
        leftHandRig = rig.transform.Find("Camera Offset/Left Hand");
        rightHandRig = rig.transform.Find("Camera Offset/Right Hand");
    }

    // Update is called once per frame
    private void Update()
    {
        if(photonView.IsMine)
        {
            MapPosition(head, headRig);
            MapPosition(leftHand, leftHandRig);
            MapPosition(rightHand, rightHandRig);
        }
    }
    

    void MapPosition(Transform target,Transform rigTransform)
    {    
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}
