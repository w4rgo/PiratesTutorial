package improbable.behaviours

import improbable.papi.entity.EntityBehaviour
import improbable.papi.world.World
import improbable.ship.PlayerControlsWriter

import scala.concurrent.duration._
import scala.util.Random

class SteerRandomlyBehaviour(world: World, playerControlsWriter: PlayerControlsWriter) extends EntityBehaviour {

  override def onReady(): Unit = {
    world.timing.every(1 second) {
      val randomTargetSpeed = 2 * Random.nextFloat() - 1
      val randomTargetSteering = 2 * Random.nextFloat() - 1
      playerControlsWriter.update.targetSpeed(randomTargetSpeed).targetSteering(randomTargetSteering).finishAndSend()
    }
  }
}