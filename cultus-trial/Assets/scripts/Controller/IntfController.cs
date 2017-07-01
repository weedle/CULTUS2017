using UnityEngine;
using System.Collections;

public interface IntfController {
    bool inProgress();

    void handleTurn();

    void wait();
}
