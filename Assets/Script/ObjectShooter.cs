using System;
using UnityEngine;

public class ObjectShooter : MonoBehaviour
{
	//　弾のプレハブ
	[SerializeField] private GameObject bullet;
	//　レンズからのオフセット値
	[SerializeField] private float offset;

	[SerializeField] private float waitTime = 0.1f;
	
	//　経過時間
	private float elapsedTime = 0f;
	
	private void Start()
	{
		if (!bullet)
		{
			SetBullet();
		}
	}

	void Update () {
		
		elapsedTime += Time.deltaTime;
		if (elapsedTime < waitTime) {
			return;
		}

		if (Input.GetButton ("Fire1")) {
			//　カメラのレンズの中心を求める
			var centerOfLens = UnityEngine.Camera.main.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, UnityEngine.Camera.main.nearClipPlane + offset));
			//　カメラのレンズの中心から弾を飛ばす
			var bulletObj = Instantiate (bullet, centerOfLens, Quaternion.identity) as GameObject;
		}
		
		if (bullet == null)
		{
			return;
		}

	}

	private void SetBullet()
	{
		SetBullet(GameObject.CreatePrimitive(PrimitiveType.Sphere));
	}
	
	private void SetBullet(GameObject bulletObject)
	{
		bullet = bulletObject;
		try
		{
			bullet.GetComponent<BuletComponent>();
			bullet.GetComponent<Rigidbody>().isKinematic = true;

		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			bullet.AddComponent<BuletComponent>();
			bullet.GetComponent<Rigidbody>().isKinematic = false;
			throw;
		}
	}
}
