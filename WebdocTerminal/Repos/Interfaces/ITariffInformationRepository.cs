using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface ITariffInformationRepository : IRepo<TariffInformation>
    {
        List<Tariff> GetTariffs();
        //List<Tariff> GetTariffsByPerametersId(long? ShippingAgentId, long? ConsigneId, long? ClearingAgentId, long? GoodId, long? ShipperId, string ContainerType , long percentAgeShippingAgentId , long? portAndTerminalId);
        List<TariffInfoViewModel> GetTariffsByPerametersId(long? ShippingAgentId, long? ConsigneId, long? ClearingAgentId, long? GoodId, long? ShipperId, long? ContainerType  , long? portAndTerminalId   , string indexCargoType );
        List<TariffInfoViewModel> GetTariffsByCollectionId(long? collectionid);
        long  GetStorageFreeDaysByPerametersId(long? ShippingAgentId, long? ConsigneId, long? ClearingAgentId, long? GoodId, long? ShipperId, long? ContainerType ,   long? portAndTerminalId , string indexCargoTypeid);

        List<ShippingAgent> GetShippingAgentFromPercentAgeTariff();

        void ExecuteNonSQL(string sql, params object[] param);

        List<TariffInformation> GetTariffInformations(long? shippingAgentID, long?  ConsigneId  , long?  ClearingAgentId , long?  GoodsHeadId , long?  ShipperId , long?  ISOCodeHeadId , long? PortAndTerminalId , string IndexCargoType );

    }
}
