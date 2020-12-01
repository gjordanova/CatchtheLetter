using System.Collections.Generic;
using UnityEngine;

namespace AD.Services.Tests
{
    /// <summary>
    /// Simple test of the log service mechanics.
    /// </summary>
    public class LogTest
        : MonoBehaviour
    {
        public int Value;

        public void Start()
        {
            var data = new Dictionary<string, object> {{"testValue", Value}};
            LogService.SendEvent("TestEvent", data);
        }
    }
}
