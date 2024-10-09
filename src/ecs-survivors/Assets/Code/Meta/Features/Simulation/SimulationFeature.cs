﻿using Code.Infrastructure.Systems;

namespace Code.Meta.Features.Simulation
{
    public class SimulationFeature : Feature
    {
        public SimulationFeature(ISystemFactory systems)
        {
            Add(systems.Create<AFKGoldGainSystem>());
            Add(systems.Create<CalculateGoldGainSystem>());
            Add(systems.Create<BoosterDurationSystem>());
            Add(systems.Create<UpdateSimulationTimeSystem>());
        }
    }
}
