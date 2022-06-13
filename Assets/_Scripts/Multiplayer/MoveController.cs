using LucidSightTools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colyseus;

public class MoveController : ExampleNetworkedEntityView
{

	/*
	private CharacterController controller;
	private Vector3 playerVelocity;
	private bool groundedPlayer;
	private float playerSpeed = 2.0f;
	private float jumpHeight = 1.0f;
	private float gravityValue = -9.81f;


	// Use this for initialization
	public float speed = 6.0f;
	float rotatespeed = 1.0f;
	public float jumpSpeed = 8.0f;
	//   public float gravity = 20.0f;
	*/
	public float speed = 6.0f;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	Vector3 pos;
	Transform thisTrans;
	public Camera cam;

	public Transform target;
	float strength = 5;
	public float moveSpeed = 3;

	Rigidbody rb;
	Vector3 velocity;



	protected override void Start()
	{
		autoInitEntity = false;
		base.Start();
		/*
		controller = gameObject.AddComponent<CharacterController>();
		*/
		controller = GetComponent<CharacterController>();
		// let the gameObject fall down
		thisTrans = transform;

		rb = GetComponent<Rigidbody>();
		cam = Camera.main;

	//	this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

		StartCoroutine("WaitForConnect");
	}

	IEnumerator WaitForConnect()
    {
        if (ExampleManager.Instance.CurrentUser != null && !IsMine) yield break;

		while(!ExampleManager.Instance.IsInRoom)
		{
			yield return 0;
		}
		LSLog.LogImportant("HAS JOINED ROOM - CREATING ENTITY");
		ExampleManager.CreateNetworkedEntityWithTransform(new Vector3(0f, 0f, 0f), Quaternion.identity, new Dictionary<string, object>() { ["prefab"] = "VMEViewPrefab" }, this, (entity) => {
			LSLog.LogImportant($"Network Entity Ready {entity.id}");
		});
	}

	public override void OnEntityRemoved()
	{
		base.OnEntityRemoved();
		LSLog.LogImportant("REMOVING ENTITY", LSLog.LogColor.lime);
		Destroy(this.gameObject);
	}

	protected override void Update()
	{
		base.Update();
		/*
		if (!HasInit || !IsMine) return;
		/*
		groundedPlayer = controller.isGrounded;
		if (groundedPlayer && playerVelocity.y < 0)
		{
			playerVelocity.y = 0f;
		}
	

		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		controller.Move(move * Time.deltaTime * playerSpeed);

		if (move != Vector3.zero)
		{
			gameObject.transform.forward = move;
		}

		// Changes the height position of the player..
		if (Input.GetButtonDown("Jump") && groundedPlayer)
		{
			playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
		}

		playerVelocity.y += gravityValue * Time.deltaTime;
		controller.Move(playerVelocity * Time.deltaTime);
		*/
		moveDirection = new Vector3(0.0f, 0.0f, 1);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection = moveDirection * speed;

		// Move the controller
		controller.Move(moveDirection * Time.deltaTime);
		Vector3 pos = thisTrans.position;
		if (pos.y != 0.0f)

		{
			thisTrans.position = new Vector3(pos.x, 0, pos.z);

		}
		//limiting Y
		if (Application.isEditor)
		{
			// thisTrans.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * 300, 0, Space.World);
		}

		{
			thisTrans.localRotation = Quaternion.LookRotation(TouchRotateSingle.eulerRotation);
			
		
		//	thisTrans.localRotation = Quaternion.LookRotation(TouchRotateSingle.eulerRotation);

		}


		if (Input.GetKeyDown(KeyCode.F))
		{
			LSLog.Log("fire weapon key");
			ExampleManager.RFC(state.id, "FireWeaponRFC", new object[] { new ExampleVector3Obj(transform.forward), "aWeapon" });
		}

	}

	public void FireWeaponRFC(object v3, string weaponType)
	{
		ExampleVector3Obj aVec = ParseRFCObject<ExampleVector3Obj>(v3);

		GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		bullet.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
		bullet.transform.LookAt(new Vector3((float)aVec.x, (float)aVec.y, (float)aVec.z) * 100f);
		bullet.AddComponent<SphereCollider>();
		var rb = bullet.AddComponent<Rigidbody>();
		rb.AddForce(bullet.transform.forward * 15f, ForceMode.Impulse);
	}

}
