//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication22.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public partial class Request2_Repairs_Result
    {
        [Display(Name = "Номер машины")]
        public int cars_number { get; set; }
        [Display(Name = "Вид ремонта")]
        public string type_rep { get; set; }
        [Display(Name = "Время выполнения ремонта")]
        public Nullable<int> time_rep { get; set; }
        [Display(Name = "Марка машины")]
        public string brand { get; set; }
        [Display(Name = "Дата и время начала ремонта")]
        public Nullable<System.DateTime> tableJoin { get; set; }
    }
}
