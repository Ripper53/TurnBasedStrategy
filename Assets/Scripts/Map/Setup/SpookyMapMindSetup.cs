using ArtificialIntelligence.Map;
using ArtificialIntelligence.Map.Works;

public class SpookyMapMindSetup : MapMindSetup {

    protected override void SetupMapMind(MapMind mapMind) {
        mapMind.Add(new RandomMovementMapWork());
    }

}
