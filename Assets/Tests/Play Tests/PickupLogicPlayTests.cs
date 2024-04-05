using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// A PickupLogicPlayTests osztály tartalmazza a PickupLogic játéktesztjeit a játék közbeni logika tesztelésére.
/// </summary>

public class PickupLogicPlayTests
{

    /// <summary>
    /// Teszteli a felvett objektum megsemmisítését.
    /// </summary>
    /// <returns>Az IEnumerator típusú teszt, amely vár egy másodpercet a felvett objektum megsemmisítése után.</returns>

    [UnityTest]
    public IEnumerator DestroyPickupTest()
    {

        //Előkészítés
        GameObject pickup = new GameObject();


        //Cselekvés
        PickupLogic pickupLogic = pickup.AddComponent<PickupLogic>();


        pickupLogic.DestroyPickup();


        //Várakozás
        yield return new WaitForSeconds(1f);

        //Ellenőrzés
        Assert.IsTrue(pickup == null || pickup.Equals(null));
    }
}
