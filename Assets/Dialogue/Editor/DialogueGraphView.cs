using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Linq;
using UnityEngine;
using UnityEditor;

/// <summary>
/// The graph editor inside the window
/// </summary>
public class DialogueGraphView : GraphView
{
    public Blackboard blackboard;
    public readonly Vector2 defaultNodeSize = new Vector2(150, 200);
    private NodeSearchWindow searchWindow;
    public List<ExposedProperty> exposedProperties = new List<ExposedProperty>();
    public DialogueGraphView(EditorWindow editorWindow)
    {
        styleSheets.Add(Resources.Load<StyleSheet>("DialogueGraph"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();

        AddElement(generateEntryPointNode());
        addSearchWindow(editorWindow);
    }

    public void clearBlackboardAndExposedProperties()
    {
        exposedProperties.Clear();
        blackboard.Clear();
    }

    public void addPropertyToBlackboard(ExposedProperty exposedProperty)
    {
        var localPropertyName = exposedProperty.propertyName;
        var localPropertyValue = exposedProperty.propertyValue;

        while (exposedProperties.Any(x => x.propertyName == localPropertyName))
            localPropertyName = $"{localPropertyName}(1)";

        var property = new ExposedProperty();
        property.propertyName = exposedProperty.propertyName;
        property.propertyValue = exposedProperty.propertyValue;

        exposedProperties.Add(property);

        var container = new VisualElement();
        var blackboardField = new BlackboardField { text = property.propertyName, typeText = "string property" };
        container.Add(blackboardField);

        var propertyValueTextField = new TextField("Value: ")
        {
            value = localPropertyValue
        };
        propertyValueTextField.RegisterValueChangedCallback(evt =>
        {
            var changingPropertyIndex = exposedProperties.FindIndex(x => x.propertyName == property.propertyName);
            exposedProperties[changingPropertyIndex].propertyValue = evt.newValue;
        });
        var blackboardValueRow = new BlackboardRow(blackboardField, propertyValueTextField);
        container.Add(blackboardValueRow);

        blackboard.Add(container);
    }

    private void addSearchWindow(EditorWindow editorWindow)
    {
        searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
        searchWindow.Init(editorWindow, this);
        nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
    }

    /// <summary>
    /// make a list of compatible ports for each nodes.
    /// </summary>
    /// <param name="startPort"></param>
    /// <param name="nodeAdaptor"></param>
    /// <returns></returns>
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdaptor)
    {
        var compatiblePorts = new List<Port>();

        ports.ForEach((port) =>
        {
            if (startPort != port && startPort.node != port.node)
            {
                compatiblePorts.Add(port);
            }
        });
        return compatiblePorts;
    }

    /// <summary>
    /// generating a port in the node, either input or output
    /// </summary>
    /// <param name="node"></param>
    /// <param name="portDirection"></param>
    /// <param name="capacity"></param>
    /// <returns></returns>
    private Port generatePort(DialogueNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }

    /// <summary>
    /// create a dialogue node with an inputport, button to add options
    /// </summary>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    public DialogueNode createDialogeNode(string nodeName, Vector2 position)
    {
        var dialogueNode = new DialogueNode
        {
            title = nodeName,
            dialogueText = nodeName,
            GUID = Guid.NewGuid().ToString()
        };

        var inputPort = generatePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        dialogueNode.inputContainer.Add(inputPort);

        dialogueNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        var button = new Button(() =>
        {
            addChoicePort(dialogueNode);
        });

        button.text = "New Choice";
        dialogueNode.titleContainer.Add(button);

        var textField = new TextField(string.Empty);

        textField.RegisterValueChangedCallback(evt =>
        {
            dialogueNode.dialogueText = evt.newValue;
            dialogueNode.title = evt.newValue;
        });

        textField.SetValueWithoutNotify(dialogueNode.title);
        dialogueNode.mainContainer.Add(textField);

        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();
        dialogueNode.SetPosition(new Rect(position, defaultNodeSize));

        return dialogueNode;
    }

    /// <summary>
    /// Add an output port to the node 
    /// </summary>
    /// <param name="dialogueNode"></param>
    public void addChoicePort(DialogueNode dialogueNode, string overriddenPortName = "")
    {
        var generatedPort = generatePort(dialogueNode, Direction.Output);

        var oldLabel = generatedPort.contentContainer.Q<Label>("type");
        generatedPort.contentContainer.Remove(oldLabel);

        var outputPortCount = dialogueNode.outputContainer.Query("connector").ToList().Count;

        var choisePortName = string.IsNullOrEmpty(overriddenPortName)
            ? $"Choice {outputPortCount + 1}"
            : overriddenPortName;


        var textField = new TextField
        {
            name = string.Empty,
            value = choisePortName
        };

        textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
        generatedPort.contentContainer.Add(new Label("  "));
        generatedPort.contentContainer.Add(textField);
        var deleteButton = new Button(
            () => removePort(dialogueNode, generatedPort))
        { text = "X" };
        generatedPort.contentContainer.Add(deleteButton);


        generatedPort.portName = choisePortName;
        dialogueNode.outputContainer.Add(generatedPort);
        dialogueNode.RefreshPorts();
        dialogueNode.RefreshExpandedState();
    }

    private void removePort(DialogueNode dialogueNode, Port generatedPort)
    {
        var targetEdge = edges.ToList().Where(x => x.output.portName == generatedPort.portName && x.output.node == generatedPort.node);

        if (!targetEdge.Any())
        {
            var _edge = targetEdge.First();
            _edge.input.Disconnect(_edge);
            RemoveElement(targetEdge.First());
        }

        var edge = targetEdge.First();

        edge.input.Disconnect(edge);
        RemoveElement(targetEdge.First());

        dialogueNode.outputContainer.Remove(generatedPort);
        dialogueNode.RefreshPorts();
        dialogueNode.RefreshExpandedState();
    }


    /// <summary>
    /// add a node to the graphView
    /// </summary>
    /// <param name="nodeName"></param>
    public void createNode(string nodeName, Vector2 mousePosition)
    {
        AddElement(createDialogeNode(nodeName, mousePosition));
    }

    /// <summary>
    /// generate a specific type of node with only outputs
    /// </summary>
    /// <returns></returns>
    private DialogueNode generateEntryPointNode()
    {
        var node = new DialogueNode
        {
            title = "START",
            GUID = Guid.NewGuid().ToString(),
            dialogueText = "ENTRYPOINT",
            isEntryPoint = true
        };

        var generatedPort = generatePort(node, Direction.Output);
        generatedPort.portName = "Next";

        node.outputContainer.Add(generatedPort);

        node.capabilities &= ~Capabilities.Movable;
        node.capabilities &= ~Capabilities.Deletable;

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(100, 200, 100, 150));
        return node;
    }
}
