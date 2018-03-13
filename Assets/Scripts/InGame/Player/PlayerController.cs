using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
	#region Variables

	[HideInInspector]
	public int IdPlayer;
	public int LifePlayer = 3;
	public int TimeToRegen = 3;
	public float MoveSpeed;
	[Range(0,1)]
	public float SlowDriveBack = 0.5f;
	public GameObject ItemLostObj;
	public float radialDeadZone = 0.3f;
	public float maxAngle = 2;

	//public float DashDistance = 5;
	//public float DashTime = 1;
	public float DistToDropItem = 1;
	public int nbItemBeforeBigBag = 10;
	public Animator animPlayer;
	public Transform WeaponPos;
	public Transform BagPos;
	public Transform BoxPlace;
	public float DistProjDead;
	public float TimeProjDead;
	public float TimeDead = 1;
	public float TimeInvincible;
	public int PourcLootLost = 20;
	[HideInInspector]
	public float CdShoot = 0;

	[HideInInspector]
	public float GetSpeed = 0;

	[HideInInspector]
	public float SpeedReduce;
	[Range (0, 1)]
	[Tooltip ("Speed reduce pendant qu'on pousse la caisse")]
	public float SpeedReduceOnBox = 0.1f;
	public float SmoothRotateOnBox = 10;

	public float aimSensitivity = 10;
	public float aimSensitivityEnemy = 2;

	[HideInInspector]
	public List<GameObject> AllItem;

	[HideInInspector]
	public bool driveBox = false;
	[HideInInspector]
	public bool dead = false;

	[HideInInspector]
	public List<EnemyInfo> AllEnemy;

	// ----- Score
	[HideInInspector]
	public int CurrScore = 0;
	[HideInInspector]
	public int CurrLootScore = 0;
	[HideInInspector]
	public int CurrKillScore = 0;
	[HideInInspector]
	public int NbrDead = 0;
	[HideInInspector]
	public int WeaponSwitch = 0;
	[HideInInspector]
	public int WeaponThrow = 0;
	[HideInInspector]
	public int WeaponCatch = 0;
	[HideInInspector]
	public int ShootBullet = 0;
	[HideInInspector]
	public int ShootSucceed = 0;
	[HideInInspector]
	public float TimeWBox = 0;
	[HideInInspector]
	public int SpawmShoot = 0;
	[HideInInspector]
	public float TotalDist = 0;
	[HideInInspector]
	public int NbrTouchInteract = 0;
	[HideInInspector]
	public int NbrChest = 0;
	[HideInInspector]
	public int NbrEnemy = 0;
	[HideInInspector]
	public int currentEnemy = 0;
	[HideInInspector]
	public int LostItem = 0;
	[HideInInspector]
	public bool checkAward = false;
	// -----
	public Transform AmmoUI;
	public float UiAmmoX;
	public float UiAmmoY;
	[HideInInspector]
	public int CurrItem = 0;
    
	[HideInInspector]
	public Image UiAmmo;
	[HideInInspector]
	public InteractAbstract currInt;
	[HideInInspector]
	public bool autoShoot = true;
	public bool canCauldron = false;
	PlayerController thisPC;
	WeaponAbstract thisWeapon;
	Transform thisTrans;
	Transform getBoxWeapon;
	Rigidbody thisRig;
	CameraFollow GetCamFoll;
	Player inputPlayer;
	Camera getCam;
	WeaponBox thisWB;

	int lifePlayer;

	bool shooting = false;
	bool dashing = false;
	bool canDash = true;
	bool canEnterBox = false;
	bool canShoot = true;
	bool checkShoot = true;
	bool canTakeDmg = true;
	bool checkShootScore = true;

	bool checkAuto = false;
	bool checkUIBorder = false;
	bool checkUIBorderY = false;
	bool checkUpdate = true;

	public Text WeapText;
	Tween tweenRegen;

	#endregion

	#region Mono

	void Awake ()
	{
		AllEnemy = new List<EnemyInfo> ();
		lifePlayer = LifePlayer;
		thisPC = GetComponent<PlayerController> ();

		System.Array thisArray = System.Enum.GetValues (typeof(TypeEnemy));

		for (int a = 0; a < thisArray.Length; a++) {
			AllEnemy.Add (new EnemyInfo ());
			AllEnemy [a].ThisType = (TypeEnemy)thisArray.GetValue (a);
		}
	}

	void Start ()
	{
		thisTrans = transform;
		thisRig = GetComponent<Rigidbody> ();

		AmmoUI.gameObject.SetActive (true);
		UiAmmo = AmmoUI.Find ("Ammo Inside").GetComponent<Image> ();
		inputPlayer = ReInput.players.GetPlayer (IdPlayer);
		getBoxWeapon = Manager.GameCont.WeaponB.transform;
		getCam = Manager.GameCont.MainCam;
		GetCamFoll = Manager.GameCont.GetCameraFollow;
		thisWB = getBoxWeapon.GetComponent<WeaponBox> ();
	}

	void Update ()
	{
		thisRig.velocity = Vector3.zero;

		if (!checkUpdate) {
			return;
		}

		float getDeltaTime = Time.deltaTime;

		if (!dead) {
			inputAction (getDeltaTime);
		}

		checkBorder ();

		if (!checkUIBorder) {
			//AmmoUI.localScale = Vector3.one;

			AmmoUI.position = getCam.WorldToScreenPoint (thisTrans.position - Vector3.right * UiAmmoX + Vector3.up * UiAmmoY);
		} else {
			//AmmoUI.localScale = new Vector3(-1, 1, 1);

			AmmoUI.position = getCam.WorldToScreenPoint (thisTrans.position + Vector3.right * UiAmmoX + Vector3.up * UiAmmoY);
		}

		if (AllItem.Count > 0 && Vector3.Distance (thisTrans.position, getBoxWeapon.position) < DistToDropItem) 
		{
			emptyBag ();
		}
	}

	#endregion

	#region Public Methods

	public void UpdateWeapon (WeaponAbstract thisWeap = null)
	{
		if (thisWeap != null) {
			thisWeap.OnFloor = false;
			autoShoot = thisWeap.AutoShoot;
			SpeedReduce = thisWeap.SpeedReduce;
			thisWeapon = thisWeap;
			CdShoot = thisWeap.CoolDown;
		} else {
			thisWeapon = null;
		}
	}

	public void GetDamage (Transform thisEnemy, int intDmg = 1)
	{
		if (canTakeDmg && checkUpdate) {
			lifePlayer -= intDmg;

			if (lifePlayer <= 0 && !dead) {
				animeDead (thisEnemy.position);
			}
		}

	}

	#endregion

	#region Private Methods

	void checkBorder ()
	{
		Vector3 getCamPos = getCam.WorldToViewportPoint (thisTrans.position);
		Vector3 getDir = Vector3.zero;

		if (getCamPos.x > 0.97f) {
			getDir -= Vector3.right;
			thisTrans.position = new Vector3 (getCam.ViewportToWorldPoint (new Vector3 (0.97f, getCamPos.y, getCamPos.z)).x, thisTrans.position.y, thisTrans.position.z);
		} else if (getCamPos.x < 0.03f) {
			getDir += Vector3.right;
			thisTrans.position = new Vector3 (getCam.ViewportToWorldPoint (new Vector3 (0.03f, getCamPos.y, getCamPos.z)).x, thisTrans.position.y, thisTrans.position.z);
		}

		if (getCam.WorldToViewportPoint (thisTrans.position - Vector3.right * UiAmmoX).x < 0.03f) {
			checkUIBorder = true;
		} else {
			checkUIBorder = false;
		}

		if (getCamPos.y > 0.85f) {
			getDir -= Vector3.up;
			thisTrans.position = new Vector3 (thisTrans.position.x, thisTrans.position.y, getCam.ViewportToWorldPoint (new Vector3 (getCamPos.x, 0.85f, getCamPos.z)).z);
		} else if (getCamPos.y < 0.03f) {
			getDir += Vector3.up;
			thisTrans.position = new Vector3 (thisTrans.position.x, thisTrans.position.y, getCam.ViewportToWorldPoint (new Vector3 (getCamPos.x, 0.03f, getCamPos.z)).z);
		}

		if (getDir != Vector3.zero) {
			RaycastHit[] allHit;
			string getTag;

			allHit = Physics.RaycastAll (thisTrans.position, getDir, 0.5f);

			foreach (RaycastHit thisRay in allHit) {
				getTag = thisRay.collider.tag;

				if (getTag == Constants._Wall) {

					checkUpdate = false;

					thisTrans.DOKill (true);
					thisTrans.DOMove (getBoxWeapon.position, 0.5f, true).OnComplete (() => {
						checkUpdate = true;
					});
					break;
				}
			}
		}
	}

	float checkBorderDead (Vector3 customPos)
	{
		Vector3 getCamPos = getCam.WorldToViewportPoint (customPos);
		Vector3 getDist = customPos;

		if (getCamPos.x > 0.97f) {
			customPos = new Vector3 (getCam.ViewportToWorldPoint (new Vector3 (0.97f, getCamPos.y, getCamPos.z)).x, customPos.y, customPos.z);
		} else if (getCamPos.x < 0.03f) {
			customPos = new Vector3 (getCam.ViewportToWorldPoint (new Vector3 (0.03f, getCamPos.y, getCamPos.z)).x, customPos.y, customPos.z);
		}

		if (getCamPos.y > 0.97f) {
			customPos = new Vector3 (customPos.x, customPos.y, getCam.ViewportToWorldPoint (new Vector3 (getCamPos.x, 0.97f, getCamPos.z)).z);
		} else if (getCamPos.y < 0.03f) {
			customPos = new Vector3 (customPos.x, customPos.y, getCam.ViewportToWorldPoint (new Vector3 (getCamPos.x, 0.03f, getCamPos.z)).z);
		}

		return Vector3.Distance (customPos, getDist);
	}

	void inputAction (float getDeltaTime)
	{
		playerShoot (getDeltaTime);

		if (canDash) {
			//playerDash ( );
		}

		if (!dashing) 
		{
			interactPlayer ();
			playerMove (getDeltaTime);
		}

		playerAim (getDeltaTime);
	}

	void playerMove (float getDeltaTime)
	{
		float Xmove = inputPlayer.GetAxis ("MoveX");
		float Ymove = inputPlayer.GetAxis ("MoveY");

		float speed = Mathf.Abs (Xmove) + Mathf.Abs (Ymove) * 2;

		animPlayer.SetFloat ("Velocity", speed);
		float getSpeed = MoveSpeed;

		if (shooting) 
		{
			getSpeed *= SpeedReduce;
		} 
		else if (driveBox) 
		{
			Xmove = 0;
			thisWB.CurrTime += getDeltaTime;
			thisWB.ThisGauge.value = thisWB.CurrTime;
			TimeWBox += getDeltaTime;
			getSpeed *= SpeedReduceOnBox;
		}

		getSpeed = getDeltaTime * getSpeed;
		TotalDist += getSpeed;

		if ( !driveBox )
		{
			thisTrans.position += getSpeed * new Vector3 (Xmove, 0, Ymove);
		}
		else if ( Mathf.Abs(Ymove)  > radialDeadZone )
		{
			Quaternion newAngle = Quaternion.LookRotation (new Vector3 (Xmove, 0, Ymove), thisTrans.up);

			if ( Ymove < 0 )
			{
				Ymove *= SlowDriveBack;
			}
			thisTrans.position += getSpeed * Ymove * thisTrans.forward;
		}
	}

	void playerAim (float getDeltaTime)
	{
		float Xaim = inputPlayer.GetAxis ("AimX");
		float Yaim = inputPlayer.GetAxis ("AimY");

		Vector2 stickInput = new Vector2 (Xaim, Yaim);

		if (stickInput.magnitude < radialDeadZone) 
		{
			stickInput = Vector2.zero;
		} 
		else 
		{
			float actuFocus = aimSensitivity;

			if ( thisWeapon != null )
			{
				RaycastHit hit;
				if (Physics.Raycast (thisWeapon.SpawnBullet.position, thisTrans.forward, out hit)) 
				{
					if (hit.transform.tag == Constants._Enemy) 
					{
						actuFocus = aimSensitivityEnemy;
					}
				}
			}

			Quaternion newAngle = Quaternion.LookRotation (new Vector3 (Xaim, 0, Yaim), thisTrans.up);

			float difAngle = Quaternion.Angle (thisTrans.rotation, newAngle);

			if (difAngle > maxAngle) 
			{
				if (driveBox) 
				{
					thisTrans.localRotation = Quaternion.Slerp (thisTrans.rotation, newAngle, SmoothRotateOnBox * getDeltaTime);
				} 
				else 
				{
					thisTrans.localRotation = Quaternion.Slerp (thisTrans.rotation, newAngle, actuFocus * getDeltaTime);
					// thisTrans.localRotation = Quaternion.LookRotation(new Vector3(Xaim, 0, Yaim), thisTrans.up);
				}
			}
		}

	}

	void playerShoot (float getDeltaTime)
	{
		float shootInput = inputPlayer.GetAxis ("Shoot");
		if (!canShoot) 
		{
			if (shootInput > 0) 
			{
				if (driveBox) 
				{
					thisWB.AttackCauld ();
					return;
				}

				useBoxWeapon ();
			} 
			else 
			{
				return;
			}

		}

		if (shootInput == 1 && checkShootScore) {
			checkShootScore = false;
			SpawmShoot++;
		} else if (shootInput < 0.3f) {
			checkShootScore = true;
		}

		if (shootInput == 0 && checkAuto) {
			checkShoot = true;
		}

		if (shootInput > 0.3f && thisWeapon != null && checkShoot) {
			if (!autoShoot) {
				checkAuto = false;
				checkShoot = false;
				DOVirtual.DelayedCall (CdShoot, () => {
					checkAuto = true;
				});
			}

			thisWeapon.weaponShoot (thisTrans);
			animPlayer.SetBool ("Attack", true);
			shooting = true;
			if (thisWeapon != null) { //&& thisWeapon.Damage == 1)
				//Debug.Log("Shoot");
				if (thisWeapon.Damage <= Manager.VibM.DamagesLow)
					Manager.VibM.ShootLowVibration (inputPlayer);
				else if (thisWeapon.Damage > Manager.VibM.DamagesLow && thisWeapon.Damage <= Manager.VibM.DamagesMedium)
					Manager.VibM.ShootMediumVibration (inputPlayer);
				else if (thisWeapon.Damage > Manager.VibM.DamagesLow && thisWeapon.Damage <= Manager.VibM.DamagesMedium)
					Manager.VibM.ShootHighVibration (inputPlayer);
			}
		} else {
			animPlayer.SetBool ("Attack", false);
			shooting = false;
		}
	}

	public void AddItem ()
	{
		if (AllItem.Count <= nbItemBeforeBigBag) 
		{
			animPlayer.SetTrigger ("Bag_Up");
		} else if (AllItem.Count > nbItemBeforeBigBag) 
		{
			animPlayer.SetTrigger ("Bag_Up2");
		}
	}

	void interactPlayer ()
	{
		bool interactInput = inputPlayer.GetButtonDown ("Interact");

		if (interactInput) {
			NbrTouchInteract++;
			if (canEnterBox || driveBox) {
				useBoxWeapon ();
				return;
			}
		}
		if (inputPlayer.GetButtonDown ("Cauldron")) {   
			if (canCauldron) {
				if (thisWeapon != null) {
					Destroy (thisWeapon.gameObject);
				}

				Manager.GameCont.WeaponB.NewWeapon (thisPC);
			} else if (driveBox) {
				thisWB.ActionSpe ();
			} else if (currInt != null) {
				currInt.OnInteract (thisPC);
				AddItem ();
			}
		}
	}

	void useBoxWeapon ()
	{
		if (Manager.GameCont.WeaponB.CanControl) 
		{
			thisWB.ThisGauge.gameObject.SetActive (true);
			Manager.Ui.CauldronButtons (true);
			GetCamFoll.UpdateTarget (thisTrans);
			WeaponPos.gameObject.SetActive (false);
			AmmoUI.GetComponent<CanvasGroup> ().alpha = 0;
			canShoot = false;
			Physics.IgnoreCollision (GetComponent<Collider> (), getBoxWeapon.GetComponent<Collider> (), true);
			Manager.GameCont.WeaponB.CanControl = false;
			getBoxWeapon.SetParent (BoxPlace);

			getBoxWeapon.DOLocalMove (Vector3.zero, 0.5f);
			getBoxWeapon.DOLocalRotateQuaternion (Quaternion.identity, 0.5f);

			driveBox = true;
		} 
		else if (driveBox)
		 {
			thisWB.GetComponent<Collider>().isTrigger = false;
			thisWB.gameObject.tag = Constants._BoxTag;
			thisWB.transform.DOKill(true);
			thisWB.ThisGauge.gameObject.SetActive (false);
			Manager.Ui.checkDrive = false;
			Manager.Ui.CauldronButtons (false);
			AmmoUI.GetComponent<CanvasGroup> ().alpha = 1;
			getBoxWeapon.DOKill ();
			WeaponPos.gameObject.SetActive (true);
			GetCamFoll.UpdateTarget (getBoxWeapon);

			canShoot = true;
			Physics.IgnoreCollision (GetComponent<Collider> (), getBoxWeapon.GetComponent<Collider> (), false);
			Manager.GameCont.WeaponB.CanControl = true;
			getBoxWeapon.SetParent (null);

			driveBox = false;
		}
	}

	void emptyBag ()
	{
		Manager.Ui.PopPotions (PotionType.Plus);
		animPlayer.SetTrigger ("BagUnfull");
		GameObject[] getBagItems = AllItem.ToArray ();
		Transform getBoxTrans = getBoxWeapon;
		Transform currTrans;

		Manager.GameCont.WeaponB.AddItem (CurrItem);
		CurrScore += CurrItem;
		CurrLootScore += CurrItem;

		for (int a = 0; a < getBagItems.Length; a++) 
		{
			currTrans = getBagItems [a].transform;
			currTrans.DOKill ();

			currTrans.gameObject.SetActive (true);
			currTrans.SetParent (null);
			currTrans.SetParent (getBoxTrans);

			currTrans.position = BagPos.position + new Vector3 (Random.Range (-0.2f, 0.21f), 3, Random.Range (-0.2f, 0.21f));
			
			dropItem (currTrans);
		}
		AllItem.Clear ();
	}

	void dropItem (Transform currTrans)
	{
		DOVirtual.DelayedCall (Random.Range (0, 0.2f), () => {
			currTrans.DOLocalMove (Vector3.zero + Vector3.up * 3, 1f).OnComplete (() => {
				currTrans.DOLocalMove (Vector3.zero, 0.5f);
				currTrans.DOScale (Vector3.one, 0.5f).OnComplete (() => {
					currTrans.DOScale (Vector3.zero, 0.7f).OnComplete (() => {
						Destroy (currTrans.gameObject);
					});
				});
			});
		});
	}

	void animeDead (Vector3 pointColl)
	{
		currentEnemy = 0;
		NbrDead++;
		canTakeDmg = false;
		dead = true;

		if (driveBox) 
		{
			useBoxWeapon ();
		}

		WeaponPos.gameObject.SetActive (false);
		GetComponent<Collider> ().isTrigger = true;

		Vector3 getDirect = Vector3.Normalize (thisTrans.position - pointColl);
		getDirect = new Vector3 (getDirect.x, thisTrans.localPosition.y, getDirect.z);

		float getDist = DistProjDead;
		float getTime = TimeProjDead;
		string getTag;

		RaycastHit[] allHit;

		getDist -= checkBorderDead (thisTrans.position + getDirect * DistProjDead);
		allHit = Physics.RaycastAll (thisTrans.position, getDirect, getDist);

		foreach (RaycastHit thisRay in allHit) 
		{
			getTag = thisRay.collider.tag;

			if (getTag == Constants._Wall) 
			{
				if (thisRay.distance < getDist) 
				{
					getDist = thisRay.distance - 1;
					getTime = TimeProjDead / (DistProjDead / getDist);
				}
			}
		}

		thisTrans.DOKill ();

		thisTrans.DOLocalMove (thisTrans.localPosition + getDirect * getDist, getTime);

		DOVirtual.DelayedCall (getTime + TimeDead + TimeProjDead - getTime, () => {
			GetComponent<Collider> ().isTrigger = false;
			WeaponPos.gameObject.SetActive (true);
			lifePlayer = LifePlayer;
			dead = false;
			thisWeapon.canShoot = true;

			DOVirtual.DelayedCall (TimeInvincible, () => {
				canTakeDmg = true;
			});
		});
		/*for ( a = 0; a < getList.Length; a ++ )
        {
            Destroy(getList[a]);	
        }*/

		lostItem();
	}

	void lostItem ( )
	{
		GameObject[] getList = AllItem.ToArray ();
		if (getList.Length > 0 ) 
		{
			int a;
			ItemLost getItem;
			GameObject newObj = (GameObject)Instantiate (ItemLostObj, thisTrans.position, thisTrans.rotation);
			GameObject newObjUi = (GameObject)Instantiate (Manager.Ui.PotionGet, Manager.Ui.GetInGame);
			PotionFollowP thisPFP = newObjUi.GetComponent<PotionFollowP> ();
			newObj.GetComponent<ItemObjLost> ().ThisObj = newObjUi;
			thisPFP.ThisPlayer = newObj.transform;
			thisPFP.getCam = getCam;

			int getNbr = (int)(CurrItem - (CurrItem * PourcLootLost) * 0.01f);
			LostItem += CurrItem;
			thisPFP.Nbr = getNbr;
			newObj.GetComponent<ItemObjLost> ().NbrItem = getNbr;
			thisPFP.GetComponent<CanvasGroup> ().DOFade (1, 0.1f);
			
			for (a = getList.Length - 1; a > getList.Length * 0.5f; a--) 
			{
				getItem = getList [a].transform.GetComponent<ItemLost> ();
				getItem.gameObject.SetActive (true);
				getItem.transform.localScale = Vector3.one;
				getItem.transform.localPosition = Vector3.zero;

				if (!getItem) 
				{
					getItem = getList [a].AddComponent<ItemLost> ();
				}

				//getItem.EnableColl(true);
				getItem.transform.SetParent (newObj.transform);
				AllItem.RemoveAt (a);
			}

			getList = AllItem.ToArray ();
			for (a = 0; a < getList.Length; a++) 
			{
				Destroy (getList [a]);
			}

			AllItem.Clear ();
			Destroy (newObj, 60);
		}
	}

	void OnTriggerEnter (Collider thisColl)
	{
		string getTag = thisColl.tag;
		if (thisColl.tag == Constants._EnterCont) {
			canEnterBox = true;
		} else if (getTag == Constants._EnemyBullet && canTakeDmg && checkUpdate/*|| getTag == Constants._Enemy*/) {
			lifePlayer--;

			Manager.VibM.StunVibration (inputPlayer);

			if (lifePlayer <= 0 && !dead) 
			{
				animeDead (thisColl.transform.position);
			} 
			else 
			{
				if (tweenRegen != null) 
				{
					tweenRegen.Kill ();
				}

				DOVirtual.DelayedCall (TimeToRegen, () => 
				{
					lifePlayer = LifePlayer;
				});
			}
		}
	}

	void OnTriggerExit (Collider thisColl)
	{
		if (thisColl.tag == Constants._EnterCont) 
		{
			canEnterBox = false;
		}
	}

	void OnCollisionEnter (Collision thisColl)
	{
		/*string getTag = thisColl.collider.tag;

        if ( getTag == Constants._EnemyBullet || getTag == Constants._Enemy )
        {
            lifePlayer --;

            if ( lifePlayer <= 0 )
            {
                dead = true;
                animeDead ( );
            }
        }*/
	}

	#endregion
}



