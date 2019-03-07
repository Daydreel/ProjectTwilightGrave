using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

public abstract class NodeBasedEditor : EditorWindow
{
    //Gui elements
    private List<Node> nodes;
    private List<Connection> connections;

    
    private GUIStyle nodeStyle;
    private GUIStyle selectedNodeStyle;
    private GUIStyle inPointStyle;
    private GUIStyle outPointStyle;

    private ConnectionPoint selectedInPoint;
    private ConnectionPoint selectedOutPoint;

    //Drag coordinates for every drag events
    private Vector2 drag;

    //Menu Bar
    private float menuBarHeight = 20f;
    private Rect menuBar;

    private void OnEnable()
    {
        //Node Style
        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        nodeStyle.border = new RectOffset(12, 12, 12, 12);

        //Selected Node Style
        selectedNodeStyle = new GUIStyle();
        selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

        //Connection Points Style
        inPointStyle = new GUIStyle();
        inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        inPointStyle.border = new RectOffset(4, 4, 12, 12);

        outPointStyle = new GUIStyle();
        outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        outPointStyle.border = new RectOffset(4, 4, 12, 12);

    }

    private void OnGUI()
    {
        DrawGrid(20, 0.2f, Color.gray);
        DrawGrid(100, 0.4f, Color.gray);
        DrawMenuBar();

        DrawNodes();
        OnDrawConnections();

        DrawConnectionLine(Event.current);

        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);

        if (GUI.changed)
            Repaint();
    }

    private void DrawMenuBar()
    {
        menuBar = new Rect(0, 0, position.width, menuBarHeight);

        GUILayout.BeginArea(menuBar, EditorStyles.toolbar);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button(new GUIContent("Save"), EditorStyles.toolbarButton, GUILayout.Width(35)))
        {
            //Save();
            Debug.Log("Save button");
        }

        GUILayout.Space(5);

        if (GUILayout.Button(new GUIContent("Load"), EditorStyles.toolbarButton, GUILayout.Width(35)))
        {
            //Load();
            Debug.Log("Load button");
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }


    private void DrawNodes()
    {
        if (nodes == null)
        {
            return;
        }

        foreach (Node node in nodes)
        {
            node.Draw();
        }
    }

    private void OnDrawConnections()
    {
        if (connections == null)
        {
            return;
        }

        foreach (Connection connection in connections)
        {
            connection.Draw();
        }
    }

    private void ProcessEvents(Event e)
    {

        drag = Vector2.zero;
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    ClearConnectionSelection();
                }
                if (e.button == 1)
                {
                    ProcessContextMenu(e.mousePosition);
                }
                break;

            case EventType.MouseDrag:
                if (e.button == 0)
                {
                    OnDrag(e.delta);
                }
                break;
        }
    }

    private void ProcessNodeEvents(Event e)
    {
        if(nodes == null)
        {
            return;
        }

        foreach (Node node in nodes)
        {
            bool guiChanged = node.ProcessEvents(e);

            if (guiChanged)
            {
                GUI.changed = true;
            }
        }
    }

    private void ProcessContextMenu( Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add Node"), false, () => OnClickAddNode(mousePosition));
        genericMenu.ShowAsContext();
    }

    private void OnClickAddNode(Vector2 mousePosition)
    {
        if (nodes == null)
        {
            nodes = new List<Node>();
        }

        nodes.Add(new Node(mousePosition, 200, 50, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode));
    }

    private void OnClickInPoint(ConnectionPoint inPoint)
    {
        selectedInPoint = inPoint;

        if (selectedOutPoint != null)
        {
            if (selectedOutPoint.node !=selectedInPoint.node)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    private void OnClickOutPoint(ConnectionPoint outPoint)
    {
        selectedOutPoint = outPoint;

        if (selectedInPoint != null)
        {
            if (selectedOutPoint.node != selectedInPoint.node)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    private void OnClickRemoveConnection(Connection connection)
    {
        connections.Remove(connection);
    }
    
    private void CreateConnection()
    {
        if (connections == null)
        {
            connections = new List<Connection>();
        }

        connections.Add(new Connection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection));
    }

    private void ClearConnectionSelection()
    {
        selectedInPoint = null;
        selectedOutPoint = null;
    }

    private void OnClickRemoveNode(Node node)
    {
        if (connections != null)
        {
            List<Connection> connectionsToRemove = new List<Connection>();

            foreach (Connection connection in connections)
            {
                if (connection.inPoint == node.inPoint || connection.outPoint == node.outPoint)
                {
                    connectionsToRemove.Add(connection);
                }
            }

            foreach (Connection connection in connectionsToRemove)
            {
                connections.Remove(connection);
            }

            connectionsToRemove = null;
        }

        nodes.Remove(node);
    }

    private void OnDrag(Vector2 delta)
    {
        drag = delta;
        if (nodes != null)
        {
            foreach (Node node in nodes)
            {
                node.Drag(delta);
            }
        }

        GUI.changed = true;
    }

    private void DrawConnectionLine(Event e)
    {
        if (selectedInPoint != null && selectedOutPoint == null)
        {
            Handles.DrawBezier(
                selectedInPoint.rect.center,
                e.mousePosition,
                selectedInPoint.rect.center + Vector2.left * 50f,
                e.mousePosition - Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }

        if (selectedOutPoint != null && selectedInPoint == null)
        {
            Handles.DrawBezier(
                selectedOutPoint.rect.center,
                e.mousePosition,
                selectedOutPoint.rect.center - Vector2.left * 50f,
                e.mousePosition + Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }
    }
    #region Draw Grid functionnality
    private Vector2 offset;

    private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        offset += drag * 0.5f;
        Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);

        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();

    }


    #endregion


    //WIP Save and Load function
    /*
        #region Save/load functions
        //TODO Save graph as scriptable objects
        private void Save()
        {
            if (nodes == null)
            {
                return;
            }
            XMLOp.Serialize(nodes, "Assets/Scripts/Tools/Node Based Editor/Saves/nodes.xml");


            if (connections == null)
            {
                return;
            }
            XMLOp.Serialize(connections, "Assets/Scripts/Tools/Node Based Editor/Saves/connections.xml");
        }

        private void Load()
        {
            var nodesDeserialized = XMLOp.Deserialize<List<Node>>("Assets/Scripts/Tools/Node Based Editor/Saves/nodes.xml");
            var connectionsDeserialized = XMLOp.Deserialize<List<Connection>>("Assets/Scripts/Tools/Node Based Editor/Saves/connections.xml");

            nodes = new List<Node>();
            connections = new List<Connection>();

            foreach (Node nodeDeserialized in nodesDeserialized)
            {
                nodes.Add(new Node(
                    nodeDeserialized.rect.position,
                    nodeDeserialized.rect.width,
                    nodeDeserialized.rect.height,
                    nodeStyle,
                    selectedNodeStyle,
                    inPointStyle,
                    outPointStyle,
                    OnClickInPoint,
                    OnClickOutPoint,
                    OnClickRemoveNode,
                    nodeDeserialized.inPoint.id,
                    nodeDeserialized.outPoint.id
                    )
                );
            }

            foreach (Connection connectionDeserialized in connectionsDeserialized)
            {
                var inPoint = nodes.First(n => n.inPoint.id == connectionDeserialized.inPoint.id).inPoint;
                var outPoint = nodes.First(n => n.outPoint.id == connectionDeserialized.outPoint.id).outPoint;
                connections.Add(new Connection(inPoint, outPoint, OnClickRemoveConnection));
            }
        }

        #endregion
        */
}
