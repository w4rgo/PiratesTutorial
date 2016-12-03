package improbable.behaviours

import improbable.papi.entity.{Entity, EntityBehaviour}
import improbable.papi.world.World
import improbable.papi.world.messaging.CustomMsg
import improbable.ship.{HealthWriter, HitByCannonball}

case object YouKilledMe extends CustomMsg

class DecrementHealthBehaviour(entity: Entity,
                               world: World,
                               health: HealthWriter) extends EntityBehaviour {

  override def onReady(): Unit = {
    val hitByCannonballWatcher = entity.watch[HitByCannonball]

    hitByCannonballWatcher.onHit {
      hitEvent =>
        health.update.current(health.current - 20).finishAndSend()
        if (health.current <= 0) {
          world.messaging.sendToEntity(hitEvent.firerEntityId, YouKilledMe)
        }
    }
  }

}