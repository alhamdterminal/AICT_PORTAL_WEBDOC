using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IContainerIndexTariffRepository : IRepo<ContainerIndexTariff>
    {
        List<TariffViewModel> GetContainerIndexTariffs(long ContainerIndexId);

        //List<TariffViewModel> GetExportContainerTariffs(long cargoId);

        List<Tariff> GetUnassignedontainerIndexTariffs(long ContainerIndexId);

        //List<Tariff> GetUnassignedExportContainerGDTariffs(long cargoId);

        List<Tariff> GetUnassignedContainerTariffsCY(string igm, long indexNo);

        List<TariffViewModel> GetContainerIndexTariffsCY(string igm, long indexNo);

        //List<TariffViewModel> GetCargoTariffs(long EnteringCargoId);
    }
}
