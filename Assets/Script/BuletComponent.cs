using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuletComponent : MonoBehaviour
{
	[SerializeField] private float power = 50f;
	[SerializeField] private float deleteTime = 10f;
	private Rigidbody rigid;
	private Ray ray;
 
	void Awake() {
		//　Rigidbodyを取得し速度を0に初期化
		rigid = GetComponent <Rigidbody> ();
	}
 
	//　弾がアクティブになった時
	void OnEnable() {
 
		//　カメラからクリックした位置にレイを飛ばす
		ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
 
		//　弾を発射してから指定した時間が経過したら自動で削除
		Destroy (this.gameObject, deleteTime);
	}
 
	void OnCollisionEnter(Collision col) {
		// Enemyタグがついた敵に衝突したら自身と敵を削除
		if (col.gameObject.CompareTag ("Enemy")) {
			Destroy (this.gameObject);
			Destroy (col.gameObject);
		}
	}
	//　弾が存在していればレイの方向に力を加える
	void FixedUpdate() {
		rigid.AddForce (ray.direction * power, ForceMode.Force);
	}
}
