using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSToU.Core;
namespace CSToU.Units
{
    public class UnitManager
    {
        List<UnitBase> units = new List<UnitBase>();
        public List<string> factions = new List<string>();

        public void UnitReady(UnitBase unitToAdd)
        {
            units.Add(unitToAdd);
           

            if (factions.Contains(unitToAdd.CurrentFaction) == false)
            {
                factions.Add(unitToAdd.CurrentFaction);
                CSToUGameManager.I.TextBoxFactionCounter.SetText("Faction: ");
            }
        }


        public GameObject GetPrefab(UnitBase.Class unitBase)
        {
            GameObject returnGo = Resources.Load<GameObject>(@"Graphics\" + unitBase.ToString());
            
            return returnGo;
        }
        #region faction color

        List<Color> FactionColors
        {
            get { return CSToUGameManager.I.CurrentConfiguration.FactionColors; }
        }

        public Color GetColor(string factionName)
        {
            int index = -1;
            for (int i = 0; i < factions.Count; i++)
            {
                if (factions[i] == factionName)
                {
                    index = i;
                }
            }
            if (index < 0)
            {
                Debug.LogError("Faction " + factionName + " Not found");
                return Color.clear;
            }
            return FactionColors[index];
        }

        #endregion
    }


}
