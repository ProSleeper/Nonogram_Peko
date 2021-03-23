using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchManager : MonoBehaviour
{
    enum eDirection
    {
        NONE,
        BLOCK,
        HORIZEN,    //가로 터치만 가능
        VERTICAL    //세로 터치만 가능
    }
    Vector3 mousePosition;
    Vector3 touchPosition;
    GameObject startBlock;
    eDirection touchDirection = eDirection.NONE;

#if UNITY_EDITOR
    private void Update()
    {
        BoardManager.instance.height.text = touchDirection.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            //Debug.Log(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, 15f);
            Debug.DrawRay(mousePosition, transform.forward * 10, Color.red, 0.3f);
            //ray = Camera.main.ScreenPointToRay(mousePosition);

            if (hit)
            {
                hit.collider.GetComponent<Block>().SpriteChange();
                startBlock = hit.transform.gameObject;
                touchDirection = eDirection.BLOCK;
            }
            else
            {
                //Debug.Log("not");
            }
        }

        if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            //Debug.Log(mousePosition);



            if (touchDirection == eDirection.HORIZEN)
            {
                //Debug.Log(mousePosition.y);
                //Debug.Log(startBlock.transform.position.y);
                mousePosition.y = startBlock.transform.position.y + (startBlock.transform.localScale.y / 2);

            }
            else if (touchDirection == eDirection.VERTICAL)
            {
                mousePosition.x = startBlock.transform.position.x - (startBlock.transform.localScale.x / 2);
            }
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, 15f);
            //ray = Camera.main.ScreenPointToRay(mousePosition);


            if (hit)
            {


                //Debug.Log("마우스: " + mousePosition);
                //Debug.Log("히트: " + hit.transform.position);
                //마우스 좌표는 계속해서 변함
                //hit좌표는 변하지 않음. 정확한건 아닌데 hit의 좌표는 아마 충돌하는 object의 좌표인 것 같음
                if (touchDirection == eDirection.NONE)
                {
                    return;
                }

                var hitPos = hit.collider.GetComponent<Block>().ArrayPosition();
                var startPos = startBlock.GetComponent<Block>().ArrayPosition();

                if (touchDirection == eDirection.BLOCK)
                {
                    GameObject hitBlock = hit.transform.gameObject;
                    if (startBlock != hitBlock)
                    {
                        Vector3 value = hitBlock.transform.position - startBlock.transform.position;

                        ////startBlock과 hitBlock이 대각선 상에 위치해 있을 때
                        //if (Mathf.Sqrt((value.x * value.x) + (value.y * value.y)) > startBlock.transform.localScale.x)
                        //{
                        //    //이 부분 때문에 만약 빠른 이동으로 인해서 1칸이 씹혀 버리면 문제가 생기는 부분이 있는듯
                        //    //추후에 버그 수정이 가능할지 모르겠지만 그때 해결해보자.
                        //    return;
                        //}

                        if ((hitPos.x != startPos.x) && (hitPos.y != startPos.y))
                        {
                            return;
                        }


                        //이 부분도 물리보다는 위에 hitpos와 startpos의 index좌표를 이용해서 하는 게 더 효율적일듯?
                        //추후 변경

                        //x좌표 거리와 y좌표 거리를 계산해서 더 큰 쪽으로 터치가 가능하도록 함
                        if (Mathf.Abs(value.x) > Mathf.Abs(value.y))
                        {
                            touchDirection = eDirection.HORIZEN;
                        }
                        else
                        {
                            touchDirection = eDirection.VERTICAL;
                        }
                    }
                }
                //이 부분이 나중에 로직 체크 부분도 될
                BoardManager.instance.BlockSpriteChange(hitPos.x, hitPos.y, startPos.x, startPos.y);

            }
            else
            {
                //Debug.Log("not");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            touchDirection = eDirection.NONE;
        }

    }

    //터치 어느 정도 구현 완료. 문제는 엄청나게 빠르게 터치 이동시 씹힘 현상이 있음
    //이건 코드상에서 해결을 봐야할듯. 해결 방법은
    //터치 시작지점과 현재 터치 되고 있는 지점 & 터치 끝 지점을 구해서
    //시작부터 해당 지점까지 충돌체크가 안된 블록들을 충돌 처리 해주는 방식으로 구현하기
#elif UNITY_ANDROID
    void Update()
    {
        if (Input.touchCount > 0)
        {
            // 싱글 터치.
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    //Debug.Log(mousePosition);

                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, transform.forward, 15f);
                    //ray = Camera.main.ScreenPointToRay(mousePosition);

                    if (hit)
                    {
                        hit.collider.GetComponent<Block>().SpriteChange();
                        startBlock = hit.transform.gameObject;
                        touchDirection = eDirection.BLOCK;
                    }
                    else
                    {
                        //Debug.Log("not");
                    }

                    break;
                case TouchPhase.Moved:
                    touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    //Debug.Log(mousePosition);



                    if (touchDirection == eDirection.HORIZEN)
                    {
                        //Debug.Log(mousePosition.y);
                        //Debug.Log(startBlock.transform.position.y);
                        touchPosition.y = startBlock.transform.position.y + (startBlock.transform.localScale.y / 2);

                    }
                    else if (touchDirection == eDirection.VERTICAL)
                    {
                        touchPosition.x = startBlock.transform.position.x - (startBlock.transform.localScale.x / 2);
                    }
                    hit = Physics2D.Raycast(touchPosition, transform.forward, 15f);
                    //ray = Camera.main.ScreenPointToRay(mousePosition);


                    if (hit)
                    {
                        //Debug.Log("마우스: " + mousePosition);
                        //Debug.Log("히트: " + hit.transform.position);
                        //마우스 좌표는 계속해서 변함
                        //hit좌표는 변하지 않음. 정확한건 아닌데 hit의 좌표는 아마 충돌하는 object의 좌표인 것 같음
                        if (touchDirection == eDirection.NONE)
                        {
                            return;
                        }

                        var hitPos = hit.collider.GetComponent<Block>().ArrayPosition();
                        var startPos = startBlock.GetComponent<Block>().ArrayPosition();

                        if (touchDirection == eDirection.BLOCK)
                        {
                            GameObject hitBlock = hit.transform.gameObject;
                            if (startBlock != hitBlock)
                            {
                                Vector3 value = hitBlock.transform.position - startBlock.transform.position;

                                //if (Mathf.Sqrt((value.x * value.x) + (value.y * value.y)) > startBlock.transform.localScale.x)
                                //{
                                //    return;
                                //}

                                if ((hitPos.x != startPos.x) && (hitPos.y != startPos.y))
                                {
                                    return;
                                }

                                if (Mathf.Abs(value.x) > Mathf.Abs(value.y))
                                {
                                    touchDirection = eDirection.HORIZEN;
                                }
                                else
                                {
                                    touchDirection = eDirection.VERTICAL;
                                }
                            }
                        }

                        //hit.collider.GetComponent<Block>().SpriteChange();  //이 부분이 나중에 로직 체크 부분도 될듯
                        
                        //이 부분이 나중에 로직 체크 부분도 될
                        BoardManager.instance.BlockSpriteChange(hitPos.x, hitPos.y, startPos.x, startPos.y);
                    }
                    else
                    {
                        //Debug.Log("not");
                    }
                    break;

                case TouchPhase.Stationary:
                    // 터치 고정 시.
                    // Debug.Log("터치 고정");
                    break;

                case TouchPhase.Ended:
                    touchDirection = eDirection.NONE;
                    break;

                case TouchPhase.Canceled:
                    // 터치 취소 시. ( 시스템에 의해서 터치가 취소된 경우 (ex: 전화가 왔을 경우 등) )
                    // Debug.Log("터치 취소");
                    break;
            }
        }
    }
#endif
}
