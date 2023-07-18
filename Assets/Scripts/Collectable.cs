using Jiufen.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Collectable : MonoBehaviour
{
    public float speed = 1;
    public bool isBad = false;
    public KirasMovement kira;
    public GameObject deathPrefab;

    public Action OnResetGame;
    private float heightMultiplier;
    private float widthMultiplier;

    public void Start()
    {
        heightMultiplier = 1920f / Screen.height;
        widthMultiplier = 1080f / Screen.width;
    }

    void Update()
    {
        transform.position += (Vector3)(Vector2.down * 100 * (speed / heightMultiplier) * Time.deltaTime);

        float yPos = transform.position.y * heightMultiplier;

        if (yPos < 500)
        {
            Destroy(this.gameObject);
            return;
        }

        if (yPos < 750 && yPos > 500)
            if (Mathf.Abs((kira.transform.position.x * widthMultiplier) - (transform.position.x * widthMultiplier)) < 100)
                PickupThing();

    }

    private void PickupThing()
    {
        if (isBad)
        {
            if (ScoreManager.score > PlayerPrefs.GetInt("Score"))
                PlayerPrefs.SetInt("Score", ScoreManager.score);

            Instantiate(deathPrefab, Camera.main.ScreenToWorldPoint(kira.transform.position), Quaternion.identity);
            OnResetGame?.Invoke();
        }
        else
        {
            ScoreManager.score++;
            AudioManager.PlayAudio("SFX_FISHCATCH");
        }


        Destroy(this.gameObject);
    }
}
