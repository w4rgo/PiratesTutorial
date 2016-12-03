package improbable.worldApps

import improbable.math.Vector3d
import improbable.natures.{Pirate, Terrain}
import improbable.papi.world.AppWorld
import improbable.papi.worldapp.WorldApp
import improbable.utils.{SpawnerParameters, SpawnerUtils}

import scala.concurrent.duration._

class WorldInitialiser(val world: AppWorld) extends WorldApp {

  final val DISTANCE_BETWEEN_SHIPS = 15
  final val SPAWN_DELAY = 200

  world.entities.spawnEntity(Terrain(Vector3d.zero))

  spawnPirates(SpawnerParameters.MAX_SHIPS.get())

  def spawnPirates(piratesToSpawn: Int): Unit = {
    var piratesSpawned = 0
    world.timing.every(SPAWN_DELAY milliseconds) {
      if (piratesSpawned < piratesToSpawn) {
        val spawnPosition = SpawnerUtils.getSpiralSpawnPosition (piratesSpawned, DISTANCE_BETWEEN_SHIPS)
        world.entities.spawnEntity(Pirate(spawnPosition))
        piratesSpawned += 1
      }
    }
  }

}