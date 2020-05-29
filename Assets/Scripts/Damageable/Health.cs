using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _lives = 3;

    public event Action<int> OnLivesChange = lives => {};

    private SpriteRenderer _spriteRenderer;

    protected void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Damage()
    {
        OnLivesChange(--_lives);
        StartBlink();
    }

    private static readonly int FlashAmount = Shader.PropertyToID("_FlashAmount");

    private Coroutine _blinkCoroutine;
    
    public void StartBlink( )
    {
        if (_blinkCoroutine != null)
        {
            StopCoroutine(_blinkCoroutine);
        }

        _blinkCoroutine = StartCoroutine( ShowBlinkRoutine( ));
    }

    IEnumerator ShowBlinkRoutine( )
    {
        for (int i = 50; i >= 0 && _spriteRenderer != null; i--)
        {
            var intensity = i / 100f;
            _spriteRenderer.material.SetFloat(FlashAmount, intensity);
            yield return null;
        }
    }
}
