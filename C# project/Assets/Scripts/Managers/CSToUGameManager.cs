using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSToU.Units;
using System;
using CSToU.UI;
namespace CSToU.Core
{

    public class CSToUGameManager : MonoBehaviour
    {
        #region variables Declarations

        public UnitManager MngUnit;

        public UnitBase unitBase;

        public TextBox TextBoxFactionCounter;

        public AppConfiguration CurrentConfiguration;

        //public List<Color> FactionColors; 

        public static CSToUGameManager I
        {
            get { return _instance; }
            set { _instance = value; } 
        }

        private static CSToUGameManager _instance;

        

        #endregion

        private void Awake()
        {
            if (I == null)
            {
                I = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        #region SetUp
        void Start() 
        {
            Setup();
        }

        void Setup() 
        {

            setupSpawnPoints();
            // PresetUp
            if (GlobalEventManager.OnPreSetUp != null)
                GlobalEventManager.OnPreSetUp();
           

            MngUnit = new UnitManager();

            if (GlobalEventManager.OnPostSetUp != null)
                GlobalEventManager.OnPostSetUp();

            TextBoxFactionCounter.SetText("Factions number: " + MngUnit.factions.Count.ToString() );

        }
        #endregion

        #region SpawnPoints
        List<Vector3> spawnPositions = new List<Vector3>();
        void setupSpawnPoints()
        {
            GameObject[] SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
            for (int i = 0; i < SpawnPoints.Length; i++)
            {
                spawnPositions.Add(SpawnPoints[i].transform.position);
            }
        }

        //Vector3 toAvoid;
        public Vector3 GetRandomSpawnPosition()
        {
           //toAvoid = unitBase.AvoidPosition();
           //spawnPositions.Remove(toAvoid);
           Vector3 returnValue = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Count -1)];
           //spawnPositions.Add(toAvoid);
           return returnValue; 
        }
        #endregion

        

    
    }

    [Serializable]
    public class AppConfiguration
    {
        public List<Color> FactionColors;
    }
}
