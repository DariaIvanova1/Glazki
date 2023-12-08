//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ivanoav_Glazki
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;

    public partial class Agent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Agent()
        {
            this.AgentPriorityHistory = new HashSet<AgentPriorityHistory>();
            this.ProductSale = new HashSet<ProductSale>();
            this.Shop = new HashSet<Shop>();
        }
    
        public int ID { get; set; }
        public int AgentTypeID { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public int Priority { get; set; }
        public string DirectorName { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
    
        public virtual AgentType AgentType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgentPriorityHistory> AgentPriorityHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSale> ProductSale { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shop> Shop { get; set; }
        public string AgentTypeString
        {
            get
            {
                return AgentType.Title;
            }
        }
        public int Sales
        {
            get
            {
                int Total = 0;
                foreach(ProductSale productSale in this.ProductSale)
                {
                    Total += productSale.ProductCount + Convert.ToInt32(productSale.Product.MinCostForAgent);
                }
                return Total;
            }
        }
        public int SalePersent
        {
            get
            {
                int total = 0;
                foreach (ProductSale productSale in this.ProductSale)
                {
                    total += productSale.ProductCount * 10000;
                }
                int sale = 0;
                if (total > 10000 && total < 50000)
                {
                    sale = 5;
                }
                if (total > 50000 && total < 150000)
                {
                    sale = 10;
                }
                if (total > 150000 && total < 500000)
                {
                    sale = 20;
                }
                if ( total >= 500000)
                {
                    sale = 25;
                }
                return sale;
            }
        }
        public SolidColorBrush Fonstyle
        {  get
            {
                if (SalePersent > 20)
                {
                    return (SolidColorBrush)new BrushConverter().ConvertFromString("LightGreen");
                }
                else
                {
                    return (SolidColorBrush)new BrushConverter().ConvertFromString("White");
                }
            }
        }
    }
   
}
