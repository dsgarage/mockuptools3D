using System;
using UnityEngine;

namespace Script.Player.CharactorController
{
    enum CaractorControllerType
    {
        Move,
        SimpleMove,
    }
    
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]private CaractorControllerType _caractorControllerType = CaractorControllerType.Move;
        
        [SerializeField]private float speed = 3.0F;
        [SerializeField]private float rotateSpeed = 3.0F;
        public float jumpPower = 5;
        private Vector3 moveDirection;
        public float gravity = 10f;
        
        
        [SerializeField]private CharacterController controller;

        void Start()
        {
            // コンポーネントの取得
            controller = GetComponent<CharacterController>();

        }

        void Update()
        {
            switch (_caractorControllerType)
            {
                case CaractorControllerType.Move:
                    CharactorcontrollerMove();
                    break;
                case CaractorControllerType.SimpleMove:
                    CharactorcontrollerSimpleMove();
                    break;

                default:
                    CharactorcontrollerMove();
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        void CharactorcontrollerMove()
        {
            if (controller.isGrounded)
            {
                // 回転
                transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

                // キャラクターのローカル空間での方向
                moveDirection = transform.transform.forward * speed * Input.GetAxis("Vertical");

                // ジャンプ
                if (Input.GetButtonDown("Jump")) moveDirection.y = jumpPower;

            }
            else
            {

                // 重力
                moveDirection.y -= gravity * Time.deltaTime;

            }

            // Move関数で移動させる
            controller.Move(moveDirection * Time.deltaTime);
        }

        void CharactorcontrollerSimpleMove()
        {
            // 回転
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

            // キャラクターのローカル空間での方向
            Vector3 forward = transform.transform.forward;

            float curSpeed = speed * Input.GetAxis("Vertical");

            // SimpleMove関数で移動させる
            controller.SimpleMove(forward * curSpeed);
        }

    }
}
