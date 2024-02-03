using UnityEngine;
using TowerDefense;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class ProjectileHoming : Projectile
    {
        [SerializeField] private float searchRadius;

        [SerializeField] private float explodeRadius;

        [SerializeField] private int explodeDamage;

        [SerializeField] private float detectTime;

        [SerializeField] private GameObject explodePrefab;

        private SpaceShip target;

        private Destructible destructible;

        private float time;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (target == null && collision.transform.root.GetComponent<SpaceShip>() != null)
            {
                if (collision.transform.root.GetComponent<SpaceShip>().ID != parentID) target = collision.transform.root.GetComponent<SpaceShip>();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {

            if (target != null && target.ID != parentID)
            {
                time += Time.deltaTime;

                if (time > detectTime)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.root.GetComponent<SpaceShip>() == target)
            {
                target = null;
                time = 0;
            }
        }

        protected override void OnProjectileLifeEnd()
        {
            SpawnExplodeEffect();

            var allAffected = Physics2D.OverlapCircleAll(transform.position, explodeRadius);
            foreach (var affectObject in allAffected)
            {
                Enemy destructible = affectObject.transform.root.GetComponent<Enemy>();

                destructible?.TakeDamage(explodeDamage, damageType);
            }

            Destroy(gameObject);
        }

        private void SpawnExplodeEffect()
        {
            GameObject explodeEffect = Instantiate(explodePrefab, transform.position, transform.rotation);
            Destroy(explodeEffect, 1);
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Handles.color = new Color(1, 1, 0, 0.3f);
            Handles.DrawSolidDisc(transform.position, transform.forward, searchRadius);
            Handles.color = new Color(1, 0, 0, 0.3f);
            Handles.DrawSolidDisc(transform.position, transform.forward, explodeRadius);
        }

        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = searchRadius;
        }

#endif
    }
}