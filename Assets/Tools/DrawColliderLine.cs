using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawColliderLine : MonoBehaviour
{
    public GameObject line;

    public GameObject Knob;

    // 线宽

    private const float LineWidth = 0.01f;

    private LineRenderer lr;

    private EdgeCollider2D edgeCollier;

    private int index;

    private Vector3 upPoint = Vector3.zero;

    private List<Vector2> colliderPos = new List<Vector2>();

    void Update()

    {

        // 鼠标按下创建一条线的obj

        if (Input.GetMouseButtonDown(0))

        {

            // 清理上一条线的是碰撞盒数据

            colliderPos.Clear();

            GameObject newLine = Instantiate(line, line.transform.position, Quaternion.identity).gameObject;

            newLine.transform.SetParent(this.transform);

            edgeCollier = newLine.GetComponent<EdgeCollider2D>();

            lr = newLine.GetComponent<LineRenderer>();

            lr.startWidth = LineWidth;

            lr.endWidth = LineWidth;

            index = 0;

        }

        // 鼠标按住不放，根据鼠标位置添加点位置

        if (Input.GetMouseButton(0))

        {

            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));

            if (lr != null)

            {

                if (upPoint != point)

                {

                    // 设置LineRender点数量和位置

                    index++;

                    lr.positionCount = index;

                    lr.SetPosition(index - 1, point);

                    // 记录需要碰撞的位置

                    colliderPos.Add(new Vector2(point.x, point.y));

                    upPoint = point;

                }

            }

        }

        // 鼠标抬起，结束画线，并根据数据生成碰撞盒

        if (Input.GetMouseButtonUp(0))

        {

            // 设置LineRender点碰撞和位置

            edgeCollier.points = colliderPos.ToArray();

            edgeCollier.edgeRadius = LineWidth / 2;

            Knob.GetComponent<Rigidbody2D>().gravityScale = 1f;

        }

    }
}
