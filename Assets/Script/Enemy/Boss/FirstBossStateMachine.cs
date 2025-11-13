using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossStateMachine : BossStateMachine
{
    [SerializeField] private BossPhysics bossPhysics;
    [SerializeField] private GameObject player;

    [Header("IDLE STATE")]
    [SerializeField] private string idleAnimationName;
    [SerializeField] private float minIdleTime;
    [SerializeField] private float maxIdleTime;
    private float idleStateTimer;

    [Header("TELEPORT STATE")]
    [SerializeField] private string teleportOutAnimationName;
    [SerializeField] private string teleportInAnimationName;
    [SerializeField] private float minTeleportTime;
    [SerializeField] private float maxTeleportTime;
    [SerializeField] private Transform[] teleportPoints;
    private float teleportStateTimer;
    private int teleportIndex;
    private int lastTeleportIndex;
    private bool canCheckTeleportInfo;

    [Header("ATTACK STATE")]
    [SerializeField] private string attackAnimationName;
    [SerializeField] private float attackMeleeCooldownTime;
    private float meleeAttackTimer;

    [Header("RANGE ATTACK STATE")]
    [SerializeField] private string attackRangeAnimationName;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootingPoint;

    [Header("DEATH STATE")]
    [SerializeField] private string deathAnimationName;
    [SerializeField] private GameObject headPrefab;



    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        if (player == null)
        {
            Controller controller = FindObjectOfType<Controller>();
            if (controller != null)
            {
                player = controller.gameObject;
            }
        }
    }

    #region IDLE
    public override void EnterIdle()
    {
        anim.Play(idleAnimationName);
        idleStateTimer = Random.Range(minIdleTime, maxIdleTime);
    }

    public override void UpdateIdle()
    {
        meleeAttackTimer -= Time.deltaTime;
        if (bossPhysics.inAttackRange)
        {
            if (meleeAttackTimer <= 0)
                ChangeState(BossState.Attack);
            return;
        }

        idleStateTimer -= Time.deltaTime;
        if (idleStateTimer <= 0)
            ChangeState(BossState.Teleport);
    }
    #endregion

    #region TELEPORT
    public override void EnterTeleport()
    {
        bossPhysics.DisableStatsCol();
        teleportIndex = Random.Range(0, teleportPoints.Length);
        while (teleportIndex == lastTeleportIndex)
        {
            teleportIndex = Random.Range(0, teleportPoints.Length);
        }
        lastTeleportIndex = teleportIndex;
        anim.Play(teleportOutAnimationName);
    }

    public override void UpdateTeleport()
    {
        if (!canCheckTeleportInfo)
            return;
        teleportStateTimer -= Time.deltaTime;
        if (bossPhysics.inAttackRange)
        {
            ChangeState(BossState.Attack);
        }
        else if (teleportStateTimer <= 0)
        {
            //ChangeState(BossState.Idle);
            ChangeState(BossState.RangeAttack);
        }
    }

    public override void ExitTeleport()
    {
        canCheckTeleportInfo = false;
    }

    public void Teleport()
    {
        int randomChance = Random.Range(0, 2);
        if(randomChance == 0)
        {
            transform.position = teleportPoints[teleportIndex].position;
        }
        else
        {
            player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                player = GameObject.Find("Player");
            }
            if (player != null)
            {
                transform.position = player.transform.position + Vector3.up * 1.6f;
            }
            else
            {
                transform.position = teleportPoints[teleportIndex].position;
            }
        }
        anim.Play(teleportInAnimationName);
    }

    public void EnableCheckingTeleport()
    {
        canCheckTeleportInfo = true;
        teleportStateTimer = Random.Range(minTeleportTime, maxTeleportTime);
        bossPhysics.EnableStatsCol();
        bossPhysics.EnableDetectionCol();
        anim.Play(idleAnimationName);
    }
    #endregion

    #region ATTACK
    public override void EnterAttack()
    {
        anim.Play(attackAnimationName);
        bossPhysics.DisableDetectionCol();
        bossPhysics.inAttackRange = false;
    }

    public override void ExitAttack()
    {
        meleeAttackTimer = attackMeleeCooldownTime;
    }

    public void ChangeStateToIdle()
    {
        ChangeState(BossState.Idle);
    }
    #endregion

    #region RANGE ATTACK
    public override void EnterRangeAttack()
    {
        anim.Play(attackRangeAnimationName);
    }

    public void SpawnBossProjectile()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        BossProjectile projectile = Instantiate(projectilePrefab, shootingPoint.position,transform.rotation).GetComponent<BossProjectile>();
        if(player != null)
        {
            projectile.MoveProjectile(player.transform);
        }
        else
        {
            Destroy(projectile.gameObject);
        }
    }
    #endregion

    #region DEATH
    public override void EnterDeath()
    {
        anim.Play(deathAnimationName);
        bossPhysics.DisableAllColliders();
    }

    public void deathAnimationEvent()
    {
        Instantiate(headPrefab, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
    #endregion


}
