using UnityEngine;

namespace Script.Player.RigidbodyMove
{

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float rotateSpeed = 120f;
        float jumpPower = 5;
        
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>(); // Rigidbody
            
            
            _rigidbody.constraints = 
                RigidbodyConstraints.FreezeRotationX | 
                RigidbodyConstraints.FreezeRotationZ;
            /// Rigidbodyを使うと転んでしまうのでRotateを固定
        }

        void FixedUpdate()
        {
            //　ジャンプ、ジャンプ中はRigidbodyの重力を無効化する
            if(Input.GetButtonDown("Jump")
               // && !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")
               // && !animator.IsInTransition(0)		//　遷移途中にジャンプさせない条件
            ) {
               // animator.SetBool("Jump", true);
               _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, jumpPower, _rigidbody.velocity.z);
            }
            
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 velocity = new Vector3(0, 0, v);

            // キャラクターのローカル空間での方向に変換
            velocity = transform.TransformDirection(velocity);

            // キャラクターの移動
            transform.localPosition += velocity * speed * Time.fixedDeltaTime;

            // キャラクターの回転
            transform.Rotate(0, h * rotateSpeed * Time.fixedDeltaTime, 0);
        }

    }
}
