//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace psk_1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ПриготовленныеСоусы
    {
        public int СоусId { get; set; }
        public string Название { get; set; }
        public Nullable<decimal> Количество { get; set; }
        public Nullable<int> НомерПартии { get; set; }
        public Nullable<System.DateTime> ДатаПроизводства { get; set; }
        public string Ответственный { get; set; }
        public Nullable<int> РецептId { get; set; }
    
        public virtual Рецепты Рецепты { get; set; }
        public virtual Соусы Соусы { get; set; }
    }
}
