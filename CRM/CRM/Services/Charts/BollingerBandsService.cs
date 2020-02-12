using Business;
using Business.Contexts;
using Business.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Models.Charts;

namespace CRM.Services.Charts
{
    public class BollingerBandsService
    {
        private List<NeuralSignal> Signals = new List<NeuralSignal>();
        public BollingerBandsService() { }

        public BollingerBandsModel Load(ChartsFilter filter)
        {
            using (MySQLContext context = new MySQLContext())
            {
                Signals = context.NeuralSignals
                    .Where(x => x.Time >= filter.StartDate && x.Time <= filter.EndDate)
                    .OrderBy(x => x.Time).ToList();
            }
            BollingerBandsModel model = new BollingerBandsModel();

            model.Dates = Signals.Select(x => x.Time).ToList();
            model.ProbaSellValues = Signals.Select(x => x.ProbaSell).ToList();
            model.ProbaBuyValues = Signals.Select(x => x.ProbaBuy).ToList();
            model.BBLValues = Signals.Select(x => x.BBL).ToList();

            return model;
        }

    }
}
