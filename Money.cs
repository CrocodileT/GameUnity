using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    private Character character;

    private void Awake()
    {
        character = FindObjectOfType<Character>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit is Character)
        {
            character.points++;
            //ScoreText.text = "" + character.points;
            //Debug.Log(character.points);
            Destroy(gameObject);
        }
    }
}
