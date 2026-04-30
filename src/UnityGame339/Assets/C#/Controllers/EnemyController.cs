using System;
using System.Collections;
using Game.Runtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
   private TurnEngine _turnEngine;

   private void Awake()
   {
      ServiceResolver.Resolve<TurnEngine>().TurnEnd += AttackPlayer;
   }

   private void OnDestroy()
   {
      ServiceResolver.Resolve<TurnEngine>().TurnEnd -= AttackPlayer;
   }

   private void AttackPlayer()
   {
      StartCoroutine(AttackDelay());
      return;

      IEnumerator AttackDelay()
      {
         yield return new WaitForSeconds(0.75f);
         PlayerController.Instance.EnemyAttack();
      }
   }
}
