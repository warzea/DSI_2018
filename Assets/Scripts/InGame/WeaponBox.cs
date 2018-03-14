
	List<PlayerWeapon> updateWeapon; 
	List<Tween> getAllTween;
	Transform getChild;
	int nbrTotalSlide = 1;
	bool invc = false;
	bool checkAttack = false;
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.DOKill(true);
            getChild.DOKill(true);

            float rdmY = UnityEngine.Random.Range(-30, 30);
            float rdmZ = UnityEngine.Random.Range(-30, 30);

            Material mat = getChild.GetComponent<Renderer>().material;
            mat.DOKill(true);
            Debug.Log(mat);
            mat.DOColor(Color.red, .15f).OnComplete(() => {
                mat.DOColor(Color.white, .15f);
            });

            transform.DOPunchRotation(new Vector3(0, rdmY, rdmZ), .3f, 3, 1).SetEase(Ease.InBounce);
            getChild.transform.DOPunchPosition(new Vector3(rdmY / 16, rdmZ / 16, 0), .3f, 3, 1).SetEase(Ease.InBounce);
        }
    }

    #region Mono
    void Awake ( )
	{
		getAllTween = new List<Tween>();
		updateWeapon = new List<PlayerWeapon>();
		GetTrans = transform;
		getChild = GetTrans.Find("Inside");
		for ( int a = 0; a < 4; a ++)
		{
			updateWeapon.Add ( new PlayerWeapon () );
			updateWeapon[a].IDPlayer = a;
		}
	}
	#endregion
	
	#region Public Methods
	public void AttackCauld ( )
	{
		if ( !checkAttack )
		{
			GetComponent<Collider>().isTrigger = true;
			checkAttack = true;
			gameObject.tag = Constants._PlayerBullet;
	public void AddItem ( int lenghtItem, bool inv = false )
	{
        if(inv)
		{
            Manager.Ui.PopPotions(PotionType.Less);
            
            transform.DOKill(true);
            getChild.DOKill(true);

            float rdmY = UnityEngine.Random.Range(-30, 30);
            float rdmZ = UnityEngine.Random.Range(-30, 30);

            Material mat = getChild.GetComponent<Renderer>().material;
            mat.DOKill(true);
            Debug.Log(mat);
            mat.DOColor(Color.red, .15f).OnComplete(() => {
                mat.DOColor(Color.white, .15f);
            });

            transform.DOPunchRotation(new Vector3(0, rdmY, rdmZ), .3f, 3, 1).SetEase(Ease.InBounce);
            transform.DOPunchPosition(new Vector3(rdmY / 10, rdmZ / 10, 0),.3f,3,1).SetEase(Ease.InBounce);

        }