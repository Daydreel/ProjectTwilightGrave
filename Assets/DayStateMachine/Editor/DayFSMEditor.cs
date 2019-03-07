using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System.IO;

public class DayFSMEditor : EditorWindow
{
    //TODO build a state, a unique State

    //Load all the DayFSM for editor
    List<DayFSM> FSMs;
    List<string> someTitles;

    string fsmName = "Hello World";

    [MenuItem("Tool/ Day FSM Editor")]
    private static void OpenWindow()
    {
        DayFSMEditor window = GetWindow<DayFSMEditor>("Day FSM Editor");
    }

    private void OnGUI()
    {

        FSMs = new List<DayFSM>();

        if (FSMs != null)
        {
            loadOne();
            foreach (DayFSM fsm in FSMs)
            {
                GUILayout.Label(fsm.name, EditorStyles.boldLabel);
            }
        }
        else
        {
            GUILayout.Label("No Titles", EditorStyles.boldLabel);
        }

        fsmName = EditorGUILayout.TextField("Hello", fsmName);

        if (GUILayout.Button("Save Title"))
        {
            DayFSM fsm = new DayFSM();
            fsm.name = fsmName;

            //TestFSM();

            saveOne(fsm);
        }
    }

    #region Save/Load

    void loadOne()
    {
        // deserialize JSON directly from a file
        using (StreamReader file = File.OpenText(@"Assets/DayStateMachine/Data/data.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            DayFSM FSM = (DayFSM)serializer.Deserialize(file, typeof(DayFSM));
            FSMs.Add(FSM);
        }
    }


    void saveOne(DayFSM fsm)
    {
        // serialize JSON directly to a file
        using (StreamWriter file = File.CreateText(@"Assets/DayStateMachine/Data/data.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, fsm);
        }
    }
    #endregion


    void TestFSM()
    {
        DayFSM testFSM = new DayFSM();

        testFSM.name = "Hello World";

        JsonConvert.SerializeObject(testFSM);

        // serialize JSON directly to a file
        using (StreamWriter file = File.CreateText(@"Assets/DayStateMachine/Data/data.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, testFSM);
        }
    }
}
