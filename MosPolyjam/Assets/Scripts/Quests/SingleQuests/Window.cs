using System.Collections;
using UnityEngine;

public class Window : PhysicalObject, IQuestable
{
    [SerializeField] protected Material _cleanWindow;
    [SerializeField] protected GameObject _stranger;
    [SerializeField] protected Renderer _text;
    [SerializeField] protected float _wipeTime;
    [SerializeField] float _strangerTime;
    [SerializeField] protected float _showTextTime;
    [SerializeField] protected float _keepTextTime;
    protected Renderer _renderer;
    protected AudioSource _audioSrc;
    protected bool _isActivated;
    protected bool _isDone;

    protected override void Awake()
    {
        base.Awake();
        _renderer = GetComponent<Renderer>();
        _audioSrc = GetComponent<AudioSource>();
        _stranger.SetActive(false);
        _text.gameObject.SetActive(false);

        Color col = _text.material.color;
        col.a = 0;
        _text.material.color = col;
    }

    public void Activate()
    {
        _isActivated = true;
    }

    public override void Interact()
    {
        if (!IsInteracted())
            return;

        _isActivated = false;

        _audioSrc.Play();
        StartCoroutine(MeetStranger());
    }

    protected IEnumerator WipeSpot()
    {
        while (_renderer.material.color.a > 0)
        {
            _renderer.material.color -= new Color(0, 0, 0, Time.deltaTime / _wipeTime);
            yield return null;
        }
    }

    protected IEnumerator MeetStranger()
    {
        yield return WipeSpot();
        _renderer.material = _cleanWindow;

        _stranger.SetActive(true);
        yield return new WaitForSeconds(_strangerTime);
        _stranger.SetActive(false);

        _text.gameObject.SetActive(true);
        yield return AppearMessage();
        yield return new WaitForSeconds(_keepTextTime);
        _text.gameObject.SetActive(false);

        _isActivated = false;
        _isDone = true;
    }

    public override bool IsInteracted()
    {
        return _isActivated;
    }

    protected IEnumerator AppearMessage()
    {
        while (_text.material.color.a < 1)
        {
            _text.material.color += new Color(0, 0, 0, Time.deltaTime / _showTextTime);
            yield return null;
        }
    }

    public IEnumerator WaitForFinish()
    {
        while (!_isDone)
            yield return null;
    }
}