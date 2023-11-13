using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PickupLogicPlayTests
{
    [UnityTest]
    public IEnumerator DestroyPickupTest()
    {

        GameObject pickup = new GameObject();



        PickupLogic pickupLogic = pickup.AddComponent<PickupLogic>();


        pickupLogic.DestroyPickup();



        yield return new WaitForSeconds(1f);


        Assert.IsTrue(pickup == null || pickup.Equals(null));
    }
}
