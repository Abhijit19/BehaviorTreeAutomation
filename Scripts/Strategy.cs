using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public interface IStrategy
    {
        
        Node.State Process();
        void reset();

    }
    public class PlayerFollowCheckLeaf : IStrategy
    {
        int currentindex;
        
        
        public PlayerFollowCheckLeaf()
        {
            
            
            
        
        }

        public Node.State Process()
        {



            if (Enterarea.detour)
            {
                return Node.State.success;
            }
            else
            {
                return Node.State.failure;
            }


           
            

        }

        public void reset()
        {

            currentindex = 0;
        }
    }

    public class DestFollowCheckLeaf : IStrategy
    {
        int currentindex;
        

        public DestFollowCheckLeaf()
        {
            

        
        }

        public Node.State Process()
        {
            Debug.Log("in the check still");
            if (Enterarea.detour)
            {
                Debug.Log("failurecheck");
                return Node.State.failure;
            }
            else
            {
                Debug.Log("checksucceeded");
                return Node.State.success;
            }


            
            

        }

        public void reset()
        {
            currentindex = 0;
        }
    }
    public class PlayerReturnCheckLeaf : IStrategy
    {
        int currentindex;
        GameObject currentpos;
        GameObject dest;

        public PlayerReturnCheckLeaf(GameObject currentpos, GameObject dest)
        {
            this.currentpos = currentpos;
            this.dest = dest;



        }

        public Node.State Process()
        {

           

           // float direction = Vector3.Dot(currentpos.transform.position, dest.transform.position);
            if (Enterarea.exiting)
            {
                Debug.Log("CheckSucceeded");
                return Node.State.success;
            }
            else
            {
                
                return Node.State.failure;
            }




        }

        public void reset()
        {

            currentindex = 0;
        }
    }

    public class PlayerReturnLeaf : IStrategy
    {
        int currentindex;
        int count;
        GameObject currentpos;
        GameObject dest;
        GameObject chatpanels;
        GameObject uiwin;
        public PlayerReturnLeaf(GameObject currentpos, GameObject dest, GameObject chatpanels, GameObject uiwin)
        {

            this.currentpos = currentpos;
            this.dest = dest;
            this.chatpanels = chatpanels;
            this.uiwin = uiwin;
        }

        public Node.State Process()
        {



            float direction = Vector3.Dot(currentpos.transform.position, dest.transform.position);
            
                if (Enterarea.exited)
                {

                        uiwin.SetActive(true);
                        Enterarea.exiting = false;
                        return Node.State.success;
                                    
                }
           

            if (count < 1)
            {
                chatpanels.SetActive(true);
                
            }
            count++;
            return Node.State.running;

        }

        public void reset()
        {

            currentindex = 0;
        }
    }



    public class PlayerAction : IStrategy
    {
        GameObject currentpos;
        GameObject dest;
        int currentindex;
        float speed = 2.4f;
        float tolerance = 4.1f;
        bool condition;
        
        public PlayerAction(GameObject currentpos, GameObject dest, bool condition)
        {
                
            this.currentpos = currentpos;
            this.dest = dest;
            this.condition = condition;
        }

        public Node.State Process()
        {



            Debug.Log("beginning");
            if (Enterarea.detour|| Enterarea.exiting)
            {
                Debug.Log("failure");
                return Node.State.failure;
            }
            float dist = Vector3.Distance(currentpos.transform.position, dest.transform.position);
            if (dist < tolerance)
            {
                Debug.Log("success");
                return Node.State.success;
            }
           
            Vector3 direction = (dest.transform.position - currentpos.transform.position).normalized;

           
            currentpos.transform.position += direction * speed * Time.deltaTime;
            Debug.Log("must be running");
           
            return Node.State.running;
        }

        public void reset()
        {

            currentindex = 0;
        }
    }
    public class FollowPlayer : IStrategy
    {
        GameObject currentpos;
        GameObject dest;
        int currentindex;
        float speed = 7f;
        float tolerance = 4.1f;
        bool condition;
        public FollowPlayer(GameObject currentpos, GameObject dest, bool condition)
        {

            this.currentpos = currentpos;
            this.dest = dest;
            this.condition = condition;
        }

        public Node.State Process()
        {


            if (Enterarea.exiting)
            {
                return Node.State.failure;
            }

            
            float dist = Vector3.Distance(currentpos.transform.position, dest.transform.position);
            if (dist < tolerance)
            {
                Enterarea.detour = false;
                return Node.State.success;
            }

            Vector3 direction = (dest.transform.position - currentpos.transform.position).normalized;


            currentpos.transform.position += direction * speed * Time.deltaTime;

            
            return Node.State.running;
        }
         
        public void reset()
        {

            currentindex = 0;
        }
    }

}


