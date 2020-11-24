using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace Tests
{
    public class MovementTest
    {
        GameObject lvl1Object;
        List<PatrolPlanet> patrolPlanets;

        [SetUp]
        public void Setup()
        {
            lvl1Object = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Level_1"));
            patrolPlanets = new List<PatrolPlanet>(lvl1Object.GetComponentsInChildren<PatrolPlanet>());
        }

        [UnityTest]
        public IEnumerator PlanetsExist()
        {
            Assert.Greater(patrolPlanets.Count, 0);
            yield return null;
        }

        [UnityTest]
        public IEnumerator PointIsWithinBounds()
        {
            for (int i = 0; i < patrolPlanets.Count; i++)
            {
                Vector2 randomPos = patrolPlanets[i].GetRandomPos();
                yield return null;

                Assert.True(CheckWithinBounds(patrolPlanets[i].minMaxX, patrolPlanets[i].minMaxY, randomPos));
            }
        }

        [UnityTest]
        public IEnumerator GameOverOnCollision()
        {

            Assert.False(GameManager._gameOver);

            Debug.Log("Planets: " + patrolPlanets.Count);
            Assert.GreaterOrEqual(patrolPlanets.Count, 2);
            patrolPlanets[0].transform.position = patrolPlanets[1].transform.position;

            yield return new WaitForSeconds(.1f);

            Assert.True(GameManager._gameOver);
        }

        public bool CheckWithinBounds(Vector2 _minMaxX, Vector2 _minMaxY, Vector2 _value)
        {
            //*Less than Max X        More than Min X           Less than Max Y         More than Min Y
            if (_value.x < _minMaxX.y && _value.x > _minMaxX.x && _value.y < _minMaxY.y && _value.y > _minMaxY.x)
                return true;
            else
                return false;
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(lvl1Object);
        }
    }
}
