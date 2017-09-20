using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingTower
{
    public class Box : MonoBehaviour {

        void Update()
        {
            if (posDrop != Vector3.zero)
            {
                if (MoveToPoint(posDrop))
                {
                    posDrop = Vector3.zero;
                    float _hieght = scale.y * 300;
                    BuildingControl.Instance.OnDropFinish(_hieght);
                }
            }
        }
        public Transform posTop;
        float scaleSpeed = 0.2f;
        public float moveSpeed = 1000;
        public Vector2 scale { get; private set; }
        Vector3 posDrop;
        public bool isWait { get; private set; }
        public void OnInit()
        {
            scaleSpeed = Random.Range(0.5f, 1.0f);
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            scale = transform.localScale;
            isWait = true;
        }
        public void OnHold()
        {
            if (scale.x < 1.1f && isWait)
            {
                scale += Vector2.one * scaleSpeed * Time.deltaTime;
                transform.localScale = scale;
            }
        }
        public void OnDrop(Vector3 pos)
        {
            posDrop = pos;
            isWait = false;
        }
        public bool MoveToPoint(Vector3 point)
        {
            float dist = Vector3.Distance(point, transform.position);
            if (dist < 0.05f)
            {
                return true;
            }
            Vector3 dir = (point - transform.position).normalized;
            transform.Translate(dir * Mathf.Min(dist, moveSpeed * Time.deltaTime), Space.World);
            return false;
        }
    }
}