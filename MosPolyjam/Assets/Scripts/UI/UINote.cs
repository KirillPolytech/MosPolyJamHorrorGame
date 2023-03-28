using System.Collections;
using UnityEngine;
using TMPro;

public class UINote : MonoBehaviour
{
    [SerializeField] private TMP_Text _help;
    [SerializeField] private TMP_Text _speech;
    [Range(0, 20)]
    [SerializeField] private float _speechTime;
    protected bool _isSpeech;

    protected void Awake()
    {
        _help.gameObject.SetActive(false);
        _speech.gameObject.SetActive(false);
    }

    public void Help(bool isActive)
    {
        _help.gameObject.SetActive(isActive);
    }

    public void Say(string speech)
    {
        if(_isSpeech)
            return;

        _speech.text = speech;
        _speech.gameObject.SetActive(true);
        StartCoroutine(WaitForSpeech());
    }

    private IEnumerator WaitForSpeech()
    {
        _isSpeech = true;
        yield return new WaitForSeconds(_speechTime);
        _speech.gameObject.SetActive(false);
        _isSpeech = false;
    }
}
