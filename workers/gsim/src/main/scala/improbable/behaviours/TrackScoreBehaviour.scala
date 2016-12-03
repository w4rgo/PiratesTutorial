package improbable.behaviours

import improbable.papi.entity.EntityBehaviour
import improbable.papi.world.World
import improbable.ship.ScoreWriter

class TrackScoreBehaviour(world: World, score: ScoreWriter) extends EntityBehaviour {

  override def onReady(): Unit = {
    world.messaging.onReceive {
      case YouKilledMe =>
        score.update.numberOfKills(score.numberOfKills + 1).finishAndSend()
    }
  }
}