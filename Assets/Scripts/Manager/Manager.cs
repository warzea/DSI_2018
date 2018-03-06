using UnityEngine;

public class Manager : MonoBehaviour
{
	#region Variables
   	static Manager mainManagerInstance;

	//Add new managers here
	static UiManager ui;
	public static UiManager Ui { get { return ui; } }

	static GameController gamecont;
	public static GameController GameCont { get { return gamecont; } }

	static EventManager evnt;
	public static EventManager Event { get { return evnt; } }
	#endregion
	
	#region Mono
	void Awake()
	{
		PlayerPrefs.DeleteAll ( );
	
		Application.targetFrameRate = 60;

		//Keep manager a singleton
		if ( mainManagerInstance != null )
		{
			Destroy ( gameObject );
		}
		else
		{
			DontDestroyOnLoad ( gameObject );
			mainManagerInstance = this;
			InitializeManagers ( );
		}       
	}
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	void InitializeManagers()
	{
		InitializeManager ( ref evnt );
		InitializeManager ( ref gamecont );
		InitializeManager ( ref ui );
	}

	void InitializeManager<T>(ref T manager) where T : ManagerParent
	{
		Debug.Log("Initializing managers");
		T[] managers = GetComponentsInChildren<T>();

		if(managers.Length == 0)
		{
		    Debug.LogError("No manager of type: " + typeof(T) + " found.");
		    return;
		}

		//Set to first manager
		manager = managers[0];
		manager.Initialize();

		if(managers.Length > 1) //Too many managers
		{
		    Debug.LogError("Found " + managers.Length + " managers of type " + typeof(T));
		    for(int i = 1; i < managers.Length; i++)
		    {
		        Destroy(managers[i].gameObject);
		    }
		} 
	}
	#endregion
}
