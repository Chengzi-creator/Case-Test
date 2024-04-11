using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageChange : MonoBehaviour
{
    private bool _isDragging = false;//左右键同时按下才可change
    public Material newMaterial; //需要更新的新的材质
    public Material oldMaterial; //需要更新的旧的材质
    
    void Start()
    {
        //newMaterial = Resources.Load<Material>("Arts/TryTexture");
        //这样好像get不到，暂时也不需要，直接public！
    }


    void Update()
    {
        if (_isDragging)
        {
            if (Input.GetMouseButtonDown(1)) //这时按下右键
            {
                //因为这个脚本是直接挂在物体身上的所以不需要再用射线检测？只要有点击即可,后续证明需要检测点击到的是当前物体？
                //向屏幕鼠标处发射一条射线
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {

                    //获取物体
                    GameObject hitObject = hit.collider.gameObject;
                    GameObject thisGameObject = gameObject;
                    
                    if(hitObject == thisGameObject)
                    {      
                        MeshRenderer renderer = thisGameObject.GetComponent<MeshRenderer>();
                        if(renderer != null)
                        //更换物体的材质,目前还没做翻回去的效果
                        renderer.material = newMaterial;
                    }
                }
            }
        }
    
        if (Input.GetMouseButtonUp(0))//当松开文件时自动返回第一页
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
                    MeshRenderer renderer = thisGameObject.GetComponent<MeshRenderer>();
                    if(renderer != null)
                        //更换物体的材质,目前还没做翻回去的效果
                        renderer.material = oldMaterial;
                }
            }
        }
    }


    void OnMouseDown()//检测鼠标左键按下
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
        }
    }
}
