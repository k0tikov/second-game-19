﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
	NavMeshAgent agent;
	Animator animator;
	
	const float lokomotionAnimationSmoothTime = .1f;

    void Start()
    {
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
		float speedPercent = agent.velocity.magnitude / agent.speed;
		animator.SetFloat("speedPercent", speedPercent, lokomotionAnimationSmoothTime, Time.deltaTime);
        
    }
}
