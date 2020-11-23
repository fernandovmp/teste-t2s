using System;
using AutoMapper;
using TesteT2S.WebApi.Features.ShipHandling.Enums;
using TesteT2S.WebApi.ViewModels;

namespace TesteT2S.WebApi.Features.ShipHandling.Mappers
{
    public class HandlingTypeConverter : ITypeConverter<HandlingType, EnumViewModel>
    {
        public EnumViewModel Convert(HandlingType source, EnumViewModel destination, ResolutionContext context)
        {
            string description = source switch
            {
                HandlingType.GateIn => "Gate In",
                HandlingType.GateOut => "Gate Out",
                HandlingType.Positioning => "Posicionamento",
                HandlingType.Scanner => "Scanner",
                HandlingType.Shipment => "Embarque",
                HandlingType.Stack => "Pilha",
                HandlingType.Unload => "Descarga",
                HandlingType.Weighing => "Pesagem",
                _ => throw new Exception(
                    $"There is no supported conversion to value {source} of {nameof(HandlingType)}")
            };
            return new EnumViewModel
            {
                Id = (int)source,
                Descricao = description
            };
        }
    }
}
