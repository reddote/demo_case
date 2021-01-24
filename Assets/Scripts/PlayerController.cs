using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public Animator animatorPlayer;
    public float playerSpeed;
    public FloatingJoystick sweepController;
    public Ranking ranking;
    public bool cantMove;
    public GameObject paintingWall;
    public Camera mainCamera;
    public Camera paintCamera;
    public TextMeshProUGUI paintText;
    public GameObject restartButton;
    public bool isPlayerDead;
    private bool _isFinished;
    private float _sweepSpeedMultiplier;
    private Rigidbody _playerRb;
    private float _maxSpeed = 5f;
    private bool _isOnRotate;
    private float _impactForRotation;

    void Start(){
        _playerRb = GetComponent<Rigidbody>();
        animatorPlayer.SetBool("isRunning", true);
    }

    void Update(){
        if (isPlayerDead || transform.position.y <= -2f){
            restartButton.SetActive(true);
        }
    }
    

    private void FixedUpdate(){
        if (!cantMove && !_isFinished){
            MovementOfPlayer();
        } else if (_isFinished){
            _playerRb.velocity = Vector3.zero;
        } else{
            _playerRb.constraints = RigidbodyConstraints.None;

        }
    }

    private void MovementOfPlayer(){
        if (_playerRb.velocity.magnitude <= _maxSpeed){
            _playerRb.AddForce(new Vector3(
                0,
                0,
                -playerSpeed * Time.fixedDeltaTime
            ), ForceMode.VelocityChange);
        }

        var transform1 = transform;
        _playerRb.MovePosition(transform1.position + transform1.right * (-playerSpeed * sweepController.Horizontal * Time.fixedDeltaTime));
        SlidePlayer(_isOnRotate);
    }

    private void SlidePlayer(bool checker){
        if (checker){
            _playerRb.MovePosition(transform.position + transform.right * (-playerSpeed/_impactForRotation * Time.fixedDeltaTime));
        }
    }

    private void OnCollisionStay(Collision other){
        if (other.gameObject.CompareTag("RotatingPlatform")){
            _impactForRotation = other.gameObject.GetComponent<Rotator>().impactSpeed;
            _isOnRotate = true;
        } else{
            _isOnRotate = false;
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Finish")){
            if (!ranking.last.Contains(transform)){
                ranking.last.Add(transform);
            }
            _isFinished = true;
            ranking.isGameFinished = true;
            animatorPlayer.SetBool("isRunning", false);
            paintingWall.SetActive(true);
            paintText.gameObject.SetActive(true);
            mainCamera.gameObject.SetActive(false);
            paintCamera.gameObject.SetActive(true);
        }
    }
}