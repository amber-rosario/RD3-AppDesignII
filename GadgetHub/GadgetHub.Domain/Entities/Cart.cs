using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GadgetHub.Domain.Entities
{
    public class CartLine
    {
        public Gadget Gadget { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();


    public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

        public void AddItem(Gadget mygadget, int myquantity)
        {
            CartLine line = lineCollection
                            .Where(g => g.Gadget.GadgetID == mygadget.GadgetID)
                            .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Gadget = mygadget,
                    Quantity = myquantity

                });
            }

            else
            {
                line.Quantity += myquantity;
            }
        }

        public void RemoveLine(Gadget mygadget)
        {
            lineCollection.RemoveAll(l => l.Gadget.GadgetID == mygadget.GadgetID); 
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Gadget.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
    }
}
