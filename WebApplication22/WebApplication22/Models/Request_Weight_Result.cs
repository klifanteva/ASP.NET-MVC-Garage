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
    public partial class Request_Weight_Result
    {
        [Display(Name = "Номер машины")]
        public Nullable<int> number { get; set; }
        [Display(Name = "Вес груза (кг)")]
        public Nullable<float> weight { get; set; }
        [Display(Name = "Дата и время начала поездки")]
        public Nullable<System.DateTime> start_time_trip { get; set; }
        [Display(Name = "Дата и время окончания поездки")]
        public Nullable<System.DateTime> end_time_trip { get; set; }
        [Display(Name = "Грузовая машина")]
        public Nullable<bool> tableJoin { get; set; }
    }
}