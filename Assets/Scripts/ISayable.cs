using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISayable
{
    string sentence { get; set; }
    int ID_round { get; set; }
    int ID_variant { get; set; }
    int[] ID { get; set; }
}
