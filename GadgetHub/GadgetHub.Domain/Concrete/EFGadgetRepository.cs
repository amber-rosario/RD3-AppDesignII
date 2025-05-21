using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GadgetHub.Domain.Concrete
{
    public class EFGadgetRepository : IGadgetRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Gadget> Gadgets
        {
            get { return context.Gadgets; }
        }

        void IGadgetRepository.SaveGadget(Gadget gadget)
        {
            if (gadget.GadgetID == 0)
            {
                // Add new gadget
                context.Gadgets.Add(gadget);
            }
            else
            {
                // Update existing gadget
                Gadget dbEntry = context.Gadgets.Find(gadget.GadgetID);
                if (dbEntry != null)
                {
                    dbEntry.Name = gadget.Name;
                    dbEntry.Description = gadget.Description;
                    dbEntry.Price = gadget.Price;
                    dbEntry.Category = gadget.Category;
                    
                    dbEntry.ImageData = gadget.ImageData;
                    dbEntry.ImageMimeType = gadget.ImageMimeType;
                }
            }
            context.SaveChanges();

        }

        public Gadget DeleteGadget(int gadgetID)
        {
            Gadget dbEntry = context.Gadgets.Find(gadgetID);
            if (dbEntry != null)
            {
                context.Gadgets.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }

}
