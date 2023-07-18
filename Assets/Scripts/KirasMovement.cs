using Jiufen.Audio;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KirasMovement : MonoBehaviour
{
    public float initPosX;
    public Image image;
    public Sprite mouthClosed;
    public Sprite mouthOpen;
    public float widthMultiplier;

    private void Start()
    {
        AudioManager.PlayAudio("OST_MAINTHEME", new AudioJobOptions() { loop = true });
        widthMultiplier = 1080f / Screen.width;
        initPosX = transform.position.x * widthMultiplier;
    }

    void Update()
    {
        if (Mouse.current.leftButton.isPressed || Touchscreen.current.primaryTouch.press.isPressed)
        {
            image.sprite = mouthOpen;
            Vector2 inputPosition = Mouse.current.position.ReadValue();
            if (Touchscreen.current.primaryTouch.press.isPressed)
                inputPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            FollowPosition(inputPosition.x);
        }
        else
            image.sprite = mouthClosed;
    }

    public void FollowPosition(float positionX)
    {
        float maxValue = 300;
        if (positionX * widthMultiplier > initPosX + maxValue)
            positionX = (initPosX + maxValue) / widthMultiplier;
        if (positionX * widthMultiplier < initPosX - maxValue)
            positionX = (initPosX - maxValue) / widthMultiplier;
        transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
    }
}
