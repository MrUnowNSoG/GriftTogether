using Photon.Pun;
using TMPro;
using UnityEngine;

namespace GriftTogether {

    public class SkinServicePhotonAgent : SkinServiceAgent, IPunInstantiateMagicCallback {

        [SerializeField] private TextMeshProUGUI _userName;

        public void SetUserName(string str) {
            _userName.text = str;
        }

        public void OnPhotonInstantiate(PhotonMessageInfo info) {
            
            PhotonView photonView = gameObject.GetComponent<PhotonView>();
            GameRoot.ServiceLocator.Resolve(out SkinService skinService);

            SetUserName(photonView.Owner.NickName);

            var data = photonView.InstantiationData;
            string hat = (string)data[0];
            string color = (string)data[1];
            string face = (string)data[2];

            skinService.SetSkinAgent(this);
            skinService.RecreatePlayerSkin(hat, color, face);
            skinService.ResolveCurrentSkin();
        }
    }
}
