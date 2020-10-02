using EFDataAccessLibrary.Models.Proveedores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFDataAccessLibrary.Models.Compras
{
    public class OrdenPago : BaseModel
    {
        [Key]
        public int Id { get; set; }

        //Proveedor al que hace referencia
        [DisplayName("Proveedor")]
        [Required(ErrorMessage = "Se requiere un proveedor")]
        public int ProveedorId { get; set; }

        // Fecha en la que se emite
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Se requiere una fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Emision")]
        public DateTime FechaEmision { get; set; } = DateTime.Now;


        // Concepto
        [DisplayName("Concepto")]
        public string Concepto { get; set; }

        public Proveedor Proveedor { get; set; }

        public ICollection<OrdenPagoItem> OrdenPagoItems { get; set; }

    }

    public class OrdenPagoItem : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Se requiere una ordenPago")]
        public int OrdenPagoId { get; set; }



        public OrdenPago OrdenPago { get; set; }
        public FacturaCompra FacturaCompra { get; set; }


    }
}
