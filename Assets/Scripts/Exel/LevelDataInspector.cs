
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChunkData))]
[CanEditMultipleObjects]
public class LevelDataInspector : Editor
{
    private GameObject parentWeapon;
    private char endLine = '\n';
    private char endChar = ',';
    public TextAsset Csv;
    public GameObject weaponPref;

    void OnEnable()
    {
    }

    public override void OnInspectorGUI()
    {
        ChunkData myCurrentlyInspectedThing;
        myCurrentlyInspectedThing = (ChunkData)target;

        // Debug.Log(target);
        // base.OnInspectorGUI();
        Undo.RecordObject(myCurrentlyInspectedThing, "tweaking");
        EditorGUILayout.BeginHorizontal();
        Csv = (TextAsset)EditorGUILayout.ObjectField(Csv, typeof(TextAsset));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        weaponPref = (GameObject)EditorGUILayout.ObjectField(weaponPref, typeof(GameObject));
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        if (parentWeapon != null)
        {
            EditorGUILayout.TextField("Parent Name: ", parentWeapon.name);

        }
        else
        {
            EditorGUILayout.TextField("Parent Name: ", " No parent");
        }
        if (GUILayout.Button("Get Parent", EditorStyles.miniButton))
        {
            GetParent();
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Read Exel", EditorStyles.miniButton))
        {
            ReadExel();
        }

        EditorGUILayout.EndHorizontal();
    }

    public void GetParent()
    {
        parentWeapon = Selection.gameObjects[0];
    }

    public void ReadExel()
    {
        string[] doc = Csv.text.Split(endLine);

        foreach (string line in doc)
        {
            string[] Caracts = line.Split(endChar);
            GameObject weapon = (GameObject)Instantiate(weaponPref, weaponPref.transform.position, weaponPref.transform.rotation, parentWeapon.transform);
            WeaponAbstract wpAbstract = weapon.GetComponent<WeaponAbstract>();
            weapon.name = Caracts[0].ToString();
            bool.TryParse(Caracts[1], out wpAbstract.AutoShoot);
            float.TryParse(Caracts[2], out wpAbstract.FireRate);
            float.TryParse(Caracts[3], out wpAbstract.CoolDown);
            float.TryParse(Caracts[4], out wpAbstract.Range);
            bool.TryParse(Caracts[5], out wpAbstract.Projectile);
            float.TryParse(Caracts[6], out wpAbstract.ScaleBullet);
            float.TryParse(Caracts[7], out wpAbstract.SpeedBullet);
            int.TryParse(Caracts[8], out wpAbstract.NbrBullet);
            bool.TryParse(Caracts[9], out wpAbstract.Gust);
            float.TryParse(Caracts[10], out wpAbstract.SpaceBullet);
            float.TryParse(Caracts[11], out wpAbstract.Angle);
            float.TryParse(Caracts[12], out wpAbstract.WidthRange);
            float.TryParse(Caracts[13], out wpAbstract.SpeedZone);
            float.TryParse(Caracts[14], out wpAbstract.TimeDest);
            float.TryParse(Caracts[15], out wpAbstract.FarEffect);
            float.TryParse(Caracts[16], out wpAbstract.TimeFarEffect);
            bool.TryParse(Caracts[17], out wpAbstract.Through);
            bool.TryParse(Caracts[18], out wpAbstract.Explosion);
            float.TryParse(Caracts[19], out wpAbstract.Diameter);
            int.TryParse(Caracts[20], out wpAbstract.BulletCapacity);
            float.TryParse(Caracts[21], out wpAbstract.BackPush);
            float.TryParse(Caracts[22], out wpAbstract.SpeedReduce);
            int.TryParse(Caracts[23], out wpAbstract.Damage);
            int.TryParse(Caracts[24], out wpAbstract.WeightRandom);
        }
    }
}
