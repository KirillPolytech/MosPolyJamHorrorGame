using System.Collections.Generic;
using UnityEngine;

public class PhysicalObject : MonoBehaviour, Interactable
{
    public Color GlowColor = new Color(250, 250, 250);
    protected Renderer _renderer;
    protected UINote _ui;

    protected Renderer[] _meshes;
    protected List<Color> _initialColors = new List<Color>();

    protected virtual void Awake()
    {
        _meshes = GetComponentsInChildren<Renderer>();
        SaveColor();

        _ui = FindObjectOfType<UINote>();
    }

    protected virtual void SaveColor()
    {
        _initialColors.Clear();
        foreach(var mesh in _meshes)
        {
            foreach (var mat in mesh.materials)
            {
                _initialColors.Add(mat.color);
            }
        }
    }

    protected virtual void Glow(bool isGlow)
    {
        int index = 0;
        foreach(var mesh in _meshes)
        {
            foreach (var mat in mesh.materials)
            {
                GlowMat(isGlow, mat, _initialColors[index]);
                index++;
            }
        }
    }

    protected virtual void GlowMat(bool isGlow, Material mat, Color initial)
    {
        if(isGlow)
        {
            mat.color = initial + GlowColor;
        }
        else 
        {
            mat.color = initial;
        }

    }

    public virtual void Interact()
    {
    }

    public virtual bool IsInteracted()
    {
        return true;
    }

    public virtual void Show(bool show)
    {
        Glow(show);
        _ui.Help(show);
    }
}