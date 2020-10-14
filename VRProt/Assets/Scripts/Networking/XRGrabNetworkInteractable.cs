using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

namespace Networking
{
    public class XRGrabNetworkInteractable : XRGrabInteractable
    {
        private PhotonView photonView;

        private void Start()
        {
            photonView = GetComponent<PhotonView>();
        }

        protected override void OnSelectEnter(XRBaseInteractor interactor)
        {
            photonView.RequestOwnership();
            base.OnSelectEnter(interactor);
        }
    }
}
