package improbable.behaviours

import improbable.papi.entity.{Entity, EntityBehaviour}
import improbable.ship.HitByCannonball
import improbable.unity.fabric.PhysicsEngineConstraint

class DelegateCombatToFSimBehaviour(entity: Entity) extends EntityBehaviour {
  override def onReady(): Unit = {
    entity.delegateState[HitByCannonball](PhysicsEngineConstraint)
  }
}