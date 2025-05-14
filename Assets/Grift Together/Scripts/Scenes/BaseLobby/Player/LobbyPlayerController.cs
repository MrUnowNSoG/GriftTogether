using Photon.Pun;
using UnityEngine;

namespace GriftTogether {

    public class LobbyPlayerController : MonoBehaviour {

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;

        private PhotonView _photonView;
        private Rigidbody _rigidbody;

        private Vector3 _movementInput;

        private void Awake() {
            _photonView = GetComponent<PhotonView>();
            _rigidbody = GetComponent<Rigidbody>();

            _rigidbody.freezeRotation = true;
        }

        private void Update() {

            if (_photonView.IsMine == false) return;

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            _movementInput = new Vector3(h, 0f, v).normalized;
        }


        private void FixedUpdate() {
            if (_photonView.IsMine == false) return;

            Vector3 moveDelta = _movementInput * moveSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + moveDelta);

   
            if (_movementInput.sqrMagnitude > 0.001f) {
  
                Quaternion targetRot = Quaternion.LookRotation(_movementInput, Vector3.up);
                Quaternion newRot = Quaternion.Slerp(_rigidbody.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);

                _rigidbody.MoveRotation(newRot);
            }
        }
    }
}
