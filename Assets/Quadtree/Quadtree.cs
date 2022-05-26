using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadtree : MonoBehaviour
{
    private int MAXOBJECTS = 2;
    private int DEPTH = 16;
    public int level;
    public List<FlockAgent> objects;
    public Rect bounds;
    public Quadtree[] nodes;

    public Quadtree(int pLevel, Rect pBounds){
        level = pLevel;
        objects = new List<FlockAgent>();
        bounds = pBounds;
        nodes = new Quadtree[4];
        Debug.DrawLine((Vector3)new Vector2(bounds.x,bounds.y),(Vector3)new Vector2(bounds.x+bounds.width,bounds.y));
        Debug.DrawLine((Vector3)new Vector2(bounds.x,bounds.y),(Vector3)new Vector2(bounds.x,bounds.y+bounds.height));
        Debug.DrawLine((Vector3)new Vector2(bounds.x,bounds.y+bounds.height),(Vector3)new Vector2(bounds.x+bounds.width,bounds.y+bounds.height));
        Debug.DrawLine((Vector3)new Vector2(bounds.x+bounds.width,bounds.y),(Vector3)new Vector2(bounds.x+bounds.width,bounds.y+bounds.height));
    }

    public void clear(){
        if(objects.Count>0){
        objects.Clear();
        }
        if(nodes[0]!=null){
        for (int i = 0; i< nodes.Length; i++){
            nodes[i].clear();
            nodes[i] = null;
        }
        }
    }

    private void split(){
        float subW =bounds.width/2;
        float subH =bounds.height/2;
        float x = bounds.x;
        float y = bounds.y;

        nodes[0] = new Quadtree(level+1, new Rect(x+subW, y, subW, subH));
        nodes[1] = new Quadtree(level+1, new Rect(x, y, subW, subH));
        nodes[2] = new Quadtree(level+1, new Rect(x, y+subH, subW, subH));
        nodes[3] = new Quadtree(level+1, new Rect(x+subW, y+subH, subW, subH));
    }

    public int getIndex(FlockAgent pRect){
    int index = -1;
    double vMidpoint = bounds.x + (bounds.width / 2);
    double hMidpoint = bounds.y + (bounds.height / 2);
 
   
   bool topQuad = (pRect.transform.position.y < hMidpoint);
   bool botQuad = (pRect.transform.position.y  > hMidpoint);
 
   if (pRect.transform.position.x  < vMidpoint) {
      if (topQuad) {
        index = 1;
      }
      else if (botQuad) {
        index = 2;
      }
    }
    // Object can completely fit within the right quadrants
    else if (pRect.transform.position.x > vMidpoint) {
     if (topQuad) {
       index = 0;
     }
     else if (botQuad) {
       index = 3;
     }
   }
 
   return index;
    }


    public void insert(FlockAgent objectt){
        if(nodes[0] != null){
            int index = getIndex(objectt);
        if(index != -1){
                nodes[index].insert(objectt);
                return;
            }
        }
        objects.Add(objectt);

        if(objects.Count > MAXOBJECTS && level < DEPTH){
            if(nodes[0]==null){
                split();
            }

            int i = 0;

            while(i<objects.Count){
                int index = getIndex(objects[i]);
                    if(index != -1){
                        nodes[index].insert(objects[i]);
                        objects.Remove(objects[i]);
                    }
                    else{
                        i++;
                    }
                }
            } 
        }


  public List<FlockAgent> retrieve(List<FlockAgent> objects, FlockAgent prect){
        int index = getIndex(prect);
        if(index != -1 && nodes[0] != null){
            nodes[index].retrieve(objects,prect);
        }
        objects.AddRange(objects);

        return objects;
    }
}

