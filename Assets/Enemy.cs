using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.FantasyMonsters.Scripts;
using DG.Tweening;
namespace Dev_ChickenMerge
{
    [System.Serializable]
    public class StatEnemy
    {
        public TypeStat typeStat;
        public float stat;
    }
    public enum TypeRange
    {
        Melee,
        Shoot,
        Kamikaze,
    }

    public enum NameEnemy
    {
        Nothing,
        //Enemy 1
        Larva,
        Caterpillar,
        MeatBug,
        BugQueen,
        //Enemy 2
        Crawler,
        CaveCaterpillar,
        MossQueen,
        //Enemy 3
        Bat,
        MohawkBat,
        //Enemy 4
        Wolf,
        WhiteWolf,
        WinterWarg,
        // Enemy 5 //shoot
        PirateBoar,
        RoyalBoar,
        //
        Wasp,
    }
    public enum TypeStat
    {
        Speed,
        RangeAttack,
        TimeAttack,
        Health,
        Damage, 
    }

    public class Enemy : MonoBehaviour
    {
        
        public NameEnemy nameEnemy;
        public TypeRange typeRange;
        public List<StatEnemy> listOfStatEnemy = new List<StatEnemy>();
        Dictionary<TypeStat, float> enemyStats = new Dictionary<TypeStat, float>();
        public ParticleSystem vfxPoison;
        public LayerMask layerMask;
        float speed;
        float timeAttack;
        float HpCurrent;
        float timePoison = 1;
        float timeEndPoison =3;
        bool isDie;
        bool isPoison;
        public Monster monster;
        Vector2 target;
        Collider2D m_Coll;
        Rigidbody2D m_Rg;
        // Start is called before the first frame update
        void Start()
        {
            ConvertListToDictionary();
            SetStartTGame();
        }

        // Update is called once per frame
        void Update()
        {
            SetTimePoison();
            /*if (isDie || GamePlayController.ins.isFreeze)
            {
                return;
            }*/
            timeAttack -= Time.deltaTime;
            if (AttackableDistance())
            {
                StopMove();
                Attack();
            }
            else
            {
                Move();
            }
        }
        void SetStartTGame()
        {
            m_Coll = GetComponent<Collider2D>();
            m_Rg = GetComponent<Rigidbody2D>();
            monster = GetComponent<Monster>();
            monster.AddEnemy(this);
            m_Rg.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            timeAttack = GetStat(TypeStat.TimeAttack);
            HpCurrent = GetStat(TypeStat.Health);
            speed = GetStat(TypeStat.Speed);
        }
        
        #region Move
        private void Move()
        {
            m_Rg.velocity = new Vector2(-speed, m_Rg.velocity.y);
            AnimWalk();
        }
        public void StopMove()
        {
            m_Rg.velocity = Vector2.zero;
            AnimIdle();
        }
        #endregion
        #region Spawn
        //public void SpawnBullet(Vector2 point_)
        //{
        //    //Spawn bullet
        //    GameObject a_ = PoolingObj.ins.GetProjectile(4);
        //    a_.SetActive(true);
        //    a_.transform.position = point_;
        //    BulletEnemy bulletEnemy_ = a_.GetComponent<BulletEnemy>();
        //    bulletEnemy_.Shoot(target);
        //    bulletEnemy_.AddDamage(GetStat(TypeStat.Damage));

        //    //Spawn Flash
        //    GameObject b_ = PoolingObj.ins.GetFlash(0);
        //    b_.SetActive(true);
        //    b_.transform.position = point_;
        //    Explosion explosion_ = b_.GetComponent<Explosion>();
        //    explosion_.SetActiveObj();
        //}
        //void SpawnExplosionEnemyKamikaze()
        //{
        //    GameObject vfx_ = PoolingObj.ins.GetExplosion(5);
        //    vfx_.transform.position = transform.position;
        //    vfx_.SetActive(true);
        //    Explosion explosionClone = vfx_.GetComponent<Explosion>();
        //    explosionClone.SetActiveObj();
        //    GamePlayController.ins.ShakeCamera(0.5f);
        //}
        void SpawnEggEnemy()
        {   //-4.7
            float point_1 = 0;
            float point_2 = 0;
            float point_3 = 0;
            float posY_ = transform.position.y;
            float posX_ = transform.position.x;
            if (posY_ >= 2.36f)
            {
                point_1 = posY_;
                point_2 = posY_;
                point_3 = posY_ - 1.03f;
            }
            else if (posY_ <= -2.79f)
            {
                point_1 = posY_ + 1.03f;
                point_2 = posY_;
                point_3 = posY_;
            }
            else
            {
                point_1 = posY_ + 1.03f;
                point_2 = posY_;
                point_3 = posY_ - 1.03f;
            }

            for (int i = 0; i < 10; i++)
            {
                GameObject a_ = MonsterSpawnController.ins.GetEnemy(0);
                MonsterSpawnController.ins.AddToListEnemy(a_);
                a_.SetActive(true);
                int rand_ = Random.Range(0, 100);
                float rand_PosX_ = Random.Range(transform.position.x - 1.5f, transform.position.x + 1);
                //a_.transform.position = transform.position;
                float posZ = (MonsterSpawnController.ins.listEnemy.Count / 100f);
                a_.transform.position = new Vector3(transform.position.x, transform.position.y, posZ);
                if (rand_ < 30)
                {
                    a_.transform.DOMove(new Vector2(rand_PosX_, point_1), 0.5f).SetEase(Ease.Linear);
                }
                else if (rand_ >= 30 && rand_ < 60)
                {
                    a_.transform.DOMove(new Vector2(rand_PosX_, point_2), 0.5f).SetEase(Ease.Linear);

                }
                else
                {
                    a_.transform.DOMove(new Vector2(rand_PosX_, point_3), 0.5f).SetEase(Ease.Linear);
                }
            }
        }
        #endregion
        #region SetDamage
        public void SetDamage()
        {
            float damage_ = GetStat(TypeStat.Damage);
            WallController.ins.ReceiveDamage(damage_);
        }
        public void ReceiveDamage(float damage_)
        {
            if (isDie == false)
            {
                HpCurrent -= damage_;
                if (HpCurrent <= 0)
                {
                    Die();
                }
            }
        }
        void Die()
        {
            if (monster.Animator.enabled == false)
            {
                monster.Animator.enabled = true;
            }
            isDie = true;
            StopMove();
            MonsterSpawnController.ins.RemoveEnemy(this);
            AnimDeath();
            StartCoroutine(ResetDataPool());
            if (nameEnemy == NameEnemy.BugQueen)
            {
                SpawnEggEnemy();
            }
            if (nameEnemy == NameEnemy.RoyalBoar)
            {
                if (typeRange == TypeRange.Kamikaze)
                {
                    //SpawnExplosionEnemyKamikaze();
                }
            }
        }
        #endregion
        #region Attack
        bool AttackableDistance()
        {
            float rangeAttack_ = GetStat(TypeStat.RangeAttack);
            Vector2 endPos = transform.position - Vector3.right * rangeAttack_;
            Debug.DrawLine(transform.position, endPos, Color.blue);
            RaycastHit2D hit = Physics2D.Linecast(transform.position, endPos, layerMask);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    target = hit.point;
                    return true;
                }
            }
            return false;
        }
        void Attack()
        {
            if (timeAttack <= 0)
            {
                monster.Attack();
                timeAttack = GetStat(TypeStat.TimeAttack);
            }
        }
        public void Enemykamikaze()
        {
            SetDamage();
            //SpawnExplosionEnemyKamikaze();
            gameObject.SetActive(false);
            Die();
        }

        #endregion
        #region Animator
        public void AnimWalk()
        {
            monster.SetState(MonsterState.Walk);
        }
        public void AnimIdle()
        {
            monster.SetState(MonsterState.Idle);
        }
        public void AnimDeath()
        {
            monster.SetState(MonsterState.Death);
        }
        public void SetActiveAnim(bool isCheck)
        {
            monster.Animator.enabled = isCheck;
        }
        #endregion
        #region Poison
        void SetTimePoison()
        {
            if (isPoison)
            {
                timePoison -= Time.deltaTime;
                if (timePoison <= 0)
                {
                    vfxPoison.gameObject.SetActive(true);
                    ReceiveDamage(10);
                    timePoison = 1;
                }
                timeEndPoison -= Time.deltaTime;
                if (timeEndPoison <= 0)
                {
                    vfxPoison.gameObject.SetActive(false);
                    isPoison = false;
                }
            }
        }
        //public void HitPoison(Projectile projectile_)
        //{
        //    projectile = projectile_;
        //    isPoison = true;
        //    timeEndPoison = 3;
        //    speed = 0.1f;
        //}
        #endregion
        #region Add Stat
        float GetStat(TypeStat typeStat_)
        {
            if (enemyStats.ContainsKey(typeStat_))
            {
                return enemyStats[typeStat_];
            }
            else
            {
                return 0f;
            }
        }

        void ConvertListToDictionary()
        {
            foreach (var statEnemy in listOfStatEnemy)
            {
                if (!enemyStats.ContainsKey(statEnemy.typeStat))
                {
                    enemyStats[statEnemy.typeStat] = statEnemy.stat;
                }
            }
        }
        #endregion


        IEnumerator ResetDataPool()
        {
            m_Coll.enabled = false;
            yield return new WaitForSeconds(2);
            HpCurrent = GetStat(TypeStat.Health);
            isDie = false;
            m_Coll.enabled = true;
            gameObject.transform.position = MonsterSpawnController.ins.PointStart();
            gameObject.SetActive(false);
        }

       
    }
}
