using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallChange : MonoBehaviour
{
   
    public Material newMaterial; //需要更新的新的材质
    //public Material oldMaterial; //需要更新的旧的材质
    public GameObject parentObject;
    
    void Start()
    {
        
    }


    void Update()
    {
        
    }


    void OnMouseDown()//检测鼠标左键按下
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            //获取物体
            GameObject hitObject = hit.collider.gameObject;
            GameObject thisGameObject = gameObject;
                    
            if(hitObject == thisGameObject)
            {      
                MeshRenderer renderer = parentObject.GetComponent<MeshRenderer>();
                if(renderer != null)
                    //更换物体的材质,目前还没做翻回去的效果
                    renderer.material = newMaterial;
            }
        }
    }
}
