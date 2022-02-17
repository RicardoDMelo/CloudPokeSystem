﻿using PokemonSystem.Common.SeedWork;
using PokemonSystem.Evolution.Domain.PokemonAggregate;
using System;

namespace PokemonSystem.Evolution.Domain.Events
{
    internal class PokemonEvolvedDomainEvent : INotification
    {
        public PokemonEvolvedDomainEvent(Pokemon pokemon)
        {
            Pokemon = pokemon ?? throw new ArgumentNullException(nameof(pokemon));
        }

        public Pokemon Pokemon { get; private set; }
    }
}
