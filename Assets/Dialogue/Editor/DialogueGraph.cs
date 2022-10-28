using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System;
using System.Linq;

/// <summary>
/// An window for the graph editor
/// </summary>
public class DialogueGraph : EditorWindow
{
    private DialogueGraphView graphView;
    private string fileName = "New Narrative";

    /// <summary>
    /// making a menu item in the top toolbar in unity
    /// make a new window with a specific title
    /// </summary>
    [MenuItem("Graph/Dialogue Graph")]
    public static void OpenDialogueGraphWindow()
    {
        var window = GetWindow<DialogueGraph>();
        window.titleContent = new GUIContent("Dialogue Graph");
    }

    /// <summary>
    /// Make a graph view inside this window
    /// </summary>
    private void ConstructGraphView()
    {
        graphView = new DialogueGraphView(this)
        {
            name = "Dialogue Graph"
        };

        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);
    }

    /// <summary>
    /// when window is opened
    /// </summary>
    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
        generateMiniMap();
        generateBlackBoard();
    }

    /// <summary>
    /// generate the blackboard going into the graph view
    /// </summary>
    private void generateBlackBoard()
    {
        var blackboard = new Blackboard(graphView);
        blackboard.Add(new BlackboardSection { title = "Exposed properties" });
        blackboard.editTextRequested = (blackboard, element, newValue) =>
        {
            var oldPropertyName = ((BlackboardField)element).text;
            if (graphView.exposedProperties.Any(XboxBuildSubtarget => XboxBuildSubtarget.propertyName == newValue))
            {
                EditorUtility.DisplayDialog("Error", "This property name already exists, please choose another one", "OK");
                return;
            }

            var propertyIndex = graphView.exposedProperties.FindIndex(x => x.propertyName == oldPropertyName);
            graphView.exposedProperties[propertyIndex].propertyName = newValue;
            ((BlackboardField)element).text = newValue;
        };
        blackboard.addItemRequested = _blackboard => { graphView.addPropertyToBlackboard(new ExposedProperty()); };
        blackboard.SetPosition(new Rect(10, 30, 200, 300));
        graphView.blackboard = blackboard;
        graphView.Add(blackboard);
    }


    /// <summary>
    /// Generate the minimap going into the graph view
    /// </summary>
    private void generateMiniMap()
    {
        var miniMap = new MiniMap { anchored = true };
        var coords = graphView.contentViewContainer.WorldToLocal(new Vector2(this.maxSize.x - 10, 30));
        miniMap.SetPosition(new Rect(coords.x, coords.y, 200, 140));
        graphView.Add(miniMap);
    }

    /// <summary>
    /// The toolbar at the top of the window, allowing ex. a button for instantiating nodes
    /// </summary>
    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        // TEXT FIELD FOR THE FILE NAME
        var fileNameTextField = new TextField("File Name:");
        fileNameTextField.SetValueWithoutNotify(fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => fileName = evt.newValue);
        toolbar.Add(fileNameTextField);

        toolbar.Add(new Button(() => requestDataOperation(true)) { text = "Save Data" });
        toolbar.Add(new Button(() => requestDataOperation(false)) { text = "Load Data" });


        rootVisualElement.Add(toolbar);
    }

    /// <summary>
    /// loads or saves data for the graph
    /// </summary>
    /// <param name="save"></param>
    private void requestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            EditorUtility.DisplayDialog("Invalid file name!", "please enter a valid file name", "OK");
        }

        var saveUtility = GraphSaveUtility.getInstance(graphView);
        if (save)
        {
            saveUtility.saveGraph(fileName);
        }
        else
        {
            saveUtility.loadGraph(fileName);
        }
    }

    /// <summary>
    /// When the window closes
    /// </summary>
    private void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }
}
