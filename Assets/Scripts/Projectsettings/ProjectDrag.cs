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
    private int oldLayerID;   //记录原来的层级
    
    public Transform target; //手动拖入父文件的 Transform 组件
    public float distanceHold; //吸附范围
    
    void Start()
    {
        //保存原来的层级，似乎修改层级没有用？
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

    #region 物体拖拽，吸附和图层问题
    
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
        //————拖拽
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            Vector3 newPosition = transform.position;
            newPosition.z += 5.0f;//鼠标松开时物体的Y轴减少一点
            transform.position = newPosition;
            
            //将物体的层级恢复到原来的层级
            gameObject.layer = oldLayerID;
            Debug.Log("2");
        }
        
        //————吸附
        //检测鼠标释放位置是否在另一父文件附近
        Vector3 mousePosition = Input.mousePosition;
        Vector3 targetPosition = Camera.main.WorldToScreenPoint(target.position);
        
        float distance = Vector3.Distance(mousePosition, targetPosition);

        //如果鼠标释放位置在文件2附近，则将文件1的位置移动到文件2的位置，同时还要设置层级关系
        if (distance < distanceHold)
        {
            transform.position = target.position ;
        }
    }
    #endregion

    
}
