# BehaviorTreeAutomation
Automation and speeding up workflow with custom UI

PR: Behavior Tree Editor Window with Runtime Execution Support


Summary: To build a custom Behavior Tree Editor Window for Unity that allows content integrators or developers to create behavior trees visually via the Editor. This setup allows rapid prototyping of AI behavior without hardcoding trees every time new tree or nodes need to be created or added.

Problem: In Unity, behavior trees are often hardcoded or require third-party visual editors. This made it time-consuming to test or iterate AI behaviors during development. I wanted a faster way to prototype behaviors without constantly rewriting scripts like in my case -AIChar, and ideally, build everything visually inside Unity.

My Approach: Create a window → Create UI in the window → Add functionality using buttons. Instead of attaching an AI character script and hardcoding behavior, I created constructors for each strategy. These are used to generate leaf nodes, which can then be attached to a sequence node. Sequences are added to selectors, selectors to repeater decorators, and finally the repeater node is assigned to the tree. I built a full UI that allows:

• Selecting node types (leaf, sequence, selector, repeater)

• Selecting strategy types for leaf nodes and entering required parameters (like AI/player/destination GameObjects)

• Creating nodes and organizing them into a hierarchy using dropdowns

• Assigning the root to the tree and saving it into memory for runtime access

I designed the system to allow users to set and adjust values at runtime wherever possible. The tree is stored in a static memory holder BTMemoryStore.tree, which is referenced at runtime to avoid losing the structure on play.

How to Use:

  1.Add the scripts to the assets folder in the project.

2. Create a scene- Player gameobject, AI gameobject, AImanager for AI Char script attached.

3. Play and open the behavior tree from custom tools menu.

4. Name all the nodes before adding them. First add leaf node of DestFollowCheck strategy.Then add leaf of PlayerAction strategy with the player and AI assigned and keep the bool false.

5. Then go ahead and add a sequence. Now, in the parent, select sequence and in the child select the destcheck leaf first, add child and then playeraction next, add it to the sequence.

6. Now add a selector and add the sequence created before as child to it.

7. Then add a repeaterselector and add the selector to this.

8. Now click on build tree and observe the ai move towards the player. 


Features: 

• Used different node types (Leaf, Sequence, Selector, Repeater) and strategies (PlayerFollowCheck, PlayerAction, etc.)

• Created a menu item under CustomTools -> Behavior Tree to launch the custom window.

• UI to:

o Input node names.

o Choose node types and attach strategies for leaves.

o Drag/drop GameObjects like AI and Player directly into the editor window.

o Attach child nodes to parent nodes hierarchically.

o Build a full behavior tree with runtime memory storage.

• Tree is stored using a static BTMemoryStore.tree variable for runtime access.

Strategy Support: • PlayerFollowCheck • DestFollowCheck • PlayerAction • FollowPlayer

Additional Details:

• Parameters like AI GameObject, Destination, Player, and conditional booleans can be assigned per strategy.

• Runtime execution confirmed using debug logs after assigning the built tree to an AI controller script.

• I optimized the scripts necessary for the task.

Further extensions:

• If new strategies are coded, i can extend this tool so that they can be added to the window and used for the leaf.

• The trees can be built before runtime and retrieved during runtime using scriptable objects or JSON. I did test this with JSON successfully but to keep the implementation light weighted, I removed this part. 
