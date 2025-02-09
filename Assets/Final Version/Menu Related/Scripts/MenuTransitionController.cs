﻿using UnityEngine;
using System.Collections;
using System.Linq.Expressions;

public class MenuTransitionController : MonoBehaviour
{

    private Animator _animator;
    private CanvasGroup _canvasGroup;

    public bool IsOpen
    {

        get { return _animator.GetBool("IsOpen"); }
        set { _animator.SetBool("IsOpen",value); }
    }

    public void Awake()
    {
		gameObject.SetActive (true);
        _animator = GetComponent<Animator>();
        _canvasGroup = GetComponent<CanvasGroup>();

        var rect = GetComponent<RectTransform>();
        rect.offsetMax = rect.offsetMin = new Vector2(0, 0);
    }

    public void Update()
    {
        //preventing panels behind active panel to be truly disabled while main panel open
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
        }

        else
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
        }
    }


}
