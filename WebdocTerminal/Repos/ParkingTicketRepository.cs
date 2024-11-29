using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class ParkingTicketRepository : RepoBase<ParkingTicket> , IParkingTicketRepository
    {
        public ParkingTicketRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }


        public long GenerateParkingTicketNumber()
        {

            var lastParkingTicketNumber = "0001";

            var lastGeneratedParkingTicketNumber = GetAll().OrderByDescending(l => l.ParkingTicketId).FirstOrDefault();

            if (lastGeneratedParkingTicketNumber != null)
            {
                lastParkingTicketNumber = Regex.Match(lastGeneratedParkingTicketNumber.ParkingTicketNumber, @"\d+").Value;
                return Int32.Parse(lastParkingTicketNumber) + 1;

            }
            else
            {
                lastParkingTicketNumber = "0001";
                return Int32.Parse(lastParkingTicketNumber);

            }

        }
    }
}
