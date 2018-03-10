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
    public float MoveSpeed;
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
    [Range(0, 1)]
    [Tooltip("Speed reduce pendant qu'on pousse la caisse")]
    public float SpeedReduceOnBox = 0.1f;
    public float SmoothRotateOnBox = 10;

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
    // -----
    [HideInInspector]
    public Transform AmmoUI;
    public float UiAmmoX;
    public float UiAmmoY;
    [HideInInspector]
    public Image UiAmmo;
    PlayerController thisPC;
    WeaponAbstract thisWeapon;
    Transform thisTrans;
    Transform getBoxWeapon;
    Rigidbody thisRig;
    CameraFollow GetCamFoll;
    Player inputPlayer;
    Camera getCam;

    int lifePlayer;

    bool shooting = false;
    bool dashing = false;
    bool canDash = true;
    bool canEnterBox = false;
    bool canShoot = true;
    bool autoShoot = true;
    bool checkShoot = true;
    bool canTakeDmg = true;
    bool checkShootScore = true;
    bool checkAward = false;
    bool checkAuto = false;
    bool checkUIBorder = false;
    bool checkUIBorderY = false;

    #endregion

    #region Mono
    void Awake()
    {
        AllEnemy = new List<EnemyInfo>();
        lifePlayer = LifePlayer;
        thisPC = GetComponent<PlayerController>();

        System.Array thisArray = System.Enum.GetValues(typeof(TypeEnemy));

        for (int a = 0; a < thisArray.Length; a++)
        {
            AllEnemy.Add(new EnemyInfo());
            AllEnemy[a].ThisType = (TypeEnemy)thisArray.GetValue(a);
        }
    }
    void Start()
    {
        thisTrans = transform;
        thisRig = GetComponent<Rigidbody>();

        AmmoUI.gameObject.SetActive(true);
        UiAmmo = AmmoUI.Find("Ammo Inside").GetComponent<Image>();
        inputPlayer = ReInput.players.GetPlayer(IdPlayer);

        getBoxWeapon = Manager.GameCont.WeaponB.transform;
        getCam = Manager.GameCont.MainCam;
        GetCamFoll = Manager.GameCont.GetCameraFollow;
    }

    void Update()
    {
        thisRig.velocity = Vector3.zero;
        float getDeltaTime = Time.deltaTime;

        if (!dead)
        {
            inputAction(getDeltaTime);
        }

        checkBorder();
        
        if ( !checkUIBorder )
        {
            //AmmoUI.localScale = Vector3.one;
            
            AmmoUI.position = getCam.WorldToScreenPoint(thisTrans.position - Vector3.right * UiAmmoX + Vector3.up * UiAmmoY);
        } 
        else
        {
            //AmmoUI.localScale = new Vector3(-1, 1, 1);
            
            AmmoUI.position = getCam.WorldToScreenPoint(thisTrans.position + Vector3.right * UiAmmoX + Vector3.up * UiAmmoY);
        }

        if (AllItem.Count > 0 && Vector3.Distance(thisTrans.position, getBoxWeapon.position) < DistToDropItem)
        {
            emptyBag();
        }
    }
    #endregion

    #region Public Methods
    public void UpdateWeapon(WeaponAbstract thisWeap = null)
    {
        if (thisWeap != null)
        {
            thisWeap.OnFloor = false;
            autoShoot = thisWeap.AutoShoot;
            SpeedReduce = thisWeap.SpeedReduce;
            thisWeapon = thisWeap;
            CdShoot = thisWeap.CoolDown;
        }
        else
        {
            thisWeapon = null;
        }
    }

    public void GetDamage(Transform thisEnemy, int intDmg = 1)
    {
        if (canTakeDmg)
        {
            lifePlayer -= intDmg;

            if (lifePlayer <= 0 && !dead)
            {
                animeDead(thisEnemy.position);
            }
        }

    }
    #endregion

    #region Private Methods
    void checkBorder()
    {
        Vector3 getCamPos = getCam.WorldToViewportPoint(thisTrans.position);

        if (getCamPos.x > 0.97f)
        {
            thisTrans.position = new Vector3(getCam.ViewportToWorldPoint(new Vector3(0.97f, getCamPos.y, getCamPos.z)).x, thisTrans.position.y, thisTrans.position.z);
        }
        else if (getCamPos.x < 0.03f)
        {
            thisTrans.position = new Vector3(getCam.ViewportToWorldPoint(new Vector3(0.03f, getCamPos.y, getCamPos.z)).x, thisTrans.position.y, thisTrans.position.z);
        }

        if (getCam.WorldToViewportPoint(thisTrans.position - Vector3.right * UiAmmoX).x < 0.03f)
        {
            checkUIBorder = true;
        }
        else
        {
            checkUIBorder = false;
        }

        if (getCamPos.y > 0.97f)
        {
            thisTrans.position = new Vector3(thisTrans.position.x, thisTrans.position.y, getCam.ViewportToWorldPoint(new Vector3(getCamPos.x, 0.97f, getCamPos.z)).z);
        }
        else if (getCamPos.y < 0.03f)
        {
            thisTrans.position = new Vector3(thisTrans.position.x, thisTrans.position.y, getCam.ViewportToWorldPoint(new Vector3(getCamPos.x, 0.03f, getCamPos.z)).z);
        }
    }

    float checkBorderDead(Vector3 customPos)
    {
        Vector3 getCamPos = getCam.WorldToViewportPoint(customPos);
        Vector3 getDist = customPos;

        if (getCamPos.x > 0.97f)
        {
            customPos = new Vector3(getCam.ViewportToWorldPoint(new Vector3(0.97f, getCamPos.y, getCamPos.z)).x, customPos.y, customPos.z);
        }
        else if (getCamPos.x < 0.03f)
        {
            customPos = new Vector3(getCam.ViewportToWorldPoint(new Vector3(0.03f, getCamPos.y, getCamPos.z)).x, customPos.y, customPos.z);
        }

        if (getCamPos.y > 0.97f)
        {
            customPos = new Vector3(customPos.x, customPos.y, getCam.ViewportToWorldPoint(new Vector3(getCamPos.x, 0.97f, getCamPos.z)).z);
        }
        else if (getCamPos.y < 0.03f)
        {
            customPos = new Vector3(customPos.x, customPos.y, getCam.ViewportToWorldPoint(new Vector3(getCamPos.x, 0.03f, getCamPos.z)).z);
        }

        return Vector3.Distance(customPos, getDist);
    }

    void inputAction(float getDeltaTime)
    {
        if (canShoot)
        {
            playerShoot(getDeltaTime);
        }

        if (canDash)
        {
            //playerDash ( );
        }

        if (!dashing)
        {
            interactPlayer();
            playerMove(getDeltaTime);
        }

        playerAim(getDeltaTime);
    }

    /*void playerDash ( )
	{
		bool getDash = inputPlayer.GetButtonDown ( "Dash" );

		if ( !getDash )
		{
			return;
		}		

		float Xmove = inputPlayer.GetAxis("MoveX");
		float Ymove = inputPlayer.GetAxis("MoveY");

		Vector3 getDirect = new Vector3 ( Xmove, 0, Ymove );

		if ( getDirect == Vector3.zero )
		{
			return;
		}

		float getDist = DashDistance;
		string getTag;
		
		RaycastHit[] allHit;
		allHit = Physics.RaycastAll ( thisTrans.position, getDirect, DashDistance );

		foreach ( RaycastHit thisRay in allHit )
		{
			getTag = LayerMask.LayerToName (  thisRay.collider.gameObject.layer );
			
			if ( getTag != Constants._BulletPlayer && getTag != Constants._Charct )
			{
				if ( thisRay.distance < getDist )
				{
					getDist = thisRay.distance;
				}
			}
		}

		canDash = false;
		dashing = true;
		
		thisTrans.DOMove ( thisTrans.position + getDirect * getDist, DashTime * ( getDist / DashDistance) ).OnComplete ( () =>
		{
			canDash = true;
			dashing = false;
		});
	}*/

    void playerMove(float getDeltaTime)
    {
        float Xmove = inputPlayer.GetAxis("MoveX");
        float Ymove = inputPlayer.GetAxis("MoveY");

        float speed = Mathf.Abs(Xmove) + Mathf.Abs(Ymove) * 2;

        animPlayer.SetFloat("Velocity", speed);
        float getSpeed = MoveSpeed;

        if (shooting)
        {
            getSpeed *= SpeedReduce;
        }
        else if (driveBox)
        {
            TimeWBox += getDeltaTime;
            getSpeed *= SpeedReduceOnBox;
        }

        getSpeed = getDeltaTime * getSpeed;
        TotalDist += getSpeed;
        //GetSpeed = getDeltaTime * MoveSpeed;
        thisTrans.position += getSpeed * new Vector3(Xmove, 0, Ymove);
    }

    void playerAim(float getDeltaTime)
    {
        float Xaim = inputPlayer.GetAxis("AimX");
        float Yaim = inputPlayer.GetAxis("AimY");

        if (Xaim != 0 && Yaim != 0)
        {
            if (driveBox)
            {
                thisTrans.localRotation = Quaternion.Slerp(thisTrans.rotation, Quaternion.LookRotation(new Vector3(Xaim, 0, Yaim), thisTrans.up), SmoothRotateOnBox * getDeltaTime);
            }
            else
            {
                thisTrans.localRotation = Quaternion.LookRotation(new Vector3(Xaim, 0, Yaim), thisTrans.up);
            }
        }
    }

    void playerShoot(float getDeltaTime)
    {
        float shootInput = inputPlayer.GetAxis("Shoot");



        /*if ( shootInput == 0 )
		{
			checkShoot = true;
		}*/
        /*if ( shootInput > 0 )
		{
			bool checkBox = false;
			if ( shooting == false )
			{
				RaycastHit[] allHit;
				string getTag;

				allHit = Physics.RaycastAll ( thisTrans.position, thisTrans.forward, 2 );
				foreach ( RaycastHit thisRay in allHit )
				{
					getTag = thisRay.collider.tag;

					if ( getTag == Constants._BoxTag )
					{
						if ( thisWeapon != null )
						{
							Destroy (thisWeapon.gameObject);
						}
						
						checkBox = true;
						Manager.GameCont.WeaponB.NewWeapon ( thisPC );
						break;
					}
				}
			}
			
			if ( !checkBox && thisWeapon != null )
			{
				thisWeapon.weaponShoot( thisTrans );
			}

			shooting = true;
		}*/
        if (shootInput == 1 && checkShootScore)
        {
            checkShootScore = false;
            SpawmShoot++;

            
        }
        else if (shootInput < 0.3f)
        {
            checkShootScore = true;
        }

        if ( shootInput == 0 && checkAuto )
        {
            checkShoot = true;
        }

        if (shootInput > 0.3f && thisWeapon != null && checkShoot)
        {
            if (!autoShoot)
            {
                checkAuto = false;
                checkShoot = false;
                DOVirtual.DelayedCall(CdShoot, () =>
                {
                    checkAuto = true;
                });
            }

            thisWeapon.weaponShoot(thisTrans);
            animPlayer.SetBool("Attack", true);
            shooting = true;
            if (thisWeapon != null) //&& thisWeapon.Damage == 1)
            {
                //Debug.Log("Shoot");
                if(thisWeapon.Damage <= Manager.VibM.DamagesLow)
                    Manager.VibM.ShootLowVibration(inputPlayer);
                else if(thisWeapon.Damage > Manager.VibM.DamagesLow && thisWeapon.Damage <= Manager.VibM.DamagesMedium)
                    Manager.VibM.ShootMediumVibration(inputPlayer);
                else if (thisWeapon.Damage > Manager.VibM.DamagesLow && thisWeapon.Damage <= Manager.VibM.DamagesMedium)
                    Manager.VibM.ShootHighVibration(inputPlayer);
            }
        }
        else
        {
            animPlayer.SetBool("Attack", false);
            shooting = false;
        }
    }

    public void AddItem()
    {
        if (AllItem.Count <= nbItemBeforeBigBag)
        {
            animPlayer.SetTrigger("Bag_Up");
        }
        else if (AllItem.Count > nbItemBeforeBigBag)
        {
            animPlayer.SetTrigger("Bag_Up2");
        }
    }

    void interactPlayer()
    {
        bool interactInput = inputPlayer.GetButtonDown("Interact");

        if (interactInput)
        {
            NbrTouchInteract++;
            if (canEnterBox || driveBox)
            {
                useBoxWeapon();
                return;
            }

            RaycastHit[] allHit;
            string getTag;

            allHit = Physics.RaycastAll(thisTrans.position, thisTrans.forward, 1);
            foreach (RaycastHit thisRay in allHit)
            {
                getTag = thisRay.collider.tag;

                if (getTag == Constants._BoxTag)
                {
                    if (thisWeapon != null)
                    {
                        Destroy(thisWeapon.gameObject);
                    }

                    Manager.GameCont.WeaponB.NewWeapon(thisPC);

                    break;
                }
                else if (getTag == Constants._ContainerItem)
                {
                    thisRay.collider.GetComponent<InteractAbstract>().OnInteract(thisPC);
                    AddItem();
                    break;
                }
            }
        }
    }

    void useBoxWeapon()
    {
        if (Manager.GameCont.WeaponB.CanControl)
        {
            GetCamFoll.UpdateTarget(thisTrans);
            WeaponPos.gameObject.SetActive(false);

            canShoot = false;
            Physics.IgnoreCollision(GetComponent<Collider>(), getBoxWeapon.GetComponent<Collider>(), true);
            Manager.GameCont.WeaponB.CanControl = false;
            getBoxWeapon.SetParent(BoxPlace);

            getBoxWeapon.DOLocalMove(Vector3.zero, 0.5f);
            getBoxWeapon.DOLocalRotateQuaternion(Quaternion.identity, 0.5f);

            driveBox = true;
        }
        else if (driveBox)
        {
            getBoxWeapon.DOKill();
            WeaponPos.gameObject.SetActive(true);
            GetCamFoll.UpdateTarget(getBoxWeapon);

            canShoot = true;
            Physics.IgnoreCollision(GetComponent<Collider>(), getBoxWeapon.GetComponent<Collider>(), false);
            Manager.GameCont.WeaponB.CanControl = true;
            getBoxWeapon.SetParent(null);

            driveBox = false;
        }
    }

    void emptyBag()
    {
        Manager.Ui.PopPotions(PotionType.Plus);
        animPlayer.SetTrigger("BagUnfull");
        GameObject[] getBagItems = AllItem.ToArray();
        Transform getBoxTrans = getBoxWeapon;
        Transform currTrans;

        Manager.GameCont.WeaponB.AddItem(getBagItems.Length);
        CurrScore += getBagItems.Length;
        CurrLootScore += getBagItems.Length;


        for (int a = 0; a < getBagItems.Length; a++)
        {
            currTrans = getBagItems[a].transform;

            currTrans.gameObject.SetActive(true);
            currTrans.SetParent(null);
            currTrans.SetParent(getBoxTrans);

            currTrans.position = BagPos.position + new Vector3(Random.Range(-0.2f, 0.21f), 0, Random.Range(-0.2f, 0.21f));

            dropItem(currTrans);
        }
        AllItem.Clear();
    }

    void dropItem(Transform currTrans)
    {
        DOVirtual.DelayedCall(Random.Range(0, 0.2f), () =>
        {
            currTrans.DOLocalMove(Vector3.zero, 1.3f);
            currTrans.DOScale(Vector3.one, 0.5f).OnComplete(() =>
            {
                currTrans.DOScale(Vector3.zero, 0.7f).OnComplete(() =>
                {
                    Destroy(currTrans.gameObject);
                });
            });
        });
    }

    void animeDead(Vector3 pointColl)
    {
        currentEnemy = 0;
        NbrDead++;
        canTakeDmg = false;
        dead = true;

        if (driveBox)
        {
            useBoxWeapon();
        }

        WeaponPos.gameObject.SetActive(false);
        GetComponent<Collider>().isTrigger = true;

        GameObject[] getList = AllItem.ToArray();
        ItemLost getItem;
        Vector3 getDirect = Vector3.Normalize(thisTrans.position - pointColl);
        getDirect = new Vector3(getDirect.x, thisTrans.localPosition.y, getDirect.z);
        GameObject newObj = (GameObject)Instantiate(new GameObject(), thisTrans.position, thisTrans.rotation);

        int a;
        float getDist = DistProjDead;
        float getTime = TimeProjDead;
        string getTag;

        RaycastHit[] allHit;

        getDist -= checkBorderDead(thisTrans.position + getDirect * DistProjDead);
        allHit = Physics.RaycastAll(thisTrans.position, getDirect, getDist);


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

        thisTrans.DOKill();

        //thisTrans.SetParent(getBoxWeapon);

        thisTrans.DOLocalMove(thisTrans.localPosition + getDirect * getDist, getTime).OnComplete(() =>
    {
        DOVirtual.DelayedCall(TimeDead + TimeProjDead - getTime, () =>
          {
              GetComponent<Collider>().isTrigger = false;
              WeaponPos.gameObject.SetActive(true);
              lifePlayer = LifePlayer;
              dead = false;
              thisWeapon.canShoot = true;

              DOVirtual.DelayedCall(TimeInvincible, () =>
                {
                    canTakeDmg = true;
                });
          });
    });

        /*for ( a = 0; a < getList.Length; a ++ )
		{
			Destroy(getList[a]);	
		}*/
        int getNbr = (int)(getList.Length - (getList.Length * PourcLootLost) * 0.01f);
        LostItem += getList.Length;

        for (a = getList.Length - 1; a > getNbr - 1; a--)
        {
            getItem = getList[a].transform.GetComponent<ItemLost>();

            if (!getItem)
            {
                getItem = getList[a].AddComponent<ItemLost>();
            }

            getItem.EnableColl(true);
            getItem.transform.SetParent(newObj.transform);
            AllItem.RemoveAt(a);
        }

        getList = AllItem.ToArray();
        for (a = 0; a < getList.Length; a++)
        {
            Destroy(getList[a]);
        }

        AllItem.Clear();
        Destroy(newObj, 60);


        //AllItem.Clear();

        //thisTrans.DOKill ( );

        /*	thisTrans.DOLocalMove(Vector3.zero, 1f);
            thisTrans.DOScale (Vector3.zero, 1f).OnComplete ( () => 
            {
                DOVirtual.DelayedCall ( 3, ( ) => 
                {
                    thisTrans.SetParent ( null );
                    thisTrans.DOLocalMove( thisTrans.localPosition + Vector3.right, 0.25f);
                    thisTrans.DOScale ( Vector3.one, 0.25f).OnComplete ( () => 
                    {
                        WeaponPos.gameObject.SetActive(true);
                        lifePlayer = LifePlayer;
                        dead = false;
                        thisWeapon.canShoot = true;
                    });
                });
            });*/
    }

    void OnTriggerEnter(Collider thisColl)
    {
        string getTag = thisColl.tag;
        if (thisColl.tag == Constants._EnterCont)
        {
            canEnterBox = true;
        }
        else if (getTag == Constants._EnemyBullet && canTakeDmg /*|| getTag == Constants._Enemy*/ )
        {
            lifePlayer--;

            Manager.VibM.StunVibration(inputPlayer);

            if (lifePlayer <= 0 && !dead)
            {
                animeDead(thisColl.transform.position);
            }
        }
    }

    void OnTriggerExit(Collider thisColl)
    {
        if (thisColl.tag == Constants._EnterCont)
        {
            canEnterBox = false;
        }
    }

    void OnCollisionEnter(Collision thisColl)
    {
        Debug.Log("collide");
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
