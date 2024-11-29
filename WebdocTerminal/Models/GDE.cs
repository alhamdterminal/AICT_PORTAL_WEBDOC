using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class GDE : CommonProperties
    {
        public long GDEId { get; set; }

        public int?  TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string GDNumber { get; set; }

        public string DocumentRequired { get; set; }

        public string ContainerNumber { get; set; }

        public string ContainerMode { get; set; }

        public string SL { get; set; }

        public string ExporterName { get; set; }

        public string ExporterNTN { get; set; }

        public string ExporterAddress { get; set; }

        public string HSCode { get; set; }

        public string HSCodeDescription { get; set; }

        public string ExporterCityCode { get; set; }

        public string ExporterCountryCode { get; set; }

        public string PackageType { get; set; }

        public double NumberOfPackages { get; set; }

        public double Quantity { get; set; }

        public string UOM { get; set; }

        public double GrossWeight { get; set; }

        public string ClearingAgent { get; set; }

        public string ImporterName { get; set; }

        public string ImporterAddress { get; set; }

        public string ImporterCityCode { get; set; }

        public string ImporterCountryCode { get; set; }

        public string TelephoneNumber { get; set; }

        public string DestinationCountry { get; set; }

        public string DestinationPort { get; set; }

        public string Terminal { get; set; }

        public string OperationType { get; set; }

        public DateTime? Performed { get; set; }

        public string FileName { get; set; }
    }
}
