using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

public class AudioManager : ManagerParent
{
	#region Variable
	[Tooltip ("Create a new ScriptableAudio and drag and drop here (in project : create -> Scriptable -> Audio)")]
	public List<AudioScriptable> AllMF;
	public Dictionary<AudioType, int> VolumeAudio;
	Dictionary<AudioType, AudioSource> audioParent;
	List<ParentAud> audioChild;
	#endregion

	#region Mono
	#endregion

	#region Public
	public AudioSource OpenAudio (AudioType thisType, string thisName = "", bool loopAudio = false, System.Action thisAct = null, bool playIfEmpty = false)
	{
		AudioSource thisSource;

		List<AudioScriptable> getAMF = AllMF;
		List<AllAudio> getAllAudio;
		List<ParentAud> getAC = audioChild;;
		List<string> getAllName = new List<string> ();
		int a;
		int b;
		int c;

		if (!audioParent.TryGetValue (thisType, out thisSource))
		{
			return thisSource;
		}
		else if (thisName == "")
		{
			for (a = 0; a < getAMF.Count; a++)
			{
				for (b = 0; b < getAMF [a].AllMF.Count; b++)
				{
					if (getAMF [a].AllMF [b].ThisType == thisType)
					{
						for (c = 0; c < getAMF [a].AllMF [b].SetAudio.Count; c++)
						{
							getAllName.Add (getAMF [a].AllMF [b].SetAudio [c].AudioName);
						}
					}
				}
			}

			if (getAllName.Count == 0)
			{
				return null;
			}

			thisName = getAllName [Random.Range (0, getAllName.Count)];
		}

		int getVolume;

		if (!VolumeAudio.TryGetValue (thisType, out getVolume))
		{
			getVolume = 1;
		}

		for (a = 0; a < getAMF.Count; a++)
		{
			for (b = 0; b < getAMF [a].AllMF.Count; b++)
			{
				if (getAMF [a].AllMF [b].ThisType == thisType)
				{
					getAllAudio = getAMF [a].AllMF [b].SetAudio;
					for (c = 0; c < getAllAudio.Count; c++)
					{
						if (getAllAudio [c].AudioName == thisName)
						{
							if (loopAudio)
							{
								thisSource.enabled = true;
								thisSource.volume = getAllAudio [c].Volume * getVolume;
								thisSource.pitch = getAllAudio [c].Pitch;
								thisSource.clip = getAllAudio [c].Audio;
								thisSource.Play ();

								thisSource.loop = true;
								return thisSource;
							}
							else
							{
								int d;

								if (playIfEmpty)
								{
									for (d = 0; d < getAC.Count; d++)
									{
										if (getAC [d].ThisType == thisType && getAC [d].ThoseSource.Count > 0)
										{
											return null;
										}
									}
								}

								thisSource.loop = false;

								AudioSource getAud = thisSource.gameObject.AddComponent<AudioSource> ();

								getAud.loop = false;
								getAud.volume = getAllAudio [c].Volume * getVolume;
								getAud.pitch = getAllAudio [c].Pitch;
								getAud.enabled = true;
								getAud.clip = getAllAudio [c].Audio;
								getAud.Play ();

								for (d = 0; d < getAC.Count; d++)
								{
									if (getAC [d].ThisType == thisType)
									{
										getAC [d].ThoseSource.Add (getAud);
										break;
									}
								}

								StartCoroutine (waitEndAudio (getAud.clip.length - 0.1f, d, getAud, thisAct));
								return getAud;
							}
						}
					}
				}
			}
		}

		return null;
	}

	public AudioSource GetSource (AudioType thisType)
	{
		AudioSource thisSource;

		if (audioParent.TryGetValue (thisType, out thisSource))
		{
			return thisSource;
		}

		return null;
	}

	public void CloseAudio (AudioType thisType)
	{
		AudioSource thisSource;

		if (audioParent.TryGetValue (thisType, out thisSource))
		{
			thisSource.enabled = false;
		}
	}

	public void CloseUnLoopAudio (AudioType thisType, bool allUnloop = false)
	{
		List<ParentAud> getAC = audioChild;

		int b;
		for (int a = 0; a < getAC.Count; a++)
		{
			if (getAC [a].ThisType == thisType || allUnloop)
			{
				for (b = 0; b < getAC [a].ThoseSource.Count; b++)
				{
					if (getAC [a].ThoseSource [b] != null)
					{
						Destroy (getAC [a].ThoseSource [b]);
					}
				}

				getAC [a].ThoseSource.Clear ();
				if (!allUnloop)
				{
					break;
				}
			}
		}
	}

	public void CloseAllAudio ()
	{
		Dictionary<AudioType, AudioSource> setDict = audioParent;
		List<ParentAud> getAC = audioChild;

		foreach (KeyValuePair<AudioType, AudioSource> thisKV in setDict)
		{
			thisKV.Value.enabled = false;
		}

		int b;
		for (int a = 0; a < getAC.Count; a++)
		{
			for (b = 0; b < getAC [a].ThoseSource.Count; b++)
			{
				if (getAC [a].ThoseSource [b] != null)
				{
					Destroy (getAC [a].ThoseSource [b]);
				}
			}

			getAC [a].ThoseSource.Clear ();
		}
	}

	public bool IsAudioLaunch (AudioType thisType)
	{
		List<ParentAud> getAC = audioChild;

		int b;
		for (int a = 0; a < getAC.Count; a++)
		{
			if (getAC [a].ThisType == thisType)
			{
				for (b = 0; b < getAC [a].ThoseSource.Count; b++)
				{
					if (getAC [a].ThoseSource [b] != null)
					{
						return true;
					}
				}
			}
		}

		return false;
	}
	#endregion

	#region Private
	protected override void InitializeManager ()
	{
		Dictionary<AudioType, AudioSource> setDict = new Dictionary<AudioType, AudioSource> ();
		Dictionary<AudioType, int> setVol = new Dictionary<AudioType, int> ();
		Transform currT = transform;

		List<ParentAud> getAC = new List<ParentAud> ();

		setDict.Add (AudioType.FxSound, currT.Find ("Fx").GetComponent<AudioSource> ());
		setDict.Add (AudioType.OtherMusic, currT.Find ("OtherMusic").GetComponent<AudioSource> ());
		setDict.Add (AudioType.OtherSound, currT.Find ("OtherFx").GetComponent<AudioSource> ());
		setDict.Add (AudioType.Other, currT.Find ("Other").GetComponent<AudioSource> ());
		setDict.Add (AudioType.MusicBackGround, currT.Find ("Music").GetComponent<AudioSource> ());
		setDict.Add (AudioType.Shoot, currT.Find ("Shoot").GetComponent<AudioSource> ());

		setVol.Add (AudioType.OtherMusic, 1);
		setVol.Add (AudioType.OtherSound, 1);
		setVol.Add (AudioType.MusicTrash, 1);
		setVol.Add (AudioType.MusicBackGround, 1);

		setVol.Add (AudioType.Other, 1);
		setVol.Add (AudioType.FxSound, 1);

		setVol.Add (AudioType.Shoot, 1);

		AudioType [] getTypes = (AudioType []) System.Enum.GetValues (typeof (AudioType));

		for (int a = 0; a < getTypes.Length; a++)
		{
			getAC.Add (new ParentAud ());
			getAC [a].ThoseSource = new List<AudioSource> ();
			getAC [a].ThisType = getTypes [a];
		}

		audioChild = getAC;
		audioParent = setDict;
		VolumeAudio = setVol;
	}

	IEnumerator waitEndAudio (float time, int currIndex, AudioSource thisSource, System.Action thisAct = null)
	{
		yield return new WaitForSeconds (time);

		if (currIndex <= audioChild.Count - 1 && audioChild [currIndex].ThoseSource.Contains (thisSource))
		{
			audioChild [currIndex].ThoseSource.Remove (thisSource);
		}

		if (thisSource != null)
		{
			if (thisAct != null)
			{
				thisAct.Invoke ();
			}

			Destroy (thisSource);
		}
	}
	#endregion
}

public class ParentAud
{
	public AudioType ThisType;
	public List<AudioSource> ThoseSource;
}