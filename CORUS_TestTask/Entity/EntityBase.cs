using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORUS_TestTask.Entity
{
   public abstract class EntityBase
    {
        public long Id { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public virtual double Weight { get; set; }

        public virtual double Dimension => this.Weight * this.Height * this.Depth;


    }

    public sealed class Pallet : EntityBase
    {
        public List<Box> Boxes { get; set; }

        public Pallet()
        {
            Boxes = new List<Box>();
        }

        public DateTime? EndDate => Boxes.Any() ? Boxes.Min(b=>b.EndDate): null;

        public override double Weight => Boxes.Sum(b => b.Weight) + 30;

        public override double Dimension => Boxes.Sum(b => b.Dimension) + base.Dimension;
    }

    public sealed class Box : EntityBase
    {
        //Term id days
        public int Term => 100;

        public DateTime? ProductionDate { get; set; }

        public DateTime? EndDate => this.ProductionDate.HasValue ? (DateTime?)this.ProductionDate.Value.AddDays(100) : null;
    }
}
