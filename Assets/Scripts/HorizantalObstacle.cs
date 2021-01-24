using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizantalObstacle : MovingObstacles{
    //bu nesneler hareketin olacağı noktalara ait transformlar
    public Transform startingPoint;
    public Transform endingPoint;
    //end
    private bool _isMove;

    void Start(){
    }

    void Update(){
        ObstacleMovementController();
    }

    private void ObstacleMovementController(){
        if (transform.position.x >= startingPoint.position.x){
            _isMove = true;
        }
        if (transform.position.x <= endingPoint.position.x){
            _isMove = false;
        }

        if (_isMove){
            transform.Translate(new Vector3(-1,0,0) * (speed * Time.deltaTime));
        }
        if (!_isMove){
            transform.Translate(new Vector3(1,0,0) * (speed * Time.deltaTime));
        }
    }

}