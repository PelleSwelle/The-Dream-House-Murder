using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class GraphSaveUtility
{
    private DialogueGraphView targetGraphView;
    private DialogueContainer containerCache;

    private List<Edge> edges => targetGraphView.edges.ToList();
    private List<DialogueNode> nodes => targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();
    public static GraphSaveUtility getInstance(DialogueGraphView _targetGraphView)
    {
        return new GraphSaveUtility
        {
            targetGraphView = _targetGraphView
        };
    }

    public void saveGraph(string fileName)
    {
        var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();
        if (!saveNodes(dialogueContainer)) return;
        saveExposedProperties(dialogueContainer);

        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
        {
            AssetDatabase.CreateFolder("Assets", "Resources");
        }

        AssetDatabase.CreateAsset(dialogueContainer, $"Assets/Resources/{fileName}.asset");
    }

    private void saveExposedProperties(DialogueContainer container)
    {
        container.exposedProperties.AddRange(targetGraphView.exposedProperties);
    }

    private bool saveNodes(DialogueContainer dialogueContainer)
    {
        if (!edges.Any()) return false;


        var connectedPorts = edges.Where(x => x.input.node != null).ToArray();

        for (int i = 0; i < connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as DialogueNode;
            var inputNode = connectedPorts[i].input.node as DialogueNode;

            dialogueContainer.nodeLinks.Add(new NodeLinkData
            {
                baseNodeGuid = outputNode.GUID,
                portName = connectedPorts[i].output.portName,
                targetNodeGuid = inputNode.GUID
            });
        }

        foreach (var dialogueNode in nodes.Where(node => !node.isEntryPoint))
        {
            dialogueContainer.DialogueNodeData.Add(new DialogueNodeData
            {
                nodeGuid = dialogueNode.GUID,
                dialogueText = dialogueNode.dialogueText,
                position = dialogueNode.GetPosition().position
            });
        }
        return true;
    }


    /// <summary>
    /// load graph data from a specified file
    /// </summary>
    /// <param name="fileName"></param>
    public void loadGraph(string fileName)
    {
        containerCache = Resources.Load<DialogueContainer>(fileName);

        if (containerCache == null)
        {
            EditorUtility.DisplayDialog("File not found", "Target graph file does not exist", "OK");
            return;
        }

        clearGraph();
        createNodes();
        connectNodes();
        createExposedProperties();
    }

    /// <summary>
    /// 
    /// </summary>
    private void createExposedProperties()
    {
        targetGraphView.clearBlackboardAndExposedProperties();

        foreach (var exposedProperty in containerCache.exposedProperties)
        {
            targetGraphView.addPropertyToBlackboard(exposedProperty);
        }
    }

    private void connectNodes()
    {
        for (var i = 0; i < nodes.Count; i++)
        {
            var connections = containerCache.nodeLinks.Where(x => x.baseNodeGuid == nodes[i].GUID).ToList();
            for (int ii = 0; ii < connections.Count; ii++)
            {
                var targetNodeGuid = connections[ii].targetNodeGuid;
                var targetNode = nodes.First(x => x.GUID == targetNodeGuid);
                linkNodes(nodes[i].outputContainer[ii].Q<Port>(), (Port)targetNode.inputContainer[0]);

                targetNode.SetPosition(new Rect(
                    containerCache.DialogueNodeData.First(x => x.nodeGuid == targetNodeGuid).position,
                    targetGraphView.defaultNodeSize
                ));

            }
        }

    }

    private void linkNodes(Port output, Port input)
    {
        var tempEdge = new Edge
        {
            output = output,
            input = input
        };

        tempEdge.input.Connect(tempEdge);
        tempEdge.output.Connect(tempEdge);
        targetGraphView.Add(tempEdge);
    }

    private void createNodes()
    {
        foreach (var nodeData in containerCache.DialogueNodeData)
        {
            var tempNode = targetGraphView.createDialogeNode(nodeData.dialogueText, Vector2.zero);
            tempNode.GUID = nodeData.nodeGuid;
            targetGraphView.AddElement(tempNode);

            var nodePorts = containerCache.nodeLinks.Where(x => x.baseNodeGuid == nodeData.nodeGuid).ToList();
            nodePorts.ForEach(x => targetGraphView.addChoicePort(tempNode, x.portName));
        }
    }

    private void clearGraph()
    {
        nodes.Find(x => x.isEntryPoint).GUID = containerCache.nodeLinks[0].baseNodeGuid;

        foreach (var node in nodes)
        {
            if (node.isEntryPoint) continue;
            edges.Where(x => x.input.node == node).ToList()
                .ForEach(edge => targetGraphView.RemoveElement(edge));

            targetGraphView.RemoveElement(node);
        }
    }
}
