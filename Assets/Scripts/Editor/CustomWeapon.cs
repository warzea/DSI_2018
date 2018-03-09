using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor (typeof (WeaponAbstract))]
public class CustomWeapon : Editor 
{
	#region Variables
	SerializedProperty WeightRandom;
	SerializedProperty AutoShoot;
	SerializedProperty Projectile;
	SerializedProperty Through;
	SerializedProperty Explosion;
	SerializedProperty BulletCapacity;
	SerializedProperty FireRate;
	SerializedProperty CoolDown;
	SerializedProperty BackPush;
	SerializedProperty SpeedReduce;
	SerializedProperty Damage;
	SerializedProperty Range;
	SerializedProperty NbrBullet;
	SerializedProperty ScaleBullet;
	SerializedProperty SpeedBullet;
	SerializedProperty Gust;
	SerializedProperty SpaceBulletTime;
	SerializedProperty Angle;
	SerializedProperty WidthRange;
	SerializedProperty SpeedZone;
	SerializedProperty TimeDest;
	SerializedProperty Diameter;
	SerializedProperty Bullet;
	SerializedProperty SpawnBullet;
	GUIContent Empty;
	#endregion
	
	#region Mono
	#endregion
	
	#region Public Methods
	public void OnEnable ( )
	{
		Empty = new GUIContent ( "" );
		WeightRandom = serializedObject.FindProperty("WeightRandom");
		Bullet = serializedObject.FindProperty("Bullet");
		SpawnBullet = serializedObject.FindProperty("SpawnBullet");

		// -- bool
		AutoShoot = serializedObject.FindProperty("AutoShoot");
		Projectile = serializedObject.FindProperty("Projectile");
		Through = serializedObject.FindProperty("Through");
		Explosion = serializedObject.FindProperty("Explosion");
		// -- 

		BulletCapacity = serializedObject.FindProperty("BulletCapacity");
		BackPush = serializedObject.FindProperty("BackPush");
		SpeedReduce = serializedObject.FindProperty("SpeedReduce");
		Damage = serializedObject.FindProperty("Damage");
		Range = serializedObject.FindProperty("Range");

		// si auto
		FireRate = serializedObject.FindProperty("FireRate");
		// fin auto

		// si manuel
		CoolDown = serializedObject.FindProperty("CoolDown");
		// fin manuel

		// -- si Projectile 
		ScaleBullet = serializedObject.FindProperty("ScaleBullet");
		SpeedBullet = serializedObject.FindProperty("SpeedBullet");
		NbrBullet = serializedObject.FindProperty("NbrBullet");
		Gust = serializedObject.FindProperty("Gust");
			// -- si Gust 
			SpaceBulletTime = serializedObject.FindProperty("SpaceBullet");
			// -- fin Gust

			// -- si spread
			Angle = serializedObject.FindProperty("Angle");
			// -- fin spread
		// -- fin projectile

		// -- si zone
		WidthRange = serializedObject.FindProperty("WidthRange");
		SpeedZone = serializedObject.FindProperty("SpeedZone");
		TimeDest = serializedObject.FindProperty("TimeDest");
		// -- fin zone

		// -- si explosion
		Diameter = serializedObject.FindProperty("Diameter");
		// -- fin explostion
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.LabelField("Weapon Inspector", EditorStyles.boldLabel);
		WeaponAbstract myTarget = (WeaponAbstract)target;
		
		var oldColor = GUI.backgroundColor;
		var buttonStyle = new GUIStyle(EditorStyles.miniButton);
		//buttonStyle.normal.textColor = Color.green;
		//buttonStyle.normal.textColor = Color.red;
		serializedObject.Update ( );
		
		EditorGUILayout.PropertyField ( WeightRandom );
		
		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical();
			EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField ( "Bullet Object" );
				EditorGUILayout.LabelField ( "Spawn Pos Bullet" );
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PropertyField ( Bullet, Empty );
				EditorGUILayout.PropertyField ( SpawnBullet, Empty );
			EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();
		EditorGUILayout.Space();
		
		// -- bool
		EditorGUILayout.BeginHorizontal();
		if ( myTarget.AutoShoot )
		{
			GUI.backgroundColor = Color.green;
		}
		else
		{
			GUI.backgroundColor = Color.red;
		}

		if ( GUILayout.Button ( "AutoShoot", buttonStyle ) )
		{
			myTarget.AutoShoot = !myTarget.AutoShoot;
		}
		//EditorGUILayout.PropertyField ( AutoShoot );

		if ( myTarget.Projectile )
		{
			GUI.backgroundColor = Color.green;
		}
		else
		{
			GUI.backgroundColor = Color.red;
		}

		if ( GUILayout.Button ( "It's a Projectile", buttonStyle ) )
		{
			myTarget.Projectile = !myTarget.Projectile;
		}
		EditorGUILayout.EndHorizontal();
		//EditorGUILayout.PropertyField ( Projectile );
		
		EditorGUILayout.BeginHorizontal();
		if ( myTarget.Through )
		{
			GUI.backgroundColor = Color.green;
		}
		else
		{
			GUI.backgroundColor = Color.red;
		}

		if ( GUILayout.Button ( "Can Through", buttonStyle ) )
		{
			myTarget.Through = !myTarget.Through;
		}
		//EditorGUILayout.PropertyField ( AutoShoot );

		if ( myTarget.Explosion )
		{
			GUI.backgroundColor = Color.green;
		}
		else
		{
			GUI.backgroundColor = Color.red;
		}

		if ( GUILayout.Button ( "Can Explose", buttonStyle ) )
		{
			myTarget.Explosion = !myTarget.Explosion;
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space();

		//EditorGUILayout.PropertyField ( Through );
		//EditorGUILayout.PropertyField ( Explosion );

		// -- 
		GUI.backgroundColor = oldColor;
		
		EditorGUILayout.PropertyField ( BulletCapacity );
		EditorGUILayout.PropertyField ( Damage );
		EditorGUILayout.PropertyField ( BackPush );
		EditorGUILayout.PropertyField ( SpeedReduce );
		EditorGUILayout.PropertyField ( Range );

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("WeaponCustom -------", EditorStyles.boldLabel);
		
		EditorGUI.indentLevel = 1;
		
		// si auto
		if ( myTarget.AutoShoot )
		{
			EditorGUILayout.LabelField("AutoShoot -------", EditorStyles.boldLabel);
			EditorGUILayout.PropertyField ( FireRate );
			EditorGUILayout.LabelField("-------", EditorStyles.boldLabel);
		}	// fin auto
		else // si manuel
		{
			EditorGUILayout.LabelField("ManualShoot -------", EditorStyles.boldLabel);
			EditorGUILayout.PropertyField ( CoolDown );
			EditorGUILayout.LabelField("-------", EditorStyles.boldLabel);
		}
		// fin manuel

		// -- si Projectile 
		if ( myTarget.Projectile )
		{
			EditorGUILayout.LabelField("Projectile -------", EditorStyles.boldLabel);
			EditorGUILayout.PropertyField ( NbrBullet );
			EditorGUILayout.PropertyField ( ScaleBullet );
			EditorGUILayout.PropertyField ( SpeedBullet );

			if ( myTarget.Gust )
			{
				GUI.backgroundColor = Color.green;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}

			if ( GUILayout.Button ( "Gust", buttonStyle ) )
			{
				myTarget.Gust = !myTarget.Gust;
			}

			GUI.backgroundColor = oldColor;
			//EditorGUILayout.PropertyField ( Gust );

			EditorGUI.indentLevel = 2;
			
			// -- si Gust 
			if ( myTarget.Gust )
			{
				EditorGUILayout.LabelField("Gust -------", EditorStyles.boldLabel);
				EditorGUILayout.PropertyField ( SpaceBulletTime );
				EditorGUILayout.LabelField("-------", EditorStyles.boldLabel);
			} // -- fin Gust
			else // -- si spread
			{
				EditorGUILayout.LabelField("Spread -------", EditorStyles.boldLabel);
				EditorGUILayout.PropertyField ( Angle );
				EditorGUILayout.LabelField("-------", EditorStyles.boldLabel);
			}
			// -- fin spread
			EditorGUI.indentLevel = 1;
			EditorGUILayout.LabelField("-------", EditorStyles.boldLabel);
		}
		else // -- fin projectile
		{
			// -- si zone
			EditorGUILayout.PropertyField ( WidthRange );
			EditorGUILayout.PropertyField ( SpeedZone );
			EditorGUILayout.PropertyField ( TimeDest );
			// -- fin zone
		}

		EditorGUI.indentLevel = 2;
		
		if ( myTarget.Explosion )
		{
			// -- si explosion
			EditorGUILayout.LabelField("Explosion -------", EditorStyles.boldLabel);
			EditorGUILayout.PropertyField ( Diameter );
			EditorGUILayout.LabelField("-------", EditorStyles.boldLabel);
			// -- fin explostion
		}	

		EditorGUI.indentLevel = 0;
		EditorGUILayout.LabelField("-------", EditorStyles.boldLabel);
		serializedObject.ApplyModifiedProperties ( );
	}
	#endregion

	#region Private Methods
	#endregion

}
