using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quad
    {
        private float selfx;
        public float X {get{return selfx;}}
        private float selfy;
        public float Y {get{return selfy;}}
        private float selfWidth;
        public float Width {get{return selfWidth;}}
        private float selfHeight;
        public float Height {get{return selfHeight;}}

        private int MAXPOINTS = 2;
        private int DEPTH = 9;

        private quad[] nodes;
        public quad[] Nodes {get{return nodes;}}

        private GameObject selfObjectType;
        public GameObject ObjectType {get{return selfObjectType;}}

        private List<GameObject> objects;

        private int selfIndex;
        public int Index {get{return selfIndex;}}
        private int selfLevel;
        public int Level {get{return selfLevel;}}

        private bool isDivided = false;

        private float hMid;
        private float vMid;


        public quad(float x, float y, float width, float height, GameObject objectType, int level, int index = -1)
        {
            Color[] randomColors = {Color.red,Color.green, Color.white, Color.blue};
            int randNum = Random.Range(0,3);
            if(index!=-1){
            Debug.DrawLine(new Vector3(x,y,0),new Vector3(x+width,y,0),randomColors[index]);
            Debug.DrawLine(new Vector3(x,y,0),new Vector3(x,y+height,0),randomColors[index]);
            Debug.DrawLine(new Vector3(x+width,y,0),new Vector3(x+width,y+height,0),randomColors[index]);
            Debug.DrawLine(new Vector3(x,y+height,0),new Vector3(x+width,y+height,0),randomColors[index]);
            }
            else{
            Debug.DrawLine(new Vector3(x,y,0),new Vector3(x+width,y,0),Color.white);
            Debug.DrawLine(new Vector3(x,y,0),new Vector3(x,y+height,0),Color.white);
            Debug.DrawLine(new Vector3(x+width,y,0),new Vector3(x+width,y+height,0),Color.white);
            Debug.DrawLine(new Vector3(x,y+height,0),new Vector3(x+width,y+height,0),Color.white);

            }
            this.selfx = x;
            this.selfy = y;
            this.selfWidth = width;
            this.selfHeight = height;
            this.selfIndex = index;
            this.selfObjectType = objectType;
            this.selfLevel = level;
            objects = new List<GameObject>();
            nodes = new quad[4];
            this.hMid = x+width/2;
            this.vMid = y+height/2;

        }

        public void insert(GameObject objectIn){
            if(objectIn.tag == selfObjectType.tag){
                if(nodes[0] != null){
                    int index = getIndex(objectIn);

                    if(index!= -1){
                        nodes[index].insert(objectIn);
                        return;
                    }
                }

                objects.Add(objectIn);

                if(objects.Count>MAXPOINTS && selfLevel < DEPTH){
                    if(!isDivided){
                        subDivide();
                    }

                    int i = 0;

                    while(i<objects.Count){
                        int index = getIndex(objects[i]);
                        if(index != -1){
                            nodes[index].insert(objects[i]);
                            objects.RemoveAt(i);    
                        }
                        else{
                            i++;
                        }
                    }
                }
            }

        }

        void createQuad(float x, float y, float width, float height,GameObject objectType,int level,int index = -1)
        {
            quad child = new quad(x,y,width,height,objectType,level,index);
            if(index!=-1){
                nodes[index] = child;
            }
        }

        public void subDivide()
        {
            isDivided = true;
            createQuad(this.selfx, this.selfy, this.selfWidth/2, this.selfHeight/2, this.selfObjectType, this.selfLevel+1, 0);

            createQuad(hMid, this.selfy, this.selfWidth/2, this.selfHeight/2 ,this.selfObjectType, this.selfLevel+1,1);

            createQuad(this.selfx, vMid, this.selfWidth/2, this.selfHeight/2, this.selfObjectType, this.selfLevel+1, 2);

            createQuad(hMid, vMid, this.selfWidth/2, this.selfHeight/2, this.selfObjectType, this.selfLevel+1, 3);
        }


        public void clear(){
        objects.Clear();
        for(int i = 0; i< nodes.Length; i++){
            if(nodes[i]!=null){
            nodes[i].clear();
            nodes[i] = null;
            }
        }
        isDivided = false;
        }

        public int getIndex(GameObject objectIn){
            int index = -1;
            Vector3 pos = objectIn.transform.position;
            if(pos.x<hMid&&pos.x>selfx){
                if(pos.y<vMid&&pos.y>selfy){
                    index = 0;
                }
                if(pos.y>vMid&&pos.y<selfy+selfHeight){
                    index = 2;
                }
            }
            if(pos.x>hMid&&pos.x<selfx+selfWidth){
                if(pos.y<vMid&&pos.y>selfy){
                    index = 1;
                }
                if(pos.y>vMid&&pos.y<selfy+selfHeight){
                    index = 3;
                }
            }
            return index;

        }

    public List<GameObject> retrieveObjects(List<GameObject> returnObjects, GameObject refer){

            int index = getIndex(refer);
            if(nodes[0]!=null && index != -1){
                nodes[index].retrieveObjects(returnObjects,refer);
            }
            returnObjects.AddRange(objects);
            return returnObjects; 

        }
    }