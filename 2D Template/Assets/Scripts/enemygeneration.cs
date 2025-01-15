using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class enemygeneration : MonoBehaviour
{
    public Transform[] healerPositions;
    public GameObject cursenun;
    public GameObject attacknun;
    public GameObject healernun;

    public float minstep = 3f;
    public float maxstep = 7f;

    public float hs;
    public float vs;

    public bool DrawRangeWhenSelectedGizmo;

    int round = -1;
    public int[] rounds;

    private void Start()
    {

    }
    void Generate(float hspan, float vspan, int enemylimit)
    {
        float enemies = enemylimit;
        float atckenemies = Mathf.Floor(enemies * UnityEngine.Random.Range(0.5f, 0.8f));
        Vector3 spawn = new Vector3(transform.position.x - hspan / 2, transform.position.y + vspan / 2, 0); ;
        
        while (spawn.y > transform.position.y - vspan / 2)
        {
            spawn += new Vector3(UnityEngine.Random.Range(minstep, maxstep), 0 , 0);
            if (spawn.x - transform.position.x > hspan / 2)
            {
                spawn = new Vector3(transform.position.x + hspan / 2, spawn.y, 0);
            }
            GameObject newenemy;
            if (atckenemies == 0)
            {

                newenemy = Instantiate(cursenun);
            }
            else
            {

                newenemy = Instantiate(attacknun);
            }

            enemies -= 1;
            if (atckenemies > 0)
            {
                Debug.Log("atckenemeies: " + atckenemies);
                atckenemies -= 1;
            }
            newenemy.transform.position = spawn;
            if (spawn.x == transform.position.x + hspan / 2)
            {
                spawn = new Vector3(transform.position.x - hspan / 2, spawn.y - UnityEngine.Random.Range(minstep, maxstep), 0);
            }
            if (enemies == 0)
            {
                break;
            }
        }
    }
    void RefreshHealers()
    {
        foreach (Transform newhealerpos in healerPositions)
        {
            GameObject newhealer = Instantiate(healernun);
            newhealer.transform.position = newhealerpos.position;
        }
    }
    void Update()
    {
        if (FindObjectsOfType<enemy>().Length == 0)
        {
            round++;
            Generate(hs, vs, rounds[round]);
            RefreshHealers();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, 2.5f);

        if(DrawRangeWhenSelectedGizmo == false)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(transform.position, new Vector3(hs, vs, 0));
        }

    }

    private void OnDrawGizmosSelected()
    {
        if(DrawRangeWhenSelectedGizmo == true)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(transform.position, new Vector3(hs, vs, 0));
        }
    }

//#if UNITY_EDITOR
//    [CustomEditor(typeof(enemygeneration))]
//    public class EnemyGenerationEditor : Editor
//    {
//        private int numberOfRounds;

//        public override void OnInspectorGUI()
//        {
//            enemygeneration script = (enemygeneration)target;

//            DrawPropertiesExcluding(serializedObject, "rounds");

//            EditorGUILayout.Space();
//            EditorGUILayout.LabelField("Rounds Configuration", EditorStyles.boldLabel);

//            numberOfRounds = EditorGUILayout.IntField("Number of Rounds", numberOfRounds);

//            if (GUILayout.Button("Set Number of Rounds"))
//            {
//                AdjustRoundsArray(script, numberOfRounds);
//            }

//            SerializedProperty roundsProperty = serializedObject.FindProperty("rounds");
//            for (int i = 0; i < roundsProperty.arraySize; i++)
//            {
//                SerializedProperty round = roundsProperty.GetArrayElementAtIndex(i);
//                EditorGUILayout.PropertyField(round, new GUIContent($"Round {i + 1}"));
//            }

//            serializedObject.ApplyModifiedProperties();
//        }

//        private void AdjustRoundsArray(enemygeneration script, int newSize)
//        {
//            SerializedProperty roundsProperty = serializedObject.FindProperty("rounds");
//            roundsProperty.arraySize = newSize;

//            for (int i = 0; i < newSize; i++)
//            {
//                if (roundsProperty.GetArrayElementAtIndex(i).intValue == 0)
//                {
//                    roundsProperty.GetArrayElementAtIndex(i).intValue = 0;
//                }
//            }

//            serializedObject.ApplyModifiedProperties();
//        }
//    }
//#endif
}
//2













//written by a shitty retard :DDDDDDDD
//everyone's stupid you're stupid I'm cr@@@ZY FUCK EVERYTHING FUCK THE UNIVERSE GO TO FUCKING HELL MOTHERFUCKER DAMNIT
//I'M @ $tUp1D @D0R@b13 5 Y3@r 01D, r0@$t m3 M0tH3RfUck3R LOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOL
//i'M @n IDi0t LOL FUCK YOU FUCK ME FUCK THE UNIVERSE LOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOLOL
//HEY MOTHERFUCKER ROAST ME MOTHERFUCKER I HATE YOU MOTHERFUCKER DAMMITDAMMIT II HATE MY FUCKING SELF
// I"M A sh1TtY LiL M0th3RfUCk3R Fr0M pR3-K, I @m cUt3 @Nd n@1v3 BLLBLBLBLBLBLBLBLLBLBLBLBLBLBLBLBLLBLBLBLBLBLBLBLB
//K1$$ m3 M0tHErFuCK3r h01D mY h@Nd m0TherFUck3r I'm a shiiiiityyyyy 3-year-old tard M0tHerFuCKer
//h01D My h@nD m0Th3RfUCk3R fUcK mY$31f
//Ima sTUpId lIl b0y weewewewewewewe lolololololololollolololol
//yopu are sooooooooooooooooooooooooooooooooooooooooooooo0o0o0o0o0o0ooo0oo0o0oo0o0o0o0o CUUUUUTEETEEE
//YOU NAIVE LITTLE SHIITTTING MOTHERFUCKOING DAMN FUCKKING ASSHOLE TARD SHIIT CRAP 5-yE@r 01D
//
