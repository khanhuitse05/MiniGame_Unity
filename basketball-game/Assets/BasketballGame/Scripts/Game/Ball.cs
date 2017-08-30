using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum BallState
{
    None,
    Fly,
    Fall,
    Back
}
public class Ball : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Rigidbody2D rigid;
    public float maxVelY = 25;
    public Collider2D colliderTable;
    public Collider2D colliderBasket;
    public Transform objTable;
    BallState state;
    void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        colliderTable.enabled = true;
        colliderBasket.enabled = false;
        objTable.parent = Pos.objBack.transform;
        state = BallState.None;
    }
    void Update()
    {
        switch (state)
        {
            case BallState.None:
                break;
            case BallState.Fly:
                UpdateMove();
                transform.localScale = transform.localScale - Vector3.one * (rigid.velocity.y * Time.deltaTime * 0.015f);
                if (rigid.velocity.y <= 0)
                {
                    objTable.parent = Pos.objFront.transform;
                    colliderTable.enabled = false;
                    colliderBasket.enabled = true;
                    state = BallState.Fall;
                }
                break;
            case BallState.Fall:
                UpdateMove();
                if (transform.position.y < (Pos.posBottom.y - 5))
                {
                    state = BallState.Back;
                    OnBackToBasket();
                }
                break;
            case BallState.Back:
                UpdateMove();
                break;
            default:
                break;
        }
    }
    protected void UpdateMove()
    {
        transform.Rotate(0, 0, 0 - rigid.velocity.x * Time.deltaTime * 36);
    }
    // Throw ball
    void OnThrow()
    {
        state = BallState.Fly;
        rigid.velocity = GetForce();
    }
    // Detect ball target basket
    bool isTarget = false;
    void OnTarget()
    {
        if (isTarget == false)
        {
            isTarget = true;
            AudioManager.OnPlayWin();
            GSGamePlay.Instance.OnScore();
        }
    }
    void OnFinish()
    {
        if (!isTarget)
        {
            GSGamePlay.Instance.ResetScore();
            AudioManager.OnPlayLose();
        }
        isTarget = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Basket")
        {
            OnTarget();
        }
    }
    // Force ball back to basket
    void OnBackToBasket()
    {
        OnFinish();
        colliderTable.enabled = true;
        colliderBasket.enabled = false;
        objTable.parent = Pos.objBack.transform;
        state = BallState.None;
        rigid.velocity = Vector2.zero;
        transform.localScale = Vector3.one;
        float _xx = UnityEngine.Random.Range(Pos.posLeft.x, Pos.posRight.y);
        posBallStar = new Vector3(_xx, Pos.posBottom.y, Pos.posBottom.z);
        rigid.MovePosition(posBallStar);
    }

#region Swipe
    public float maxTimeSwipe = 1.0f;
    bool isTouch;
    float beganTime;
    Vector3 posBallStar;
    Vector3 posBegan;
    Vector3 posEnded;
    public void OnPointerDown(PointerEventData eventData)
    {
        OnBegan();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        posEnded = Camera.main.ScreenToWorldPoint(eventData.position);
        OnEnded();
    }
    void OnBegan()
    {
        if (isTouch == false)
        {
            posBegan = GetPosTouch();
            if (posBegan != Vector3.zero)
            {
                isTouch = true;
                beganTime = Time.time;
            }
        }
    }
    void OnEnded()
    {
        if (isTouch == true)
        {
            isTouch = false;
            if (Time.time - beganTime < maxTimeSwipe && posEnded != Vector3.zero)
            {
                OnThrow();
            }
        }
    }
    Vector2 GetForce()
    {
        Vector2 v2 = Vector2.zero;
        v2.x = posEnded.x - posBegan.x;
        v2.y = posEnded.y - posBegan.y;
        v2.x = maxVelY * v2.x / v2.y;
        v2.y = maxVelY;
        return v2;
    }
    Vector3 GetPosTouch()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Input.mousePosition;
            pos = Camera.main.ScreenToWorldPoint(pos);
            return pos;
        }
#else
        if (Input.touchCount > 0)
        {
            Vector3 pos = Input.touches[0].position;
            pos = Camera.main.ScreenToWorldPoint(pos);
            return pos;
        }
#endif
        return Vector3.zero;
    }
#endregion

}
