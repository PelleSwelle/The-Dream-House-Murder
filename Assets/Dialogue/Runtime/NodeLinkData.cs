using System;
/// <summary>
/// Holds data about connections between two nodes
/// </summary>
[Serializable]
public class NodeLinkData
{
    public string baseNodeGuid;
    public string portName;
    public string targetNodeGuid;
}