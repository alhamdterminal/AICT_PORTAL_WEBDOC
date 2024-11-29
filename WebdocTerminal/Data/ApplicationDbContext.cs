using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebdocTerminal.Models;
using WebdocTerminal.Services;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;
using System.Linq.Expressions;
 
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebdocTerminal.Helpers;

namespace WebdocTerminal.Data
{

    //internal interface ISoftDelete
    //{
    //    bool Deleted { get; set; }
    //}


    //public static class SoftDeleteQueryExtension
    //{
    //    public static void AddSoftDeleteQueryFilter(
    //        this IMutableEntityType entityData)
    //    {
    //        var methodToCall = typeof(SoftDeleteQueryExtension)
    //            .GetMethod(nameof(GetSoftDeleteFilter),
    //                BindingFlags.NonPublic | BindingFlags.Static)
    //            .MakeGenericMethod(entityData.ClrType);
    //        var filter = methodToCall.Invoke(null, new object[] { });
    //        entityData.SetQueryFilter((LambdaExpression)filter);
    //    }

    //    private static LambdaExpression GetSoftDeleteFilter<TEntity>()
    //        where TEntity : class, ISoftDelete
    //    {
    //        Expression<Func<TEntity, bool>> filter = x => !x.Deleted;
    //        return filter;
    //    }
    //}
    public interface ISoftDeleteModel
    {
        bool IsDeleted { get; set; } 
    }
    public static class EFFilterExtensions
    {
        public static void SetSoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
        {
            SetSoftDeleteFilterMethod.MakeGenericMethod(entityType)
                .Invoke(null, new object[] { modelBuilder });
        }

        static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(EFFilterExtensions)
                   .GetMethods(BindingFlags.Public | BindingFlags.Static)
                   .Single(t => t.IsGenericMethod && t.Name == "SetSoftDeleteFilter");

        public static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
            where TEntity : class, ISoftDeleteModel
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.IsDeleted);
        }
    }
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        IUserResolveService userResolveService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserResolveService _userResolveService)
            : base(options)
        {
            userResolveService = _userResolveService;
        }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(IUserResolveService _userResolveService)
        {
            userResolveService = _userResolveService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
              
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<DeliveryOrder>()
            //.HasOne(s => s.ClearingAgent)
            //.WithMany(g => g.DeliveryOrders)
            //.HasForeignKey(s => s.ClearingAgentId);

            modelBuilder.Entity<Voyage>()
            .HasOne(s => s.Vessel)
            .WithMany(g => g.Voyages)
            .HasForeignKey(s => s.VesselId);

            //modelBuilder.Entity<ContainerIndex>()
            //.HasOne(s => s.Voyage)
            //.WithMany(g => g.ContainerIndices)
            //.HasForeignKey(c => c.VoyageId);

            //modelBuilder.Entity<GateOut>()
            //.HasOne(s => s.ShippingAgent)
            //.WithMany(g => g.GateOuts)
            //.HasForeignKey(s => s.ShippingAgentId);

            modelBuilder.Entity<DOItem>()
            .HasOne(s => s.GatePass)
            .WithMany(g => g.DOItems)
            .HasForeignKey(s => s.GatePassId);

            modelBuilder.Entity<DOItemExport>()
             .HasOne(s => s.GatePassExport)
             .WithMany(g => g.DOItemExports)
             .HasForeignKey(s => s.GatePassExportId);

            modelBuilder.Entity<GatePassExport>()
           .HasOne(s => s.DeliveryOrder)
           .WithMany(g => g.GatePasses)
           .HasForeignKey(s => s.ExportDeliveryOrderId);

            modelBuilder.Entity<ExportDeliveryOrder>()
                .Property(c => c.DONumber)
                .HasDefaultValue(0);

            modelBuilder.Entity<GateIn>()
            .HasOne(s => s.ShippingAgent)
            .WithMany(g => g.GateIns)
            .HasForeignKey(s => s.ShippingAgentId);

            modelBuilder.Entity<DestuffedContainer>()
           .HasOne(s => s.TellySheet)
           .WithMany(g => g.DestuffedContainers)
           .HasForeignKey(s => s.TellySheetId);

            modelBuilder.Entity<TellySheet>()
           .HasOne(s => s.ShippingAgent)
           .WithMany(g => g.TellySheets)
           .HasForeignKey(s => s.ShippingAgentId);

            modelBuilder.Entity<DeliveryOrder>()
            .HasOne(s => s.ContainerIndex)
            .WithMany(g => g.DeliveryOrders)
            .HasForeignKey(c => c.ContainerIndexId);

            modelBuilder.Entity<DeliveryOrder>()
          .HasOne(s => s.CYContainer)
            .WithMany(g => g.DeliveryOrders)
            .HasForeignKey(c => c.CYContainerId);

            modelBuilder.Entity<GatePass>()
            .HasOne(s => s.DeliveryOrder)
            .WithMany(g => g.GatePasses)
            .HasForeignKey(c => c.DeliveryOrderId);

            modelBuilder.Entity<GroundedContainer>()
            .HasOne(s => s.ContainerIndex)
            .WithOne(g => g.GroundedContainer);

            modelBuilder.Entity<DestuffedContainer>()
            .HasOne(s => s.ContainerIndex)
            .WithOne(g => g.DestuffedContainer);

            modelBuilder.Entity<GroundedContainer>()
           .HasOne(s => s.ContainerIndex)
           .WithOne(g => g.GroundedContainer);

           // modelBuilder.Entity<Hold>()
           // .HasOne(s => s.ContainerIndex)
           // .WithOne(g => g.Hold);

           // modelBuilder.Entity<Hold>()
           //.HasOne(s => s.CYContainer)
           //.WithOne(g => g.Hold);

            modelBuilder.Entity<Weighment>()
            .HasOne(s => s.ContainerIndex)
            .WithOne(g => g.Weighment);

            modelBuilder.Entity<User>()
            .HasOne(s => s.IdentityUser);

            modelBuilder.Entity<User>()
                .HasOne(x => x.ShippingAgent)
                .WithMany(x => x.Users)
                .HasForeignKey(s => s.ShippingAgentId);

            modelBuilder.Entity<User>()
               .HasOne(x => x.ShippingAgentExport)
               .WithMany(x => x.Users)
               .HasForeignKey(s => s.ShippingAgentExportId);

            modelBuilder.Entity<ChequeRecived>()
             .HasOne(x => x.Bank)
             .WithMany(s => s.chequeReciveds)
             .HasForeignKey(g => g.BankId);

            modelBuilder.Entity<ChequeRecived>()
            .HasOne(x => x.Party)
            .WithMany(s => s.chequeReciveds)
            .HasForeignKey(g => g.PartyId);

            modelBuilder.Entity<RefundAmount>()
                .HasOne(x => x.Party)
                .WithMany(s => s.RefundAmounts)
                .HasForeignKey(g => g.PartyId);

            modelBuilder.Entity<RefundAmount>()
                .HasOne(x => x.Bank)
                .WithMany(g => g.RefundAmounts)
                .HasForeignKey(s => s.BankId);


            modelBuilder.Entity<AmountReceived>()
                .HasOne(x => x.Invoice)
                .WithMany(g => g.AmountReceiveds)
                .HasForeignKey(s => s.InvoiceId);

            modelBuilder.Entity<AmountReceived>()
             .HasOne(x => x.Party)
             .WithMany(g => g.AmountReciveds)
             .HasForeignKey(s => s.PartyId);

            modelBuilder.Entity<InvoiceDocument>()
             .HasOne(x => x.Invoice)
             .WithMany(g => g.InvoiceDocuments)
             .HasForeignKey(s => s.InvoiceId);


            modelBuilder.Entity<Tariff>()
            .HasOne(s => s.TariffHead)
            .WithMany(g => g.Tariffs)
            .HasForeignKey(s => s.TariffHeadId);

            modelBuilder.Entity<PartyLedger>()
            .HasOne(s => s.Party)
            .WithMany(g => g.PartyLedgers)
            .HasForeignKey(c => c.PartyId);

            modelBuilder.Entity<PartyLedger>()
            .HasOne(s => s.ChequeRecived)
            .WithMany(g => g.PartyLedgers)
            .HasForeignKey(c => c.ChequeRecivedId);

            modelBuilder.Entity<PartyLedger>()
               .HasOne(s => s.AmountReceived)
               .WithMany(g => g.PartyLedgers)
               .HasForeignKey(c => c.AmountReceivedId);


            modelBuilder.Entity<EmptyGateOutContainer>()
                .HasOne(s => s.ShippingAgent)
                .WithMany(x => x.EmptyGateOutContainers)
                .HasForeignKey(x => x.ShippingAgentId);
             

            modelBuilder.Entity<VoyageExport>()
            .HasOne(s => s.VesselExport)
            .WithMany(g => g.VoyageExports)
            .HasForeignKey(s => s.VesselExportId);

            modelBuilder.Entity<VoyageExport>()
            .HasOne(s => s.PortAndTerminalExport)
            .WithMany(g => g.VoyageExports)
            .HasForeignKey(s => s.PortAndTerminalExportId);

            //modelBuilder.Entity<VesselExport>()
            //.HasOne(s => s.ShippingLine)
            //.WithMany(g => g.VesselExports)
            //.HasForeignKey(s => s.ShippingLineId);

            modelBuilder.Entity<LoadingProgram>()
                .HasOne(s => s.ShippingLineExport)
                .WithMany(s => s.LoadingPrograms)
                .HasForeignKey(s => s.ShippingLineExportId);


            //modelBuilder.Entity<LoadingProgram>()
            //    .HasOne(s => s.Shipper)
            //    .WithMany(s => s.LoadingPrograms)
            //    .HasForeignKey(s => s.ShipperId);

            //modelBuilder.Entity<LoadingProgram>()
            //    .HasOne(s => s.ClearingAgentExport)
            //    .WithMany(s => s.LoadingPrograms)
            //    .HasForeignKey(s => s.ClearingAgentExportId);

            modelBuilder.Entity<LoadingProgram>()
                .HasOne(x => x.ShippingAgentExport)
                .WithMany(x => x.LoadingPrograms)
                .HasForeignKey(x => x.ShippingAgentExportId);

            modelBuilder.Entity<LoadingProgram>()
               .HasOne(s => s.VesselExport)
               .WithMany(s => s.LoadingPrograms)
               .HasForeignKey(s => s.VesselExportId);

            modelBuilder.Entity<LoadingProgram>()
               .HasOne(s => s.VoyageExport)
               .WithMany(s => s.LoadingPrograms)
               .HasForeignKey(s => s.VoyageExportId);

            modelBuilder.Entity<LoadingProgramDetail>()
                .HasOne(s => s.LoadingProgram)
                .WithMany(s => s.LoadingProgramDetails)
                .HasForeignKey(s => s.LoadingProgramId);

            modelBuilder.Entity<ExportCargo>()
             .HasOne(s => s.LoadingProgram)
             .WithOne(g => g.ExportCargo);

            //modelBuilder.Entity<CargoReceived>()
            //.HasOne(s => s.LoadingProgram)
            //.WithMany(g => g.CargoReceiveds)
            //.HasForeignKey(f => f.LoadingProgramId);

            //modelBuilder.Entity<CargoReceived>()
            //    .HasOne(s => s.ClearingAgentExport)
            //    .WithMany(s => s.CargoReceiveds)
            //    .HasForeignKey(s => s.ClearingAgentExportId);

            //modelBuilder.Entity<Cargo>()
            //.HasOne(s => s.LoadingProgramDetail)
            //.WithOne(g => g.Cargo);


            modelBuilder.Entity<Cargo>()
                .HasOne(s => s.VesselExport)
                .WithMany(s => s.Cargos)
                .HasForeignKey(s => s.VesselExportId);

            modelBuilder.Entity<Cargo>()
                 .HasOne(s => s.VoyageExport)
                 .WithMany(s => s.Cargos)
                 .HasForeignKey(s => s.VoyageExportId);


            modelBuilder.Entity<Cargo>()
                 .HasOne(s => s.Commodity)
                 .WithMany(s => s.Cargos)
                 .HasForeignKey(s => s.CommodityId);


            modelBuilder.Entity<ExportBRT>()
               .HasOne(s => s.ExportCargo)
               .WithMany(s => s.ExportBRTs)
               .HasForeignKey(s => s.ExportCargoId);

            //modelBuilder.Entity<ExportContainer>()
            //   .HasOne(s => s.ShippingLineExport)
            //   .WithMany(s => s.ExportContainers)
            //   .HasForeignKey(s => s.ShippingLineExportId);

            modelBuilder.Entity<ExportGroundedCargo>()
            .HasOne(s => s.EnteringCargo)
            .WithOne(g => g.ExportGroundedCargo);

            //modelBuilder.Entity<ExportContainerItem>()
            //    .HasOne(s => s.Shipper)
            //    .WithMany(s => s.ExportContainerItems)
            //    .HasForeignKey(s => s.ShipperId);

            modelBuilder.Entity<ExportContainerItem>()
               .HasOne(s => s.ExportContainer)
               .WithMany(s => s.ExportContainerItems)
               .HasForeignKey(s => s.ExportContainerId);

            //modelBuilder.Entity<LoadingProgram>()
            //    .HasOne(x => x.PortAndTerminal)
            //    .WithMany(x => x.LoadingPrograms)
            //    .HasForeignKey(x => x.PortAndTerminalId);

            modelBuilder.Entity<CustomerNOC>()
                .HasOne(x => x.ShippingAgentExport)
                .WithMany(x => x.CustomerNOCs)
                .HasForeignKey(x => x.ShippingAgentExportId);

            modelBuilder.Entity<CustomerNOC>()
               .HasOne(x => x.ShippingAgentExport)
               .WithMany(x => x.CustomerNOCs)
               .HasForeignKey(x => x.ShippingAgentExportBId);

            modelBuilder.Entity<EmptyReceiving>()
               .HasOne(x => x.ShippingLine)
               .WithMany(x => x.EmptyReceivings)
               .HasForeignKey(x => x.ShippingLineId);

            modelBuilder.Entity<EmptyReceiving>()
               .HasOne(x => x.ShippingAgentExport)
               .WithMany(x => x.EmptyReceivings)
               .HasForeignKey(x => x.ShippingAgentExportId);

            modelBuilder.Entity<GDHold>()
            .HasOne(s => s.EnteringCargo)
            .WithOne(g => g.GDHold);

            modelBuilder.Entity<CargoRollOver>()
                  .HasOne(s => s.VesselExport)
                  .WithMany(s => s.CargoRollOvers)
                  .HasForeignKey(s => s.VesselExportId);

            modelBuilder.Entity<CargoRollOver>()
               .HasOne(s => s.VoyageExport)
               .WithMany(s => s.CargoRollOvers)
               .HasForeignKey(s => s.VoyageExportId);


            modelBuilder.Entity<ExportVehicle>()
                .HasOne(x => x.EnteringCargo)
                .WithMany(x => x.ExportVehicles)
                .HasForeignKey(x => x.EnteringCargoId);

            modelBuilder.Entity<InvoiceItemExport>()
            .HasOne(x => x.InvoiceExport)
            .WithMany(g => g.InvoiceItemExports)
            .HasForeignKey(s => s.InvoiceExportId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.CompanyId);
 

            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    if (typeof(ISoftDeleteModel).IsAssignableFrom(entityType.ClrType))
            //        modelBuilder.SetSoftDeleteFilter(entityType.ClrType);
            //}
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {

                //var abc = entityType.ClrType.Name;
                //modelBuilder.Entity(abc)
                //  //  .Property<bool>("IsDeleted") 
                //  .HasQueryFilter(e=> entityType.Model.FindEntityType(abc))
                //   .HasQueryFilter(e => EF.Property<bool>(e., "IsDeleted") == false);

                modelBuilder.Entity(entityType.ClrType).Property<bool>("IsDeleted");
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var body = Expression.Equal(
                    Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter, Expression.Constant("IsDeleted")),
                Expression.Constant(false));
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(body, parameter));

            }

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var directory = System.IO.Directory.GetCurrentDirectory();

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(directory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var res = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(
                     res
                    , options => options.ExecutionStrategy(c => new MyExecutionStrategy(c, 2, TimeSpan.FromSeconds(5)))
                    );

            }
        }

        public DbSet<Dictionary> Dictionaries { get; set; }

        public DbSet<Audit> Audits { get; set; }
        public DbSet<AppPage> AppPages { get; set; }

        public DbSet<AppPageItem> AppPageItems { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<SRC> SRCs { get; set; }

        public DbSet<GIIO> GIIOs { get; set; }

        public DbSet<GDIO> GDIOs { get; set; }

        public DbSet<VIRO> VIROs { get; set; }

        public DbSet<IGMO> IGMOs { get; set; }

        public DbSet<IPAO> IPAOs { get; set; }

        public DbSet<GDI> GDIs { get; set; }

        public DbSet<IHC> IHCs { get; set; }

        public DbSet<ECM> ECMs { get; set; }

        public DbSet<CRL> CRLs { get; set; }

        public DbSet<PGO> PGOs { get; set; }

        public DbSet<SVM> SVMs { get; set; }

        public DbSet<SVMO> SVMOs { get; set; }

        public DbSet<GTO> GTOs { get; set; }

        public DbSet<ECMO> ECMOs { get; set; }

        public DbSet<CRLO> CRLOs { get; set; }

        public DbSet<IHCO> IHCOs { get; set; }

        public DbSet<SRCO> SRCOs { get; set; }

        public DbSet<GTOO> GTOOs { get; set; }

        public DbSet<PGOO> PGOOs { get; set; }

        public DbSet<SCMO> SCMOs { get; set; }

        public DbSet<ACK> ACKs { get; set; }

        public DbSet<TTSO> TTSOs { get; set; }

        public DbSet<GTTO> GTTOs { get; set; }

        public DbSet<CCMO> CCMOs { get; set; }

        public DbSet<SIM> SIMs { get; set; }

        public DbSet<SIMO> SIMOs { get; set; }
        public DbSet<OSIM> OSIMs { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<IdentityUser> IdentityUsers { get; set; }

        public DbSet<TellySheet> TellySheets { get; set; }

        public DbSet<ShippingAgent> ShippingAgents { get; set; }

        public DbSet<ClearingAgent> ClearingAgents { get; set; }

        public DbSet<DeliveryOrder> DeliveryOrders { get; set; }

        public DbSet<GatePass> OrderDetails { get; set; }

        public DbSet<DOItem> DOItems { get; set; }

        public DbSet<Vessel> Vessels { get; set; }

        public DbSet<Voyage> Voyages { get; set; }

        public DbSet<ContainerIndex> ContainerIndices { get; set; }

        public DbSet<BRT> BRTs { get; set; }

        public DbSet<GateOut> GateOuts { get; set; }

        public DbSet<GateIn> GateIns { get; set; }

        public DbSet<GroundedContainer> GroundedContainers { get; set; }

        public DbSet<DestuffedContainer> DestuffedContainers { get; set; }

        public DbSet<Hold> Holds { get; set; }

        public DbSet<Weighment> Weighments { get; set; }

        public DbSet<Tariff> Tariffs { get; set; }

        public DbSet<ShippingAgentTariff> ShippingAgentTariffs { get; set; }

        public DbSet<ShippingAgentTariffExport> ShippingAgentTariffExports { get; set; }

        public DbSet<ContainerIndexTariff> ContainerIndexTariffs { get; set; }

        public DbSet<StorageSlab> StorageSlabs { get; set; }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<Party> Parties { get; set; }

        public DbSet<ChequeRecived> ChequeReciveds { get; set; }

        public DbSet<RefundAmount> RefundAmounts { get; set; }

        public DbSet<AmountReceived> AmountReceiveds { get; set; }

        public DbSet<InvoiceDocument> InvoiceDocuments { get; set; }

        public DbSet<SalesTax> SalesTaxes { get; set; }

        public DbSet<TariffHead> TariffHeads { get; set; }

        public DbSet<PartyLedger> PartyLedgers { get; set; }

        public DbSet<CYContainer> CYContainers { get; set; }

        public DbSet<OPIA> OPIAs { get; set; }

        public DbSet<OGIE> OGIEs { get; set; }

        public DbSet<OGDE> OGDEs { get; set; }

        public DbSet<OEHC> OEHCs { get; set; }

        public DbSet<OSRC> OSRCs { get; set; }

        public DbSet<OECM> OECMs { get; set; }

        public DbSet<OCRL> OCRLs { get; set; }

        public DbSet<OGDC> OGDCs { get; set; }

        public DbSet<OSVM> OSVMs { get; set; }

        public DbSet<OGTE> OGTEs { get; set; }

        public DbSet<OVAM> OVAMs { get; set; }

        public DbSet<PIA> PIAs { get; set; }

        public DbSet<GTI> GTIs { get; set; }

        public DbSet<GDE> GDEs { get; set; }

        public DbSet<EHC> EHCs { get; set; }

        public DbSet<GTE> GTEs { get; set; }

        public DbSet<VAM> VAMs { get; set; }
        public DbSet<TPCargoDetails> TPCargoDetails { get; set; }
        public DbSet<ExportContainer> ExportContainers { get; set; }

        public DbSet<PackageTypeExport> PackageTypeExports { get; set; }

        public DbSet<ExportLocationCodeList> ExportLocationCodeLists { get; set; }

        public DbSet<GateInExport> GateInExports { get; set; }

        public DbSet<ServiceCompletion> ServiceCompletions { get; set; }

        public DbSet<ContainerAssociation> ContainerAssociations { get; set; }

        public DbSet<GateoutExport> GateoutExports { get; set; }

        public DbSet<ShippingLine> ShippingLines { get; set; }

        public DbSet<EmptyGateOutContainer> EmptyGateOutContainers { get; set; }

        public DbSet<EmptyContainerGatePass> EmptyContainerGatePasses { get; set; }

        public DbSet<ShippingAgentExport> ShippingAgentExports { get; set; }

        public DbSet<Shipper> Shippers { get; set; }

        public DbSet<ClearingAgentExport> ClearingAgentExports { get; set; }

        public DbSet<PortAndTerminal> PortAndTerminals { get; set; }

        public DbSet<Commodity> Commodities { get; set; }

        public DbSet<VesselExport> VesselExports { get; set; }

        public DbSet<VoyageExport> VoyageExports { get; set; }

        public DbSet<LoadingProgram> LoadingPrograms { get; set; }

        public DbSet<LoadingProgramDetail> LoadingProgramDetails { get; set; }

        public DbSet<EnteringCargo> EnteringCargos { get; set; }

        public DbSet<ExportCargo> ExportCargos { get; set; }

        public DbSet<CargoReceived> CargoReceiveds { get; set; }

        public DbSet<Cargo> Cargos { get; set; }

        public DbSet<ExportBRT> ExportBRTs { get; set; }

        public DbSet<POLocation> POLocations { get; set; }

        public DbSet<EdiMessage> EdiMessages { get; set; }

        public DbSet<ExportGroundedCargo> ExportGroundedCargos { get; set; }

        public DbSet<ExportContainerItem> ExportContainerItems { get; set; }

        public DbSet<CustomerNOC> CustomerNOCs { get; set; }

        public DbSet<EmptyReceiving> EmptyReceivings { get; set; }

        public DbSet<GDHold> GDHolds { get; set; }

        public DbSet<CargoRollOver> CargoRollOvers { get; set; }

        public DbSet<InvoiceExport> InvoiceExports { get; set; }
        public DbSet<ExportDeliveryOrder> ExportDeliveryOrders { get; set; }
        public DbSet<PartyExport> PartyExports { get; set; }

        public DbSet<AmountReceivedExport> AmountReceivedExports { get; set; }

        public DbSet<ChequeRecivedExport> ChequeRecivedExports { get; set; }
        public DbSet<CreditToCustomer> CreditToCustomers { get; set; }

        public DbSet<InvoiceDocumentExport> InvoiceDocumentExports { get; set; }

        public DbSet<PartyLedgerExport> PartyLedgerExports { get; set; }

        public DbSet<TariffExport> TariffExports { get; set; }

        public DbSet<TariffHeadExport> TariffHeadExports { get; set; }

        public DbSet<GDTariff> GDTariffs { get; set; }

        public DbSet<InvoiceItemExport> InvoiceItemExports { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        public DbSet<StorageSlabExport> StorageSlabExports { get; set; }

        public DbSet<ExportVehicle> ExportVehicles { get; set; }

        public DbSet<GatePassExport> GatePassExports { get; set; }
        public DbSet<GateOutGD> GateOutGDs { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<TPReceiveVehicle> TPReceiveVehicles { get; set; }
        public DbSet<RefundAmountExport> RefundAmountExports { get; set; }
        public DbSet<GatePassAuction> GatePassAuctions { get; set; }
        public DbSet<Auction> Auctions{ get; set; }
        public DbSet<DocumentCode> DocumentCodes { get; set; }
        public DbSet<BrtItem> BrtItems { get; set; }
        public DbSet<UsersEmail> UsersEmails { get; set; }
        public DbSet<FailedFile> FailedFiles { get; set; }
        public DbSet<DisabledAgentTariff> DisabledAgentTariffs { get; set; }
        public DbSet<DisabledAgentTariffCY> DisabledAgentTariffCYs { get; set; }
        public DbSet<SalesTaxIndexWise> SalesTaxIndexWises { get; set; }
        public DbSet<ReturnToYard> ReturnToYards { get; set; }
        public DbSet<PreAlert> PreAlerts { get; set; }
        public DbSet<PreAlterDetail> PreAlterDetails { get; set; }
        public DbSet<PaymentUpdate> PaymentUpdates { get; set; }
        public DbSet<PaymentUpdateDetail> PaymentUpdateDetails { get; set; }
        public DbSet<ServicesInfo> ServicesInfos { get; set; }
        public DbSet<ServicesType> ServicesTypes { get; set; }
        public DbSet<ServicesSection> ServicesSections { get; set; }
        public DbSet<NatureOfPayment>  NatureOfPayments { get; set; }
        public DbSet<OperationDprt>  OperationDprts  { get; set; }
        public DbSet<BankBranch> BankBranches { get; set; }
        public DbSet<PreAlertPayOrder> PreAlertPayOrders { get; set; }
        public DbSet<PreAlertPayOrderDetail> PreAlertPayOrderDetails { get; set; }
        public DbSet<ISOCode> ISOCodes { get; set; }
        public DbSet<Consigne> Consignes { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<ImportDropOfTicket> ImportDropOfTickets { get; set; }
        public DbSet<ParkingTicket> ParkingTickets { get; set; }
        public DbSet<Bidder> Bidders { get; set; }
        public DbSet<LabourWorkOrder> LabourWorkOrders { get; set; }
        public DbSet<WorkOrderCSD> WorkOrderCSDs { get; set; }
        public DbSet<Transporter> Transporters { get; set; }         
        public DbSet<SealPurchase> SealPurchases { get; set; }
        public DbSet<SealIssue> SealIssues { get; set; }
        public DbSet<DeliveredYard> DeliveredYards { get; set; }
        public DbSet<BillToLine> BillToLines { get; set; }
        public DbSet<Waiver> Waivers { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<ExaminationRequest> ExaminationRequests { get; set; }
        public DbSet<OtherCharge> OtherCharges { get; set; }
        public DbSet<OtherChargeAssigning> ChargeAssignings  { get; set; }
        public DbSet<GoodsHead> GoodsHeads { get; set; }
        public DbSet<StorageFreeDay> StorageFreeDays { get; set; }
        public DbSet<ContainerSizeAmount>  ContainerSizeAmounts  { get; set; }
        public DbSet<PreAlertVessel>  PreAlertVessels  { get; set; }
        public DbSet<TariffInofrmationTariff> TariffInofrmationTariffs { get; set; }
         
        public DbSet<TariffInformation> TariffInformations { get; set; }
        public DbSet<TariffPercentage> TariffPercentages { get; set; }
        public DbSet<RandDClerk> RandDClerks { get; set; }
        public DbSet<GatePassWeightment> GatePassWeightments  { get; set; }
        public DbSet<ManualGatePass> ManualGatePasses  { get; set; }
        public DbSet<TruckInOut> TruckInOuts { get; set; }
        public DbSet<GatePassSample>  GatePassSamples { get; set; }
        public DbSet<AICTAndLineIndexTariff>  AICTAndLineIndexTariffs { get; set; }
        public DbSet<PortCharge> PortCharges { get; set; }
        public DbSet<LineSealAmount> LineSealAmounts { get; set; }
        public DbSet<GenerealSealAmount> GenerealSealAmounts { get; set; }
        public DbSet<TerminalAndFFShareRate> TerminalAndFFShareRates  { get; set; }
        public DbSet<ExaminationCharge> ExaminationCharges { get; set; }
        public DbSet<ExaminationChargeAssign> ExaminationChargeAssigns { get; set; }
        public DbSet<AuctionAmount> AuctionAmounts { get; set; }
        public DbSet<InvoiceInquiry> InvoiceInquiries { get; set; }
        public DbSet<ISOCodeHead> ISOCodeHeads { get; set; }
        public DbSet<ContainerWeight>  ContainerWeights { get; set; }
        public DbSet<CFSWeight> CFSWeights { get; set; }
        public DbSet<GroundingFreeDay> GroundingFreeDays { get; set; }
        public DbSet<ExaminationTariff> ExaminationTariffs { get; set; }
        public DbSet<ExaminationTariffInformation> ExaminationTariffInformation { get; set; }
        public DbSet<ExaminationTariffInformationTariff> ExaminationTariffInformationTariffs { get; set; }
        public DbSet<BRTLocation>  BRTLocations { get; set; }
        public DbSet<GroundedDays>  GroundedDays  { get; set; }
        public DbSet<EmptyContainerReceived>  EmptyContainerReceiveds { get; set; }
        public DbSet<TransportCollection>  TransportCollections { get; set; } 
        public DbSet<TransportCollectionTariff>  TransportCollectionTariffs { get; set; } 
        public DbSet<OffHireLocation>  OffHireLocations { get; set; } 
        public DbSet<EmptyContainerDelivered> EmptyContainerDelivereds { get; set; } 
        public DbSet<EmptyContainerTariff> EmptyContainerTariffs { get; set; } 
        public DbSet<EmptyContainerStorageSlab>  EmptyContainerStorageSlabs { get; set; } 
        public DbSet<EmptyContainerInvoiceItem> EmptyContainerInvoiceItems { get; set; } 
        public DbSet<EmptyContainerInvoice>  EmptyContainerInvoices { get; set; } 
        public DbSet<EmptyContainerDeliveryOrder>  EmptyContainerDeliveryOrders  { get; set; } 
        public DbSet<EmptyContainerTaxAndFreeDay> EmptyContainerTaxAndFreeDays   { get; set; } 
        public DbSet<CreditAllowed> CreditAlloweds { get; set; } 
        public DbSet<PaymentReceived> PaymentReceiveds  { get; set; } 
        public DbSet<ExcessAmountRefundSettlement> ExcessAmountRefundSettlements  { get; set; } 
        public DbSet<ElectronicDeliveryOrder> ElectronicDeliveryOrders  { get; set; } 
        public DbSet<GDCR> GDCRs { get; set; } 
        public DbSet<ScheduleOfCharge> ScheduleOfCharges { get; set; } 
        public DbSet<ScheduleOfChargeItem> ScheduleOfChargeItems { get; set; } 
        public DbSet<CreditAllowRefoundSettlement> CreditAllowRefoundSettlements { get; set; } 
        public DbSet<EDI_Maersk_Lookup> EDI_Maersk_Lookups { get; set; } 
        public DbSet<EDI_Maersk_APIResponse> EDI_Maersk_APIResponses { get; set; } 
        public DbSet<EDI_Maersk_Message> EDI_Maersk_Messages { get; set; } 
        public DbSet<EDI_Tradelens_APIResponse> EDI_Tradelens_APIResponses { get; set; } 
        public DbSet<EDI_Tradelens_Lookup> EDI_Tradelens_Lookups { get; set; } 
        public DbSet<EDI_Tradelens_Message> EDI_Tradelens_Messages { get; set; } 
        public DbSet<MasterShippingAgent> MasterShippingAgents { get; set; } 
        public DbSet<StorageFreeDaysForHoliday> StorageFreeDaysForHolidays { get; set; } 
        public DbSet<EDIMaerskMessageTOS> EDIMaerskMessageTOs { get; set; } 
        public DbSet<EDOHold> EDOHolds { get; set; } 
        public DbSet<AutoExaminationCharge> AutoExaminationCharges { get; set; } 
        public DbSet<PGateInOut> PGateInOuts { get; set; } 
        public DbSet<NatureOfTariff> NatureOfTariffs { get; set; } 
        public DbSet<DOItemExport> DOItemExports { get; set; } 
        public DbSet<TariffRateSlabWise> TariffRateSlabWises { get; set; } 
        public DbSet<DisabledAgentTariffExport> DisabledAgentTariffExports { get; set; } 
        public DbSet<ShipperExport> ShipperExports { get; set; } 
        public DbSet<ShippingLineExport> ShippingLineExports { get; set; } 

        public DbSet<PortAndTerminalExport> PortAndTerminalExports { get; set; } 
        public DbSet<BoundedTranspoter> BoundedTranspoters { get; set; } 
        public DbSet<PortOfLoading>  PortOfLoadings { get; set; } 
        public DbSet<ImportContainerLocation>  ImportContainerLocations { get; set; } 
        public DbSet<ShortExcessWeigth>  ShortExcessWeigths { get; set; } 
        public DbSet<VehicleMeasurement>  VehicleMeasurements { get; set; } 
        public DbSet<HoldPrivilege> HoldPrivileges  { get; set; } 
        public DbSet<Country> Countries { get; set; } 
        public DbSet<City> Cities { get; set; } 
        public DbSet<Department> Departments { get; set; } 
        public DbSet<AccountNature> AccountNatures { get; set; } 
        public DbSet<AccountCategory> AccountCategories { get; set; } 
        public DbSet<AccountSubCategory> AccountSubCategories { get; set; } 
        public DbSet<AccountType> AccountTypes { get; set; } 
        public DbSet<VoucherServiceType> VoucherServiceTypes { get; set; } 
        public DbSet<VoucherServiceTypeDetail> VoucherServiceTypeDetails { get; set; } 
        public DbSet<ChartOfAccount> ChartOfAccounts { get; set; } 
        public DbSet<Customer> Customers { get; set; } 
        public DbSet<AccountBalance>  AccountBalances { get; set; } 
        public DbSet<VoucherType> VoucherTypes  { get; set; } 
        public DbSet<Voucher>  Vouchers { get; set; } 
        public DbSet<VoucherDetail>  VoucherDetails { get; set; } 
        public DbSet<ChequePrinting> ChequePrintings  { get; set; }
        public DbSet<GoodsHeadExport> GoodsHeadExports { get; set; }
        public DbSet<ExportConsignee> ExportConsignees { get; set; }
        public DbSet<ExportDropOfTicket> ExportDropOfTickets { get; set; }
        public DbSet<DeliveredYardExport> DeliveredYardExports { get; set; }
        public DbSet<TariffInformationExport> TariffInformationExports { get; set; }
        public DbSet<TariffInofrmationTariffExport> TariffInofrmationTariffExports { get; set; }
        public DbSet<ExportTariff> ExportTariffs { get; set; }
        public DbSet<TariffPercentageExport> TariffPercentageExports { get; set; }
        public DbSet<TerminalAndFFShareRateExport> TerminalAndFFShareRateExports { get; set; }
        public DbSet<PortChargeExport> PortChargeExports { get; set; }
        public DbSet<OtherChargeExport> OtherChargeExports { get; set; }
        public DbSet<OtherChargeAssigningExport> OtherChargeAssigningExport { get; set; }
        public DbSet<LineInvoiceDetail> LineInvoiceDetails { get; set; }
        public DbSet<PercentForAictBillingToLine> PercentForAictBillingToLines { get; set; }
        public DbSet<AictBilling> AictBillings { get; set; }
        public DbSet<AictBillingItem> AictBillingItems { get; set; }
        public DbSet<UnlockGenerateBillRemark> UnlockGenerateBillRemarks { get; set; }
        public DbSet<CSEmptyContainerReceive> CSEmptyContainerReceives { get; set; }
        public DbSet<IndexWeighment> IndexWeighments { get; set; }
        public DbSet<EmailAlert> EmailAlerts { get; set; }
        public DbSet<CRSummaryHeadDetail> CRSummaryHeadDetails { get; set; }
        public DbSet<AllowBackDateTransaction> AllowBackDateTransaction { get; set; }
        public DbSet<StorageShareRate> StorageShareRates { get; set; }
        public DbSet<TariffPerBoxRate> TariffPerBoxRates { get; set; }
        public DbSet<ModifyPoint> ModifyPoints { get; set; }
        public DbSet<FinancialYear> FinancialYear { get; set; }
        public DbSet<FinancialYearClosure> FinancialYearClosures { get; set; }
 
 
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var auditEntries = OnBeforeSaveChanges();
            UpdateSoftDeleteStatuses();
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            OnAfterSaveChanges(auditEntries);
            return result;
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.Username = (userResolveService != null) ? userResolveService.GetCurrentSessionUserId(this) : "Automation";
                auditEntry.TableName = entry.Metadata.Relational().TableName;
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }

            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                Audits.Add(auditEntry.ToAudit());
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private int OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return 0;

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                Audits.Add(auditEntry.ToAudit());
            }

            return SaveChanges();
        }



        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                 switch (entry.State)
                 {
                     case EntityState.Added:
                         if (entry.CurrentValues.Properties.Any(n => n.Name == "IsDeleted"))
                             entry.CurrentValues["IsDeleted"] = false;
                         break;
                     case EntityState.Deleted:
                         entry.State = EntityState.Modified;
                        if (entry.CurrentValues.Properties.Any(n => n.Name == "IsDeleted"))
                        {
                            entry.CurrentValues["IsDeleted"] = true;
                            entry.CurrentValues["DeleteDate"] = DateTime.Now;
                        }

                        break;
                 }
            }

             
        }
    }

    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public string TableName { get; set; }
        public string Username { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public Audit ToAudit()
        {
            var local = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");

            var audit = new Audit();
            audit.Username = Username;
            audit.TableName = TableName;
            audit.DateTime = TimeZoneInfo.ConvertTime(DateTime.Now, local);
            audit.KeyValues = JsonConvert.SerializeObject(KeyValues);
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            return audit;
        }
    }




}
