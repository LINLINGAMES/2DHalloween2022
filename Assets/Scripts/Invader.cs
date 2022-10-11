using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Invader : MonoBehaviour
{

    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] animationSprites = new Sprite[0];
    public float animationTime = 1f;
    public int animationFrame { get; private set; }
    public int score = 10;
    public System.Action<Invader> killed;

    float number = 1f;//衝突回数を判定する変数
    private float time;
    private float vecX;
    private float vecY;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = animationSprites[0];
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), animationTime, animationTime);
    }

    private void AnimateSprite()
    {
        animationFrame++;

        // Loop back to the start if the animation frame exceeds the length
        if (animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }

        spriteRenderer.sprite = animationSprites[animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser") && (number == 1f))
        {
            time -= Time.deltaTime;

            if (time <= 0)
            {
                vecX = Random.Range(-8f, 8f);
                vecY = Random.Range(2f, 3.5f);
                this.transform.position = new Vector3(vecX, vecY, 0);
                time = 1.0f;
            }
    
            //サウンド鳴らす
            SoundManager.Instance.PlaySE(SESoundData.SE.Attack);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Laser") && (number == 2f))
        {
            killed?.Invoke(this);

            //サウンド鳴らす
            SoundManager.Instance.PlaySE(SESoundData.SE.Attack);
        }

    }

    
}
