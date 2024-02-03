// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehavior
        {
            Null,
            Patrol
        }

        #region Parameters

        private const float MAX_ANGLE = 45.0f;

        [SerializeField] private AIBehavior aIBehavior;

        public AIPointPatrol patrolPoint;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float navigationLinear;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float navigationAngular;

        [SerializeField] private float randomSelectMovePointTime;

        [SerializeField] private float findNewTargetTime;

        [SerializeField] private float shootDelay;

        [SerializeField] private float evadeRayDist;

        /// <summary>
        /// Набор точек для патрулирования.
        /// </summary>
        // public Transform[] patrolControlPoints;

        [SerializeField] private float evadeDuration;

        [SerializeField] private float fireRadius;

        [SerializeField] private float fireAngle;

        private SpaceShip spaceShip;

        private Vector3 movePosition;

        private Destructible selectedTarget;

        private Timer randomizeDirectionTimer;

        private Timer fireTimer;

        private Timer findNewTargetTimer;

        private Vector3 currentMovePosition;

        float timer;

        /// <summary>
        /// Номер текущей точки для патрулирования.
        /// </summary>
        private int numPatrolPoint = 0;

        #endregion

        #region API

        private void UpdateAI()
        {
            if (aIBehavior == AIBehavior.Patrol)
            {
                UpdateBehaviorPartol();
            }
        }

        private void UpdateBehaviorPartol()
        {
            ActionFindNewMovePosition();
            ActionFindNewAttackTarget();
            ActionFire();
            ActionEvadeCollision();
        }

        private void ActionFindNewMovePosition()
        {
            if (aIBehavior == AIBehavior.Patrol)
            {
                if (selectedTarget != null)
                {
                    movePosition = selectedTarget.transform.position + (Vector3)selectedTarget?.GetComponent<Rigidbody2D>().velocity;
                }
                else
                {
                    if (patrolPoint != null)
                    {
                        bool isInsidePatrolZone = (patrolPoint.transform.position - transform.position).sqrMagnitude < patrolPoint.Radius * patrolPoint.Radius;

                        if (isInsidePatrolZone)
                        {
                            GetNewPoint();
                        }
                        else
                        {
                            movePosition = patrolPoint.transform.position;
                        }
                    }
                }
            }
        }

        protected virtual void GetNewPoint() { }

        private void ActionEvadeCollision()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, evadeRayDist);

            if (hit)
            {
                movePosition = transform.position + transform.right * 100.0f;

                spaceShip.ThrustControl = -navigationLinear;
                /*
                if (hit.collider.transform.root.GetComponent<Asteroid>() != null)
                {
                    spaceShip.Fire(TurretMode.Primary);
                }
                */
            }
            else
            {
                ActionControlShip();
            }
        }

        private void ActionControlShip()
        {
            spaceShip.ThrustControl = navigationLinear;

            spaceShip.TorqueControl = ComputeAliginTorqueNormalized(movePosition, spaceShip.transform) * navigationAngular;
        }

        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / 45;

            return -angle;
        }

        private float AngleCalculate(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE);

            return angle;
        }

        private void ActionFindNewAttackTarget()
        {
            if (findNewTargetTimer.IsFinished)
            {
                selectedTarget = FindNearestDestructibleTarget();

                findNewTargetTimer.Start(shootDelay);
            }
        }

        private Destructible FindNearestDestructibleTarget()
        {
            float maxDist = float.MaxValue;

            Destructible potentialTarget = null;

            foreach (var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == spaceShip) continue;

                if (v.TeamId == Destructible.TEAM_ID_NEUTRAL) continue;

                if (v.TeamId == spaceShip.TeamId) continue;

                float dist = Vector2.Distance(spaceShip.transform.position, v.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }

            return potentialTarget;
        }

        private void ActionFire()
        {
            if (selectedTarget != null)
            {
                Collider2D[] hit = Physics2D.OverlapCircleAll(spaceShip.transform.position, fireRadius);

                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].transform.root.GetComponent<Destructible>() == null) continue;

                    if (hit[i].transform.root.GetComponent<Destructible>() == selectedTarget)
                    {
                        if (Mathf.Abs(AngleCalculate(movePosition, spaceShip.transform)) < fireAngle)
                            spaceShip.Fire(TurretMode.Primary);
                    }
                }
            }
        }

        #region Timers

        private void InitTimers()
        {
            randomizeDirectionTimer = new Timer(randomSelectMovePointTime);

            fireTimer = new Timer(shootDelay);

            findNewTargetTimer = new Timer(findNewTargetTime);
        }

        private void UpdateTimers()
        {
            randomizeDirectionTimer.RemoveTime(Time.deltaTime);

            fireTimer.RemoveTime(Time.deltaTime);

            findNewTargetTimer.RemoveTime(Time.deltaTime);
        }

        #endregion

        #region Unity API

        private void Start()
        {
            spaceShip = GetComponent<SpaceShip>();

            InitTimers();
        }

        private void Update()
        {
            UpdateAI();

            UpdateTimers();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            if (selectedTarget != null) Gizmos.DrawSphere(selectedTarget.transform.position + (Vector3)selectedTarget?.GetComponent<Rigidbody2D>().velocity, 0.1f);
            if (spaceShip != null) Gizmos.DrawSphere(spaceShip.transform.position, fireRadius);
        }
#endif

        #endregion

        #region Public API

        public void SetPatrolBehavior(AIPointPatrol point)
        {
            aIBehavior = AIBehavior.Patrol;
            patrolPoint = point;
        }

        #endregion

        #endregion
    }
}