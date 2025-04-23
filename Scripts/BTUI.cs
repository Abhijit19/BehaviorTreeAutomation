using UnityEditor;
using UnityEngine;
using BehaviorTree;
using System.IO;
using System.Collections.Generic;

public class BehaviorTreeEditor : EditorWindow
{
    //This is all the data required for creating the UI for the editor window with the tree creation functionality 
    private string nodeName = "NodeName"; 
    private int selectedNodeTypeIndex = 0;
    private int selectedStrategyIndex = 0;

    private string[] nodeTypes = { "Leaf", "Sequence", "Selector", "Repeater" }; //storing all the node types for the dropdown UI to select from
    private string[] strategyTypes = {
        "PlayerFollowCheck", "PlayerAction", "DestFollowCheck", "FollowPlayer"
    };     //leaf type nodes need these strategy types for their parameters for the actual behavior/action function

    //references for in game objects for AI and player along with node traversal condition
    private GameObject aiGO, playerGO, destGO; 
    private bool conditionToggle = false;
   
    private List<Node> allNodes = new List<Node>(); //contains all the nodes added 
    private Dictionary<string, Node> nodeLookup = new Dictionary<string, Node>();
    private int parentIndex = 0;
    private int childIndex = 0;

    private BehaviorTree.Tree tree;

    [MenuItem("CustomTools/Behavior Tree")] //I created a menu item on the top bar    
    public static void ShowWindow()
    {
        GetWindow<BehaviorTreeEditor>("BT Editor"); //This creates the window with the given name
    }

    void OnGUI()   //To draw on the window
    {
        GUILayout.Label("Create Node", EditorStyles.boldLabel);     //Used labels to write text on the window

        nodeName = EditorGUILayout.TextField("Node Name", nodeName);  //every node name
        selectedNodeTypeIndex = EditorGUILayout.Popup("Node Type", selectedNodeTypeIndex, nodeTypes); // dropdown menu created for selecting node type such as leaf,sequence etc

        if (nodeTypes[selectedNodeTypeIndex] == "Leaf")  // if leaf is selected strategy dropdown is displayed for the strategy type
        {
            selectedStrategyIndex = EditorGUILayout.Popup("Strategy", selectedStrategyIndex, strategyTypes);
            GUILayout.Label("Set Strategy Parameters", EditorStyles.boldLabel);

            switch (strategyTypes[selectedStrategyIndex])// for assigning parameters for the strategies
            {

                case "PlayerAction":
                    aiGO = (GameObject)EditorGUILayout.ObjectField("Current Pos (AI)", aiGO, typeof(GameObject), true);
                    destGO = (GameObject)EditorGUILayout.ObjectField("Destination", destGO, typeof(GameObject), true);
                    conditionToggle = EditorGUILayout.Toggle("Condition", conditionToggle);
                    break;
                case "FollowPlayer":
                    aiGO = (GameObject)EditorGUILayout.ObjectField("Current Pos (AI)", aiGO, typeof(GameObject), true);
                    playerGO = (GameObject)EditorGUILayout.ObjectField("Target (Player)", playerGO, typeof(GameObject), true);
                    conditionToggle = EditorGUILayout.Toggle("Condition", conditionToggle);
                    break;
            }
        }

        if (GUILayout.Button("Add Node")) // if add node button is clicked, based on the node type, the sequence or leaf or selector etc is created
        {
            

            switch (nodeTypes[selectedNodeTypeIndex])
            {
                case "Leaf":
                    leaf newleafNode = new leaf(nodeName, GetStrategy(strategyTypes[selectedStrategyIndex]));
                   
                    if (newleafNode != null)   //adding the node to the list
                    {
                        allNodes.Add(newleafNode);
                        nodeLookup[nodeName] = newleafNode;
                        Debug.Log("leaf created");
                    }
                    break;
                case "Sequence":
                    sequence newseqNode = new sequence(nodeName);
                    if (newseqNode != null)
                    {
                        allNodes.Add(newseqNode);
                        nodeLookup[nodeName] = newseqNode;
                        Debug.Log("sequence created");
                    }
                    break;
                case "Selector":
                    selector newselNode = new selector(nodeName);
                    if (newselNode != null)
                    {
                        allNodes.Add(newselNode);
                        nodeLookup[nodeName] = newselNode;
                        Debug.Log("selector created");
                    }
                    break;
                case "Repeater":
                    RepeaterDecorator newrdNode = new RepeaterDecorator(nodeName, true);
                    if (newrdNode != null)
                    {
                        allNodes.Add(newrdNode);
                        nodeLookup[nodeName] = newrdNode;
                        Debug.Log("repeaterdecorator created");
                    }
                    break;
            }

            
        }

        GUILayout.Space(15);
        GUILayout.Label("Attach Child to Parent", EditorStyles.boldLabel);

        if (allNodes.Count >= 2)// for adding hirearchy - leaf to sequence, sequence to selector, selector to repeaterdecorator and finally the repeaterdecorator to the tree
        {
            string[] names = allNodes.ConvertAll(n => n.name).ToArray();// converts allnodes list for the nodes created to display under
            parentIndex = EditorGUILayout.Popup("Parent Node", parentIndex, names);//Displays all the nodes for parent 
            childIndex = EditorGUILayout.Popup("Child Node", childIndex, names); // displays all the nodes for child

            if (GUILayout.Button("Add Child")) //for adding the child
            {
                if (parentIndex != childIndex)
                {
                    var parent = allNodes[parentIndex];
                    var child = allNodes[childIndex];
                    
                    if (parent is leaf)
                    {
                        Debug.LogWarning("Cannot add child to a leaf node.");
                    }
                    else
                    {
                        parent.AddChild(child);    //directly adds the child node to the respective parent(example: leaf to sequence)
                        Debug.Log($"Added {child.name} as child to {parent.name}");
                    }
                }
            }
        }

        GUILayout.Space(15);
        if (GUILayout.Button("Build Final Tree")) //to build tree with the existing nodes
        {
            if (allNodes.Count > 0)
            {

                tree = new BehaviorTree.Tree("mytree");
                tree.AddChild(allNodes[allNodes.Count-1]); // adds the final root to the tree that has all the nodes in the hirearchy
                BTMemoryStore.tree = tree;//for storing and retreiving at runtime
                Debug.Log("Tree built with root node: " );
            }
        }

        if (GUILayout.Button("Clear All"))
        {
            allNodes.Clear();
            nodeLookup.Clear();
            tree = null;
        }
    }

    IStrategy GetStrategy(string strategyName)  //to create constructors for the strategies in order to run the leaf
    {
        switch (strategyName)
        {
            case "PlayerFollowCheck": return new PlayerFollowCheckLeaf();
            case "DestFollowCheck": return new DestFollowCheckLeaf();
            case "PlayerAction": return new PlayerAction(aiGO, destGO,  conditionToggle);
            case "FollowPlayer": return new FollowPlayer(aiGO, destGO, conditionToggle);

            default: return new PlayerFollowCheckLeaf();
        }
    }
}
