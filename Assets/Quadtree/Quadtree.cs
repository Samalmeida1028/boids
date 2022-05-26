using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Quadtree : MonoBehaviour
{
    public float x;
    public float y;
    public float width;
    public float height;
    public quad quadtre; 
    public GameObject object1;

    void Start()
    {
        quadtre = new quad(x,y,width,height,object1,0);
    }
}

    /*void Update(){
        count++;

        quadtre.subDivide();
        quadtre.Nodes[0].subDivide();
        quadtre.Nodes[1].subDivide();
        quadtre.Nodes[2].subDivide();
        quadtre.Nodes[1].Nodes[0].subDivide();
        quadtre.Nodes[2].Nodes[1].subDivide();
        quadtre.Nodes[1].Nodes[0].Nodes[0].subDivide();
        quadtre.Nodes[2].Nodes[1].Nodes[1].subDivide();
        quadtre.Nodes[1].Nodes[0].Nodes[0].subDivide();
        quadtre.Nodes[2].Nodes[1].Nodes[1].Nodes[0].subDivide();
        

        // for(int i = 0; i<4; i++)
        // {
        //     count = 0;
        //     quadtre.Nodes[i].subDivide();
            
        // }

        // for (int i = 0; i < 4; i++)
        // {
        //     for(int j = 0; j<4; j++){
        //         count = delay/4;
        //         quadtre.Nodes[i].Nodes[j].subDivide();
        //     }
            
        // }
        
        
        // for (int i = 0; i < 4; i++)
        // {
        //     for(int j = 0; j<4; j++){
        //         for(int k = 0; k<4; k++){
        //             count =delay/16;
        //         quadtre.Nodes[i].Nodes[j].Nodes[k].subDivide();
        //         }
        //     }
            
        // }
        

    }
    */


