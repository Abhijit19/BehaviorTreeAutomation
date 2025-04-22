using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using BehaviorTree;

public class AIChar : MonoBehaviour
{
    //public GameObject destination; 
  
    //public GameObject player;
    //public GameObject ui;
    //public GameObject uiwin;
    void Awake()
    {
        

    //    PlayerFollowCheckLeaf playerFollowC = new PlayerFollowCheckLeaf();
    //    leaf playerDetour = new leaf("FollowPlayerCheck", playerFollowC);
    //    FollowPlayer playerfollow = new FollowPlayer(gameObject, destination, Enterarea.detour);

    //    leaf followplayer = new leaf("FollowPlayer", playerfollow);

        
    //    DestFollowCheckLeaf destcheck = new DestFollowCheckLeaf();
    //    leaf destC = new leaf("GotoDest", destcheck);
    //    PlayerAction GotoDest = new PlayerAction(gameObject, player, Enterarea.detour);
    //    leaf destfollow = new leaf("DestFollow", GotoDest);

    //    PlayerReturnCheckLeaf checkplayer = new PlayerReturnCheckLeaf(gameObject, player);
    //    leaf PlayerCheck = new leaf("CheckingPlayer", checkplayer);

    //    PlayerReturnLeaf endgame = new PlayerReturnLeaf(gameObject, player, ui, uiwin);
    //    leaf EndGame = new leaf("EndingGame", endgame);

    //    sequence PlayerReturnsequence = new sequence("CheckingPlayerReturn");
    //    PlayerReturnsequence.AddChild(PlayerCheck);
    //    PlayerReturnsequence.AddChild(EndGame);

    //   sequence followplayerSequence = new sequence("CheckPlayer");
    //    followplayerSequence.AddChild(playerDetour);
    //    followplayerSequence.AddChild(followplayer);

        
    //    sequence followdestSequence = new sequence("CheckDest");
    //    followdestSequence.AddChild(destC);
    //    followdestSequence.AddChild(destfollow);

    //    selector followSelector = new selector("FollowPlayerOrDestination");
    //    followSelector.AddChild(PlayerReturnsequence);
    //    followSelector.AddChild(followplayerSequence); 
    //    followSelector.AddChild(followdestSequence);
        
       

    //    RepeaterDecorator AICheck = new RepeaterDecorator("MyAICheck", !Enterarea.dest);
    //    AICheck.AddChild(followSelector);
       




    //    tree = new BehaviorTree.Tree("AIChar");
    //    tree.AddChild(AICheck);
    }

    void Update()
    {
      
        if (BTMemoryStore.tree != null)
        {
          BTMemoryStore.tree.Process();
            Debug.Log("tree is running");
        }
       
    }
}
