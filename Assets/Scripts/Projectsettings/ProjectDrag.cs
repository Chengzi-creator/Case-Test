using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ProjectDrag : MonoBehaviour
{   
    private bool isDragging = false;
    public Vector3 offset;
    public Vector3 movePosition;
    public string targetLayer = "moveFile";
    private int oldLayerID;   // 记录原来的层级
    
    void Start()
    {
        // 保存原来的层级
        oldLayerID = gameObject.layer;
    }
    
    void Update()
    {
        if (isDragging)
        {
            //更新物体位置，跟随鼠标
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 100));
            transform.position = new Vector3(curPosition.x + offset.x,curPosition.y + offset.y,transform.position.z);
        }
    }

    void OnMouseDown()//鼠标按下
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            
            // 物体当前位置与鼠标的偏移量
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 100));
            
            movePosition = transform.position;
            movePosition.z -= 5.0f;
            transform.position = movePosition; // 鼠标按下时物体的Y轴增加一点
            
            
            int targetLayerID = LayerMask.NameToLayer(targetLayer);// 修改物体的层级
            if (targetLayerID != -1)
            {
                gameObject.layer = targetLayerID;
                Debug.Log("1");
            }
            else
            {
                Debug.LogWarning("0");
            }
            transform.SetAsLastSibling();
        }
    }

    void OnMouseUp()//鼠标松开
    {
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            Vector3 newPosition = transform.position;
            newPosition.z += 5.0f;//鼠标松开时物体的Y轴减少一点
            transform.position = newPosition;
            
            // 将物体的层级恢复到原来的层级
            gameObject.layer = oldLayerID;
            Debug.Log("2");
        }
    }
}
