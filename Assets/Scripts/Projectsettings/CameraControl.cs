using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float moveSpeed ;
    public Vector2 boundaryMin;
    public Vector2 boundaryMax;//限制一下相机的移动，免得飞出去了
    public LayerMask fileLayerMask ; // 检测右键是否在文件层级上
    
    public float rotateSpeed = 5f; //旋转速度
    public float zoomSpeed = 5f; // 拉进速度
    public float rotationMin; // 旋转角度最小值
    public float rotationMax; // 旋转角度最大值
    
    void Start()
    {
        fileLayerMask = LayerMask.GetMask("File");
    }
    
    void Update()
    {
        #region 拖拽
        
        //右键拖拽部分
        Vector3 currentPosition = transform.position;
        if (Input.GetMouseButton(1)) // 检测鼠标右键的输入 1 表示鼠标右键
        {
            // 检测鼠标点击处是否有指定的物体
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, fileLayerMask))
            {

                // 获取鼠标移动的距离
                float moveX = Input.GetAxis("Mouse X");
                float moveY = Input.GetAxis("Mouse Y");

                // 根据鼠标的移动来控制镜头的移动
                Vector3 targetPosition = transform.position + new Vector3(-moveX, -moveY, 0) * moveSpeed * Time.deltaTime;
                        
                targetPosition.x = Mathf.Clamp(targetPosition.x, boundaryMin.x, boundaryMax.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y, boundaryMin.y, boundaryMax.y);

                // 更新相机位置
                transform.position = targetPosition;
            }
        }
        #endregion

        #region 缩放
        // 获取鼠标滚轮的滚动量
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // 根据滚动量调整相机的缩放,并且限定范围
        Vector3 scrollPosition = transform.position + new Vector3(0, -scroll, 0) * zoomSpeed* Time.deltaTime;
        scrollPosition.y = Mathf.Clamp(scrollPosition.y, boundaryMin.y, boundaryMax.y);
        transform.position = scrollPosition;
        
        // 限制旋转角度在指定范围内
        float rotationX = transform.eulerAngles.x + -scroll * rotateSpeed * Time.deltaTime;
        //rotationX = Mathf.Clamp(rotationX, rotationMin, rotationMax); 
        transform.rotation = Quaternion.Euler(rotationX, transform.eulerAngles.y, 0);
        
        #endregion
        
    }
}
