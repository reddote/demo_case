using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ranking : MonoBehaviour{
    public Transform player;
    public List <Transform> racers;
    public TextMeshProUGUI rankText;
    public int rank = 1;
    public List<Transform> last;
    public bool isGameFinished;
    private int _counter = 0;

    private void Update(){
        if (!isGameFinished){
            RankCalculator();
        }else if(_counter < 1){
            LastRank();
            _counter += 1;
        }
    }

    private void RankCalculator(){
        rank = 1;
        foreach (var x in racers){
            if (player.position.z > x.position.z){
                rank += 1;
            }
        }

        rankText.text = "RANK : " + rank;
    }

    private void LastRank(){
        int a = last.IndexOf(player) + 1;
        rankText.text = "RANK : " + (a);
    }
}